using DG.Tweening;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

namespace DouShuQiTan {
    public class CardPkgSystem : SystemBase {
        public List<GameObject> Cards;
        public CardLibrary CardLibrary;

        public UI_CardShowPanel CardShowPanel;
        
        
        public float ScrollSpeed;
        public float DragSpeed;
        Vector2 startPosition;
        Vector2 EndPosition;



        public bool IsUpGrade = false;



        private void Awake() {
            Cards.Clear();
            var CardInfos = GameData.CardData.CardInfos;
            foreach (var cardInfo in CardInfos) {
                var card = CardLibrary.FindCard(cardInfo.Key);
                if (card != null) {
                    cardInfo.Value.InitCard(card);
                    CardShowPanel.AddShowCard(card);
                    Cards.Add(card);
                }
            }
            AddCardFromIndex(0);
        }

        public void AddCard(string name) {
            var CardInfo = new CardInfo(name);
            GameData.CardData.CardInfos.Add(name, CardInfo);
            CardShowPanel.AddShowCard(CardLibrary.FindCard(name));
        }

        public void AddCardFromIndex(int index) {
            var CardTemplate = CardLibrary.Cards[index];
            var CardInfo = new CardInfo(CardTemplate.Name);
            GameData.CardData.CardInfos.Add(CardTemplate.Name, CardInfo);
            CardShowPanel.AddShowCard(CardTemplate.RuntimeCard);
        }
        public void RemoveCard(string name) {
            GameData.CardData.CardInfos.Remove(name);

            var removeCard = Cards.Find(x => x.GetComponent<RuntimeCard>().Name == name);
            Cards.Remove(removeCard);
            CardShowPanel.RemoveShowCard(removeCard.gameObject);
            Destroy(removeCard);
            CardShowPanel.PlayRemoveCardAnimation();
        } 

        public void UpgradeCard(string name) {
            IsUpGrade = false;
            var CardInfo = GameData.CardData.CardInfos[name];
            CardInfo.IsUpgrade = true;
            GameData.CardData.CardInfos[name] = CardInfo;
            CardShowPanel.PlayUpgradeCardAnimation();
        }

        
        public GameObject[] GetRandomCards(int num) {
            if (num > Cards.Count) {
                Debug.LogError("Requested more cards than are available in the pool.");
                return null; // 或者处理错误
            }
            List<GameObject> cardPool = new List<GameObject>(Cards);
            GameObject[] result = new GameObject[num];
            for (int i = 0; i < num; i++) {
                int randomIndex = Random.Range(0, cardPool.Count);
                result[i] = cardPool[randomIndex];
                cardPool.RemoveAt(randomIndex);
            }

            return result;
        }

        public void Changed(int cnt) {
            if (cnt > GameData.CardData.CardInfos.Count) {
                Debug.LogError("Count is not enough");
                return;
            }
            
            var res = GetRandomCards(cnt);
            
            foreach (var card in res) {
                var newCard = CardLibrary.Cards[Random.Range(0, CardLibrary.Cards.Count)];
                var cardInfo = new CardInfo(newCard.Name);
                GameData.CardData.CardInfos.Remove(card.name);
                GameData.CardData.CardInfos.Add(newCard.Name,cardInfo);
                
                GameObject newCardObj = newCard.RuntimeCard;
                Cards.Add(newCardObj);
                cardInfo.InitCard(newCardObj);

                CardShowPanel.AddReplaceCardAnimation( card, newCardObj);
            }
            CardShowPanel.PlayReplaceCardAnimation();
           
        }

    }

}
