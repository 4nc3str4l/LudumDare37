using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public Slider DificultySlider;
    public SpriteRenderer Background;
    public Text Difficulty;
    public Text ButtonText;
    public GameObject Apocalipsis, ApicalipsisPlayers;

    private Color _originalBackgroundColor;

	// Use this for initialization
	void Start () {
        _originalBackgroundColor = Background.color;
        if(!PlayerPrefs.HasKey("Difficulty"))
            PlayerPrefs.SetInt("Difficulty", 0);
        else
        {
            DificultySlider.value = PlayerPrefs.GetInt("Difficulty");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnDificultyChange()
    {
        int difficulty = (int)DificultySlider.value;
        ButtonText.text = "PLAY!";
        Color color = new Color((255 * difficulty) / DificultySlider.maxValue, (255 * (DificultySlider.maxValue - difficulty)) / DificultySlider.maxValue, 0, 255);
        switch (difficulty)
        {
            case 0:
                Difficulty.text = "Noob";
                break;
            case 1:
                Difficulty.text = "Mehh..";
                break;
            case 2:
                Difficulty.text = "Intermediate";
                break;
            case 3:
                Difficulty.text = "Advanced";
                break;
            case 4:
                Difficulty.text = "Impossible";
                break;
            case 5:
                Difficulty.text = "APOCALIPSIS";
                ButtonText.text = "ARE YOU SURE???";
                break;
        }
        Difficulty.color = color;
        if(difficulty == DificultySlider.maxValue)
        {
            Background.color = color;
            Apocalipsis.SetActive(true);
            ApicalipsisPlayers.SetActive(true);
        }
        else
        {
            Background.color = _originalBackgroundColor;
            Apocalipsis.SetActive(false);
            ApicalipsisPlayers.SetActive(false);
        }
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }
}
