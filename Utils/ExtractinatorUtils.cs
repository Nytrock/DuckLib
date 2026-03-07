using DuckLib.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuckLib.Utils {
    public static class ExtractinatorUtils {
        internal readonly static Dictionary<int, Func<bool, Item>> ItemsConversions = [];
        internal readonly static List<int> NewExtractinatorItems = [];

        private static int[] ExtractinatorConversions => ItemID.Sets.ExtractinatorMode;

        public static void AddDirectConversion(int item, int result) {
            AddConversion(item, (isChlorophyte) => new Item(result));
        }

        public static void AddConversion(int item, Func<bool, Item> conversion) {
            ExtractinatorConversions[item] = item;
            ItemsConversions.Add(item, conversion);
            NewExtractinatorItems.Add(item);
        }

        public static void AddItemToConversionGroup(int hostItem, int newItem) {
            ExtractinatorConversions[newItem] = hostItem;
            NewExtractinatorItems.Add(newItem);
        }

        public static void RemoveConversion(int item) {
            ExtractinatorConversions[item] = -1;
            NewExtractinatorItems.Remove(item);
        }
    }

    internal class ExtractinatorConversionsImplementer : GlobalItem {
        public override void SetDefaults(Item item) {
            if (item.useStyle != ItemUseStyleID.None)
                return;

            if (item.IsType(ExtractinatorUtils.NewExtractinatorItems))
                item.MakeUsableWithChlorophyteExtractinator();
        }

        public override void ExtractinatorUse(int extractType, int extractinatorBlockType, ref int resultType, ref int resultStack) {
            bool isChlorophyteExtractinator = extractinatorBlockType == TileID.ChlorophyteExtractinator;

            foreach (var conversion in ExtractinatorUtils.ItemsConversions) {
                if (conversion.Key == extractType) {
                    Item resultItem = conversion.Value.Invoke(isChlorophyteExtractinator);
                    resultType = resultItem.type;
                    resultStack = resultItem.stack;
                    return;
                }
            }
        }
    }
}
