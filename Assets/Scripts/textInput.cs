using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textInput : MonoBehaviour
{
    public TextMeshPro bartext;
    // Start is called before the first frame update
    void Start()
    {
        bartext.text = "";
    }

    // Update is called once per frame
    public void ControlSearchBar(string input)
    {
        if (input == "backspace")
        {
            if(bartext.text.Length > 0)
            {
                bartext.text = bartext.text.Remove(bartext.text.Length - 1);
            }
        }
        else if (input == "space")
        {
            bartext.text += " ";
        }
        else if (input == "esc")
        {
            bartext.text = " ";
        }
        else
        {
            bartext.text += input;
        }
    }
}
