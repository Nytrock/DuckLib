using System;
using Terraria;

namespace DuckLib {
    public static class DuckCondition {
        public static readonly Condition SlimeRain = CreateCondition("SlimeRain", () => Main.slimeRain);
        public static readonly Condition NotSlimeRain = CreateCondition("NotSlimeRain", () => !Main.slimeRain);
        public static readonly Condition NoDungeon = CreateCondition("NoDungeon", () => DuckWorldObserver.DungeonObserver.NoInWorld);
        public static readonly Condition NoTemple = CreateCondition("NoTemple", () => DuckWorldObserver.TempleObserver.NoInWorld);
        public static readonly Condition NoDemonAltars = CreateCondition("NoDemonAltars", () => DuckWorldObserver.DemonAltarsObserver.NoInWorld);

        internal static Condition CreateCondition(string name, Func<bool> predicate) {
            return new("Mods.DuckLib.Conditions." + name, predicate);
        }
    }
}
