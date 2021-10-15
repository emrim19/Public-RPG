using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void NewGame() {
        SceneConnector.SetNewGame(true);
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadGame() {
        SceneConnector.SetLoadGame(true);
        if (SceneConnector.GetLoadGame()) {
            SceneManager.LoadScene("SampleScene");
        }
    }


    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

}
