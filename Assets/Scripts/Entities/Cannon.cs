using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public Participant player;
    public GameObject ProjectilePrefab;
    public bool CanFire = false;
    public const float FIRE_SPEED = 0.5f;
    private float _nextShot = 0;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot()
    {
        GameObject go = GameObject.Instantiate(ProjectilePrefab, this.transform.position, this.transform.rotation) as GameObject;
        go.GetComponent<Projectile>().SetOwner(player);
        JukeBox.Instance.PlaySound(JukeBox.SOUNDS.Misile);
    }
}
