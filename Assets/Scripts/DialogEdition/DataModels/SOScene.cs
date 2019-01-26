using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class NodeData
{
    /* Node position */
    float x;
    float y;

    string nodeID;
    List<NodeData> childs = new List<NodeData>();
}

[CreateAssetMenu(menuName = "GameData/Scene")]
public class SOScene : ScriptableObject
{
    public string SceneTitle;

    //public List<Node> nodes = new List<Node>();
    //public List<Connection> connections = new List<Connection>();
    //public List<SODialogBox> dialogbox = new List<SODialogBox>();
}
