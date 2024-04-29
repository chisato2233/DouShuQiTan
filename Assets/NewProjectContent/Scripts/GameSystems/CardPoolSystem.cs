using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    public class CardPoolSystem:SystemBase {
        public List<RuntimeCard> CardPool;
        public GameObject CardPkgSystem;

        public IEnumerator HandleAllCards() {
            foreach (var runtimeCard in CardPool) {
                yield return runtimeCard.HandleCard();
            }
        }

    }
}
