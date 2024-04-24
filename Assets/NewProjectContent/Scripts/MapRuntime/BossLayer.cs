using DouShuQiTan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLayer : MapNodeLayer
{
    public override void InstantiateLine(MapNodeLayer lastLayer) {
        foreach (var node in lastLayer.NodeList) {
            var runtimeMapNode = node.GetComponent<RuntimeMapNode>();
            runtimeMapNode.LinkedList.Add(NodeList[0].GetComponent<RuntimeMapNode>());

            RenderLine(runtimeMapNode);

        }
    }
}
