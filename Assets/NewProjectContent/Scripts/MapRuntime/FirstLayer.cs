
using UnityEngine;

namespace DouShuQiTan{
    public class FirstLayer : MapNodeLayer{

        public override void Generate() {
            var cnt = Random.Range(2, 4);
            var indeces = RandomTools.UniqueRandomNumbers(0, NodeGrides.Count, cnt);
            foreach (var i in indeces) {
                AvailableNodes.Add(RandomTools.Probability(0.5f) ? 
                    (Library.Find("风") ,i): 
                    (Library.Find("云"),i)
                );
            }

        }

        public override void InstantiateLine(MapNodeLayer lastLayer) {
        }
    }
}
