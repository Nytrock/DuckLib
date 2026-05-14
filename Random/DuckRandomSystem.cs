using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DuckLib.Random {
    public class DuckRandomSystem : ModSystem {
        private static Dictionary<string, DuckRandom> _random;
        private const string RANDOM_KEY = "duckRandom";

        public static DuckRandom GetOrCreateRandom(string key, int chanceDenominator) {
            if (!_random.TryGetValue(key, out DuckRandom random)) {
                random = new(key, chanceDenominator);
                _random.Add(key, random);
                return random;
            }

            if (random.Denominator != chanceDenominator)
                random.SetDenominator(chanceDenominator);

            return random;
        }

        public override void SaveWorldData(TagCompound tag) {
            tag[RANDOM_KEY] = _random.Values.ToList();
        }

        public override void LoadWorldData(TagCompound tag) {
            var randoms = tag.Get<List<DuckRandom>>(RANDOM_KEY);

            _random = [];
            foreach (var random in randoms)
                _random.Add(random.Name, random);
        }

        public override void ClearWorld() {
            _random = [];
        }
    }

}
