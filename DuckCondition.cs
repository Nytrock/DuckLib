using Terraria;

namespace DuckLib {
    public static class DuckCondition {
        public static readonly Condition SlimeRain = new("Mods.DuckLib.Conditions.SlimeRain", () => Main.slimeRain);
        public static readonly Condition NotSlimeRain = new("Mods.DuckLib.Conditions.NotSlimeRain", () => !Main.slimeRain);
    }
}
