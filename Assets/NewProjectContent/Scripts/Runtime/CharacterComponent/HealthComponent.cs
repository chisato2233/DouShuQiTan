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
        public float MaxHp = 100f;
        public float InitShield = 0f;

        public float CurrentHealth { get; set; }
        public float CurrentShield { get; set; }


        public UnityEvent<float> OnHealthChange;
        public UnityEvent<float> OnShieldChange;



        void Start() {
            CurrentShield = InitShield;
            CurrentHealth = MaxHp;
        }

        // 造成伤害
        public void ApplyDamage(float val) {
            var outDamage = HandleShieldDamage(val);
            HandleHealthDamage(outDamage);
        }

        
        public void ApplyHeal(float val) {
            var old = CurrentHealth;
            CurrentHealth = Mathf.Clamp(val+CurrentHealth, 0, MaxHp);
            OnHealthChange?.Invoke(val - old);
        }

        public void ChangeShield(float val) {
            var Old = CurrentShield;
            CurrentShield = Mathf.Max(val, 0);
            OnShieldChange?.Invoke(val - Old);
        }

        public void ChangeHealth(float val) {
            var Old = CurrentHealth;
            CurrentHealth = Mathf.Clamp(val, 0,MaxHp);
            OnShieldChange?.Invoke(val - Old);
        }


        float HandleShieldDamage(float val) {
            if (CurrentShield <= 0) return val;
            var shieldDamage = Mathf.Min(CurrentShield, val); //对护盾伤害
            CurrentShield -= shieldDamage;
            OnShieldChange?.Invoke(shieldDamage);
            return MathF.Max(0, val - shieldDamage); //返回溢出伤害，要么是0要么是一个正数
        }

        void HandleHealthDamage(float val) {
            if (val <= 0) return;
            var old = CurrentHealth;
            CurrentHealth = Mathf.Clamp(CurrentHealth - val, 0, MaxHp);
            
            OnHealthChange?.Invoke(CurrentHealth - old);
        }
    }
}
