using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {

    public static bool isGameOver = false;
    public static float _timeOut = 0f;
    public static int sceneToLoad = 0;

    public void Awake()
    {
        isGameOver = false;
    }

    public static void GameOverLogic()
    {
        isGameOver = true;
        _timeOut = Time.time + 2f;
        sceneToLoad = 2;
        Debug.Log("Game Over");
    }

    internal static void WinLogic()
    {
        isGameOver = true;
        _timeOut = Time.time + 2f;
        sceneToLoad = 3;
    }

    void UnsubscribeAllEvents()
    {
        Participant.OnPlayerDead += null;
        Participant.OnPlayerHurt += null;
        MapController.OnAssasinChange += null;
    }
    
    void Update()
    {
        if (isGameOver && _timeOut < Time.time)
        {
            UnsubscribeAllEvents();
            SceneManager.LoadScene(sceneToLoad);
        }              
    }
}
