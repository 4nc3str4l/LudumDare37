using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    Participant _participant;

	// Use this for initialization
	void Start () {
        _participant = GetComponent<Participant>();
        _participant.IsThePlayer = true;
	}
	
	// Update is called once per frame
	void Update () {

        var objectPos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - objectPos;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));


        if (Input.GetKey(KeyCode.W))
        {
            _participant.MoveForward();
        }

        if (Input.GetKey(KeyCode.A))
        {
            _participant.TurnLeft();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _participant.SpecialSkill();
        }

        if (Input.GetKey(KeyCode.S))
        {
            _participant.MoveBack();
        }

        if (Input.GetKey(KeyCode.D))
        {
            _participant.TurnRight();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _participant.PrincipalSkill();
        }

        if (Input.GetButton("Fire2"))
        {
            _participant.SecondarySkill();
        }
    }
}
