using System.Linq;
using Terraria;

namespace DuckLib.Extensions {
    public static class NPCExtension {
        public static bool IsType(this NPC npc, params int[] types) {
            return types.Contains(npc.type);
        }
    }
}
