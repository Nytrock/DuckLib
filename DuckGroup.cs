using DuckLib.Utils;
using Terraria.ID;
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
            AnyCopperBar = RecipeUtils.CreateSimpleGroup(ItemID.CopperBar, ItemID.TinBar);
            AnySilverBar = RecipeUtils.CreateSimpleGroup(ItemID.SilverBar, ItemID.TungstenBar);
            AnyGoldBar = RecipeUtils.CreateSimpleGroup(ItemID.GoldBar, ItemID.PlatinumBar);
            AnyEvilBar = RecipeUtils.CreateSimpleGroup(ItemID.DemoniteBar, ItemID.CrimtaneBar);
            AnyCobaltBar = RecipeUtils.CreateSimpleGroup(ItemID.CobaltBar, ItemID.PalladiumBar);
            AnyMythrilBar = RecipeUtils.CreateSimpleGroup(ItemID.MythrilBar, ItemID.OrichalcumBar);
            AnyAdamantiteBar = RecipeUtils.CreateSimpleGroup(ItemID.AdamantiteBar, ItemID.TitaniumBar);
        }
    }
}
