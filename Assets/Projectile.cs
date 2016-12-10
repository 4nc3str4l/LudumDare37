using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public const float DMG = 13f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * 10 * Time.deltaTime);    
	}

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Participant>().GetHurt(DMG);
        }
        Destroy(this.gameObject);
    }
}
