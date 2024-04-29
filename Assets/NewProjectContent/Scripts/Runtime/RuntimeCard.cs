using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DouShuQiTan {
    public class RuntimeCard : MonoBehaviour {
        public string Name;

        public bool IsUpgrade;
        public bool Has阵型;
        public List<int[]> 阵型;
        public bool IsTrigger=false;

        public UnityEvent OnCardStart = new UnityEvent();
        public UnityEvent OnCardEnd = new UnityEvent();

        public IEnumerator HandleCard() {
            if (!IsTrigger) yield return null;
            else {
                OnCardStart?.Invoke();
                yield return OnHandle();
                OnCardEnd?.Invoke();
            }
            
        }


        protected virtual IEnumerator OnHandle() {
            yield return null;
        }

    }
}
