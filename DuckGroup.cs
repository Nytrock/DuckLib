using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DuckLib {
    // Remove cobalt, mythril and adamantite groups in 1.4.5
    public class DuckGroup : ModSystem {
        public static int AnyCopperBar { get; private set; }
        public static int AnySilverBar { get; private set; }
        public static int AnyGoldBar { get; private set; }
        public static int AnyEvilBar { get; private set; }
        public static int AnyCobaltBar { get; private set; }
        public static int AnyMythrilBar { get; private set; }
        public static int AnyAdamantiteBar { get; private set; }

        public override void AddRecipeGroups() {
            AnyCopperBar = CreateSimpleGroup(ItemID.CopperBar, ItemID.TinBar);
            AnySilverBar = CreateSimpleGroup(ItemID.SilverBar, ItemID.TungstenBar);
            AnyGoldBar = CreateSimpleGroup(ItemID.GoldBar, ItemID.PlatinumBar);
            AnyEvilBar = CreateSimpleGroup(ItemID.DemoniteBar, ItemID.CrimtaneBar);
            AnyCobaltBar = CreateSimpleGroup(ItemID.CobaltBar, ItemID.PalladiumBar);
            AnyMythrilBar = CreateSimpleGroup(ItemID.MythrilBar, ItemID.OrichalcumBar);
            AnyAdamantiteBar = CreateSimpleGroup(ItemID.AdamantiteBar, ItemID.TitaniumBar);
        }

        public static int CreateSimpleGroup(int mainItem, params int[] itemsInGroup) {
            itemsInGroup = [mainItem, .. itemsInGroup];
            string mainItemName = Lang.GetItemNameValue(mainItem);

            RecipeGroup recipeGroup = new(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {mainItemName}",
                itemsInGroup
            );
            return RecipeGroup.RegisterGroup("Any" + mainItemName.Replace(" ", ""), recipeGroup);
        }
    }
}
