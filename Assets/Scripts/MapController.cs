using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour {


    public enum MAP_STATE { PLAYING, WAITING, CHOOSING }
    public MAP_STATE _currentMapState;

    public static MapController Instance;

    private SpriteRenderer _background;
    public List<Participant> Players;
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

    public const float RADIUS = 14.3f;

    private List<SpriteRenderer> _mapComponents;

    public delegate void MapMethod(Participant player);
    public static event MapMethod OnAssasinChange;

    private const float MIN_ASSASIN_TIME = 20f;
    private float _changeRoomState;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        ChangeRoomState(MAP_STATE.CHOOSING);
    }

	// Use this for initialization
	void Start () {
        Players = new List<Participant>();
        _mapComponents = new List<SpriteRenderer>();
        _background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        Participant.OnPlayerDead += OnParticipantDead;


        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            Players.Add(go.GetComponent<Participant>());
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MapComponent"))
        {
            _mapComponents.Add(go.GetComponent<SpriteRenderer>());
        }
	}

	// Update is called once per frame
	void Update () {
        switch (_currentMapState)
        {
            case MAP_STATE.PLAYING:

                if (Input.GetKeyDown(KeyCode.F1) || _changeRoomState < Time.time)
                {
                    ChangeRoomState(MAP_STATE.CHOOSING);
                    if (OnAssasinChange!=null)
                        OnAssasinChange(null); 
                }
                break;
            case MAP_STATE.WAITING:
                if(_changeRoomState < Time.time)
                {
                    SetAssassin();
                    UIController.Instance.ShowMessage("OCTAROOM SAYS: THE KILLER IS " + _assassin.Name, _assassin.PlayerColor);
                    _changeRoomState = Time.time + MIN_ASSASIN_TIME + Random.Range(0, 10f);
                    ChangeRoomState(MAP_STATE.PLAYING);
                }
                break;
            case MAP_STATE.CHOOSING:
                UIController.Instance.ShowMessage("OCTAROOM SAYS: TIME TO CHOOSE A NEW KILLER...", Color.white);
                SetMapTheme(Color.white);
                _changeRoomState = Time.time + 3f;
                ChangeRoomState(MAP_STATE.WAITING);
                break;
        }

	}

    public void ChangeRoomState(MAP_STATE newState)
    {
        Debug.Log("Current Map State: " + newState);
        _currentMapState = newState;
    }

    public void SetAssassin()
    {
        //Pick a random player from the list
        _assassin = Players[Random.Range(0, Players.Count)];
        SetMapTheme(_assassin.PlayerColor);
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
        Players.Remove(participant);
    }

    /// <summary>
    /// Pick two random numbers in the range (0, 1), namely a and b. If b < a, swap them. Your point is (b*R*cos(2*pi*a/b), b*R*sin(2*pi*a/b)).
    /// </summary>
    /// <returns></returns>
    public static Vector3 GenerateRandomPointInsideMap()
    {
        float aux = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);
        float a = Mathf.Max(aux, b);
        b = Mathf.Min(aux, b);
        float randomPoint1 = b * RADIUS * Mathf.Cos(2 * Mathf.PI * a / b);

        aux = Random.Range(0, 1f);
        b = Random.Range(0, 1f);
        a = Mathf.Max(aux, b);
        float ramdomPoint2 = b * RADIUS * Mathf.Cos(2 * Mathf.PI * a / b);

        return new Vector3(randomPoint1, ramdomPoint2, 0);
    }
}
