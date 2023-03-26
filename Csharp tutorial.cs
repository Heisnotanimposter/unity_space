using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class TestEditWindow : EditorWindow {
    private void OnGUI() {
        if (GUILayout.Button ("All Materials load and apply"))  {
            // Scene에 있는 모든 렌더러를 찾아서 없앰 (초기화 작업)
            var allRenders = FindObjectsOfType<Renderer> ();
            if (allRenders != null )  {
                for ( int i = 0; i < allRenders.Length; i++)
                    GameObject.DestroyImmediate (allRenders [i].gameObject);
            }
            var resultGUI = AssetDatabase.FindAssets ("t:Material");
            if (resultGUI != null )  {
                for ( int i = 0; i< resultGUI.Length; i++)  {
                    var guid = resultGUI [i];
                    var path = AssetDatabase.GUIDToAssetPath (guid);
                    // 경로로 변환한 Asset을 Material class로 불러옴
                    var loadedMat = AssetDatabase.LoadAssetAtPath<Material> (path);
                    if (loadedMat != null )  {
                        Debug.Log ($"Material loaded : {path}");
                        // 큐브 모양 생성
                        var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
                        cube.transform.position = new Vector3 (i * 2, 0, 0 );
                        cube.GetComponent<Renderer> ().material = loadedMat;
                    }
                }
            }
        }
    }
}
