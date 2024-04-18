using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    [System.Serializable]
    public struct CardInfo {
        public string CardName;
        public bool IsUpgrade;
        public bool Has阵型;
        public List<int[]> 阵型;

        public CardInfo(string cardName) {
            CardName = cardName;
            IsUpgrade = false;
            Has阵型 = false;
            阵型 = new List<int[]>();
        }

        public void InitCard(GameObject runtimeCard) {
            var runtime = runtimeCard.GetComponent<RuntimeCard>();
            if (runtime != null) {
                runtime.Name = CardName;
                runtime.Has阵型 = Has阵型;
                runtime.IsUpgrade = IsUpgrade;
                runtime.阵型 = 阵型;
            }
        }
    }


}
