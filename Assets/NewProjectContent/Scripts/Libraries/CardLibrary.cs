using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace DouShuQiTan {
    public class CardLibrary:ScriptableObject {
        public List<CardTemplate> Cards;

        private Dictionary<string, CardTemplate> CardDict = new Dictionary<string, CardTemplate>();
        void Awake() {
            foreach (var cardTemplate in Cards) {
                CardDict[cardTemplate.Name] = cardTemplate;
            }
        }


        public CardTemplate FindCardTemplate(string CardName) {
            return Cards.Find(x => x.Name == CardName);
        }

        public GameObject FindCard(string CardName) {
            return FindCardTemplate(CardName).RuntimeCard;
        }
    }
}
