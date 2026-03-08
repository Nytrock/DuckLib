using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace DuckLib {
    public class DuckLootCondition {
        public class NoDungeon : IItemDropRuleCondition, IProvideItemConditionDescription {
            public bool CanDrop(DropAttemptInfo info) => !DuckWorldObserver.DungeonObserver.HaveInWorld;
            public bool CanShowItemDropInUI() => !DuckWorldObserver.DungeonObserver.HaveInWorld;
            public string GetConditionDescription() => Language.GetTextValue("Mods.DuckLib.Conditions.NoDungeon");
        }

        public class NoDemonAltars : IItemDropRuleCondition, IProvideItemConditionDescription {
            public bool CanDrop(DropAttemptInfo info) => !DuckWorldObserver.DemonAltarsObserver.HaveInWorld;
            public bool CanShowItemDropInUI() => !DuckWorldObserver.DemonAltarsObserver.HaveInWorld;
            public string GetConditionDescription() => Language.GetTextValue("Mods.DuckLib.Conditions.NoDemonAltars");
        }

        public class NoTemple : IItemDropRuleCondition, IProvideItemConditionDescription {
            public bool CanDrop(DropAttemptInfo info) => !DuckWorldObserver.TempleObserver.HaveInWorld;
            public bool CanShowItemDropInUI() => !DuckWorldObserver.TempleObserver.HaveInWorld;
            public string GetConditionDescription() => Language.GetTextValue("Mods.DuckLib.Conditions.NoTemple");
        }
    }
}
