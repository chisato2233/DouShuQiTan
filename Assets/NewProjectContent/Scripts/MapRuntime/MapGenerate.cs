using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    public class MapGenerate : MonoBehaviour {

        [SerializeField] private List<MapNodeLayer> MapNodes = new List<MapNodeLayer>();

        void Awake() {
            for (int i = 9; i >= 0; --i) {
                MapNodes.Add(transform.GetChild(i).GetComponent<MapNodeLayer>());
            }
        }


        void Start() {
            InitMap();
        }




        private void InitMap() {
            if (GameIntro.MapLayer == 0) {
                GameIntro.MapLayer = 1;
                GeneratorNewMap();
            }
            else  LoadMap();
            
        }

        private void GeneratorNewMap() {
            var lastLayer = MapNodes[0];
            foreach (MapNodeLayer layer in MapNodes) {
                layer.Generate();
                layer.InstantiateMapNode();
                layer.InstantiateLine(lastLayer);
                lastLayer = layer; 
            }
        }

        private void SaveMap() {
            
        }

        private void LoadMap() {

        }
    }
}
