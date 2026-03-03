using System;
using Terraria;
using Terraria.ModLoader;

namespace DuckLib {
    public class DuckHook : ModSystem {
        public static event Action<NPC> OnNPCDeath;

        public override void Load() {
            // Hooks
        }

        private class DuckHookNPC : GlobalNPC {
            public override void HitEffect(NPC npc, NPC.HitInfo hit) {
                if (npc.life > 0) return;
                OnNPCDeath?.Invoke(npc);
            }
        }
    }
}
