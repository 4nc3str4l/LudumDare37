using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public static InputHandler Instance;
    public Participant participant;

	// Use this for initialization
	void Start () {
        Instance = this;
        participant = GetComponent<Participant>();
        participant.IsThePlayer = true;
	}
	
	// Update is called once per frame
	void Update () {

        var objectPos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - objectPos;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));


        if (Input.GetKey(KeyCode.W))
        {
            participant.MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            participant.SpecialSkill();
        }

        if (Input.GetKey(KeyCode.S))
        {
            participant.MoveBack();
        }

        if (Input.GetKey(KeyCode.A))
        {
            participant.TurnLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            participant.TurnRight();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            participant.PrincipalSkill();
        }

        if (Input.GetButton("Fire2"))
        {
            participant.SecondarySkill();
        }
    }
}
