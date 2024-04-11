using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace DouShuQiTan {
    public class HealthComponent : TurnHandler {
        public float Hp = 100f;
        public float Shield = 0f;


        private float CurrentHealth;
        private float CurrentShield;
        public UnityEvent<HealthComponent> OnHealthChange;
        public UnityEvent<HealthComponent> OnShieldChange;
        

        // 造成伤害
        public void ApplyDamage(float val) {
            if (Shield > 0) {
                var shieldDamage = Mathf.Min(Shield, val);
                var OutDamage = val - shieldDamage;
                Shield -= shieldDamage;
                if (OutDamage > 0) {
                    CurrentHealth -= Mathf.Clamp(OutDamage, 0, Hp - CurrentHealth);
                    OnHealthChange?.Invoke(this);
                }
            }
        }

        public float GetCurrentHealth() {
            return CurrentHealth;
        }

        public float GetCurrentShield() {
            return CurrentShield;
        }


        public void ApplyHeal(float val) {
            Mathf.Clamp(val, 0, Hp - CurrentHealth);
            CurrentHealth += val;
            OnHealthChange?.Invoke(this);
        }

        public void ChangeShield(float val) {
            CurrentShield = Mathf.Max(CurrentShield + val, 0);
            OnShieldChange?.Invoke(this);
        }

        
        protected override IEnumerator OnTurn() {
            ChangeShield(-CurrentShield);
            yield return null; 
        }
    }
}
