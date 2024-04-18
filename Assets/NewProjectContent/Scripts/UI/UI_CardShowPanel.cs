using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace DouShuQiTan  {
    public class UI_CardShowPanel : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler,IScrollHandler {
        List<GameObject> ShowCards = new List<GameObject>();

        public  Events events;
        public Rest rest;
        
        public GameObject CardGrip;
        private Sequence sequence;

        public bool IsForgetRest = false;
        public bool IsForgetEvent = false;

        public float bottom;
        public float top;
        public float ScrollSpeed;
        public float DragSpeed;
        bool IsDrag = false;
        bool IsScroll = false;
        Vector2 startPosition;
        Vector2 endPosition;
        

        public void AddShowCard(GameObject RuntimeCard) {
            ShowCards.Add(RuntimeCard);
            CardGrip.transform.GetChild(ShowCards.Count - 1).GetComponent<Image>().overrideSprite = 
                RuntimeCard.GetComponent<SpriteRenderer>().sprite;
        }

        public void RemoveShowCard(GameObject RuntimeCard) {
            CardGrip.transform.GetChild(ShowCards.Count - 1).GetComponent<Image>().sprite = null;
            ShowCards.Remove(RuntimeCard);
            Destroy(RuntimeCard);
        }
        public Sequence AddReplaceCardAnimation(GameObject oldCard, GameObject newCard) {
            sequence.Append(oldCard.transform.DOScale(Vector3.zero, 0.5f))
                .OnComplete(() => Destroy(oldCard))
                .Insert(0, newCard.transform.DOScale(Vector3.one, 0.5f));
            return sequence;
        }

        public void PlayReplaceCardAnimation() {
            sequence.OnComplete(() => {
                events.GetComponent<Animator>().Play("eventDisappear");
                events.node.save();
                GetComponent<PackageScroll>().enabled = true;
            });
            sequence.Play();
        }

        public void PlayRemoveCardAnimation() {
            if (IsForgetRest) {
                IsForgetRest = false;
                rest.GetComponent<Animator>().Play("HeiPing");
                rest.node.save();
            }
            else if (IsForgetEvent) {
                IsForgetEvent = false;
                events.GetComponent<Animator>().Play("eventDisappear");
                events.node.save();
            }
            gameObject.SetActive(false);
        }

        public void PlayUpgradeCardAnimation() {
            events.GetComponent<Animator>().Play("eventDisappear");
            events.node.save();
            gameObject.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData) {
            startPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        }

        public void OnDrag(PointerEventData eventData) {
            endPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            Vector2 move = endPosition - startPosition;
            startPosition = endPosition;
            Move(new Vector2(0, -move.y * DragSpeed));
        }

        public void OnEndDrag(PointerEventData eventData) {
            IsDrag = false;
        }

        public void OnScroll(PointerEventData eventData) {
            Move(eventData.scrollDelta * ScrollSpeed);
        }

        void Move(Vector2 delta) {
            Vector2 position = CardGrip.GetComponent<RectTransform>().anchoredPosition;
            if (position.y >= top && position.y <= bottom) {
                IsScroll = true; IsDrag = true;
                position -= delta;
                position.y = Mathf.Clamp(position.y, top, bottom);
                CardGrip.GetComponent<RectTransform>().DOAnchorPos(position, 0.2f);
            }
        }
    }
}
