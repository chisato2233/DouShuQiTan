using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_棋槽 : UI_枢元Reciver {
    public List<UI_棋槽> 棋槽List = new List<UI_棋槽>();
    public UnityEvent<UI_棋槽> On棋槽Change = new UnityEvent<UI_棋槽>();

    protected void Start() {
        base.Start();
        InitSlot();
    }


    void InitSlot() {

        for (int i = 0; i < transform.childCount; i++) {
            var cell = transform.GetChild(i).GetComponent<UI_棋槽>();
            棋槽List.Add(cell);

            cell.OnChange.AddListener(() => {
                On棋槽Change?.Invoke(cell);
            });

        }

    }

}
