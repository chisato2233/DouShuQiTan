using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace DouShuQiTan{
    public class Runtime枢元 :MonoBehaviour,
        IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,
        IBeginDragHandler,IDragHandler, IEndDragHandler 
    {
        
        
        [Header("State")]
        public 枢元Library Library;
        public bool Enable = true;
        public UI_枢元Reciver Receiver;

        private Vector2 StartPosition;


        [Header("Attribute")]
        public float ExtraGradeProbability = 0;
        [SerializeField] private int currentGrade = 1;
        [SerializeField] private bool IsTopOnTop = true;
        [SerializeField] private GameObject Text;
        
        public string CurrentType { get; private set; }

        [Header("Event")]
        public UnityEvent<int> OnUpgrade = new UnityEvent<int>();
        public UnityEvent<bool,string> OnChangeType = new UnityEvent<bool,string>();


        public UnityEvent<UI_枢元Reciver> OnChangeReceiver = new UnityEvent<UI_枢元Reciver>();

        [Header("Configuration")]
        private Dictionary<int, float> UpgradeProbability = new Dictionary<int, float> {
            {2, 0.10f}, // 当grade0等于2时，基础概率为10%
            {3, 0.55f}, // 当grade0等于3时，基础概率为55%
            {4, 0.85f}  // 当grade0等于4时，基础概率为85%
        };

        public Dictionary<string, float> TypeChangeProbability = new Dictionary<string, float> {
            {"火",0.2f},
            {"水",0.2f},
            {"土",0.2f},
            {"无属性",0.2f},
            {"风",0.2f},
        };

        public int CurrentGrade {
            get => currentGrade;
            set {
                if (currentGrade != value) {
                    currentGrade = value;
                    OnUpgrade?.Invoke(currentGrade); // 触发事件
                }
            }
        }


        void Start() {
            OnUpgrade.AddListener(x=>Text.GetComponent<TextMeshProUGUI>().text = $"<sprite={x}>");
        }

        public void RegisterReceiver(UI_枢元Reciver receiver) {
            Receiver = receiver;
            OnChangeReceiver?.Invoke(receiver);
        }

        public void Upgrade() {
            TryUpgrade();
        }

        void TryUpgrade() {
            UpgradeProbability.TryGetValue(CurrentGrade, out var basicValue);
            var probability = basicValue + 2 * ExtraGradeProbability;
            if (RandomTools.Probability(probability)) {
                CurrentGrade++;
                TryChangeType();

            }
        }

        void TryChangeType() {
    
            List<float> probabilities = new List<float>();
            List<string> keys = new List<string>();

            
            foreach (var kvp in TypeChangeProbability) {
                if (CurrentType != "无属性" && kvp.Key == "无属性") { continue; }
                keys.Add(kvp.Key);
                probabilities.Add(kvp.Value);
            }

            int selectedIndex = RandomTools.ChooseIndexByProbability(probabilities.ToArray());
            string selectedKey = keys[selectedIndex];
            ChangeTypeImpl(selectedKey);
        }


        void ChangeTypeImpl(string Type) {
            OnChangeType?.Invoke(IsTopOnTop, Type);
            if (IsTopOnTop) {
                transform.Find("顶").GetComponent<Image>().overrideSprite = 
                    Library.FindTop(Type);
            }
            else {
                transform.Find("底").GetComponent<Image>().overrideSprite =
                    Library.FindBottom(Type);
            }

            this.CurrentType = Type;
        }




        //UI Callback=====================================================================================
        public void OnPointerEnter(PointerEventData eventData) {
            foreach (var componentsInChild in GetComponentsInChildren<Image>()) {
                if (componentsInChild == GetComponent<Image>()) continue;
                componentsInChild.DOFade(0.5f, 0.5f);
            }
        }

        public void OnPointerExit(PointerEventData eventData) {
            foreach (var componentsInChild in GetComponentsInChildren<Image>()) {
                if (componentsInChild == GetComponent<Image>()) continue;
                componentsInChild.DOFade(1, 0.5f);
            }
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (!Enable) return;
        }
        public void OnBeginDrag(PointerEventData eventData) {
            StartPosition = GetComponent<RectTransform>().anchoredPosition;
            GetComponent<Image>().raycastTarget = false;
            GetComponentInChildren<TextMeshProUGUI>().raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData) {
            var rectTrans = GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),eventData.position,Camera.main,out var Pos
            );
            rectTrans.anchoredPosition = Pos;
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (eventData.pointerEnter?.GetComponent<UI_枢元Reciver>() != null) {
                var to = eventData.pointerEnter.GetComponent<UI_枢元Reciver>();
                Debug.Log($"!!!  {to} !!!");
                GameIntro.GameSystem.UserOperationSystem.OnMove枢元?.Invoke(Receiver, to, this);
            }
            else GetComponent<RectTransform>().DOAnchorPos(StartPosition, 0.5f);
            GetComponent<Image>().raycastTarget = true;
        }

    }
}
