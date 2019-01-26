using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public SODialogBox current_dialog;
    public Text displayed_text;
    public Text displayed_speaker;

    private int current_pos = 0;


    void RefreshCanvas()
    {
        displayed_speaker.text = current_dialog.speaker.ToString();
        displayed_text.text = "";
        current_pos = 0;

        //GetComponent<Image>().sprite = current_dialog.background;

        Button[] choice_buttons = GetComponentsInChildren<Button>(true);
        for(int i = 0; i  < choice_buttons.Length; i++)
        {
            choice_buttons[i].gameObject.SetActive(false);
        }
        if(current_dialog.next.Length > 1)
        {
            for(int i = 0 ; i < current_dialog.next.Length ; i++)
            {
                Button b = choice_buttons[i];
                choice_buttons[i].gameObject.SetActive(true);
                Choice choice = current_dialog.next[i];
                b.name = choice.name;
                b.GetComponentInChildren<Text>().text = choice.name;
                b.onClick.AddListener(delegate{changeDialog(choice.dialogBox);});
                
            }
        }

    }

    void changeDialog(SODialogBox next)
    {
        current_dialog = next;
        RefreshCanvas();
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
        if(displayed_text.text != current_dialog.content)
        {
            StartCoroutine(WriteText());
        }
        else if(Input.GetKeyDown(KeyCode.Return) && current_dialog.next.Length == 1)
        {
           changeDialog(current_dialog.next[0].dialogBox);
        }
    }
}