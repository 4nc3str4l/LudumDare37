using UnityEngine;

public class Participant : MonoBehaviour {

    public string Name;
    public const float SPEED = 5f;
    public const float ASSASSIN_PENALIZATION_SPPED = 1.5f;
    public const float MAX_HEALTH = 100f;
    public Cannon Cannon;
    public Color PlayerColor;
    public float Health = MAX_HEALTH;

    public delegate void ParticipantMethod(Participant participant);
    public static event ParticipantMethod OnPlayerDead;
    public static event ParticipantMethod OnPlayerHurt;
    private float _actualSpeed;

    private Participant _target;

	// Use this for initialization
	void Start () {
        UIController.Instance.CreateHealthController(this);
        PlayerColor = this.GetComponent<SpriteRenderer>().color;
        MapController.OnAssasinChange += OnAssasinChange;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
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
        transform.Translate(Vector3.right * _actualSpeed * Time.deltaTime);
    }

    public void MoveBack()
    {
        transform.Translate(Vector3.left * _actualSpeed * Time.deltaTime);
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

    public void OnAssasinChange(Participant participant)
    {
        Cannon.CanFire = participant.Equals(this);
        _actualSpeed = participant.Equals(this) ? SPEED - ASSASSIN_PENALIZATION_SPPED : SPEED;
    }

}
