using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameAssets : MonoBehaviour
{
    //On GameManager
    //Used for accessing uneque Game Assets

    #region singelton

    public static GameAssets instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning: More than one instance of GameAsstes found!");
            return;
        }
        instance = this;
    }
    #endregion

    public Transform damagePopup;


}
