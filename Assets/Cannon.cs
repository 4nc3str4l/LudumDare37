using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public Participant player;
    public GameObject ProjectilePrefab;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot()
    {
        GameObject go = GameObject.Instantiate(ProjectilePrefab, this.transform.position, this.transform.rotation) as GameObject;
        go.GetComponent<Projectile>().SetColor(player.PlayerColor);
        JukeBox.Instance.PlaySound(JukeBox.SOUNDS.SHOOT);
    }
}
