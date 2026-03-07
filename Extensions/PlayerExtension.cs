using System;
using System.Linq;
using Terraria;

namespace DuckLib.Extensions {
    public static class PlayerExtension {
        public static bool HaveBuff(this Player player, int buffID) {
            return player.buffType.Contains(buffID);
        }
    }
}
