using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace DuckLib.Extensions {
    public static class ItemExtension {
        public static bool IsType(this Item item, int[] types) {
            return types.Contains(item.type);
        }

        public static bool IsType(this Item item, IEnumerable<int> types) {
            return types.Contains(item.type);
        }
    }
}
