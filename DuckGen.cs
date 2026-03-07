using Terraria;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;

namespace DuckLib {
    public static class DuckGen {
        public static void PlaceItemFrame(int x, int y, int item) {
            PlaceItemFrame(x, y, new Item(item));
        }

        public static void PlaceItemFrame(int x, int y, Item item) {
            for (int i = x; i <= x + 1; i++) {
                for (int j = y; j <= y + 1; j++) {
                    Tile tile = Main.tile[i, j];
                    tile.ResetToType(TileID.ItemFrame);
                    tile.TileFrameX = (short)((i - x) * 18);
                    tile.TileFrameY = (short)((j - y) * 18);
                }
            }

            TEItemFrame.Place(x, y);
            TEItemFrame.TryPlacing(x, y, item, 1);
        }
    }
}
