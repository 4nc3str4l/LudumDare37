using UnityEngine;
using System.Collections;
using Assets.Scripts.Skills;

public class BasePowerUP : MonoBehaviour {

    public enum POWER_UP_TYPE {COOL_DOWN, INVISIBILITY, HEALTH, SHIELD }
    public POWER_UP_TYPE PowerUpType = POWER_UP_TYPE.HEALTH;
    public Color Color;
    public string text;
    OverlayText t;
    Skill _skill;

	// Use this for initialization
	void Start () {
        t = UIController.Instance.CreateOverlayText(transform.position, Color, text);
        switch (PowerUpType)
        {
            case POWER_UP_TYPE.HEALTH:
                _skill = new HealSkill();
                break;
            case POWER_UP_TYPE.INVISIBILITY:
                break;
            case POWER_UP_TYPE.SHIELD:
                break;
            case POWER_UP_TYPE.COOL_DOWN:
                _skill = new FasterCooldownSkill();
                break;
            default:
                _skill = new HealSkill();
                break;
                
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player") return;
        _skill.Execute(collider.gameObject.GetComponent<Participant>());
        Destroy(t.gameObject);
        Destroy(gameObject);
    }
}
