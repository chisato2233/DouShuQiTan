using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using UnityEngine;

public class NormalLayer : MapNodeLayer {
    [SerializeField] private Vector2Int NodeCntRange = new Vector2Int(3,6);

    public override void Generate() {
        var cnt = Random.Range(NodeCntRange.x, NodeCntRange.y);
        var RandomIndex = RandomTools.UniqueRandomNumbers(0, NodeGrides.Count, cnt);
        foreach (var index in RandomIndex)  {
            GenerateImpl(index);
        }

    }

    private void GenerateImpl(int index) {
        var Index = RandomTools.ChooseIndexByProbability(0.35f, 0.25f, 0.2f, 0.15f,0.05f);
        switch (Index) {
            case 0:
                AvailableNodes.Add((Library.Find("风"),index));
                break;
            case 1:
                AvailableNodes.Add((Library.Find("云"),index));
                break;
            case 2:
                AvailableNodes.Add((Library.Find("事件"),index));
                break;
            case 3:
                AvailableNodes.Add((Library.Find("休整"),index));
                break;
            case 4:
                AvailableNodes.Add((Library.Find("饥民"),index));
                break;
        }
    }

}
