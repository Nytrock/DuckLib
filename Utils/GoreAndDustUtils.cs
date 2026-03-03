using Terraria.ModLoader;

namespace DuckLib.Utils {
    public static class GoreAndDustUtils {
        public static int CreateGore(this ModType modType, string goreName) {
            BaseGore gore = new(modType.Name, goreName);
            modType.Mod.AddContent(gore);
            return gore.Type;
        }

        public static int CreateDust(this ModType modType) {
            BaseDust dust = new(modType.Name);
            modType.Mod.AddContent(dust);
            return dust.Type;
        }

        private class BaseGore(string groupName, string goreName) : ModGore {
            public override string Name => groupName + goreName;
            public override string Texture => Mod.Name + "/Assets/Gore/" + groupName + "/" + goreName;
        }

        private class BaseDust(string dustName) : ModDust {
            public override string Name => dustName;
            public override string Texture => Mod.Name + "/Assets/Dust/" + dustName;
        }
    }
}
