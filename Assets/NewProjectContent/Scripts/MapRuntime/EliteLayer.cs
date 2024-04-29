using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using UnityEngine;

public class EliteLayer : MapNodeLayer {

    void Awake() {
        NodeList.Add(transform.GetChild(0).gameObject);
    }
    public override void InstantiateLine(MapNodeLayer lastLayer) {
        foreach (var node in lastLayer.NodeList) {
            var runtimeMapNode = node.GetComponent<RuntimeMapNode>();
            runtimeMapNode.LinkedList.Add(NodeList[0].GetComponent<RuntimeMapNode>());
            
            RenderLine(runtimeMapNode);

        }
    }
}
