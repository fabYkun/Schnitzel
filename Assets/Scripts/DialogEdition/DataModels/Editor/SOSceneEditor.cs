using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SOScene))]
public class                SOSceneEditor : Editor
{
    Dictionary<string, Node> loadedNodes = new Dictionary<string, Node>();


    private Node            LoadNodes(SODialogBox data)
    {
        if (loadedNodes.ContainsKey(data.id)) return loadedNodes[data.id];
        Node newNode = NodeBasedEditor.instance.ImportNode(data);
        loadedNodes.Add(data.id, newNode);
        if (data.next == null) return newNode;

        for (int i = 0; i < data.next.Length; ++i)
        {
            if (data.next[i].dialogBox)
            {
                Node node = LoadNodes(data.next[i].dialogBox);
                NodeBasedEditor.instance.AddConnection(newNode, node);
            }
        }
        return newNode;
    }

    public override void    OnInspectorGUI()
    {
        SOScene scene = target as SOScene;

        DrawDefaultInspector();
        if (GUILayout.Button("Load"))
        {
            loadedNodes.Clear();
            NodeBasedEditor.instance.scene = scene;
            NodeBasedEditor.instance.ClearInstance();
            LoadNodes(scene.root);
            NodeBasedEditor.instance.Repaint();
        }
        if (GUILayout.Button("Save"))
        {
            scene.root = NodeBasedEditor.instance.entryNode.dialogBox;
            EditorUtility.SetDirty(scene);
            AssetDatabase.SaveAssets();
        }
    }
}
