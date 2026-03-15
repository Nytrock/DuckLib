using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace DuckLib.ItemDropRules {
    public class DungeonWallDropRule(DungeonWallType wallType, int chanceDenominator, int amountDroppedMinimum = 1, int amountDroppedMaximum = 1, int chanceNumerator = 1) : CommonDrop(ItemID.None, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, chanceNumerator) {
        public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info) {
            itemId = DuckDungeon.GetDungeonWallInWorld(wallType);
            return base.TryDroppingItem(info);
        }
    }
}
