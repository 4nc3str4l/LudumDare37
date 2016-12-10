using UnityEngine;

public class Projectile : MonoBehaviour {

    private GameObject _owner;
    public GameObject ParticlePrefab;
    public Color _color;

    public const float DMG = 13f;

    private float _nextParticleTrow = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * 10 * Time.deltaTime);
        if(_nextParticleTrow < Time.time)
            CreateParticle();
            
	}


    void CreateParticle()
    {
        GameObject particle = GameObject.Instantiate(ParticlePrefab, 
            new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f),
                        transform.position.y + Random.Range(-0.1f, 0.1f),
                        transform.position.z), transform.rotation) as GameObject;
        particle.GetComponent<ProjectileParticle>().Initialize(_color);
        _nextParticleTrow = Time.time + Random.Range(0.01f, 0.03f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_owner == collision.collider.gameObject) return;

        if(collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Participant>().GetHurt(DMG);
            collision.collider.transform.position = collision.collider.transform.position + transform.forward * Random.Range(5f, 20f);
        }
        Destroy(this.gameObject);
    }

    internal void SetOwner(Participant player)
    {
        GetComponent<SpriteRenderer>().color = player.PlayerColor;
        _owner = player.gameObject;
        _color = player.PlayerColor;
    }
}
