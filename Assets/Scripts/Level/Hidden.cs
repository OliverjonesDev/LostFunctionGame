using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{

    public float opacity;
    bool opacityInc;
    public Color color;
    public bool opacityChange;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        changeColour();
    }
    void changeColour()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, opacity);
        }
        if (opacityChange)
        {
            if (opacity >= 1)
            {
                opacityInc = false;
            }
            if (opacity <= .2)
            {
                opacityInc = true;
            }
            if (opacityInc == true)
            {
                opacity += .01f;
            }
            else
            {
                opacity -= .01f;
            }
        }
    }
}
