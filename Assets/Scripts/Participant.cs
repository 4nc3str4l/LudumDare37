using UnityEngine;
using System.Collections;

public class Participant : MonoBehaviour {

    public const float SPEED = 5f;
    public const float MAX_HEALTH = 100f;
    public Cannon Cannon;
    public Color PlayerColor;
    public float Health = MAX_HEALTH;

    public delegate void ParticipantMethod(Participant participant);
    public static event ParticipantMethod OnPlayerDead;
    public static event ParticipantMethod OnPlayerHurt;

	// Use this for initialization
	void Start () {
        UIController.Instance.CreateHealthController(this);
        PlayerColor = this.GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        var objectPos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - objectPos;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
        */
    }

    public void Shoot()
    {
        Cannon.Shoot();
    }

    public void TurnLeft()
    {
        transform.Rotate(new Vector3(0, 0, 1) * 360 * Time.deltaTime);
    }

    public void TurnRight()
    {
        transform.Rotate(new Vector3(0, 0, -1) * 360 * Time.deltaTime);
    }

    public void MoveForward()
    {
        transform.Translate(Vector3.right * SPEED * Time.deltaTime);
    }

    public void MoveBack()
    {
        transform.Translate(Vector3.left * SPEED * Time.deltaTime);
    }

    public void GetHurt(float dmg)
    {
        Health -= dmg;
        if (OnPlayerHurt != null)
            OnPlayerHurt(this);
        if (Health <= 0)
            Dead();
    }

    public void Dead()
    {
        if (OnPlayerDead != null)
            OnPlayerDead(this);
        Destroy(gameObject);
    }

}
