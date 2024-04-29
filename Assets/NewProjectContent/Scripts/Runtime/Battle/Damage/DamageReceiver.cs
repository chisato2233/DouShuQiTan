using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader.Log;
using Unity.VisualScripting;
using UnityEngine;

namespace DouShuQiTan {
    public class DamageReceiver : MonoBehaviour {
        public List<float> DamageCoefficients = new List<float>();
        private HealthComponent healthComponent;

        void Awake() {
            healthComponent = GetComponent<HealthComponent>();
            if (healthComponent == null) {
                healthComponent = gameObject.AddComponent<HealthComponent>();
            }
        }
        public void HandleDamage(float damageCnt) {
            healthComponent.ApplyDamage(damageCnt);

        }
    }
}
