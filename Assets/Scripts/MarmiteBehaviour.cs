using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarmiteBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("bite");
        if (other.gameObject.tag == "Food")
        {
            Debug.Log("coucou");
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("bite");
        if (other.gameObject.tag == "Food")
        {
            Debug.Log("coucou");
        }
    }
}
