using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace DuckLib.Utils {
    public static class ItemUtils {
        public static bool IsType(this Item item, int[] types) {
            return types.Contains(item.type);
        }

        public static bool IsType(this Item item, IEnumerable<int> types) {
            return types.Contains(item.type);
        }
    }
}
