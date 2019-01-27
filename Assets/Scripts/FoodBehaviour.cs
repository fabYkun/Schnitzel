using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBehaviour : MonoBehaviour
{

    public int sweet;
    public int salty;
    public int spicy;

    private bool heldDown = false;

    public CookingManager cookingManager;

    private Canvas side_menu;

    // Start is called before the first frame update
    void Start()
    {
        cookingManager = Camera.main.GetComponent<CookingManager>();
        side_menu = GetComponentInChildren<Canvas>(true);

        side_menu.GetComponentInChildren<Button>(true).onClick.AddListener(delegate { cookingManager.DisplayTaste(spicy, sweet, salty); });
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void OnMouseDrag()
    {
        float old_z = transform.position.z;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = old_z;
        this.gameObject.transform.position = newPos;
        heldDown = true;
        side_menu.gameObject.SetActive(false);
    }

    void OnMouseUpAsButton()
    {
        heldDown = false;
        side_menu.gameObject.SetActive(true);
    }

    void OnMouseEnter()
    {
        side_menu.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        side_menu.gameObject.SetActive(side_menu.GetComponent<UIBehaviour>().isMouseOverUI());
    }

    //void OnTriggerEnter2D()
    //{
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    Collider2D coll = GetComponent<Collider2D>();

    //    rb.bodyType = RigidbodyType2D.Static;
    //    rb.gravityScale = 50;
    //    Debug.Log("CollisionEnter");
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //    Collider2D coll = GetComponent<Collider2D>();
    //    if (true)
    //    {
    //        rb.bodyType = RigidbodyType2D.Dynamic;
    //        rb.gravityScale = 50;
    //        Debug.Log("CollisionExit");
    //    }
    //}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Marmite" && !heldDown)
        {
            cookingManager.AddIngredient(spicy, sweet, salty);
            Destroy(gameObject);
        }
    }

}
