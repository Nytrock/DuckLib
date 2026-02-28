using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuckLib.Utils {
    public class ShimmerUtils : ModSystem {
        private static int[] TransformsList => ItemID.Sets.ShimmerTransformToItem;
        private static readonly List<ShimmerWithCondition<int>> _transformsWithConditions = [];
        private static readonly List<ShimmerWithCondition<Action>> _actionsWithConditions = [];

        public static void Add(int ingredient, int result) {
            TransformsList[ingredient] = result;
        }

        public static int Get(int ingredient) {
            return TransformsList[ingredient];
        }

        public static void InsertAfter(int ingredient, int insertedItem) {
            int originalResult = Get(ingredient);
            if (originalResult == -1)
                return;

            Add(ingredient, insertedItem);
            Add(insertedItem, originalResult);
        }

        public static void RemoveTransform(int item) {
            TransformsList[item] = -1;
        }

        public static void RemoveDecrafts(int item) {
            int[] recipes = ItemID.Sets.CraftingRecipeIndices[item];
            foreach (var recipeId in recipes)
                Main.recipe[recipeId].DisableDecraft();
        }

        public static void RemoveEverything(int item) {
            RemoveTransform(item);
            RemoveDecrafts(item);
        }

        public static void AddLoop(params int[] items) {
            for (int i = 1; i < items.Length; i++)
                TransformsList[items[i - 1]] = items[i];
            TransformsList[items[^1]] = items[0];
        }

        public static void AddLoopWithConditions(int[] items, params Condition[] conditions) {
            for (int i = 1; i < items.Length; i++)
                AddWithConditions(items[i - 1], items[i], conditions);
            AddWithConditions(items[^1], items[0]);
        }

        public static void AddWithConditions(int ingredient, int result, params Condition[] conditions) {
            ShimmerWithCondition<int> transform = new(ingredient, result, conditions);
            _transformsWithConditions.Add(transform);
            RemoveEverything(ingredient);
        }

        public static void AddEvent(int ingredient, Action function, params Condition[] conditions) {
            ShimmerWithCondition<Action> action = new(ingredient, function, conditions);
            _actionsWithConditions.Add(action);
            RemoveEverything(ingredient);
        }

        public override void Load() {
            IL_Item.GetShimmered += HookGetShimmered;
            IL_Item.CanShimmer += HookCanShimmer;
        }

        private bool OnCanShimmer(bool returnValue, Item item) {
            foreach (var transform in _transformsWithConditions) {
                if (!transform.IsIngredient(item))
                    continue;
                return transform.ConditionsAreMet();
            }

            foreach (var action in _actionsWithConditions) {
                if (!action.IsIngredient(item))
                    continue;

                return action.ConditionsAreMet();
            }

            return returnValue;
        }

        private void OnGetShimmered(Item item) {
            foreach (var transform in _transformsWithConditions) {
                if (!transform.IsIngredient(item) || !transform.ConditionsAreMet())
                    continue;

                int originalStack = item.stack;
                item.SetDefaults(transform.Result);
                item.stack = originalStack;
                item.shimmered = true;
                return;
            }

            foreach (var action in _actionsWithConditions) {
                if (!action.IsIngredient(item) || !action.ConditionsAreMet())
                    continue;

                action.Result.Invoke();
                item.stack--;
                if (item.stack <= 0)
                    item.type = ItemID.None;
                else
                    item.shimmered = true;
                return;
            }
        }

        private class ShimmerWithCondition<T>(int ingredient, T result, Condition[] conditions) {
            private readonly int _ingredient = ingredient;
            private readonly T _result = result;
            private readonly Condition[] _conditions = conditions;

            public T Result => _result;

            public bool IsIngredient(Item item) {
                return _ingredient == item.type;
            }

            public bool ConditionsAreMet() {
                foreach (Condition condition in _conditions)
                    if (!condition.IsMet())
                        return false;
                return true;
            }
        }

        private void HookCanShimmer(ILContext il) {
            try {
                ILCursor c = new(il);
                for (int _ = 0; _ < 3; _++)
                    c.GotoNext(i => i.MatchRet());

                c.Index -= 2;
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate(OnCanShimmer);
            } catch {
                MonoModHooks.DumpIL(ModContent.GetInstance<DuckLib>(), il);
            }
        }

        private void HookGetShimmered(ILContext il) {
            try {
                ILCursor c = new(il);
                c.GotoNext(i => i.MatchLdarg0());

                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate(OnGetShimmered);
            } catch {
                MonoModHooks.DumpIL(ModContent.GetInstance<DuckLib>(), il);
            }
        }
    }

}