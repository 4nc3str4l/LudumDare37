using UnityEngine;
using System.Collections;

public class BasePowerUP : MonoBehaviour {
    
    public Color Color;
    public string text;
    OverlayText t;

	// Use this for initialization
	void Start () {
        t = UIController.Instance.CreateOverlayText(transform.position + Vector3.up * 0.7f, Color, text);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player") return;
        Destroy(t.gameObject);
        Destroy(gameObject);
    }
}
