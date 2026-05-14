using System.Linq;
using Terraria;

namespace DuckLib.Extensions {
    public static class NPCExtension {
        public static bool IsType(this NPC npc, params int[] types) {
            return types.Contains(npc.type);
        }

        public static bool AnyNPCs(params int[] types) {
            if (types.Length == 0) return true;

            bool[] anyNPC = new bool[types.Length];
            for (int i = 0; i < 200; i++) {
                NPC npc = Main.npc[i];
                if (!npc.active)
                    continue;

                for (int j = 0; j < types.Length; j++)
                    if (npc.type == types[j])
                        anyNPC[j] = true;

                if (anyNPC.All(x => x))
                    return true;
            }

            return false;
        }
    }
}
