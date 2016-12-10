using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Participant Player;
    private Image _image;


	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();
        _image.color = Player.PlayerColor;
        Participant.OnPlayerHurt += OnPlayerGetsHurted;
        Participant.OnPlayerDead += OnPlayerDead;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position);
    }

    public void OnPlayerGetsHurted(Participant player)
    {
        if (player.Equals(Player))
        {
            this._image.fillAmount = Player.Health / Participant.MAX_HEALTH;
        }
    }

    public void OnPlayerDead(Participant player)
    {

        if (player.gameObject.Equals(Player.gameObject))
        {
            Destroy(gameObject);
        }
    }

}
