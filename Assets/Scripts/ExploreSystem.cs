using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;
using TMPro;
using Newtonsoft.Json;
//using System.Int32;

public static class GameData {
    /// <summary>
    /// 无需存档
    /// </summary>
    public static int NanDu = 0;
    //风0-2，云3-5，精英6，Boss7，各位置上的数字表示有多少该敌人
    public static int[] Enemy = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    //是否属于挑战关，奖励不同
    public static bool IsChallenge = false;
    //是否是第一关
    public static bool IsOne = false;
    //记录该结点为第几个结点，战斗结算用，为了存档
    public static int abscissa;
    public static int ordinate;
    public static bool IsWin = false;
    /// <summary>
    /// 需存档
    /// </summary>
    public static float HP = 50;
    public static float MaxHp = 50;
    //当前持有咒印
    public static List<int> ZhouYin = new List<int> { };
    
    
    //牌数不大于16，全部手牌通过字典存储
    //当前持有手牌，一个位置代表一张卡牌，数字为该卡牌类型在字典中的序号，初始化为初始手牌，进入战斗后读取载入对应预制体
    public static List<int> HoldCard = new List<int> {  };
    //卡牌是否升级，与上一个List一一对应，表明该牌是否升级，bool值内存小
    public static List<bool> IsUpGrade = new List<bool> {  };
    //卡牌是否有阵型
    public static List<bool> HasZhenXing = new List<bool> { };
    //卡牌阵型
    public static List<List<int[]>> ZhenXing = new List<List<int[]>> { };

    //枢元等级强化效果//初始等级，默认值为0（1级（个别
    public static List<int> ExtraGrade = new List<int> { };
    //每次升级额外升级，默认值为0（每次升一级（个别
    public static List<int> OnceUpGrade = new List<int> { };
    //进化概率，前三次升级进化出属性的概率（全部（提高的次数，每次提高2%）
    public static List<int> ExtraRandom = new List<int> { };
    //属性概率，0无1土2水3火4风（全部（百分号前的数）
    public static List<int[]> ShuXingGaiLv = new List<int[]> { };
    //各属性是否强化
    public static int HuoUp, ShuiUp, TuUp, FengUp;
}



[System.Serializable]
public class SerializableNodes {
    public List<NodeWrapper> allNodes = new List<NodeWrapper>();

    // NodeWrapper类用于包装每个节点以及其属于的层级信息
    [System.Serializable]
    public class NodeWrapper {
        public node node;
        public int layerIndex; // 节点所属的层级

        public NodeWrapper(node node, int layerIndex) {
            this.node = node;
            this.layerIndex = layerIndex;
        }
    }

    // 将嵌套列表转换为一维列表并记录层级信息
    public void ConvertFromNestedList(List<List<node>> nestedList) {
        allNodes.Clear();
        for (int i = 0; i < nestedList.Count; i++) {
            foreach (var node in nestedList[i]) {
                allNodes.Add(new NodeWrapper(node, i));
            }
        }
    }

    // 从一维列表恢复嵌套列表
    public List<List<node>> ConvertToNestedList() {
        List<List<node>> nestedList = new List<List<node>>();
        foreach (var wrapper in allNodes) {
            // 确保有足够的层级列表
            while (nestedList.Count <= wrapper.layerIndex) {
                nestedList.Add(new List<node>());
            }
            nestedList[wrapper.layerIndex].Add(wrapper.node);
        }
        return nestedList;
    }
}



public class ExploreSystem : MonoBehaviour
{
    public static bool IsNewExplo=true;
    public GameObject map;
    public GameObject backGround;
    public GameObject HPimage;
    public GameObject HPText;
    Vector3 HpStart;
    Vector3 HpStartPos;

    public int layerNumber=10;
    public float layerdistance=2f;
    public bool IsNewLayer=false;
    public static int nowLayer = 10;
    public List<int> elite = new List<int>{ 5 };
    public List<float> scale;
    //存数据
    public List<List<node>> Nodes;
    //放物体
    public List<List<GameObject>> NodeObjects;
    private ScrollController scrollController;
    //string Filepath = "D:"+"/map.txt";
    //导出后改
    public string Filepath { get; private set; } 

    // Start is called before the first frame update
    private void Awake() {
        Filepath = Application.dataPath + "/map.json";
        layerNumber = 10;
        elite = new List<int> { 5 };
        layerdistance = 2.4f;

        HPimage = GameObject.Find("PlayerXie");
        HPText = GameObject.Find("HPtext");
        HpStart = HPimage.transform.localScale;
        HpStartPos = HPimage.transform.position;
        UpdateHP();

        AudioManager.Instance.PlaySfx("EnterNewScene");
        Nodes = new List<List<node>>{ };
        NodeObjects = new List<List<GameObject>> { };
        map = GameObject.Find("mapRoot");
        backGround = GameObject.Find("backGround");
        scale = new List<float> { 1.8f, 1.8f, 2f };
        //每次回到这个界面都是新一层
        IsNewLayer = true;
        scrollController = backGround.GetComponent<ScrollController>();
        scrollController.enabled = false;
        if(nowLayer==1)
        {
            GameData.IsOne = true;
        }

        CreateMap();
        PlayerPrefs.SetInt("StopExplo", 1);
    }
    public void PlayClickSFX()
    {
        AudioManager.Instance.PlaySfx("icon");
    }
    public void PlayEnterSFX()
    {
        AudioManager.Instance.PlaySfx("PlaceMouse");
    }

    public void UpdateHP() {
        if(HPimage!=null) {
            float HP=GameData.HP,MaxHp=GameData.MaxHp;
            float HalfLenth = 0.8f;
            
            //0位小数
            if(HPText!=null)
                HPText.GetComponent<TextMeshPro>().text = HP.ToString("F0") + "/" + MaxHp.ToString("F0");
            float newX = HP * HpStart.x / MaxHp;
            HPimage.transform.localScale = new Vector3(newX, HpStart.y, HpStart.z);
            float newPosX1 = HP * HalfLenth / MaxHp;
            float newPosX = HalfLenth - newPosX1;
            HPimage.transform.position = new Vector3(HpStartPos.x - newPosX, HpStartPos.y, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //地图生成完
        if (Nodes.Count >= layerNumber)
        {
            if (scrollController.enabled == false)
                scrollController.enabled = true;
            //if(backGround.GetComponent<ScrollController>()==null)
            //backGround.AddComponent<ScrollController>();
            if (IsNewLayer)
            {
                //给正在探索的这一层节点添加脚本
                IsNewLayer = false;
                for(int i=0;i<nowLayer-1;i++)
                {
                    for(int j=0;j<Nodes[i].Count;j++)
                    {
                        if (NodeObjects[i][j].GetComponent<Node>() != null)
                            Destroy(NodeObjects[i][j].GetComponent<Node>());
                        if (NodeObjects[i][j].GetComponent<Animator>() != null)
                            Destroy(NodeObjects[i][j].GetComponent<Animator>());
                        if (Nodes[i][j].state == 3)
                        {
                            NodeObjects[i][j].transform.GetChild(1).gameObject.SetActive(true);
                            NodeObjects[i][j].transform.GetChild(0).gameObject.SetActive(true);

                        }
                        if (Nodes[i][j].state == 1)
                        {
                            NodeObjects[i][j].transform.GetChild(0).gameObject.SetActive(true);

                        }
                    }
                }
                for (int i = 0; i < Nodes[nowLayer - 1].Count; i++)
                {
                    bool IsExplore = false;
                    node _node = Nodes[nowLayer - 1][i];
                    if (nowLayer == 1)
                        IsExplore = true;
                    else
                        for (int j = 0; j < _node.LinkedNodes.Count; j++)
                        {
                            var num = _node.LinkedNodes[j];
                            if (Nodes[nowLayer - 2][num].state == 3)
                            {
                                IsExplore = true;
                                break;
                            }
                        }

                    if (IsExplore)
                    {
                        _node.state = 2;
                        Nodes[nowLayer - 1][i] = _node;
                        if (NodeObjects[nowLayer-1][i].GetComponent<CircleCollider2D>() == null&&nowLayer!=layerNumber)
                        {
                            NodeObjects[nowLayer - 1][i].AddComponent<CircleCollider2D>();

                        }
                        else if(NodeObjects[nowLayer - 1][i].GetComponent<BoxCollider2D>() == null&&nowLayer == layerNumber)
                        {
                            NodeObjects[nowLayer - 1][i].AddComponent<BoxCollider2D>();

                        }
                        if (NodeObjects[nowLayer - 1][i].GetComponent<Node>() == null)
                        {
                            //Node _Node = new Node(nowLayer-1,i,Nodes[nowLayer][i].mark, Nodes[nowLayer][i].branch);
                            NodeObjects[nowLayer-1][i].AddComponent<Node>();
                            Node _Node = NodeObjects[nowLayer-1][i].GetComponent<Node>();
                            _Node.InitNode(nowLayer - 1, i, Nodes[nowLayer-1][i].mark, Nodes[nowLayer-1][i].branch, this);
                            
                        }
                        if (NodeObjects[nowLayer - 1][i].GetComponent<Animator>() == null)
                        {
                            NodeObjects[nowLayer - 1][i].AddComponent<Animator>();
                            var animator = NodeObjects[nowLayer - 1][i].GetComponent<Animator>();
                            if (elite.Contains(nowLayer - 1))
                                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Art/Animation/Elite");
                            else if (layerNumber == nowLayer)
                                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Art/Animation/Boss");
                            else
                                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Art/Animation/NormalNode");
                        }
                    }

                }
                SaveNodeToJason.Save(Nodes, Filepath);
            }
            
        }

    }
    void CreateBossLayer(int i) {
        //Boss
        List<node> noderow = new List<node> { };
        List<GameObject> nodeRow = new List<GameObject> { };
        node _node;
        _node.mark = 5;
        _node.branch = 0;
        //_node.scale = 1.5f;
        _node.state = 1;
        _node.position = new float[2];
        _node.position[0] = 0;
        _node.position[1] = scale[2] + layerdistance * i + map.transform.position.y;
        _node.LinkedNodes = new List<int> { };
        for (int t = 0; t < Nodes[i - 1].Count; t++) {
            DrawLine(noderow, i, noderow.Count, t, _node);
        }

        nodeRow = LoadGameObject(nodeRow, _node);

        noderow.Add(_node);
        NodeObjects.Add(nodeRow);
        Nodes.Add(noderow);
    }
    private void CreateEliteLayer(int i) {
        //精英
        List<node> noderow = new List<node> { };
        List<GameObject> nodeRow = new List<GameObject> { };
        node _node;
        _node.mark = 4;
        _node.branch = 3;
        //_node.scale = 0.5f;
        _node.state = 1;
        _node.position = new float[2];
        _node.position[0] = 0;
        _node.position[1] = layerdistance * i + map.transform.position.y;
        _node.LinkedNodes = new List<int> { };
        for (int t = 0; t < Nodes[i - 1].Count; t++) {
            DrawLine(noderow, i, noderow.Count, t, _node);
        }

        nodeRow = LoadGameObject(nodeRow, _node);

        noderow.Add(_node);
        NodeObjects.Add(nodeRow);
        Nodes.Add(noderow);
    }

    private void CreateFirstLayer(int i) {
        List<node> noderow = new List<node> { };
        List<GameObject> nodeRow = new List<GameObject> { };
        int realnum = 0;
        for (int j = 0; j < 6; j++) {
            if (j == 0) {
                node _node;
                _node.mark = 1;
                if (UnityEngine.Random.Range(0, 10) < 7)
                    _node.branch = 1;
                else
                    _node.branch = 2;
                _node.state = 2;
                _node.position = new float[2];
                _node.position[1] = UnityEngine.Random.Range(-0.2f, 0.2f) + map.transform.position.y +
                                    layerdistance * i; //y轴
                _node.position[0] = UnityEngine.Random.Range(-5f, -2f); //x轴
                _node.LinkedNodes = new List<int> { };
                nodeRow = LoadGameObject(nodeRow, _node);

                noderow.Add(_node);
                realnum++;
            }
            else if (UnityEngine.Random.Range(0, 5) >= 3 && realnum <= 3) {
                node _node;
                _node.mark = 1;
                _node.branch = UnityEngine.Random.Range(1, 3);
                _node.state = 2;
                _node.position = new float[2];
                _node.position[1] = UnityEngine.Random.Range(-0.2f, 0.2f) + map.transform.position.y +
                                    layerdistance * i; //y轴
                if (realnum == 1)
                    _node.position[0] =
                        UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 1f);
                else if (realnum == 2)
                    _node.position[0] =
                        UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 4f);
                else if (realnum == 3)
                    _node.position[0] =
                        UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 7f);
                _node.LinkedNodes = new List<int> { };
                nodeRow = LoadGameObject(nodeRow, _node);


                noderow.Add(_node);
                realnum++;
            }
        }

        NodeObjects.Add(nodeRow);
        Nodes.Add(noderow);
    }

    private void CreateRelaxLayer(int i) {
        //最后一层是休息
        List<node> noderow = new List<node> { };
        List<GameObject> nodeRow = new List<GameObject> { };
        int realnum = 0;
        for (int j = 0; j < 5; j++) {
            if (j == 0) {
                node _node;
                _node.mark = 2;
                _node.branch = 0;
                _node.state = 1;
                _node.position = new float[2];
                _node.position[1] = UnityEngine.Random.Range(-0.2f, 0.2f) + map.transform.position.y + layerdistance * i;//y轴
                _node.position[0] = UnityEngine.Random.Range(-5f, -2f);//x轴
                _node.LinkedNodes = new List<int> { };
                for (int t = 0; t < Nodes[i - 1].Count; t++)
                    if ((UnityEngine.Random.Range(2, 5) > 2 && _node.LinkedNodes.Count < 3)
                        || _node.LinkedNodes.Count < 1) {
                        DrawLine(noderow, i, noderow.Count, t, _node);
                    }
                    else if (noderow.Count > 0) {
                        if (!noderow[(noderow.Count - 1)].LinkedNodes.Contains(t))
                            DrawLine(noderow, i, noderow.Count, t, _node);

                    }
                nodeRow = LoadGameObject(nodeRow, _node);

                noderow.Add(_node);
                realnum++;
            }
            else if (UnityEngine.Random.Range(0, 5) >= 3 && realnum <= 3) {
                node _node;
                _node.mark = 2;
                _node.branch = 0;
                _node.state = 1;
                _node.position = new float[2];
                _node.position[1] = UnityEngine.Random.Range(-0.2f, 0.2f) + map.transform.position.y + layerdistance * i;//y轴
                if (realnum == 1)
                    _node.position[0] = UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 1f);
                else if (realnum == 2)
                    _node.position[0] = UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 4f);
                else if (realnum == 3)
                    _node.position[0] = UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 7f);
                _node.LinkedNodes = new List<int> { };

                for (int t = 0; t < Nodes[i - 1].Count; t++) {
                    if ((UnityEngine.Random.Range(2, 5) > 2 && _node.LinkedNodes.Count < 3)
                        || _node.LinkedNodes.Count < 1) {
                        DrawLine(noderow, i, j, t, _node);
                    }
                    else if (noderow.Count > 0) {
                        if (!noderow[(noderow.Count - 1)].LinkedNodes.Contains(t))
                            DrawLine(noderow, i, j, t, _node);
                    }
                }
                nodeRow = LoadGameObject(nodeRow, _node);


                noderow.Add(_node);
                realnum++;
            }
        }
        if (realnum == 1) {
            for (int t = 0; t < Nodes[i - 1].Count; t++) {
                if (!noderow[0].LinkedNodes.Contains(t))
                    DrawLine(noderow, i, realnum - 1, t, noderow[0]);

            }
        }
        NodeObjects.Add(nodeRow);
        Nodes.Add(noderow);
    }
    private void CreateNormalLayer(int i) {
        //除Boss和精英外
        List<node> noderow = new List<node> { };
        List<GameObject> nodeRow = new List<GameObject> { };
        int realnum = 0;
        for (int j = 0; j < 6; j++) {
            if (j == 0) {
                node _node;
                int randomNum = UnityEngine.Random.Range(0, 100);
                if (randomNum >= 0 && randomNum < 35) {
                    _node.mark = 1;
                    _node.branch = 1;
                }
                else if (randomNum >= 35 && randomNum < 60) {
                    _node.mark = 1;
                    _node.branch = 2;
                }
                else if (randomNum >= 60 && randomNum < 80) {
                    _node.mark = 2;
                    _node.branch = 0;
                }
                else {
                    _node.mark = 3;
                    _node.branch = 0;
                }

                if (_node.mark == 3) {
                    int random = UnityEngine.Random.Range(1, 1001);
                    if (random > 0 && random <= 125)
                        _node.branch = 1;
                    if (random > 125 && random <= 250)
                        _node.branch = 2;
                    if (random > 250 && random <= 300)
                        _node.branch = 3;
                    if (random > 300 && random <= 500)
                        _node.branch = 4;
                    if (random > 500 && random <= 700)
                        _node.branch = 5;
                    if (random > 700 && random <= 900)
                        _node.branch = 6;
                    if (random > 900 && random <= 1000)
                        _node.branch = 7;
                }

                _node.state = 1;
                _node.position = new float[2];
                _node.position[1] = UnityEngine.Random.Range(-0.2f, 0.2f) + map.transform.position.y +
                                    layerdistance * i; //y轴
                _node.position[0] = UnityEngine.Random.Range(-5f, -2f); //x轴
                _node.LinkedNodes = new List<int> { };

                for (int t = 0; t < Nodes[i - 1].Count; t++) {
                    if ((UnityEngine.Random.Range(2, 5) > 2 && _node.LinkedNodes.Count < 3)
                        || _node.LinkedNodes.Count < 1) {
                        DrawLine(noderow, i, noderow.Count, t, _node);
                    }
                    else if (noderow.Count > 0) {
                        if (!noderow[(noderow.Count - 1)].LinkedNodes.Contains(t))
                            DrawLine(noderow, i, noderow.Count, t, _node);

                    }
                }

                nodeRow = LoadGameObject(nodeRow, _node);

                noderow.Add(_node);
                realnum++;
            }
            else if (UnityEngine.Random.Range(0, 5) >= 3 && realnum <= 3) {
                node _node;
                int randomNum = UnityEngine.Random.Range(0, 100);
                if (randomNum >= 0 && randomNum < 35) {
                    _node.mark = 1;
                    _node.branch = 1;
                }
                else if (randomNum >= 35 && randomNum < 60) {
                    _node.mark = 1;
                    _node.branch = 2;
                }
                else if (randomNum >= 60 && randomNum < 80) {
                    _node.mark = 2;
                    _node.branch = 0;
                }
                else {
                    _node.mark = 3;
                    _node.branch = 0;
                }

                if (_node.mark == 3) {
                    int random = UnityEngine.Random.Range(1, 1001);
                    if (random > 0 && random <= 125)
                        _node.branch = 1;
                    if (random > 125 && random <= 250)
                        _node.branch = 2;
                    if (random > 250 && random <= 300)
                        _node.branch = 3;
                    if (random > 300 && random <= 500)
                        _node.branch = 4;
                    if (random > 500 && random <= 700)
                        _node.branch = 5;
                    if (random > 700 && random <= 900)
                        _node.branch = 6;
                    if (random > 900 && random <= 1000)
                        _node.branch = 7;
                }

                _node.state = 1;
                _node.position = new float[2];
                _node.position[1] = UnityEngine.Random.Range(-0.2f, 0.2f) + map.transform.position.y +
                                    layerdistance * i; //y轴
                if (realnum == 1)
                    _node.position[0] =
                        UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 1f);
                else if (realnum == 2)
                    _node.position[0] =
                        UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 4f);
                else if (realnum == 3)
                    _node.position[0] =
                        UnityEngine.Random.Range(noderow[realnum - 1].position[0] + scale[0], 7f);
                _node.LinkedNodes = new List<int> { };

                for (int t = 0; t < Nodes[i - 1].Count; t++)
                    if ((UnityEngine.Random.Range(2, 5) > 2 && _node.LinkedNodes.Count < 3)
                        || _node.LinkedNodes.Count < 1) {
                        DrawLine(noderow, i, noderow.Count, t, _node);
                    }
                    else if (noderow.Count > 0) {
                        if (!noderow[(noderow.Count - 1)].LinkedNodes.Contains(t))
                            DrawLine(noderow, i, noderow.Count, t, _node);

                    }

                nodeRow = LoadGameObject(nodeRow, _node);

                noderow.Add(_node);
                realnum++;
            }
        }

        if (realnum == 1) {
            for (int t = 0; t < Nodes[i - 1].Count; t++) {
                if (!noderow[0].LinkedNodes.Contains(t))
                    DrawLine(noderow, i, realnum - 1, t, noderow[0]);
            }
        }

        NodeObjects.Add(nodeRow);
        Nodes.Add(noderow);
    }
    void CreateNewMap() {
        IsNewExplo = false;
        CreateFirstLayer(0);
        for (int i = 1; i < layerNumber - 1; i++) {
            if (elite.Contains(i)) CreateEliteLayer(i);
            else CreateNormalLayer(i);
        }
        //CreateRelaxLayer(layerNumber - 2);
        CreateBossLayer(layerNumber - 1);
        
        SaveNodeToJason.Save(Nodes, Filepath);
    }
    void CreateMap() {
        if(IsNewExplo) 
            CreateNewMap();
        else LoadFile();
    }
    // 绘制连接节点之间的线条
    void DrawLine(List<node> currentLayerNodes, int currentLayerIndex, int currentNodeIndex, int targetNodeIndex, node currentNode) {
        // 判断是否需要绘制线条
        bool shouldDrawLine = ShouldDrawLine(currentLayerNodes, currentLayerIndex, currentNodeIndex, targetNodeIndex, currentNode);

        // 如果需要绘制，调用LoadLine进行实际的绘制
        if (shouldDrawLine) {
            LoadLine(currentLayerNodes, currentLayerIndex, currentNodeIndex, targetNodeIndex, currentNode);
            currentNode.LinkedNodes.Add(targetNodeIndex);
        }
    }

    // 检查是否需要绘制线条的逻辑
    bool ShouldDrawLine(List<node> currentLayerNodes, int currentLayerIndex, int currentNodeIndex, int targetNodeIndex, node currentNode) {
        bool isDirectlyLinked = currentNode.LinkedNodes.Contains(targetNodeIndex - 1) || targetNodeIndex == 0 || currentNode.LinkedNodes.Count <= 0;

        // 对于非精英层，且不是最后一层，检查是否有其他节点已经连接到目标节点
        if (!elite.Contains(currentLayerIndex) && currentLayerIndex != layerNumber - 1 && currentLayerIndex != 0) {
            foreach (var node in currentLayerNodes) {
                if (node.LinkedNodes.Contains(targetNodeIndex + 1)) {
                    return false; // 已有其他节点连接到目标，不需要再绘制
                }
            }
        }

        return isDirectlyLinked;
    }

    // 实际创建和定位连接线条的方法
    void LoadLine(List<node> noderow, int layerIndex, int nodeIndex, int targetIndex, node currentNode) {
        // 从Resources加载线条的Prefab
        var linePrefab = Resources.Load<GameObject>("Prefabs/line");
        GameObject lineInstance = Instantiate(linePrefab, backGround.transform.GetChild(0));

        // 设置线条的名称，表示其连接的节点
        lineInstance.name = $"line_{layerIndex}_{nodeIndex}_{targetIndex}";

        // 获取并设置LineRenderer的位置
        LineRenderer lineRenderer = lineInstance.GetComponent<LineRenderer>();
        Vector3 startPos = new Vector3(Nodes[layerIndex - 1][targetIndex].position[0], Nodes[layerIndex - 1][targetIndex].position[1], 0);
        Vector3 endPos = new Vector3(currentNode.position[0], currentNode.position[1], 0);
        lineRenderer.SetPositions(new Vector3[] { startPos, endPos });

        
    }


    public void save()
    {
        //if(!File.Exists(Application.dataPath+ "/map.txt"))
        if(!File.Exists(Filepath))
        {
            FileStream fileStream = new FileStream(Filepath, FileMode.OpenOrCreate);
            fileStream.Close();
        }
        string[] nodesFile = new string[Nodes.Count];
        for(int i=0;i<layerNumber;i++)
        {
            string noderow = "";
            for(int j=0;j<Nodes[i].Count;j++)
            {
                string linkednodes="";
                for (int x=0;x<Nodes[i][j].LinkedNodes.Count;x++)
                {
                    if (x == 0)
                        linkednodes = Nodes[i][j].LinkedNodes[x].ToString();
                    else
                        linkednodes = linkednodes + "\'" + Nodes[i][j].LinkedNodes[x];
                }
                if(j==0&&i!=0)
                    noderow= Nodes[i][j].mark + "\'" + Nodes[i][j].branch + "\'" + Nodes[i][j].position[0]
                    + "\'" + Nodes[i][j].position[1] + "\'" + Nodes[i][j].state + "\'" + linkednodes;
                else if(j!=0&&i!=0)
                    noderow = noderow+"\t" + Nodes[i][j].mark + "\'" + Nodes[i][j].branch + "\'" + Nodes[i][j].position[0] 
                    +"\'"+ Nodes[i][j].position[1] +"\'"+ Nodes[i][j].state +"\'"+ linkednodes;
                else if(j==0&&i==0)
                    noderow = Nodes[i][j].mark + "\'" + Nodes[i][j].branch + "\'" + Nodes[i][j].position[0]
                    + "\'" + Nodes[i][j].position[1] + "\'" + Nodes[i][j].state;
                else if (j != 0 && i == 0)
                    noderow = noderow + "\t" + Nodes[i][j].mark + "\'" + Nodes[i][j].branch + "\'" + Nodes[i][j].position[0]
                        + "\'" + Nodes[i][j].position[1] + "\'" + Nodes[i][j].state;
            }
            nodesFile[i] = noderow;

        }
        File.WriteAllLines(Filepath, nodesFile);
    }
    void LoadFile() {
        Nodes = SaveNodeToJason.LoadNodesFromFile(Filepath);
        

        for (int i = 0; i <Nodes.Count; i++) {

            List<node> noderow = Nodes[i];
            List<GameObject> nodeRow = new List<GameObject> { };
            for (int j = 0; j < noderow.Count; j++) {
                node _node = noderow[j];
                if (i != 0) {
                    foreach (var t in _node.LinkedNodes) {
                       LoadLine(noderow, i,j, t, _node);
                    }
                }
                nodeRow = LoadGameObject(nodeRow, _node);
            }
            NodeObjects.Add(nodeRow);
        }
        //save();
    }
    //重载探索界面时，探索完成后的物体需播放动画
    List<GameObject> LoadGameObject(List<GameObject>nodeRow,node _node)
    {
        GameObject NodeObject;
        if (_node.mark == 1)
        {
            if (_node.branch == 1)
                NodeObject = Resources.Load<GameObject>("Prefabs/Feng");
            else
                NodeObject = Resources.Load<GameObject>("Prefabs/Yun");
        }
        else if (_node.mark == 2)
            NodeObject = Resources.Load<GameObject>("Prefabs/Rest");
        else if (_node.mark == 3)
            NodeObject = Resources.Load<GameObject>("Prefabs/Events");
        else if (_node.mark == 4)
            NodeObject = Resources.Load<GameObject>("Prefabs/Elite");
        else
        {
            NodeObject = Resources.Load<GameObject>("Prefabs/Boss");
        }
        NodeObject = Instantiate(NodeObject, backGround.transform.GetChild(0));
        //if(_node.state==3)

        NodeObject.transform.position = new Vector3(_node.position[0], _node.position[1], 0);
        nodeRow.Add(NodeObject);
        return nodeRow;
    }
}
