#if UNITY_EDITOR
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using DouShuQiTan;
using UnityEditor;
using UnityEngine;
using System.Collections;
using NUnit.Framework.Internal;




public class XlslLoader: EditorWindow {

    //====================================================================================
    /// <summary>
    /// Create Pair Action From SO and DataRow
    /// </summary>
    /// <param name="SO"></param>
    /// <param name="row"></param>
    public static void MyPairAction(ScriptableObject SO, DataRow row) {
        var effect = SO as 枢元UpgradeTemplate;

        effect.Id = int.Parse(row[0].ToString());
        effect.Name = row[1].ToString();
        effect.Type = row[2].ToString();
        effect.Description = row[3].ToString();
        effect.Comment = row[6].ToString();


    }
    //=====================================================================================












    private static string ProjectPath = "Assets/NewProjectContent";
    private static string Namespace = "DouShuQiTan";
    private static string UnitName = "Unit";
    private static int TableIndex;
    private static int StartRow = 1;
    private static int EndRow = -1;



    private static string TemplateName;
    private static string RuntimeName;
    private static string LibraryName;

    static Type templateType;
    static Type libraryType;
    static Type runtimeType;


    [MenuItem("Tools/Xlsl Loader")]
    public static void ShowWindow() {
        GetWindow<XlslLoader>("Unity Xlsl Resource Manager");
    }

    void OnGUI() {
        ProjectPath = EditorGUILayout.TextField("Project Path", ProjectPath);
        Namespace = EditorGUILayout.TextField("Namespace", Namespace);
        UnitName = EditorGUILayout.TextField("Unit Name", UnitName);
        TableIndex = EditorGUILayout.IntField("Table Index", TableIndex);
        StartRow = EditorGUILayout.IntField("Start Row", StartRow);
        EndRow = EditorGUILayout.IntField("End Row", EndRow);

        TemplateName = UnitName + "Template";
        RuntimeName = "Runtime" + UnitName;
        LibraryName = UnitName + "Library";
        if (GUILayout.Button("Create Template Library Runtime Type")) {
            CreateType();
        }
        if (GUILayout.Button("Load From Excel")) {
            LoadFromExcel();
        }
    }

    private static void LoadFromExcel() {
        string path = EditorUtility.OpenFilePanel($"Select xlsl File", "", "xlsx");
        if (string.IsNullOrEmpty(path)) return;

        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
        DataSet result = excelReader.AsDataSet();
        excelReader.Close();
        stream.Close();
        templateType = Type.GetType($"{Namespace}.{TemplateName},Assembly-CSharp");
        libraryType = Type.GetType($"{Namespace}.{LibraryName},Assembly-CSharp");
        runtimeType = Type.GetType($"{Namespace}.{RuntimeName},Assembly-CSharp");


        if (templateType == null) {
            Debug.LogError($"Not Find Type {Namespace}.{TemplateName} click Create Type First");
            return;
        }
        else if (libraryType == null) {
            Debug.LogError($"Not Find Type {Namespace}.{LibraryName} click Create Type First");
            return;
        }
        else if (runtimeType == null) {
            Debug.LogError($"Not Find Type {Namespace}.{RuntimeName} click Create Type First");
            return;
        }

        var list = LoadTemplate(result);
        var library = LoadLibrary(result,list);
        LoadRuntime(result,library);
    }

    private static void CreateType() {
        CreateTemplateType();
        CreateRuntimeType();
        CreateLibraryType();
    }

    private static void CreateTemplateType() {
        string content = $@"
using System;
using UnityEngine;
namespace {Namespace} {{
    [Serializable]
    [CreateAssetMenu(
    menuName = ""Game/Templates"",
    fileName = ""{UnitName}"")]
    public class {TemplateName} : ScriptableObject {{
        public string Name;
        public GameObject {RuntimeName};
        // Add More Fields Here
    }}
}}";

        WriteScriptContent($"{ProjectPath}/Scripts/StaticTemplate",$"{TemplateName}",content);

    }

    private static void CreateLibraryType() {
        string content = $@"
using System;
using System.Collections.Generic;
using UnityEngine;
namespace {Namespace} {{
    [Serializable]
    [CreateAssetMenu(
    menuName = ""Game/Libraries"",
    fileName = ""{UnitName}"")]
    public class {LibraryName} : ScriptableObject {{
        public List<{TemplateName}> {UnitName}s;
        // Add More Fields Here
    }}
}}";

        WriteScriptContent($"{ProjectPath}/Scripts/Libraries", $"{LibraryName}", content);

    }

    private static void CreateRuntimeType() {
        string content = $@"
using System;
using UnityEngine;
namespace {Namespace} {{
    public class {RuntimeName} : MonoBehaviour {{
        // Add More Fields Here
    }}
}}";

        WriteScriptContent($"{ProjectPath}/Scripts/Runtime", $"{RuntimeName}", content);

    }

    private static void WriteScriptContent(string directory,string name,string content) {
        string scriptPath = $"{directory}/{name}.cs";

        // 确保目录存在
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(scriptPath)) {
            using (StreamWriter sw = new StreamWriter(scriptPath)) {
                sw.Write(content);
            }
            AssetDatabase.ImportAsset(scriptPath);
        }
    }




    private static object LoadTemplate(DataSet result) {
        Type listType = typeof(List<>).MakeGenericType(new Type[] { templateType });
        object templateList = Activator.CreateInstance(listType);

        DataTable table = result.Tables[TableIndex]; // 假设数据在第一个表中
        if(EndRow == -1) EndRow = table.Rows.Count;
        for (int i = StartRow - 1 ;i<EndRow;i++) {
            ScriptableObject SO = CreateInstanceWithReflection(templateType);

            MyPairAction(SO, table.Rows[i]);
            
            SaveTemplateSO(SO);
            templateList.GetType().GetMethod("Add")?.Invoke(templateList, new object[] { SO });
        }
        AssetDatabase.SaveAssets();
        Debug.Log($"{TemplateName} loaded successfully.");
        return templateList;
    }

    private static void LoadRuntime(DataSet result, ScriptableObject library) {
        FieldInfo fieldInfo = templateType.GetField($"{RuntimeName}", BindingFlags.Public | BindingFlags.Instance);
        FieldInfo NameField = templateType.GetField("Name", BindingFlags.Public | BindingFlags.Instance);


        if (fieldInfo == null) {
            Debug.LogError($"Template Type {TemplateName} Not Find RuntimeType {RuntimeName} Field");
            return;
        }

        FieldInfo unitNamesField = library.GetType().GetField($"{UnitName}s", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

        if (unitNamesField == null) {
            Debug.LogError($"Cannot find '{UnitName}s' field.");
            return;
        }
        
        IList unitNamesList = unitNamesField.GetValue(library) as IList;

        if (unitNamesList == null) {
            Debug.LogError($"'{UnitName}s' is not a list.");
            return;
        }

        
        foreach (var item in unitNamesList) {
            string path = CreateNewPrefab(
                $"{NameField?.GetValue(item) as string}"
            );
            GameObject loadedAsset = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (loadedAsset != null)
                fieldInfo.SetValue(item, loadedAsset);
            else {
                Debug.LogError($"Failed to load asset at path: {path}");
                return;
            }

            CreateAndAttachScript(NameField?.GetValue(item) as string, loadedAsset);

        }
        AssetDatabase.SaveAssets();
    }

    private static ScriptableObject LoadLibrary(DataSet result, object list) {

        ScriptableObject library = CreateInstanceWithReflection(libraryType);
        library.GetType().GetField($"{UnitName}s")?.SetValue(library, list);

        // 保存ScriptableObject到Assets目录
        string scriptDirectory = $"{ProjectPath}/Libraries";
        if (!Directory.Exists(scriptDirectory)) {
            Directory.CreateDirectory(scriptDirectory);
        }
        AssetDatabase.CreateAsset(library, $"{scriptDirectory}/{LibraryName}.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = library;
        return library;
    }

    private static void SaveTemplateSO(ScriptableObject SO) {
        FieldInfo fieldInfo = templateType.GetField("Name", BindingFlags.Public | BindingFlags.Instance);
        if (fieldInfo == null) {
            Debug.LogError($"Template Type {TemplateName} Not Find Name Field");
            return;
        }
        string scriptDirectory = $"{ProjectPath}/ScriptableObjects/{TemplateName}";
        if (!Directory.Exists(scriptDirectory)) {
            Directory.CreateDirectory(scriptDirectory);
        }
        AssetDatabase.CreateAsset(SO, $"{scriptDirectory}/{fieldInfo.GetValue(SO)}.asset");
    }
    
    
    private static void CreateAndAttachScript(string Name, GameObject prefab) {
        var scriptName = Name;
        scriptName = scriptName.Replace("（", "_");
        scriptName = scriptName.Replace("）", "");
        string scriptDirectory = $"{ProjectPath}/Scripts/Runtime/{UnitName}";
        string scriptPath = $"{scriptDirectory}/{scriptName}_{RuntimeName}.cs";

        // 确保目录存在
        if (!Directory.Exists(scriptDirectory)) {
            Directory.CreateDirectory(scriptDirectory);
        }
        Debug.Log($"Create {scriptName} ");
        if (!File.Exists(scriptPath)) {
            using (StreamWriter sw = new StreamWriter(scriptPath)) {
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("");
                sw.WriteLine("namespace DouShuQiTan{");
                sw.WriteLine($"     public class {scriptName}_{RuntimeName} : {RuntimeName} {{");
                sw.WriteLine("          // Add your script content here");
                sw.WriteLine("      }");
                sw.WriteLine("}");
            }
            AssetDatabase.ImportAsset(scriptPath);
        }

        var scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
        if (scriptAsset != null && prefab != null) {

            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            prefabInstance.AddComponent(scriptAsset.GetClass());

            PrefabUtility.SaveAsPrefabAsset(prefabInstance, $"{ProjectPath}/Prefabs/{UnitName}/{Name}.prefab");
            AssetDatabase.SaveAssets();
            GameObject.DestroyImmediate(prefabInstance);
        }
    }

    private static ScriptableObject CreateInstanceWithReflection(Type Type) {

        MethodInfo genericMethod = typeof(ScriptableObject).GetMethod("CreateInstance", Type.EmptyTypes);
        
        MethodInfo constructedMethod = genericMethod.MakeGenericMethod(Type);
        
        ScriptableObject instance = (ScriptableObject)constructedMethod.Invoke(null, null);
        
        return instance;
    }


    private static string CreateNewPrefab(string name) {
        GameObject newGameObject = new GameObject(name);

        string directory = $"{ProjectPath}/Prefabs/{UnitName}";
        string fullPath = $"{directory}/{name}.prefab";
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }

        PrefabUtility.SaveAsPrefabAsset(newGameObject, fullPath);
        AssetDatabase.SaveAssets();
        DestroyImmediate(newGameObject);

        Debug.Log($"Prefab created at {fullPath}");
        
        return fullPath;
    }
}
#endif