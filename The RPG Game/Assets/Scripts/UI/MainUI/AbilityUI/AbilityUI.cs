using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : UI
{
    public Transform parentTransform;
    public AbilityButton[] buttons;



    private void Start() {
        buttons = parentTransform.GetComponentsInChildren<AbilityButton>();
    }

    

}
