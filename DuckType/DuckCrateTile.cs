using DuckLib.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DuckLib.DuckType {
    internal class DuckCrateTile(string crateName, bool isHardmode) : ModTile {
        public override string Name => $"{crateName}Crate{TextUtils.HardmodeText(isHardmode)}";
        public override string Texture => $"{Mod.Name}/Assets/Crate/{crateName}/Tile{TextUtils.HardmodeText(isHardmode)}";

        internal bool IsHardmode => isHardmode;
        internal string CrateName => crateName;

        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileTable[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = [16, 18];
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(160, 120, 92));
        }

        public override bool CreateDust(int i, int j, ref int type) {
            return false;
        }
    }
}
