using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    Very_Slow = 20,
    Slow = 10,
    Medium = 5,
    Fast = 0
}

/// <summary>
/// A text that will be displayed as is. 
/// The text is rich as defined by Unity standards, it is displayed if the prerequisites are met. 
/// Once displayed the player can skip to the next dialog if any linked. 
/// </summary>
[CreateAssetMenu(menuName = "Objects/SODialogBox")]
public class                        SODialogBox : ScriptableObject
{
    /// <summary>Name of the speaker</summary>
    public Characters               speaker;
    /// <summary>Content in rich text</summary>
    [TextArea]
    public string                   content;
    /// <summary>The emotion the character shows in this dialogbox</summary>
    public Emotion                  emotion;

    public TypingSpeed              speed;
    public string[]                 content_lang = new string[(int) Languages.MAX_LANGUAGES];
    /// <summary>Tree of choices left to the player in the end</summary>
    public Choice[]                 next;

    public void                     OnValidate()
    {
        content_lang[0] = content;
    }
}