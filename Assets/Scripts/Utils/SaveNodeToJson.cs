using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public struct node {
    //哪种结点，1敌人2休息3事件4精英5Boss
    public int mark;
    //敌人，1风2云3精英4Boss
    //哪个事件,1风2云3精4增5减6中7赌
    public int branch;//对mark1,3有用
    //public float scale;
    public float[] position;
    public int state;//状态,1不可探索2可探索3探索完成
    public List<int> LinkedNodes;
}
public class SaveNodeToJason {
    public static void Save(List<List<node>> Nodes, string path) {
        string json = JsonConvert.SerializeObject(Nodes, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public static List<List<node>> LoadNodesFromFile(string path) {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<List<node>>>(json);
    }
}