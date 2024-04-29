using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackDict : MonoBehaviour {
    public UnityDictionary<string,Sprite> DictDic = new UnityDictionary<string, Sprite>();
    public List<GameObject> Slots = new List<GameObject>();


    void Awake() {
        DictDic.Init();
    }



    public void ShowAttackPredict(string name, int number,int index) {
        if (index > Slots.Count) return;
        Slots[index].GetComponent<Image>().overrideSprite = DictDic[name];
        Slots[index].GetComponentInChildren<TextMeshProUGUI>().text = name;
    }

}


[Serializable]
public class UnityDictionary<Key,Value> : Dictionary<Key,Value>{
    [Serializable]
    public struct KVP {
        public Key key;
        public Value value;
    }
    public List<KVP> KVPList;

    public void Init() {
        foreach (var kvp in KVPList) {
            this[kvp.key] = kvp.value;
        }
    }

}
