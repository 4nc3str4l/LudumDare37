using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {

    public static void GameOverLogic()
    {
        SceneManager.LoadScene(2);
    }

    internal static void WinLogic()
    {
        SceneManager.LoadScene(3);
    }
}
