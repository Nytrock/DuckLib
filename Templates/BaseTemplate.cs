using Terraria.ModLoader;

namespace DuckLib.Templates {
    public abstract class BaseTemplate : ILoadable {
        protected abstract string Name { get; }

        public abstract void Load(Mod mod);

        public void Unload() { }
    }
}
