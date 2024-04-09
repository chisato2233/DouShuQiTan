using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JieSuan : MonoBehaviour
{
    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;
    public List<List<node>> Nodes=new List<List<node>> { };
    string Filepath;
    //string Filepath = "D:"+"/map.txt";
    // Start is called before the first frame update
    private void Awake()
    {
        Filepath = Application.dataPath + "/map.txt";
        transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();

        if (!GameData.IsWin)
        {
            PlayerPrefs.SetInt("StopExplo", 0);
            sceneName = "start";
        }
        else
        {
            sceneName = "map";
            Load();
            save();

            if (GameData.Enemy[7] == 1)
                ExploreSystem.IsNewExplo = true;
        }
    }
    void Start()
    {
        transitionManager.LoadScene(sceneName, "Fade", 0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Load()
    {
        //更新数据
        string[] nodesFile = File.ReadAllLines(Filepath);
        for (int i = 0; i < nodesFile.Length; i++)
        {
            //List<string> noderow = new List<string> { };
            //if (i == (int)nodesFile[i][0])
            //{
            //    noderow.Add(nodesFile[i]);

            //}
            string[] noderowstring = nodesFile[i].Split('\t');
            List<node> noderow = new List<node> { };
            List<GameObject> nodeRow = new List<GameObject> { };
            for (int j = 0; j < noderowstring.Length; j++)
            {

                string[] singlenode = noderowstring[j].Split('\'');
                node _node;
                //_node.mark = Int32.Parse(singlenode[0]);
                Int32.TryParse(singlenode[0], out _node.mark);
                Int32.TryParse(singlenode[1], out _node.branch);
                _node.position = new float[2];
                float.TryParse(singlenode[2], out _node.position[0]);
                float.TryParse(singlenode[3], out _node.position[1]);

                if (GameData.IsWin && i == GameData.abscissa && j == GameData.ordinate)
                {
                    GameData.IsWin = false;
                    _node.state = 3;
                    ExploreSystem.nowLayer++;
                    PlayerPrefs.SetInt("nowLayer", ExploreSystem.nowLayer);
                }
                else if (i ==GameData.abscissa&&j!=GameData.ordinate)
                {
                    _node.state = 1;
                }
                else
                    Int32.TryParse(singlenode[4], out _node.state);

                _node.LinkedNodes = new List<int> { };
                if (i != 0)
                {
                    for (int x = 5; x < singlenode.Length; x++)
                    {
                        int linkednodes = 0;
                        Int32.TryParse(singlenode[x], out linkednodes);
                        _node.LinkedNodes.Add(linkednodes);
                    }
                }
                noderow.Add(_node);


            }
            Nodes.Add(noderow);

        }
    }
    void save()
    { 
        //Nodes加载完毕
        
        string[] nodesFile = new string[Nodes.Count];
        for (int i = 0; i < Nodes.Count; i++)
        {
            string noderow = "";
            for (int j = 0; j < Nodes[i].Count; j++)
            {
                string linkednodes = "";
                for (int x = 0; x < Nodes[i][j].LinkedNodes.Count; x++)
                {
                    if (x == 0)
                        linkednodes = Nodes[i][j].LinkedNodes[x].ToString();
                    else
                        linkednodes = linkednodes + "\'" + Nodes[i][j].LinkedNodes[x];
                }
                if (j == 0 && i != 0)
                    noderow = Nodes[i][j].mark + "\'" + Nodes[i][j].branch + "\'" + Nodes[i][j].position[0]
                    + "\'" + Nodes[i][j].position[1] + "\'" + Nodes[i][j].state + "\'" + linkednodes;
                else if (j != 0 && i != 0)
                    noderow = noderow + "\t" + Nodes[i][j].mark + "\'" + Nodes[i][j].branch + "\'" + Nodes[i][j].position[0]
                        + "\'" + Nodes[i][j].position[1] + "\'" + Nodes[i][j].state + "\'" + linkednodes;
                else if (j == 0 && i == 0)
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
}
