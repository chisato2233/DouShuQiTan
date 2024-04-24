using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    [Serializable]
    [CreateAssetMenu(
        menuName = "Game/Libraries/咒印",
        fileName = "咒印")]
    public class 咒印Library :ScriptableObject{
        public List<咒印Template> 咒印List = new List<咒印Template>();
    }


}
