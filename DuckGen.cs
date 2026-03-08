using Terraria;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DuckLib {
    public class DuckGen : ModSystem {
        private const string DUNGEON_KEY = "DUNGEON_BRICK_COLOR";
        private static DungeonBrickColor _dungeonBrickColor;

        public static int GetDungeonBrick() => GetDungeonColorItem(ItemID.PinkBrick, ItemID.GreenBrick, ItemID.BlueBrick);
        public static int GetDungeonWall(DungeonWallType wallType) {
            switch (wallType) {
                case DungeonWallType.Brick:
                    return GetDungeonColorItem(ItemID.PinkBrickWall, ItemID.GreenBrickWall, ItemID.BlueBrickWall);
                case DungeonWallType.BrickUnsafe:
                    return GetDungeonColorItem(ItemID.PinkBrickWallUnsafe, ItemID.GreenBrickWallUnsafe, ItemID.BlueBrickWallUnsafe);
                case DungeonWallType.Slab:
                    return GetDungeonColorItem(ItemID.PinkSlabWall, ItemID.GreenSlabWall, ItemID.BlueSlabWall);
                case DungeonWallType.SlabUnsafe:
                    return GetDungeonColorItem(ItemID.PinkSlabWallUnsafe, ItemID.GreenSlabWallUnsafe, ItemID.BlueSlabWallUnsafe);
                case DungeonWallType.Tiled:
                    return GetDungeonColorItem(ItemID.PinkTiledWall, ItemID.GreenTiledWall, ItemID.BlueTiledWall);
                case DungeonWallType.TiledUnsafe:
                    return GetDungeonColorItem(ItemID.PinkTiledWallUnsafe, ItemID.GreenTiledWallUnsafe, ItemID.BlueTiledWallUnsafe);
                default:
                    return ItemID.None;
            }
        }

        public static void PlaceItemFrame(int x, int y, int item) {
            PlaceItemFrame(x, y, new Item(item));
        }

        public static void PlaceItemFrame(int x, int y, Item item) {
            for (int i = x; i <= x + 1; i++) {
                for (int j = y; j <= y + 1; j++) {
                    Tile tile = Main.tile[i, j];
                    Main.tile[i, j].TileType = TileID.Stone;
                    tile.ResetToType(TileID.ItemFrame);
                    tile.TileFrameX = (short)((i - x) * 18);
                    tile.TileFrameY = (short)((j - y) * 18);
                }
            }

            TEItemFrame.Place(x, y);
            TEItemFrame.TryPlacing(x, y, item, 1);
        }

        private static int GetDungeonColorItem(int pinkItem, int greenItem, int blueItem) {
            switch (_dungeonBrickColor) {
                case DungeonBrickColor.Pink:
                    return pinkItem;
                case DungeonBrickColor.Green:
                    return greenItem;
                case DungeonBrickColor.Blue:
                    return blueItem;
                default:
                    return ItemID.None;
            }
        }

        public override void LoadWorldData(TagCompound tag) {
            int? dungeonColor = tag.GetInt(DUNGEON_KEY);
            if (dungeonColor == 0)
                GetDungeonColorFromWorld();
            else
                _dungeonBrickColor = (DungeonBrickColor)dungeonColor;
        }

        public override void SaveWorldData(TagCompound tag) {
            tag.Set(DUNGEON_KEY, (int)_dungeonBrickColor, true);
            _dungeonBrickColor = DungeonBrickColor.None;
        }

        private static void GetDungeonColorFromWorld() {
            _dungeonBrickColor = DungeonBrickColor.None;
            int offset = 40;
            for (int x = offset; x < Main.maxTilesX - offset; x++) {
                if (_dungeonBrickColor != DungeonBrickColor.None)
                    break;

                for (int y = offset; y < Main.maxTilesY - offset; y++) {
                    int tileType = Main.tile[x, y].TileType;
                    if (tileType == TileID.PinkDungeonBrick) {
                        _dungeonBrickColor = DungeonBrickColor.Pink;
                        break;
                    } else if (tileType == TileID.BlueDungeonBrick) {
                        _dungeonBrickColor = DungeonBrickColor.Blue;
                        break;
                    } else if (tileType == TileID.GreenDungeonBrick) {
                        _dungeonBrickColor = DungeonBrickColor.Green;
                        break;
                    }
                }
            }

            if (_dungeonBrickColor == DungeonBrickColor.None)
                _dungeonBrickColor = (DungeonBrickColor)Main.rand.Next(1, 4);
        }
    }

    public enum DungeonBrickColor {
        None = 0,
        Pink = 1,
        Green = 2,
        Blue = 3
    }

    public enum DungeonWallType {
        None = 0,
        Brick = 1,
        BrickUnsafe = 2,
        Slab = 3,
        SlabUnsafe = 4,
        Tiled = 5,
        TiledUnsafe = 6
    }
}
