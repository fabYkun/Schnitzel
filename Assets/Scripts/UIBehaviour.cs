using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBehaviour : MonoBehaviour, IPointerExitHandler
{

        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isMouseOverUI()
    {
        bool is_over = EventSystem.current.IsPointerOverGameObject();
        return is_over;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("blabla");
        this.gameObject.SetActive(false);
    }


}
