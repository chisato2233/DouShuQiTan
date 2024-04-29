using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DouShuQiTan;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_棋盘格 : UI_枢元Reciver,IPointerEnterHandler {

    public Vector2Int CellPos;

    protected void Start() {
        base.Start();
    }


    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log($"{CellPos}");
    }

}
