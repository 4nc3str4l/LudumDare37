using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {


    public static UIController Instance;
    public GameObject PlayerHealthPrefab, OverlayText, FloatingText, FadingText;
    public Text DialogText, DificultyText;
    public SkillPanel Skill1, Skill2, Special;

    public Sprite Octa, Pinky, Cyano, Lemon, Bloody;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

	// Use this for initialization
	void Start () {
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 0:
                DificultyText.text = "Noob";
                break;
            case 1:
                DificultyText.text = "Mehh..";
                break;
            case 2:
                DificultyText.text = "Intermediate";
                break;
            case 3:
                DificultyText.text = "Advanced";
                break;
            case 4:
                DificultyText.text = "Impossible";
                break;
            case 5:
                DificultyText.text = "APOCALIPSIS";
                DificultyText.color = Color.red;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateHealthController(Participant player)
    {
        GameObject playerHealth = GameObject.Instantiate(PlayerHealthPrefab, transform) as GameObject;
        playerHealth.GetComponent<PlayerHealth>().Player = player;
    }

    public OverlayText CreateOverlayText(Vector3 position, Color color, string text)
    {
        GameObject txtOverlay = GameObject.Instantiate(OverlayText, transform) as GameObject;
        OverlayText uiText = txtOverlay.GetComponent<OverlayText>();
        uiText.Position = position;
        uiText.color = color;
        uiText.GetComponent<Text>().text = text;
        return uiText;
    }

    public void HideAllRoomDialogs()
    {
        DialogText.enabled = false;
    }

    public void ShowMessage(string message, Color color)
    {
        DialogText.enabled = true;
        DialogText.text = message;
        DialogText.color = color;
        OctaRoom.Instance.SetColor(color);
    }

    internal void CreateFloatingText(string text, Color color, Transform position)
    {
        GameObject txtOverlay = GameObject.Instantiate(FloatingText, transform) as GameObject;
        FloatingText floatingText = txtOverlay.GetComponent<FloatingText>();
        floatingText.Initialize(color, text, position);
    }

    internal void CreateFadingPanel(string text, Color color, Sprite image)
    {
        GameObject txtOverlay = GameObject.Instantiate(FadingText, transform) as GameObject;
        FadingPanel floatingText = txtOverlay.GetComponent<FadingPanel>();
        floatingText.Init(image, color, text);
    }
}
