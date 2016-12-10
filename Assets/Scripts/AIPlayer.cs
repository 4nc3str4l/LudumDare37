using UnityEngine;
using System.Collections;

public class AIPlayer : MonoBehaviour {

    public enum GLOBAL_STATE { ASSASSIN, VICTIM }
    public enum STATE { GOING_TO_SPOT, HUNTING, KILLING }
    private Participant _participant;

    private GLOBAL_STATE _globalState = GLOBAL_STATE.VICTIM;
    private STATE _actualState = STATE.GOING_TO_SPOT;

	// Use this for initialization
	void Start () {
        _participant = GetComponent<Participant>();
        MapController.OnAssasinChange += OnAssassinChanged;
	}
	
	// Update is called once per frame
	void Update () {

        switch (_globalState)
        {
            case GLOBAL_STATE.ASSASSIN:
                PredatorBehaivour();
                break;
            case GLOBAL_STATE.VICTIM:
                VictimBehaivour();
                break;
            default:
                break;
        }      
	}

    public void PredatorBehaivour()
    {
        switch (_actualState)
        {
            case STATE.HUNTING:
                Hunting();
                break;
            case STATE.KILLING:
                Killing();
                break;
            default:
                Debug.LogError("This should not happen!");
                break;
        }
    }

    public void Hunting()
    {

    }

    public void Killing()
    {

    }

    public void VictimBehaivour()
    {
        _participant.Shoot();
        if (DetectMapBoundsDistance() < 10f)
        {
            _participant.TurnLeft();
        }
        _participant.MoveForward();
    }

    public float DetectMapBoundsDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(_participant.Cannon.transform.position, transform.right, 100);
        if (hit.collider != null)
        {
            if (!hit.collider.tag.Equals("MapComponent"))
                return float.MaxValue;
            float distance = Vector3.Distance(transform.position, hit.point);
            return distance;
        }
        return float.MaxValue;
    }

    public void OnAssassinChanged(Participant participant)
    {
        _globalState = participant.gameObject == gameObject ? GLOBAL_STATE.ASSASSIN : GLOBAL_STATE.VICTIM;
    }
}
