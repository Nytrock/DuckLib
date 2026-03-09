using System;
using Terraria;
using Terraria.ModLoader;

namespace DuckLib {
    public class DuckHook : ModSystem {
        public static event Action<NPC> OnNPCDeath;
        public static event Action OnDayStarted;

        private bool _wasDayTime;

        public override void Load() {
            // IL Hooks
        }

        public override void PostUpdateTime() {
            if (!_wasDayTime && Main.dayTime)
                OnDayStarted?.Invoke();

            _wasDayTime = Main.dayTime;
        }


        private class DuckHookNPC : GlobalNPC {
            public override void HitEffect(NPC npc, NPC.HitInfo hit) {
                if (npc.life > 0) return;
                OnNPCDeath?.Invoke(npc);
            }
        }
    }
}
