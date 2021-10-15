using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsUI : UI
{

    // Update is called once per frame
    void Update()
    {
        if (ui.activeInHierarchy) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }

    public void MainMenu() {
        GameManager.instance.SaveGame();
        SceneManager.LoadScene("MenuScene");
    } 
}
