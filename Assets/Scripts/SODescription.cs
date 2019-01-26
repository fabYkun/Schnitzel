using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for translation purposes, since its a ScriptableObject it can be serialized as JSON and referenced in any object
/// </summary>
[CreateAssetMenu(menuName = "Objects/SODescription")]
public class                        SODescription : ScriptableObject
{
    /// <summary>Name/Title of the object/action/...thing</summary>
    public string                   title;
    /// <summary>Description of the object/action...thing</summary>
    public string                   description;
}