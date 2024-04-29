using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class UI_棋槽集合 : MonoBehaviour {
    public List<GameObject> UI_棋槽List;
    public GameObject 枢元GameObject;

    void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            UI_棋槽List.Add( transform.GetChild(i).gameObject);
        }
    }
    void Start() {
        Fix枢元OnEmpty(-1);
    }


    public int Fix枢元OnEmpty(int maxCount) {
        var EmptyList = CheckEmptyNumber();
        int index = 0;
        if (maxCount < 0) maxCount = int.MaxValue;

        foreach (var o in EmptyList) {
            if (index >= maxCount) break;
            var obj = Instantiate(枢元GameObject, transform.parent);
            obj.GetComponent<Runtime枢元>().RegisterReceiver(o.GetComponent<UI_枢元Reciver>());
            o.GetComponent<UI_枢元Reciver>().Place枢元(obj);
            index++;
        }

        return index;
    }

    List<GameObject> CheckEmptyNumber() {
        List<GameObject> res = new List<GameObject>();

        foreach (var o in UI_棋槽List) {
            if (!o.GetComponent<UI_枢元Reciver>().IsOccupied) {
                res.Add(o);
            }
        }

        return res;
    }
}
