using DuckLib.DuckType;
using DuckLib.Utils;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace DuckLib.Templates {
    public abstract class CrateTemplate : BaseTemplate {
        protected virtual bool HaveStandardLoot => true;

        public int CrateType { get; private set; }
        public int CrateTypeHardmode { get; private set; }
        public int CurrentCrateType => Main.hardMode ? CrateTypeHardmode : CrateType;

        protected override void AddContent() {
            DuckCrateTile crateTile = new(TemplateName, false);
            DuckCrate crate = new(crateTile, AddNonStandardLoot, HaveStandardLoot);
            DuckCrateTile crateTileHardmode = new(TemplateName, true);
            DuckCrate crateHardmode = new(crateTileHardmode, AddNonStandardLoot, HaveStandardLoot);

            Mod.AddContent(crateTile);
            Mod.AddContent(crate);
            Mod.AddContent(crateTileHardmode);
            Mod.AddContent(crateHardmode);

            CrateType = crate.Type;
            CrateTypeHardmode = crateHardmode.Type;
            ShimmerUtils.Add(CrateTypeHardmode, CrateType);
        }

        public bool IsCrate(int type) {
            return type == CrateType || type == CrateTypeHardmode;
        }

        public virtual IItemDropRule[] AddNonStandardLoot() {
            return null;
        }
    }
}
