using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogButtons : MonoBehaviour
{
    [SerializeField]
    private int charLimit = 24;
    [SerializeField]
    private Sprite smallSprite;
    [SerializeField]
    private Sprite bigSprite;
    [SerializeField]
    private Text textComponent;
    [SerializeField]
    private Image imageComponent;
    [SerializeField]
    private float width;
    [SerializeField]
    private float speed;

    private void OnEnable() {
        RectTransform rect = gameObject.GetComponent<RectTransform>();

        if (textComponent.text.Length > charLimit ) {
            imageComponent.sprite = bigSprite;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 140);
        }
        else {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 100);
        }
    }

    private void OnDisable() {
        imageComponent.sprite = smallSprite;
    }
}
