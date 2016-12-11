using System;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Color color;
    public string text;
    public Transform trans;

    public void Initialize(Color color, string text, Transform transform)
    {
        this.color = color;
        this.text = text;
        this.trans = transform;
    }

    public void Update()
    {
        try
        {
            transform.position = Camera.main.WorldToScreenPoint(trans.position);
        }
        catch(Exception ex)
        {
            Destroy(gameObject);
        }
        
    }

	// Use this for initialization
	void Start () {
        Text t = this.GetComponentInChildren<Text>();
        t.color = color;
        t.text = text;
        Destroy(gameObject, 2.04f);
	}
}
