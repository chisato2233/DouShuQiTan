using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    public class CardPkg : ScriptableObject {
        public void AddCard(CardTemplate card) {
            if (Cards.ContainsKey(card)) {
                Cards[card]++;
                return;
            }
            Cards.Add(card, 1);
        }

        public void RemoveCard(CardTemplate card) {
            if (Cards.ContainsKey(card))
                Cards[card]--;
        }

        public int GetCardNum(CardTemplate card) {
            return Cards[card];
        }
        public void RemoveAllCard(CardTemplate card) {
            Cards.Remove(card);
        }
        private Dictionary<CardTemplate,int> Cards;
    }
}
