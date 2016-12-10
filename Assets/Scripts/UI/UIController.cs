using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {


    public static UIController Instance;
    public GameObject PlayerHealthPrefab, OverlayText;
    public Text DialogText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

	// Use this for initialization
	void Start () {
	
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
}
