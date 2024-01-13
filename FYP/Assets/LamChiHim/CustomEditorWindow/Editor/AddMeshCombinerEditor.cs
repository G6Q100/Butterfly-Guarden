using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddMeshCombinerEditor : EditorWindow
{    
    [MenuItem("Tools/Mesh Combiner")]
    static void AddEditor(){
        AddMeshCombinerEditor window = ScriptableObject.CreateInstance("AddMeshCombinerEditor") as 
                                        AddMeshCombinerEditor;
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 256, 144);
        window.ShowUtility();
    }
    static bool ValidateSelection(){
        return Selection.GetTransforms(
                SelectionMode.Deep
              | SelectionMode.ExcludePrefab
              | SelectionMode.Editable).Length > 0;
    }    
    
    private static GameObject addedGameObject = null;
    void OnGUI(){
        addedGameObject = (GameObject)EditorGUILayout.ObjectField("Game Obejct:", addedGameObject, typeof(GameObject)
                                , true);
        if(GUILayout.Button("Combined Mesh!")){
            Transform[] transforms = Selection.GetTransforms(
                SelectionMode.Deep
              | SelectionMode.ExcludePrefab
              | SelectionMode.Editable);

            foreach(Transform transform in transforms)
            {
                if(transform.GetComponent<MeshFilter>() == null && transform.GetComponent<MeshRenderer>() == null){
                    transform.gameObject.AddComponent<MeshFilter>();
                    transform.gameObject.AddComponent<MeshRenderer>();
                    if(addedGameObject != null){
                        CombineMeshes();
                    }
                }
            }
            this.Close();
        }
    }

    public void CombineMeshes(){

        Quaternion oldRot = addedGameObject.transform.rotation;

        Vector3 oldPos = addedGameObject.transform.position;

        addedGameObject.transform.rotation = Quaternion.identity;

        addedGameObject.transform.position = Vector3.zero;

        MeshFilter[] filters = addedGameObject.GetComponentsInChildren<MeshFilter>();
        
        Mesh finalMesh = new Mesh();
        
        CombineInstance[] combiners = new CombineInstance[filters.Length];

        for (int a = 0; a < filters.Length; a++){
            if(filters[a].transform == addedGameObject.transform)
                continue;
            combiners[a].subMeshIndex = 0;
            combiners[a].mesh = filters[a].sharedMesh;
            combiners[a].transform = filters[a].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combiners);

        finalMesh.name = addedGameObject.name;
        AssetDatabase.CreateAsset(finalMesh, "Assets/" + finalMesh.name + ".asset");
        AssetDatabase.SaveAssets();
        addedGameObject.GetComponent<MeshFilter>().sharedMesh = finalMesh;

        addedGameObject.transform.rotation = oldRot;

        addedGameObject.transform.position = oldPos;

        for (int a = 0; a < addedGameObject.transform.childCount; a++)
            addedGameObject.transform.GetChild(a).gameObject.SetActive(false);
    }
}
