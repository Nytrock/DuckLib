using Terraria.GameContent.ItemDropRules;

namespace DuckLib.ItemDropRules {
    public class NoItemInWorldDropRule(ElementObserver observer, int itemId, int chanceDenominator = 1, int amountDroppedMinimum = 1, int amountDroppedMaximum = 1, int chanceNumerator = 1, bool disableObserverOnSuccess = true) : CommonDrop(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, chanceNumerator) {
        private readonly ElementObserver _observer = observer;
        private readonly bool _disableObserver = disableObserverOnSuccess;

        public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info) {
            ItemDropAttemptResult result;
            if (_observer.NoInWorld && info.player.RollLuck(chanceDenominator) < chanceNumerator) {
                CommonCode.DropItem(info, itemId, info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1));
                result = default;
                if (_disableObserver)
                    _observer.Disable();
                result.State = ItemDropAttemptResultState.Success;
                return result;
            }

            result = default;
            result.State = ItemDropAttemptResultState.FailedRandomRoll;
            return result;
        }
    }
}
