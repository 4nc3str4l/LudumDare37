using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayText : MonoBehaviour {

    private Text _image;
    public Vector3 Position;
    public Color color;


    // Use this for initialization
    void Start()
    {
        _image = GetComponent<Text>();
        _image.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(Position);
    }

}
