using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class                        FlickeringText : MonoBehaviour
{
    [SerializeField]
    private Text                    text;
    [SerializeField]
    private Color                   initColor;
    [SerializeField]
    private Color                   endColor;
    [SerializeField]
    private float                   speed;
    [SerializeField]
    private AnimationCurve          curve;

    IEnumerator                     flicker()
    {
        float                       time;

        while (true)
        {
            time = 0;
            while (time < 1)
            {
                time += Time.deltaTime * speed;
                text.color = Color.Lerp(initColor, endColor, curve.Evaluate(time));
                yield return null;
            }
            while (time > 0)
            {
                time -= Time.deltaTime * speed;
                text.color = Color.Lerp(initColor, endColor, curve.Evaluate(time));
                yield return null;
            }
        }
    }

    void                            Start()
    {
        StopAllCoroutines();
        StartCoroutine(flicker());
    }
}
