using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    public bool isGameOver = false;
    public float _timeOut = 0f;
    public int sceneToLoad = 0;

    public void Awake()
    {
        Instance = this;
        isGameOver = false;
    }

    public void GameOverLogic()
    {
        isGameOver = true;
        _timeOut = Time.time + 2f;
        sceneToLoad = 2;
    }

    internal void WinLogic()
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
