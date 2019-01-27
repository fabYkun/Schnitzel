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

    private void            SaveNodes(SODialogBox data)
    {
        if (loadedNodes.ContainsKey(data.id)) return;
        EditorUtility.SetDirty(data);
        loadedNodes.Add(data.id, null);
        if (data.next == null) return;

        for (int i = 0; i < data.next.Length; ++i)
        {
            if (data.next[i].dialogBox)
            {
                SaveNodes(data.next[i].dialogBox);
            }
        }
    }

    public override void    OnInspectorGUI()
    {
        SOScene scene = target as SOScene;

        DrawDefaultInspector();
        if (GUILayout.Button("Load"))
        {
            AssetDatabase.Refresh();
            loadedNodes.Clear();
            NodeBasedEditor.instance.scene = scene;
            NodeBasedEditor.instance.ClearInstance();
            LoadNodes(scene.root);
            NodeBasedEditor.instance.Repaint();
        }
        if (GUILayout.Button("Save"))
        {
            loadedNodes.Clear();
            scene.root = NodeBasedEditor.instance.entryNode.dialogBox;
            SaveNodes(scene.root);
            EditorUtility.SetDirty(scene);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
    }
}
