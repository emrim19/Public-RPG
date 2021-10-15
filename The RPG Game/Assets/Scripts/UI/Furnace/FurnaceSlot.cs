using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurnaceSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public List<Item> items = new List<Item>();
    public Image icon;
    public Text countText;
    public Slider cookSlider;

    public void Additem(Item newItem) {
        if(items.Count == 0) {
            items.Add(newItem);

            icon.sprite = items[0].icon;
            icon.enabled = true;

            countText.text = items.Count.ToString();

            cookSlider.gameObject.SetActive(true);
        }
        else if(items.Count > 0) {
            if(items[0]._name == newItem._name) {
                items.Add(newItem);

                icon.sprite = items[0].icon;
                icon.enabled = true;

                countText.text = items.Count.ToString();

                cookSlider.gameObject.SetActive(true);
            }
        }
    }

    public void RemoveItem() {
        if(items.Count >= 1) {
            items.RemoveAt(items.Count - 1);

            countText.text = items.Count.ToString();
        }
        if(items.Count == 0) {
            ClearSlot();
        }
    }

    public void ClearSlot() {
        items.Clear();

        icon.sprite = null;
        icon.enabled = false;


        cookSlider.gameObject.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData) {
        if(items.Count != 0) {
            Tooltip.ShowTooltip_static(items[0]._name);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.HideTooltip_static();
    }

}
