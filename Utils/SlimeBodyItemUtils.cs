using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuckLib.Utils {
    public static class SlimeBodyItemUtils {
        internal static readonly List<SlimeBodyItem> BodyItems = [];
        internal static readonly Dictionary<int, Point> ItemsCount = [];

        public static void AddSlimeBodyItem(Func<NPC, bool> dropCondition, int minimumDropped = 1, int maximumDropped = 1, params int[] items) {
            BodyItems.Add(new(dropCondition, items));
            foreach (var item in items)
                ItemsCount.Add(item, new Point(minimumDropped, maximumDropped));
        }
    }

    internal class SlimeBodyItemsImplementer : ModSystem {
        public static event Action<NPC> OnAddSlimeLoot;

        public override void Load() {
            IL_NPC.AI_001_Slimes += SlimeAIHook;
            IL_SlimeBodyItemDropRule.GetDropInfo += SlimeBodyDropInfoHook;
        }

        private void AddSlimesLoot(NPC npc) {
            if (npc.ai[1] != -1)
                return;

            if (npc.netID == NPCID.Pinky || npc.netID == NPCID.BabySlime)
                return;

            foreach (var bodyItem in SlimeBodyItemUtils.BodyItems) {
                if (bodyItem.ConditionsAreMet(npc)) {
                    npc.ai[1] = bodyItem.RandomItem;
                    break;
                }
            }

            if (npc.ai[1] == -1)
                npc.ai[1] = -2;
        }

        private void GetSlimeBodyDropInfo(int itemID, ref int minimumDropped, ref int maximumDropped) {
            if (SlimeBodyItemUtils.ItemsCount.TryGetValue(itemID, out Point range)) {
                minimumDropped = range.X;
                maximumDropped = range.Y;
            }
        }

        private void SlimeAIHook(ILContext il) {
            try {
                ILCursor c = new(il);
                for (int _ = 0; _ < 2; _++)
                    c.GotoNext(i => i.MatchStfld(out FieldReference field) && field.Name == "netUpdate");
                c.Index += 2;

                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate(AddSlimesLoot);

                c = new(il);
                c.GotoNext(i => i.MatchLdcI4(1));
                c.Next.OpCode = OpCodes.Ldc_I4_2;
            } catch {
                MonoModHooks.DumpIL(ModContent.GetInstance<DuckLib>(), il);
            }
        }

        private void SlimeBodyDropInfoHook(ILContext il) {
            try {
                ILCursor c = new(il);
                c.GotoNext(i => i.MatchLdarg1());

                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_1);
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_2);
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_3);
                c.EmitDelegate(GetSlimeBodyDropInfo);
            } catch {
                MonoModHooks.DumpIL(ModContent.GetInstance<DuckLib>(), il);
            }
        }
    }

    internal class SlimeBodyItem(Func<NPC, bool> dropCondition, int[] items) {
        private readonly Func<NPC, bool> _dropCondition = dropCondition;
        private readonly int[] _items = items;

        public int RandomItem {
            get {
                if (_items.Length == 1)
                    return _items[0];
                return Main.rand.NextFromList(_items);
            }
        }

        public bool ConditionsAreMet(NPC npc) => _dropCondition.Invoke(npc);
    }
}
