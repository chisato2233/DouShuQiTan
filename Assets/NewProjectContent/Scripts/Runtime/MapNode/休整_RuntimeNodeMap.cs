using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShuQiTan {
    public class 休整_RuntimeNodeMap : RuntimeMapNode {
        void Start() {
            int random = RandomTools.ChooseIndexByProbability(
                1 / 8f, 
                1 / 8f, 
                50 / 1000f, 
                0.2f, 
                0.2f, 
                0.1f
            );

            switch (random) {
                
            }
        }
    }
}
