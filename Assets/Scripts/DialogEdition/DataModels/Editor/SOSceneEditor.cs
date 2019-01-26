using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SOScene))]
public class                SOSceneEditor : Editor
{
    public override void    OnInspectorGUI()
    {
        SOScene scene = target as SOScene;

        DrawDefaultInspector();
        //if (GUILayout.Button("Load"))
        //{
        //    foreach (Node node in scene.nodes)
        //    {
        //        Debug.Log(node.rect.x + " | " + node.rect.y);
        //    }
        //
        //    NodeBasedEditor.instance.nodes = scene.nodes;
        //    NodeBasedEditor.instance.connections = scene.connections;
        //    NodeBasedEditor.instance.Repaint();
        //}
        //if (GUILayout.Button("Save"))
        //{
        //    scene.nodes = NodeBasedEditor.instance.nodes;
        //    scene.connections = NodeBasedEditor.instance.connections;
        //    EditorUtility.SetDirty(scene);
        //    AssetDatabase.SaveAssets();
        //
        //    foreach (Node node in scene.nodes)
        //    {
        //        Debug.Log(node.rect.x + " | " + node.rect.y);
        //    }
        //}
        //
        //if (GUILayout.Button("Show"))
        //{
        //    foreach (Node node in scene.nodes)
        //    {
        //        Debug.Log(node.rect.x + " | " + node.rect.y);
        //    }
        //}
    }
}
