using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingManager : MonoBehaviour
{

    public int current_spicy = 10;
    public int current_salty = 100;
    public int current_sweet = 30;

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

        salty_text.text = current_salty.ToString();
        sweet_text.text = current_sweet.ToString();
        spicy_text.text = current_spicy.ToString();
    }
}
