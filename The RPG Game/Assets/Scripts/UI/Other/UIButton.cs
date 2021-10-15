using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string buttonName;

    public void OnPointerEnter(PointerEventData eventData) {
        Tooltip.ShowTooltip_static(buttonName);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip_static();
    }
}
