using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JieSuan : MonoBehaviour
{
    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;
    public List<List<node>> Nodes;
    string Filepath;
    //string Filepath = "D:"+"/map.txt";
    // Start is called before the first frame update
    private void Awake()
    {
        Filepath = Application.dataPath + "/map.json";
        transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();

        if (!GameData.IsWin) {
            PlayerPrefs.SetInt("StopExplo", 0);
            sceneName = "start";
        }
        else {
            sceneName = "map";
            Load();
            if (GameData.Enemy[7] == 1)
                ExploreSystem.IsNewExplo = true;
        }
    }
    void Start() {
        transitionManager.LoadScene(sceneName, "Fade", 0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Load() {
        Nodes = SaveNodeToJason.LoadNodesFromFile(Filepath);
        for (int i = 0; i < Nodes.Count; i++) {
            for (int j = 0; j < Nodes[i].Count; j++) {
                var node = Nodes[i][j];
                node.state = 1;
                Nodes[i][j] = node; 
            }
        }
        if (GameData.IsWin) {
            GameData.IsWin = false;
            var node = Nodes[GameData.abscissa][GameData.ordinate];
            node.state = 3;
            Nodes[GameData.abscissa][GameData.ordinate] = node;
            ExploreSystem.nowLayer++;
            PlayerPrefs.SetInt("nowLayer", ExploreSystem.nowLayer);
        }
        SaveNodeToJason.Save(Nodes, Filepath);
        //for (int i = 0; i < Nodes.Count; i++) {
        //    List<node> noderow = Nodes[i];
        //    for (int j = 0; j < noderow.Count; j++) {
        //        node _node = noderow[j];
        //        if (GameData.IsWin && i == GameData.abscissa && j == GameData.ordinate) {
        //            GameData.IsWin = false;
        //            _node.state = 3;
        //            ExploreSystem.nowLayer++;
        //            PlayerPrefs.SetInt("nowLayer", ExploreSystem.nowLayer);
        //        }
        //        else if (i ==GameData.abscissa&&j!=GameData.ordinate)
        //            _node.state = 1;
        //    }

        //}
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
