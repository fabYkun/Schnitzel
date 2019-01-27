using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingManager : MonoBehaviour
{

    private int current_spicy = 0;
    private int current_salty = 0;
    private int current_sweet = 0;

    public Text salty_text;
    public Text spicy_text;
    public Text sweet_text;

    public GameObject taste_effect;

    public float transitionTime = 1.0f;

    public 


    // Start is called before the first frame update
    void Start()
    {
        taste_effect.GetComponent<CanvasGroup>().alpha = 0;
        RectTransform rt = (RectTransform)taste_effect.transform.Find("Synesthesia");
        rt.sizeDelta = new Vector2(0, 0);
        taste_effect.SetActive(false);

        taste_effect.GetComponent<Button>().onClick.AddListener(delegate { if(taste_effect.GetComponent<CanvasGroup>().alpha > 0.95f) StartCoroutine("FadeOutTaste"); });


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddIngredient(int spicy, int sweet, int salty)
    {
        current_spicy += spicy;
        current_salty += salty;
        current_sweet += sweet;


        salty_text.text = current_salty.ToString();
        sweet_text.text = current_sweet.ToString();
        spicy_text.text = current_spicy.ToString();

    }

    IEnumerator FadeInTaste()
    {
        for (float f = 0; f < 1.0f; f += 0.01f)
        {
            RectTransform rt = (RectTransform)taste_effect.transform.Find("Synesthesia");
            rt.sizeDelta = new Vector2(f*1000, f*1000);
            taste_effect.GetComponent<CanvasGroup>().alpha = f;

            yield return new WaitForSeconds(transitionTime/100.0f);
        }
        yield return new WaitForSeconds(transitionTime / 100.0f);
    }

    IEnumerator FadeOutTaste()
    {
        for (float f = 1.0f; f > 0.0f; f -= 0.01f)
        {
            RectTransform rt = (RectTransform)taste_effect.transform.Find("Synesthesia");
            rt.sizeDelta = new Vector2(f * 1000, f * 1000);
            taste_effect.GetComponent<CanvasGroup>().alpha = f;

            yield return new WaitForSeconds(transitionTime / 100.0f);
        }

        taste_effect.SetActive(false);
        yield return new WaitForSeconds(transitionTime / 100.0f);
    }

    public void DisplayTaste(int spicy, int sweet, int salty)
    {
        taste_effect.SetActive(true);
        StartCoroutine("FadeInTaste");
        
        Image syn = taste_effect.transform.Find("Synesthesia").GetComponent<Image>();
        syn.material.SetFloat("_Sides", sweet);
        syn.material.SetFloat("_Frequency", salty);
        syn.material.SetColor("_Color", new Color(spicy, 0, 0)); //TODO: decide on the color
    }
}
