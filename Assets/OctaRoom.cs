using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OctaRoom : MonoBehaviour {

    public static OctaRoom Instance;
    private Image _image;
    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetColor(Color color)
    {
        _image.color = color;
    }
}
