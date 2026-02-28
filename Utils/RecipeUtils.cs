using Terraria;
using Terraria.Localization;

namespace DuckLib.Utils {
    public static class RecipeUtils {
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
