using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace DouShuQiTan {
    public class CardLibrary:ScriptableObject {
        public List<CardTemplate> Cards;

        public CardTemplate FindCardTemplate(string CardName) {
            return Cards.Find(x => x.Name == CardName);
        }

        public GameObject FindCard(string CardName) {
            return FindCardTemplate(CardName).RuntimeCard;
        }
    }
}
