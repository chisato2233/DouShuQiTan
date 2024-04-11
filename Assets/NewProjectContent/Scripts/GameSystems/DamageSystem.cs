using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.Events;

namespace DouShuQiTan {
    public class DamageSystem:SystemBase {
        
        List<DamageEvent> DamageEvents = new List<DamageEvent>();

        public void CreateDamage(CharacterObject attacker,CharacterObject target,float val) {
            DamageEvents.Add(new DamageEvent(attacker, target) { DamageValue = val });
        }

        public void HandleDamage() {
            foreach (var damage in DamageEvents) {
                damage.Excute();
            }
        }
    }

    public class DamageEvent {
        public CharacterObject Attacker;
        public CharacterObject Target;
        public float DamageValue = 0;

        public UnityEvent<CharacterObject, CharacterObject, float> OnDamage;


        public DamageEvent(CharacterObject attacker, CharacterObject target) {
            Attacker = attacker;
            Target = target;
        }


        public virtual void Excute() {
            Target.GetComponent<HealthComponent>().ApplyDamage(DamageValue);
            OnDamage.Invoke(Attacker, Target, DamageValue);
        }
    }
}
