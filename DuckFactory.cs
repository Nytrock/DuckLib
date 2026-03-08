using Terraria.Audio;
using Terraria.ModLoader;

namespace DuckLib {
    public static class DuckFactory {
        public static int CreateGore(this ModType modType, string goreName) {
            BaseGore gore = new(modType, goreName);
            modType.Mod.AddContent(gore);
            return gore.Type;
        }

        public static int CreateDust(this ModType modType) {
            BaseDust dust = new(modType);
            modType.Mod.AddContent(dust);
            return dust.Type;
        }

        public static SoundStyle CreateSound(this ModType type, string soundName, int variationsCount = -1) {
            string soundPath = $"{type.Mod.Name}/Assets/Sound/{type.Name}/{soundName}";
            SoundStyle style;
            if (variationsCount != -1)
                style = new(soundPath + '_', variationsCount);
            else
                style = new(soundPath);
            return style;
        }

        private class BaseGore(ModType type, string goreName) : ModGore {
            public override string Name => type.Name + goreName;
            public override string Texture => $"{type.Mod.Name}/Assets/Gore/{type.Name}/{goreName}";
        }

        private class BaseDust(ModType type) : ModDust {
            public override string Name => type.Name;
            public override string Texture => $"{type.Mod.Name}/Assets/Dust/{type.Name}";
        }
    }
}
