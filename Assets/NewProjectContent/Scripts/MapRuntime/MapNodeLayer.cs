using DouShuQiTan;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Random = UnityEngine.Random;

public class MapNodeLayer : MonoBehaviour {
    
    protected List<(GameObject,int)> AvailableNodes = new List<(GameObject,int)>();
    protected List<GameObject> NodeGrides = new List<GameObject>();
    public List<GameObject> NodeList = new List<GameObject>();

    public MapNodeLibrary Library;

    void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            NodeGrides.Add(transform.GetChild(i).gameObject);
        }
    }
    
    public virtual void Generate() { }




    public void InstantiateMapNode() {
        
        foreach (var (node,index) in AvailableNodes) {
            Vector2 Pos = new Vector2();
            Pos = GetNewPosition(node);
            var obj = Instantiate(node, NodeGrides[index].transform);
            NodeList.Add(obj);
        }
    }

    public virtual void InstantiateLine(MapNodeLayer lastLayer) {
        foreach (GameObject lastNode in lastLayer.NodeList) {
            //为上一层结点创建对于本层的链接并渲染线条
            RuntimeMapNode runtimeMapNode = lastNode.GetComponent<RuntimeMapNode>();
            
            CreateRandomLink(runtimeMapNode);
            
            RenderLine(runtimeMapNode);
        }
    }

    protected void CreateRandomLink(RuntimeMapNode runtimeMapNode) {
        var cnt = Random.Range(1, NodeList.Count + 1);
        var RandomIndex = RandomTools.UniqueRandomNumbers(0, NodeList.Count, cnt);

        foreach (var index in RandomIndex) {
            runtimeMapNode.LinkedList.Add(NodeList[index].GetComponent<RuntimeMapNode>());
        }

    }

    protected void RenderLine(RuntimeMapNode from) {

        UILineRenderer lineRenderer = from.GetComponentInChildren<UILineRenderer>();
        if (!lineRenderer) {
            GameObject lineObject = new GameObject("Line");
            lineObject.transform.SetParent(from.transform, false); // 设置父对象，并保留局部坐标
            RectTransform rectTransform = lineObject.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(100, 100); // 根据需要调整大小

            lineRenderer = lineObject.AddComponent<UILineRenderer>();
        }

        // 初始化位置列表
        List<Vector2> positions = new List<Vector2>();

        // 转换 from 节点的位置
        Vector2 screenPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            from.transform.parent as RectTransform, 
            from.transform.position, 
            Camera.main, 
            out screenPoint
        );
        positions.Add(screenPoint);

        // 转换 linkedNode 的位置
        foreach (var linkedNode in from.LinkedList) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                from.transform.parent as RectTransform, 
                linkedNode.gameObject.transform.position, 
                Camera.main, 
                out screenPoint
            );

            positions.Add(screenPoint);
        }

        lineRenderer.Points = positions.ToArray();
        lineRenderer.LineList = true; // 设置为虚线
        lineRenderer.LineThickness = 2;
        lineRenderer.SetVerticesDirty(); // 强制更新渲染器内容
    }



    Vector2 GetNewPosition(GameObject node) {
        RectTransform currentRect = transform.parent.GetComponent<RectTransform>();
        RectTransform nodeRect = node.GetComponent<RectTransform>();

        // 计算x坐标的最小值和最大值
        float halfNodeWidth = (nodeRect.sizeDelta.x / 2) * node.transform.localScale.x;
        float minX = -currentRect.sizeDelta.x/2 + halfNodeWidth;
        float maxX = currentRect.sizeDelta.x/2 - halfNodeWidth;

        return new Vector2(Random.Range(minX, maxX), Random.Range(-1f, 1f));
    }

    bool CheckOverlap(GameObject node,Vector2 pos) {
        
        var TargetRect = FixRect(node.GetComponent<RectTransform>());
        TargetRect.position = pos;
        foreach (var exisitNode in NodeList) {

            var ExistRect = FixRect(exisitNode.GetComponent<RectTransform>());
            ExistRect.width *= exisitNode.transform.localScale.x;

            if (TargetRect.Overlaps(ExistRect)) {
                return true;
            }
        }

        return false;
    }

    Rect FixRect(RectTransform rectTrans) {
        var rect= rectTrans.rect;
        rect.width *= rectTrans.transform.localScale.x;
        rect.height *= rectTrans.transform.localScale.y;
        return rect;
    }



}
