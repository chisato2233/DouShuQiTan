using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using UnityEngine;

[CreateAssetMenu(fileName = "Map Node Library",menuName = "Game/Libraries/Map Node Library")]
public class MapNodeLibrary : ScriptableObject {
    public List<MapNodeTemplate> MapNodes = new List<MapNodeTemplate>();

    private Dictionary<string, MapNodeTemplate> Dict = new Dictionary<string, MapNodeTemplate>();
    void Awake() {
        foreach (var node in MapNodes) {
            Dict[node.Name] = node;
        }
    }
    public GameObject Find(string name) {
        return MapNodes.Find(x => x.Name == name).RuntimeNode;
    }
}
