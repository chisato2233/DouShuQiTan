using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DouShuQiTan {
    [Serializable]
    [CreateAssetMenu(
        menuName = "Game/Templates",
        fileName = "Card",
        order = 0)]
    public class CardTemplate : ScriptableObject {
        public int Id;
        public string Name;
        public string Type;
        public string 枢元Num;
        public Vector2Int Range;
        public string Describe;
        public string UpgradeDescribe;
        public string Comment;

        public bool Has阵型;


        public GameObject RuntimeCard => Resources.Load<GameObject>($"Prefabs/Card/{Name}");
        public GameObject CardUI=>Resources.Load<GameObject>($"Prefabs/CardUI/{Name}_UI");

        public object Clone() {
            CardTemplate res = new CardTemplate();
            res.Id = Id;
            res.Name = Name;
            res.Type = Type;
            res.枢元Num = 枢元Num;
            res.Range = Range;
            res.Describe = Describe;
            res.UpgradeDescribe = UpgradeDescribe;
            res.Comment = Comment;
            res.Has阵型 = Has阵型;
            return res;
        }
    }
}
