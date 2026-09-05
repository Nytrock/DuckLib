using Terraria.ModLoader;

namespace DuckLib.Templates {
    public abstract class BaseTemplate : ModSystem {
        protected abstract string TemplateName { get; }
        protected abstract void AddContent();

        public override void Load() {
            AddContent();
        }
    }
}
