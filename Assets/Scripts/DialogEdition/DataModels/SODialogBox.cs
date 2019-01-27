using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[Serializable]
public class                        Choice
{
    public string                   name;
    public bool                     successStory;
    public SODialogBox              dialogBox;
    public int                      reward;
}

public enum                         Emotion
{
    Neutral,
    Joy,
    Anger,
    Fear,
    Surprise,
    Sadness,
    MAX_EMOTIONS
}

public enum                         Languages
{
    English,
    French,
    German,
    Japanese,
    MAX_LANGUAGES
}

public enum                         Characters
{
    Protagonist,
    Player
}

public enum                         TypingSpeed
{
    Very_Slow = 10,
    Slow = 5,
    Medium = 2,
    Fast = 0
}

/// <summary>
/// A text that will be displayed as is. 
/// The text is rich as defined by Unity standards, it is displayed if the prerequisites are met. 
/// Once displayed the player can skip to the next dialog if any linked. 
/// </summary>
[CreateAssetMenu(menuName = "GameData/DialogBox")]
public class                        SODialogBox : ScriptableObject
{
    public string                   id;
    /// <summary>Name of the speaker</summary>
    public Characters               speaker;
    /// <summary>Content in rich text</summary>
    [TextArea]
    public string                   content;
    /// <summary>The emotion the character shows in this dialogbox</summary>
    public Emotion                  emotion;
    /// <summary>Speed of the text</summary>
    public TypingSpeed              speed;
    /// <summary>Translations</summary>
    public string[]                 content_lang = new string[(int) Languages.MAX_LANGUAGES];
    /// <summary>Tree of choices left to the player in the end</summary>
    public Choice[]                 next;
    [HideInInspector]
    public float                    x, y; /* visual editing */

    public static string            GetUniqueID()
    {
        var random = new System.Random();
        DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
        double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

        return String.Format("{0:X}", Convert.ToInt32(timestamp))
            + "-" + String.Format("{0:X}", random.Next(1000000000));
    }

    public void                     Awake()
    {
        if (String.IsNullOrEmpty(id)) id = GetUniqueID();
    }

    public void                     OnValidate()
    {
        content_lang[0] = content;
    }
}