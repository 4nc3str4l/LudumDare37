using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

    private SpriteRenderer _background;
    private List<Participant> _players;
    public Participant Assassin
    {
        get
        {
            return _assassin;
        }
    }
    private Participant _assassin;
    public static Vector2 MAP_MIN = new Vector2(-10, -5);
    public const float MAP_WIDTH = 10;
    public const float MAP_HEIGHT = 10;
    public const float OFFSET = 0.5f;

    private List<SpriteRenderer> _mapComponents;
    

	// Use this for initialization
	void Start () {
        _players = new List<Participant>();
        _mapComponents = new List<SpriteRenderer>();
        _background = GameObject.Find("Background").GetComponent<SpriteRenderer>();


        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            _players.Add(go.GetComponent<Participant>());
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MapComponent"))
        {
            _mapComponents.Add(go.GetComponent<SpriteRenderer>());
        }
       
        GenerateMap();
	}

	// Update is called once per frame
	void Update () {
	
	}

    public void GenerateMap()
    {
        SetAssassin();
        SetMapTheme(_assassin.PlayerColor);
    }

    public void SetAssassin()
    {
        //Pick a random player from the list
        _assassin = _players[Random.Range(0, _players.Count)];
    }

    public void SetMapTheme(Color color)
    {
        foreach(SpriteRenderer component in _mapComponents)
        {
            component.color = color;
        }
        _background.color = new Color(color.r, color.g, color.b, 0.2f);
    }
}
