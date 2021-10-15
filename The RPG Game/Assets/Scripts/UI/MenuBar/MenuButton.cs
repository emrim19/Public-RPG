using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Text text;
    public Button button;
    public RectTransform rect;
    public float height;


    public void SetText(string text) {
        this.text.text = text;
    }
}                           
