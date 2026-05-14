using System;
using Terraria;
using Terraria.ModLoader.IO;

namespace DuckLib.Random {
    public class DuckRandom(string name, int chanceDenominator) : TagSerializable {
        public static readonly Func<TagCompound, DuckRandom> DESERIALIZER = Load;

        private int _denominator = chanceDenominator;
        private readonly string _name = name;
        private int _triesCount;

        public int Denominator => _denominator;
        public string Name => _name;

        public void SetDenominator(int newValue) {
            _denominator = newValue;
            if (_triesCount >= _denominator)
                _triesCount = _denominator;
        }

        public bool NextBool() {
            bool attempt = Main.rand.NextBool(_denominator);

            if (attempt) {
                _triesCount = 0;
            } else {
                if (_triesCount >= _denominator) {
                    attempt = true;
                    _triesCount = 0;
                } else {
                    _triesCount++;
                }
            }

            Main.NewText(_triesCount);
            return attempt;
        }

        public TagCompound SerializeData() {
            return new TagCompound {
                ["denominator"] = _denominator,
                ["name"] = _name,
                ["triesCount"] = _triesCount
            };
        }

        private static DuckRandom Load(TagCompound compound) {
            int den = compound.GetInt("denominator");
            string name = compound.GetString("name");
            DuckRandom random = new(name, den) {
                _triesCount = compound.GetInt("triesCount")
            };
            return random;
        }
    }
}
