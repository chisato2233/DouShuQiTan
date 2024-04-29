using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class UI_棋盘格集合 : MonoBehaviour {
    public List<List<GameObject>> 棋盘格List = new List<List<GameObject>>();
    public UnityEvent<UI_棋盘格> On棋盘ListChange = new UnityEvent<UI_棋盘格>();

    void Start() {
        InitializeBoard();
    }

    void InitializeBoard() {
        
        int[] piecesPerRow = new int[] { 2, 4, 6, 6, 4, 2 };

        // 遍历棋盘的每一行
        for (int i = 0; i < transform.childCount; i++) {
            GameObject rowObject = transform.GetChild(i).gameObject;
            List<GameObject> rowList = new List<GameObject>(new GameObject[6]);

            int offset = (6 - piecesPerRow[i]) / 2;

            // 遍历行中的每一个格子
            for (int j = 0; j < rowObject.transform.childCount; j++) {
                GameObject cell = rowObject.transform.GetChild(j).gameObject;
                rowList[offset + j] = cell;  // 根据偏移量设置位置
                cell.GetComponent<UI_棋盘格>().CellPos = new Vector2Int(i, j + offset);
                
                //设置改变事件的监听
                cell.GetComponent<UI_棋盘格>().OnChange.AddListener(() => {
                    On棋盘ListChange?.Invoke(cell.GetComponent<UI_棋盘格>());
                });

            }

            棋盘格List.Add(rowList);
        }

        //Print();
    }

    void Print() {
        foreach (var row in 棋盘格List) {
            string text = $"{row.GetHashCode()}：";
            foreach (var o in row) {
                if (o != null) {
                    text += 1;
                }
                else text += 0;
            }
            Debug.Log(text);
        }
    }
}
