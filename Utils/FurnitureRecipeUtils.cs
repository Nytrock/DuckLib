using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuckLib.Utils {
    public class FurnitureRecipeUtils : ModSystem {
        private static readonly Dictionary<int, Condition[]> _decraftingConditions = [];

        public override void Load() {
            AddDecraftingCondition(ItemID.LihzahrdBrick, Condition.DownedGolem);

            foreach (var brick in DuckDungeon.GetDungeonBricks())
                AddDecraftingCondition(brick, Condition.DownedSkeletron);
        }

        public static void AddDecraftingCondition(int blockType, Condition condition) {
            if (_decraftingConditions.TryGetValue(blockType, out Condition[] value))
                _decraftingConditions[blockType] = [.. value, condition];
            else
                _decraftingConditions[blockType] = [condition];
        }

        private static void AddToRecipeSavedDecraftConditions(int blockType, Recipe recipe) {
            foreach (var itemConditions in _decraftingConditions)
                if (itemConditions.Key == blockType)
                    foreach (var condition in itemConditions.Value)
                        recipe.AddDecraftCondition(condition);
        }

        public static void AddSpecificFurniture(int block, int craftingStation = -1, int torch = ItemID.Torch,
            int bathtub = -1, int bed = -1, int bookcase = -1, int candelabra = -1,
            int candle = -1, int chair = -1, int chandelier = -1, int clock = -1, int door = -1,
            int dresser = -1, int lamp = -1, int piano = -1, int sofa = -1, int table = -1,
            int workbench = -1, int chest = -1, int sink = -1, int lantern = -1, int toilet = -1, int vase = -1) {

            if (bathtub != -1)
                AddBathtub(bathtub, block, craftingStation);
            if (bed != -1)
                AddBed(bed, block, craftingStation);
            if (bookcase != -1)
                AddBookcase(bookcase, block, craftingStation);
            if (candelabra != -1)
                AddCandelabra(candelabra, block, craftingStation, torch);
            if (candle != -1)
                AddCandle(candle, block, craftingStation, torch);
            if (chair != -1)
                AddChair(chair, block, craftingStation);
            if (chandelier != -1)
                AddChandelier(chandelier, block, craftingStation, torch);
            if (clock != -1)
                AddClock(clock, block, craftingStation);
            if (door != -1)
                AddDoor(door, block, craftingStation);
            if (dresser != -1)
                AddDresser(dresser, block, craftingStation);
            if (lamp != -1)
                AddLamp(lamp, block, craftingStation, torch);
            if (piano != -1)
                AddPiano(piano, block, craftingStation);
            if (sofa != -1)
                AddSofa(sofa, block, craftingStation);
            if (table != -1)
                AddTable(table, block, craftingStation);
            if (workbench != -1)
                AddWorkbench(workbench, block, craftingStation);
            if (chest != -1)
                AddChest(chest, block, craftingStation);
            if (sink != -1)
                AddSink(sink, block, craftingStation);
            if (toilet != -1)
                AddToilet(toilet, block, craftingStation);
            if (lantern != -1)
                AddLantern(lantern, block, craftingStation, torch);
            if (vase != -1)
                AddVase(vase, block, craftingStation);
        }

        public static void AddBathtub(int bathtub, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
                .Create(bathtub)
                .AddIngredient(block, 14)
                .AddTile(craftingStation);
            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddBed(int bed, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
                .Create(bed)
                .AddIngredient(block, 15)
                .AddIngredient(ItemID.Silk, 5)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddBookcase(int bookcase, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
                .Create(bookcase)
                .AddIngredient(block, 20)
                .AddIngredient(ItemID.Book, 10)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddCandelabra(int candelabra, int block, int craftingStation = TileID.WorkBenches, int torch = ItemID.Torch) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(candelabra)
                .AddIngredient(block, 5)
                .AddIngredient(torch, 3)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddCandle(int candle, int block, int craftingStation = TileID.WorkBenches, int torch = ItemID.Torch) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(candle)
                .AddIngredient(block, 4)
                .AddIngredient(torch, 1)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddChair(int chair, int block, int craftingStation = TileID.WorkBenches) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(chair)
                .AddIngredient(block, 4)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddChandelier(int chandelier, int block, int craftingStation = TileID.Anvils, int torch = ItemID.Torch) {
            if (craftingStation == -1)
                craftingStation = TileID.Anvils;

            Recipe recipe = Recipe
                .Create(chandelier)
                .AddIngredient(block, 4)
                .AddIngredient(torch, 4)
                .AddIngredient(ItemID.Chain, 1)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddClock(int clock, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
               .Create(clock)
               .AddIngredient(block, 10)
               .AddIngredient(ItemID.Glass, 6)
               .AddRecipeGroup(RecipeGroupID.IronBar, 3)
               .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddDoor(int door, int block, int craftingStation = TileID.WorkBenches) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(door)
                .AddIngredient(block, 6)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddDresser(int dresser, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
                .Create(dresser)
                .AddIngredient(block, 16)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddLamp(int lamp, int block, int craftingStation = TileID.WorkBenches, int torch = ItemID.Torch) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(lamp)
                .AddIngredient(block, 3)
                .AddIngredient(torch, 1)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddPiano(int piano, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
                .Create(piano)
                .AddIngredient(block, 15)
                .AddIngredient(ItemID.Book, 1)
                .AddIngredient(ItemID.Bone, 4)
                .AddDecraftCondition(Condition.DownedSkeletron)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddSofa(int sofa, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
                .Create(sofa)
                .AddIngredient(block, 5)
                .AddIngredient(ItemID.Silk, 2)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddTable(int table, int block, int craftingStation = TileID.WorkBenches) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(table)
                .AddIngredient(block, 8)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddWorkbench(int workbench, int block, int craftingStation = -1) {
            Recipe recipe = Recipe
                .Create(workbench)
                .AddIngredient(block, 10);

            if (craftingStation != -1)
                recipe.AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddChest(int chest, int block, int craftingStation = TileID.WorkBenches) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(chest)
                .AddIngredient(block, 8)
                .AddRecipeGroup(RecipeGroupID.IronBar, 2)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddToilet(int toilet, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
                .Create(toilet)
                .AddIngredient(block, 6)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddSink(int sink, int block, int craftingStation = TileID.WorkBenches) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(sink)
                .AddIngredient(block, 6)
                .AddIngredient(ItemID.WaterBucket, 1)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddLantern(int sink, int block, int craftingStation = TileID.WorkBenches, int torch = ItemID.Torch) {
            if (craftingStation == -1)
                craftingStation = TileID.WorkBenches;

            Recipe recipe = Recipe
                .Create(sink)
                .AddIngredient(block, 6)
                .AddIngredient(torch, 1)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }

        public static void AddVase(int vase, int block, int craftingStation = TileID.Sawmill) {
            if (craftingStation == -1)
                craftingStation = TileID.Sawmill;

            Recipe recipe = Recipe
                .Create(vase)
                .AddIngredient(block, 9)
                .AddTile(craftingStation);

            AddToRecipeSavedDecraftConditions(block, recipe);
            recipe.Register();
        }
    }
}

