using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "GameData/Step"), Serializable]
public class StepData : ScriptableObject
{
    public Thematics thematic;
    public AudioClip audioclip;
    public Sprite foreground;
    public Sprite background;

    public enum stepType
    {
        Cooking,
        Narration
    };
    public stepType type;
    [Header("Cooking settings")]
    public int sweet;
    public int spicy;
    public int salty;
    public int delta;
    public GameObject ingredientsPrefab;
    [Header("Narration settings")]
    public SOScene scene;
}
