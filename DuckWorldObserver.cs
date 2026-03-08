using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuckLib {
    // </summary>
    // Use it as little as possible, because every HaveInWorld call scans entire world or all items in world
    // When using in if-else constructions, put it as last condition (yes, this will make difference)
    // <summary>
    public class DuckWorldObserver : ModSystem {
        public static ElementObserver DemonAltarsObserver { get; private set; }
        public static ElementObserver DungeonObserver { get; private set; }
        public static ElementObserver TempleObserver { get; private set; }
        public static ElementObserver FossilObserver { get; private set; }
        public static ElementObserver LifeCrystalObserver { get; private set; }
        public static ElementObserver CrimsonObserver { get; private set; }
        public static ElementObserver CorruptionObserver { get; private set; }

        private static readonly List<ElementObserver> _observers = [];

        public override void Load() {
            SetupObservers();
        }

        internal static void ResetObservers() {
            foreach (var observer in _observers)
                observer.Reset();
        }

        private static void SetupObservers() {
            DemonAltarsObserver = AddNewObserver();
            DemonAltarsObserver.Tiles.Observe(TileID.DemonAltar);

            DungeonObserver = AddNewObserver();
            DungeonObserver.Tiles.ObserveSet(Main.tileDungeon);
            DungeonObserver.Walls.ObserveSet(Main.wallDungeon);
            DungeonObserver.Items.Observe(ItemID.PinkBrick, ItemID.BlueBrick, ItemID.GreenBrick, ItemID.PinkBrickWallUnsafe, ItemID.PinkSlabWallUnsafe, ItemID.PinkTiledWallUnsafe, ItemID.BlueBrickWallUnsafe, ItemID.BlueSlabWallUnsafe, ItemID.BlueTiledWallUnsafe, ItemID.GreenBrickWallUnsafe, ItemID.GreenSlabWallUnsafe, ItemID.GreenTiledWallUnsafe);

            TempleObserver = AddNewObserver();
            TempleObserver.Tiles.Observe(TileID.LihzahrdBrick);
            TempleObserver.Walls.Observe(WallID.LihzahrdBrickUnsafe);
            TempleObserver.Items.Observe(ItemID.LihzahrdBrick, ItemID.LihzahrdBrickWall, ItemID.LihzahrdWallUnsafe);

            FossilObserver = AddNewObserver();
            FossilObserver.Tiles.Observe(TileID.DesertFossil);

            LifeCrystalObserver = AddNewObserver();
            LifeCrystalObserver.Tiles.Observe(TileID.Heart);

            CrimsonObserver = AddNewObserver();
            CrimsonObserver.Tiles.Observe(TileID.Sets.CrimsonBiome);
            CrimsonObserver.Tiles.SetThreshold(300);

            CorruptionObserver = AddNewObserver();
            CorruptionObserver.Tiles.Observe(TileID.Sets.CorruptBiome);
            CorruptionObserver.Tiles.SetThreshold(300);
        }

        private static void CheckTile(Tile tile, bool isAdd = true) {
            if (tile == null || !tile.HasTile)
                return;

            foreach (var observer in _observers) {
                observer.Tiles.CheckElement(tile.TileType, 1, isAdd);
                observer.Walls.CheckElement(tile.WallType, 1, isAdd);
            }
        }

        private static void CheckItem(Item item, bool isAdd = true) {
            if (item == null || item.type == ItemID.None)
                return;

            foreach (var observer in _observers) {
                observer.Items.CheckElement(item.type, item.stack, isAdd);
                if (item.createTile != -1)
                    observer.Tiles.CheckElement(item.createTile, item.stack, isAdd);
                if (item.createWall != -1)
                    observer.Walls.CheckElement(item.createWall, item.stack, isAdd);
            }
        }

        public static ElementObserver AddNewObserver() {
            ElementObserver observer = new();
            _observers.Add(observer);
            return observer;
        }

        internal static void ScanWorld() {
            int offset = 40;
            for (int x = offset; x < Main.maxTilesX - offset; x++)
                for (int y = offset; y < Main.maxTilesY - offset; y++)
                    CheckTile(Main.tile[x, y]);
        }

        internal static void ScanItems() {
            Player player = Main.LocalPlayer;
            Item[][] containers = [player.inventory, player.bank.item, player.bank2.item, player.bank3.item, player.bank4.item, Main.item];

            for (int i = 0; i < containers.Length; i++)
                for (int j = 0; j < containers[i].Length; j++)
                    CheckItem(containers[i][j]);

            foreach (Chest chest in Main.chest)
                if (chest != null)
                    foreach (Item item in chest.item)
                        CheckItem(item);
        }
    }

    public class ElementObserver {
        public ElementObserverCounter Items { get; private set; }
        public ElementObserverCounter Walls { get; private set; }
        public ElementObserverCounter Tiles { get; private set; }

        public bool HaveInWorld {
            get {
                DuckWorldObserver.ResetObservers();
                if (Items.IsObservingAnything())
                    DuckWorldObserver.ScanItems();

                if (Tiles.IsObservingAnything() || Walls.IsObservingAnything())
                    DuckWorldObserver.ScanWorld();

                return Items.HaveInWorld || Walls.HaveInWorld || Tiles.HaveInWorld;
            }
        }

        public ElementObserver() {
            Items = new();
            Walls = new();
            Tiles = new();
        }

        internal void Reset() {
            Items.Reset();
            Walls.Reset();
            Tiles.Reset();
        }
    }

    public class ElementObserverCounter {
        internal int _count = 0;
        private int _threshold = 0;
        internal readonly List<int> _observedElements = [];

        public bool HaveInWorld => _count > _threshold;

        public void Observe(params int[] elementIDs) {
            foreach (int elementID in elementIDs)
                _observedElements.Add(elementID);
        }

        public void ObserveSet(bool[] elementSet) {
            for (int i = 0; i < elementSet.Length; i++)
                if (elementSet[i])
                    _observedElements.Add(i);
        }

        public void SetThreshold(int threshold) {
            _threshold = threshold;
        }

        public bool IsObservingAnything() {
            return _observedElements.Count != 0;
        }

        internal void CheckElement(int elementID, int elementCount, bool isAdd) {
            if (!IsObservingAnything())
                return;

            if (!_observedElements.Contains(elementID))
                return;

            if (isAdd)
                _count += elementCount;
            else
                _count -= elementCount;
        }

        internal void Reset() {
            _count = 0;
        }
    }
}
