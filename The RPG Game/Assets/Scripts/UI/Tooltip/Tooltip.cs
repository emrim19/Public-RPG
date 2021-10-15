using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Text tooltipText;
    private RectTransform backgroundRectTrans;

    public static Tooltip instance;

    // Start is called before the first frame update
    void Awake() {
        instance = this;

        backgroundRectTrans = transform.Find("TooltipBackground").GetComponent<RectTransform>();
        tooltipText = GameObject.Find("TooltipText").GetComponent<Text>();

        HideTooltip();
    }

    // Update is called once per frame
    void Update() {
        FollowMouse();
    }



    private void ShowTooltip(string tooltipString) {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2, tooltipText.preferredHeight + textPaddingSize * 2);
        backgroundRectTrans.sizeDelta = backgroundSize;
    }

    private void HideTooltip() {
        gameObject.SetActive(false);
    }

    private void FollowMouse() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint + new Vector2(0, 55);
    }

    public static void ShowTooltip_static(string tooltipString) {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_static() {
        instance.HideTooltip();
    }
}
