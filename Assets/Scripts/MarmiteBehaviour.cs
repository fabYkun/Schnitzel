using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarmiteBehaviour : MonoBehaviour
{

    private Canvas side_menu;

    private CookingManager cookingManager;


    // Start is called before the first frame update
    void Start()
    {
        cookingManager = Camera.main.GetComponent<CookingManager>();
        side_menu = GetComponentInChildren<Canvas>(true);

        side_menu.transform.Find("Taste").gameObject.GetComponent<Button>().onClick.AddListener(delegate { cookingManager.DisplayTaste(); });
        side_menu.transform.Find("Reset").gameObject.GetComponent<Button>().onClick.AddListener(delegate { cookingManager.Reset(); });
        side_menu.transform.Find("Reset").gameObject.GetComponent<Button>().onClick.AddListener(delegate { cookingManager.Serve(); });

        //side_menu.GetComponentInChildren<Button>(true).onClick.AddListener(delegate { cookingManager.DisplayTaste(spicy, sweet, salty); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        side_menu.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        side_menu.gameObject.SetActive(side_menu.GetComponent<UIBehaviour>().isMouseOverUI());
    }
}
