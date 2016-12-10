using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

    public static MapController Instance;

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

    public delegate void MapMethod(Participant player);
    public static event MapMethod OnAssasinChange;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

	// Use this for initialization
	void Start () {
        _players = new List<Participant>();
        _mapComponents = new List<SpriteRenderer>();
        _background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        Participant.OnPlayerDead += OnParticipantDead;


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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetAssassin();
        }
	}

    public void GenerateMap()
    {
        SetAssassin();
       
    }

    public void SetAssassin()
    {
        //Pick a random player from the list
        _assassin = _players[Random.Range(0, _players.Count)];
        SetMapTheme(_assassin.PlayerColor);
        Debug.Log(_assassin.Name);
        if (OnAssasinChange != null)
            OnAssasinChange(_assassin);
    }

    public void SetMapTheme(Color color)
    {
        foreach(SpriteRenderer component in _mapComponents)
        {
            component.color = color;
        }
        _background.color = new Color(color.r, color.g, color.b, 0.2f);
    }

    public void OnParticipantDead(Participant participant)
    {
        _players.Remove(participant);
    }
}
