using DuckLib.Utils;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuckLib.DuckType {
    internal class DuckCrate(DuckCrateTile tile, Func<IItemDropRule[]> itemLootAction, bool addStandardLoot) : ModItem {
        public override string Name => tile.Name;
        public override string Texture => $"{Mod.Name}/Assets/Crate/{tile.CrateName}/Item" + TextUtils.HardmodeText(tile.IsHardmode);
        protected override bool CloneNewInstances => true;

        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 10;
            ItemID.Sets.IsFishingCrate[Type] = true;
        }

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.FrozenCrate);
            Item.DefaultToPlaceableTile(tile.Type);
            Item.width = 17;
            Item.height = 17;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 1);
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup) {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Crates;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot) {
            IItemDropRule[] extraRules = itemLootAction.Invoke();
            if (!addStandardLoot) {
                if (extraRules != null)
                    itemLoot.Add(ItemDropRule.AlwaysAtleastOneSuccess(extraRules));
                return;
            }

            if (tile.IsHardmode)
                AddHardmodeCrateLoot(itemLoot, itemLootAction.Invoke());
            else
                AddPreHardmodeCrateLoot(itemLoot, itemLootAction.Invoke());
        }

        public override bool CanRightClick() {
            return true;
        }

        private static void AddPreHardmodeCrateLoot(ItemLoot itemLoot, params IItemDropRule[] extraRules) {
            AddPotionsAndBaitToLoot(itemLoot);
            IItemDropRule coinsRule = ItemDropRule.NotScalingWithLuck(ItemID.GoldCoin, 4, 5, 12);

            IItemDropRule[] oresRules = [
                ItemDropRule.Common(ItemID.CopperOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TinOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.IronOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.LeadOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.SilverOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TungstenOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.GoldOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.PlatinumOre, 1, 20, 35),
            ];
            IItemDropRule oresRule = new OneFromRulesRule(7, oresRules);

            IItemDropRule[] barsRules = [
                ItemDropRule.Common(ItemID.IronBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.SilverBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.GoldBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.LeadBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.TungstenBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.PlatinumBar, 1, 6, 16),
            ];
            IItemDropRule barsRule = new OneFromRulesRule(4, barsRules);

            IItemDropRule[] potionsRules = [
                ItemDropRule.Common(ItemID.ObsidianSkinPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.SpelunkerPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.HunterPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.GravitationPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.MiningPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.HeartreachPotion, 1, 2, 4),
            ];
            IItemDropRule potionsRule = new OneFromRulesRule(4, potionsRules);

            IItemDropRule[] lootDropRules = [coinsRule, oresRule, barsRule, potionsRule, .. extraRules];
            itemLoot.Add(ItemDropRule.AlwaysAtleastOneSuccess(lootDropRules));
        }

        private static void AddHardmodeCrateLoot(ItemLoot itemLoot, params IItemDropRule[] extraRules) {
            AddPotionsAndBaitToLoot(itemLoot);

            IItemDropRule coinsRule = ItemDropRule.NotScalingWithLuck(ItemID.GoldCoin, 4, 5, 12);

            IItemDropRule[] oresRules = [
               ItemDropRule.Common(ItemID.CopperOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TinOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.IronOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.LeadOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.SilverOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TungstenOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.GoldOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.PlatinumOre, 1, 20, 35),
            ];
            IItemDropRule oresRule = new OneFromRulesRule(14, oresRules);

            IItemDropRule[] hardmodeOresRules = [
               ItemDropRule.Common(ItemID.CobaltOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.PalladiumOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.MythrilOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.OrichalcumOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.AdamantiteOre, 1, 20, 35),
                ItemDropRule.Common(ItemID.TitaniumOre, 1, 20, 35),
            ];
            IItemDropRule hardmodeOresRule = new OneFromRulesRule(14, hardmodeOresRules);

            IItemDropRule[] barsRules = [
                ItemDropRule.Common(ItemID.IronBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.SilverBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.GoldBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.LeadBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.TungstenBar, 1, 6, 16),
                ItemDropRule.Common(ItemID.PlatinumBar, 1, 6, 16),
            ];
            IItemDropRule barsRule = new OneFromRulesRule(12, barsRules);

            IItemDropRule[] hardmodeBarsRules = [
                ItemDropRule.Common(ItemID.CobaltBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.MythrilBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.AdamantiteBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.PalladiumBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.OrichalcumBar, 1, 5, 16),
                ItemDropRule.Common(ItemID.TitaniumBar, 1, 5, 16),
            ];
            IItemDropRule hardmodeBarsRule = new OneFromRulesRule(6, hardmodeBarsRules);

            IItemDropRule[] potionsRules = [
                ItemDropRule.Common(ItemID.ObsidianSkinPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.SpelunkerPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.HunterPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.GravitationPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.MiningPotion, 1, 2, 4),
                ItemDropRule.Common(ItemID.HeartreachPotion, 1, 2, 4),
            ];
            IItemDropRule potionsRule = new OneFromRulesRule(4, potionsRules);

            IItemDropRule[] lootDropRules = [coinsRule, oresRule, hardmodeOresRule, barsRule, hardmodeBarsRule, potionsRule, .. extraRules];
            itemLoot.Add(ItemDropRule.AlwaysAtleastOneSuccess(lootDropRules));
        }

        private static void AddPotionsAndBaitToLoot(ItemLoot itemLoot) {
            IItemDropRule[] potionsRules = [
                ItemDropRule.Common(ItemID.HealingPotion, 1, 5, 17),
                ItemDropRule.Common(ItemID.ManaPotion, 1, 5, 17),
            ];
            itemLoot.Add(new OneFromRulesRule(2, potionsRules));

            IItemDropRule[] baitRules = [
                ItemDropRule.Common(ItemID.JourneymanBait, 1, 2, 6),
                ItemDropRule.Common(ItemID.MasterBait, 1, 2, 6),
            ];
            itemLoot.Add(new OneFromRulesRule(2, baitRules));
        }
    }
}
