
using UnityEngine;

namespace DouShuQiTan{
    public class FirstLayer : MapNodeLayer{

        public override void Generate() {
            var cnt = Random.Range(2, 3);
            for (int i = 0; i < cnt; i++) {
                AvailableNodes.Add(RandomTools.Probability(0.5f) ? 
                    Library.Find("风") : 
                    Library.Find("云")
                );
            }

        }

        public override void InstantiateLine(MapNodeLayer lastLayer) {
        }
    }
}
