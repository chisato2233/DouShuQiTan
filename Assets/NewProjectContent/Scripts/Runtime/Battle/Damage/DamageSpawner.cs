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
    public class DamageSpawner : MonoBehaviour {

        public event Action<DamageReceiver, float> OnDamageSpawn;
        public List<float> DamageCoefficients = new List<float>();

        public void SpawnDamage(DamageReceiver to, float cnt) {
            to.HandleDamage(CalcuDamage(to, cnt));
        }

        public float CalcuDamage(DamageReceiver target, float cnt) {
            return cnt * CalcuCoefficient() * CalcuReceiverCoefficient(target);
        }

        float CalcuCoefficient() {
            float coefficients = 1;
            foreach (var damageCoefficient in DamageCoefficients) {
                coefficients *= damageCoefficient;
            }

            return coefficients;
        }

        float CalcuReceiverCoefficient(DamageReceiver target) {
            float coefficients = 1;
            foreach (var damageCoefficient in target.DamageCoefficients) {
                coefficients *= damageCoefficient;
            }

            return coefficients;
        }

    }
}
