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
    public string[]                 content_lang = new string[(int) Languages.MAX_LANGUAGES];
    /// <summary>Tree of choices left to the player in the end</summary>
    public Choice[]                 next;

    public void                     OnValidate()
    {
        content_lang[0] = content;
    }
}