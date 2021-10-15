using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{

    public GameObject ui;
    
    // Start is called before the first frame update
    void Awake()
    {
        ui = transform.GetChild(0).gameObject;
    }

    public virtual void DeactivateUI() {
        if(ui != null) {
            ui.SetActive(false);
        }
    }

    public virtual void ActivateUI() {
        if(ui != null) {
            ui.SetActive(true);
        }
    }

    public virtual void ToggleUI() {
        if(ui != null) {
            ui.SetActive(!ui.activeInHierarchy);
        }
    }

    public bool IsActive() {
        if (ui.activeInHierarchy) {
            return true;
        }
        else {
            return false;
        }
    }

    
}
