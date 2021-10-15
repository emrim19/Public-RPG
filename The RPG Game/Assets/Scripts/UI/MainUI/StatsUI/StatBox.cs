using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBox : MonoBehaviour
{
    public Text textArea;


    public void UpdateStats(Stat stat) {
        textArea.text = stat.GetValue().ToString();
    }


}
