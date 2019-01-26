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


    // Start is called before the first frame update
    void Start()
    {
        
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
}
