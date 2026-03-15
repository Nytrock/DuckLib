using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DuckLib {
    public class DuckDungeon : ModSystem {
        private const string DUNGEON_KEY = "DUNGEON_BRICK_COLOR";
        private static DungeonBrickColor _dungeonBrickColorInWorld;

        private static readonly int[] _bricks = [ItemID.PinkBrick, ItemID.GreenBrick, ItemID.BlueBrick];
        private static readonly int[] _wallsBrick = [ItemID.PinkBrickWall, ItemID.GreenBrickWall, ItemID.BlueBrickWall];
        private static readonly int[] _wallsBrickUnsafe = [ItemID.PinkBrickWallUnsafe, ItemID.GreenBrickWallUnsafe, ItemID.BlueBrickWallUnsafe];
        private static readonly int[] _wallsSlab = [ItemID.PinkSlabWall, ItemID.GreenSlabWall, ItemID.BlueSlabWall];
        private static readonly int[] _wallsSlabUnsafe = [ItemID.PinkSlabWallUnsafe, ItemID.GreenSlabWallUnsafe, ItemID.BlueSlabWallUnsafe];
        private static readonly int[] _wallsTiled = [ItemID.PinkTiledWall, ItemID.GreenTiledWall, ItemID.BlueTiledWall];
        private static readonly int[] _wallsTiledUnsafe = [ItemID.PinkTiledWallUnsafe, ItemID.GreenTiledWallUnsafe, ItemID.BlueTiledWallUnsafe];

        public static int[] GetDungeonBricks() => _bricks;
        public static int GetDungeonBrickInWorld() => GetDungeonColorItemInWorld(_bricks);

        public static int[] GetDungeonWalls(DungeonWallType wallType) {
            switch (wallType) {
                case DungeonWallType.Brick:
                    return _wallsBrick;
                case DungeonWallType.BrickUnsafe:
                    return _wallsBrickUnsafe;
                case DungeonWallType.Slab:
                    return _wallsSlab;
                case DungeonWallType.SlabUnsafe:
                    return _wallsSlabUnsafe;
                case DungeonWallType.Tiled:
                    return _wallsTiled;
                case DungeonWallType.TiledUnsafe:
                    return _wallsTiledUnsafe;
                default:
                    return [];
            }
        }

        public static int GetDungeonWallInWorld(DungeonWallType wallType) {
            int[] walls = GetDungeonWalls(wallType);
            return GetDungeonColorItemInWorld(walls);
        }

        private static int GetDungeonColorItemInWorld(int[] items) {
            switch (_dungeonBrickColorInWorld) {
                case DungeonBrickColor.Pink:
                    return items[0];
                case DungeonBrickColor.Green:
                    return items[1];
                case DungeonBrickColor.Blue:
                    return items[2];
                default:
                    return ItemID.None;
            }
        }

        public override void LoadWorldData(TagCompound tag) {
            int? dungeonColor = tag.GetInt(DUNGEON_KEY);
            if (dungeonColor == 0)
                GetDungeonColorFromWorld();
            else
                _dungeonBrickColorInWorld = (DungeonBrickColor)dungeonColor;
        }

        public override void SaveWorldData(TagCompound tag) {
            tag.Set(DUNGEON_KEY, (int)_dungeonBrickColorInWorld, true);
            _dungeonBrickColorInWorld = DungeonBrickColor.None;
        }

        private static void GetDungeonColorFromWorld() {
            _dungeonBrickColorInWorld = DungeonBrickColor.None;
            int offset = 40;
            for (int x = offset; x < Main.maxTilesX - offset; x++) {
                if (_dungeonBrickColorInWorld != DungeonBrickColor.None)
                    break;

                for (int y = offset; y < Main.maxTilesY - offset; y++) {
                    int tileType = Main.tile[x, y].TileType;
                    if (tileType == TileID.PinkDungeonBrick) {
                        _dungeonBrickColorInWorld = DungeonBrickColor.Pink;
                        break;
                    } else if (tileType == TileID.BlueDungeonBrick) {
                        _dungeonBrickColorInWorld = DungeonBrickColor.Blue;
                        break;
                    } else if (tileType == TileID.GreenDungeonBrick) {
                        _dungeonBrickColorInWorld = DungeonBrickColor.Green;
                        break;
                    }
                }
            }

            if (_dungeonBrickColorInWorld == DungeonBrickColor.None)
                _dungeonBrickColorInWorld = (DungeonBrickColor)Main.rand.Next(1, 4);
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
