using UnityEngine;

public class Projectile : MonoBehaviour {

    private GameObject _owner;
    public const float DMG = 13f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * 10 * Time.deltaTime);    
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_owner == collision.collider.gameObject) return;

        if(collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Participant>().GetHurt(DMG);
        }
        Destroy(this.gameObject);
    }

    internal void SetOwner(Participant player)
    {
        GetComponent<SpriteRenderer>().color = player.PlayerColor;
        _owner = player.gameObject;
    }
}
