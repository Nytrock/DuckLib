using System;
using System.Linq;
using Terraria;

namespace DuckLib.Utils {
    public static class PlayerUtils {
        public static bool HaveBuff(this Player player, int buffID) {
            return player.buffType.Contains(buffID);
        }
    }
}
