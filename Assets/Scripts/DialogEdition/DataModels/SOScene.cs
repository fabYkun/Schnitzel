using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Scene")]
public class SOScene : ScriptableObject
{
    public string SceneTitle;
    [HideInInspector]
    public SODialogBox root;
}
