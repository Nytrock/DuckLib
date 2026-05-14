using DuckLib.DuckType;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace DuckLib.Templates {
    public abstract class CrateTemplate : BaseTemplate {
        protected virtual bool HaveStandardLoot => true;

        public static int CrateType { get; private set; }
        public static int CrateTypeHardmode { get; private set; }
        public static int CurrentCrateType => Main.hardMode ? CrateTypeHardmode : CrateType;

        public override void Load(Mod mod) {
            DuckCrateTile crateTile = new(Name, false);
            DuckCrate crate = new(crateTile, AddNonStandardLoot, HaveStandardLoot);
            DuckCrateTile crateTileHardmode = new(Name, true);
            DuckCrate crateHardmode = new(crateTileHardmode, AddNonStandardLoot, HaveStandardLoot);

            mod.AddContent(crateTile);
            mod.AddContent(crate);
            mod.AddContent(crateTileHardmode);
            mod.AddContent(crateHardmode);

            CrateType = crate.Type;
            CrateTypeHardmode = crateHardmode.Type;
        }

        public virtual IItemDropRule[] AddNonStandardLoot() {
            return null;
        }
    }
}
