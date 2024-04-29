using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using DouShuQiTan;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class UI_枢元Reciver : MonoBehaviour {
    public bool IsOccupied = false;
    public GameObject OwnerObject = null;
    public UnityEvent OnChange = new UnityEvent();


    private bool IsTrigger = false;

    protected void Awake() {

    }

    protected void Start() {
        RegistriterEvent();
        StartCoroutine(CheckIfTrigger());
    }

    protected void Update() {
    }




    public virtual bool Place枢元(GameObject ShuYuan) {
        if (IsOccupied) return false;
        IsOccupied = true;
        OwnerObject = ShuYuan;
        Move枢元();
        OnChange?.Invoke();
        return true;
    }

    void Move枢元() {
        Vector2 thisPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

        // 将屏幕坐标转换为第二个元素的本地坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(OwnerObject.transform.parent.GetComponent<RectTransform>(), 
            thisPosition, Camera.main, out Vector2 localPoint);

        // 设置第二个元素的位置
        OwnerObject.GetComponent<RectTransform>().
            DOAnchorPos(localPoint, 0.5f);
    }

    public virtual bool Remove枢元() {
        if (!IsOccupied || OwnerObject == null) return false;
        IsOccupied = false;
        OwnerObject = null;
        OnChange?.Invoke();
        return false;
    }




    //=========================================================================================

    IEnumerator CheckIfTrigger() {
        while (true) {
            yield return new WaitForSecondsRealtime(0.2f);
            CheckIfTriggerImpl();
        }
    }

    void CheckIfTriggerImpl() {
        if (RectTransformUtility.RectangleContainsScreenPoint(
                GetComponent<RectTransform>(), Input.mousePosition
            )) {
            IsTrigger = true;
        }
        else IsTrigger = false;
    }

    void RegistriterEvent() {
        GameIntro.GameSystem.UserOperationSystem.OnRelease枢元.AddListener((Select枢元) => {
            if (!IsTrigger || IsOccupied) return;
            if (Select枢元 != null) {
                var from = Select枢元.GetComponent<Runtime枢元>().Receiver;
                GameIntro.GameSystem.UserOperationSystem.OnMove枢元?.
                    Invoke(from, this, Select枢元.GetComponent<Runtime枢元>());
            }
        });

        GameIntro.GameSystem.UserOperationSystem.OnMove枢元.AddListener(
            (from, to, Obj) => {
                from?.Remove枢元();
                to?.Place枢元(Obj.gameObject);
                Obj?.RegisterReceiver(to);
            }
        );
        GameIntro.GameSystem.UserOperationSystem.OnMove枢元.AddListener((from, to, obj) => {
            Debug.Log($"!! Move Event {from},{to},{obj}"); 
        });
    }
}

