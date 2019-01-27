using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public SODialogBox current_dialog;
    public Text displayed_text;
    public Text displayed_speaker;
    public Image character;

    public AudioSource music;

    public Sprite box_small;
    public Sprite box_large;



    private int current_pos = 0;
    private SODialogBox next_dialog = null;
    private bool go_to_next = false;
    private bool has_success_story = false;


    void RefreshCanvas()
    {
        //Reset text
        displayed_speaker.text = current_dialog.speaker.ToString();
        displayed_text.text = "";
        current_pos = 0;

        next_dialog = null;

        if(current_dialog.next.Length > 1)
        {
            for (int i = 0; i < current_dialog.next.Length && !next_dialog; i++)
            {
                Choice c = current_dialog.next[i];
                if (c.successStory = has_success_story && string.IsNullOrEmpty(c.name))
                {
                    next_dialog = c.dialogBox;
                }
            }
        }
        else if (current_dialog.next.Length == 1 && string.IsNullOrEmpty(current_dialog.next[0].name))
        {
            next_dialog = current_dialog.next[0].dialogBox;
        }

        if (string.IsNullOrEmpty(current_dialog.content))
            go_to_next = true;

        //GetComponent<Image>().sprite = current_dialog.background;


        //Change sprite and music
        character.sprite = ResourcesManager.instance.getSpriteForEmotion(current_dialog.emotion);
        //music.clip = ResourcesManager.instance.GetAudioClipForEmotion(current_dialog.emotion);

        /*
        bool hasSuccessStory = false;
        
        }*/

        //Choice management
        Button[] choice_buttons = GetComponentsInChildren<Button>(true);
        for(int i = 0; i  < choice_buttons.Length; i++)
        {
            choice_buttons[i].gameObject.SetActive(false);
        }
        if(next_dialog == null && (current_dialog.next.Length > 1 || (current_dialog.next.Length == 1 && !string.IsNullOrEmpty(current_dialog.next[0].name))))
        {
            for(int i = 0 ; i < current_dialog.next.Length ; i++)
            {

                Choice choice = current_dialog.next[i];

                if (choice.successStory == has_success_story)
                {
                    Button b = choice_buttons[i];
                    b.name = choice.name;
                    b.GetComponentInChildren<Text>().text = choice.name;

                    if (b.GetComponentInChildren<Text>().cachedTextGenerator.lineCount > 1)
                        b.image.sprite = box_large;
                    else
                        b.image.sprite = box_small;

                    b.gameObject.SetActive(true);
                    b.onClick.AddListener(delegate { next_dialog = choice.dialogBox; go_to_next = true; });// changeDialog(choice.dialogBox);});
                }
            }
        }

    }

    void changeDialog()
    {
        if(next_dialog != null)
        {
            current_dialog = next_dialog;
            go_to_next = false;
            RefreshCanvas();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshCanvas();
    }

    IEnumerator WriteText()
    {
        while (displayed_text.text != current_dialog.content)
        { 
            displayed_text.text += current_dialog.content[current_pos++];
            yield return new WaitForSeconds((int)current_dialog.speed/(float)10.0f);
         }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
            go_to_next = true;


        if (displayed_text.text != current_dialog.content)
        {
            StartCoroutine(WriteText());
        }
        else if(go_to_next)
        {
            if (next_dialog != null)
                changeDialog();
        }
    }
}