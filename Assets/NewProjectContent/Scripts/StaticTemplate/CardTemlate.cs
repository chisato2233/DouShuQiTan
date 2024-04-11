using System;
using System.Collections.Generic;
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

        public Material Material;
        public GameObject RuntimeCard;
    }
}
