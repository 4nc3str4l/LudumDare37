using UnityEngine;
using System.Collections;

public class Minigun : MonoBehaviour {

    public Participant player;
    public GameObject ProjectilePrefab;
    public bool CanFire = false;
    public const float FIRE_SPEED = 0.1f;
    private float _nextShot = 0;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot()
    {
        if (!CanFire || _nextShot > Time.time) return;
        Vector3 randomOffset = new Vector3(Random.Range(0, 0.1f), Random.Range(0, 0.1f), transform.position.z);
        GameObject go = GameObject.Instantiate(ProjectilePrefab, this.transform.position + randomOffset , this.transform.rotation) as GameObject;
        go.GetComponent<Projectile>().SetOwner(player);
        JukeBox.Instance.PlaySound(JukeBox.SOUNDS.SHOOT);
        _nextShot = Time.time + FIRE_SPEED;
    }
}
