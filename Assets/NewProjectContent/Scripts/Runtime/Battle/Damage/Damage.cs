using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShuQiTan {
    public class Damage {
        public float DamageCnt;
        public DamageSpawner Spawner;
        public DamageReceiver Receiver;
        public DamageSystem DamageSystem;

        public Damage(DamageSpawner spawner, DamageReceiver receiver, float damageCnt) {
            Spawner = spawner;
            Receiver = receiver;
            DamageCnt = damageCnt;
            DamageSystem = GameIntro.GameSystem.DamageSystem;
        }
    }
}
