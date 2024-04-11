using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DouShuQiTan;

public class CardLoaderWindow : EditorWindow {
    private string cardPath = "Assets/NewProjectContent/ScriptableObjects/CardSO/";
    private int tableIndex = 0;
    private int startRow = 4; // 默认起始行为4
    private List<string> tableOptions = new List<string>(); 
    private static CardLibrary selectedLibrary;

    [MenuItem("Tools/Card Loader Settings")]
    public static void ShowWindow() {
        GetWindow<CardLoaderWindow>("Card Loader");
    }

    private void OnGUI() {
        GUILayout.Label("Card Loader Settings", EditorStyles.boldLabel);

        
        cardPath = EditorGUILayout.TextField("Card Path:", cardPath);
        

        if (tableOptions.Count > 0) {
            tableIndex = EditorGUILayout.Popup("Select Table:", tableIndex, tableOptions.ToArray());
        }
        else {
            EditorGUILayout.HelpBox("No tables found.", MessageType.Info);
        }

        // 表格真正的起始行
        startRow = EditorGUILayout.IntField("Start Row:", startRow);

        if (GUILayout.Button("Load Cards")) {
            
            Debug.Log("Loading cards with current settings...");
            CardXlslLoader.LoadCardsFromXLSX(cardPath, tableIndex, startRow);
        }



        selectedLibrary = EditorGUILayout.ObjectField("Card Library:", selectedLibrary, typeof(CardLibrary), false) as CardLibrary;

        if (GUILayout.Button("Load Card Scripts")) {
            CardXlslLoader.CreateCardScript(selectedLibrary);
        }

        if (GUILayout.Button("My Action")) {
            CardXlslLoader.MyActionToCardLibrary(selectedLibrary);
        }
    }
}