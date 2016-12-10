using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        Participant.OnPlayerDead += OnParticipantDead;
	}
	
	// Update is called once per frame
	void Update () {
        if(player != null)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
	}

    void OnParticipantDead(Participant participant)
    {
        if (participant.gameObject.Equals(player)){
            player = null;
        }
    }
}
