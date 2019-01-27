using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{

    public int sweet;
    public int salty;
    public int spicy;

    private bool heldDown = false;

    public CookingManager cookingManager;

    // Start is called before the first frame update
    void Start()
    {
        cookingManager = Camera.main.GetComponent<CookingManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = -1.0f;
        this.gameObject.transform.position = newPos;
        heldDown = true;

    }

    void OnMouseUpAsButton()
    {
        heldDown = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Marmite" && !heldDown)
        {
            cookingManager.AddIngredient(spicy, sweet, salty);
            Destroy(gameObject);
        }
    }

    void OnCollisonEnter2D(Collision other)
    {
     
            Debug.Log("food onCollision");
    }

}
