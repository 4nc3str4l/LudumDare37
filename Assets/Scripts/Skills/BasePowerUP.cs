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

    public void Initialize()
    {
        t = UIController.Instance.CreateOverlayText(transform.position, Color, text);
        int randomNumber = Random.Range(0, 3);
        switch (randomNumber)
        {
            case 0:
                _skill = new HealSkill();
                break;
            case 1:
                _skill = new FasterCooldownSkill();
                break;
            case 2:
                _skill = new InvisibilitySkill();
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player") return;
        Debug.Log(_skill.Name);
        _skill.Execute(collider.gameObject.GetComponent<Participant>());
        Destroy(t.gameObject);
        Destroy(gameObject);
    }
}
