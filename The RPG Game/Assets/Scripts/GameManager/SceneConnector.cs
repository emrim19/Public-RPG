using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneConnector { 


    public static bool newGame = false;
    public static bool loadGame = false;


    public static void SetNewGame(bool gameState) {
        newGame = gameState;
    }

    public static void SetLoadGame(bool gamerState) {
        loadGame = gamerState;
    }

    public static bool GetNewGame() {
        return newGame;
    }

    public static bool GetLoadGame() {
        return loadGame;
    }

}
