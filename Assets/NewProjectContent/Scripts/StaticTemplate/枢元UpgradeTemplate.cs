
using System;
using UnityEngine;
namespace DouShuQiTan {
    [Serializable]
    [CreateAssetMenu(
    menuName = "Game/Templates/枢元Upgrade" ,
    fileName = "枢元Upgrade")]
    public class 枢元UpgradeTemplate : ScriptableObject {
        public string Name;
        public int Id;
        public string Type;
        public string Description;
        public string Comment;
        public GameObject Runtime枢元Upgrade;
        // Add More Fields Here
    }
}