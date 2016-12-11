using UnityEngine;
using System.Collections;
using Assets.Scripts.Skills;

public class BasePowerUP : MonoBehaviour {

    public Color Color;
    public string text = "Speed";
    OverlayText t;
    Skill _skill;
    public delegate void PowerUpDelegate(BasePowerUP powerUp);
    public static event PowerUpDelegate OnPowerUpTaken;
    public static event PowerUpDelegate OnPowerUpAppeared;

    public void Initialize()
    {
        int randomNumber = Random.Range(0, 4);
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
            case 3:
                _skill = new DamageSkill();
                break;
            default:
                break;
        }
        if(OnPowerUpAppeared!=null)
            OnPowerUpAppeared(this);
        //t = UIController.Instance.CreateOverlayText(transform.position + (Vector3.forward * 2f), Color, _skill.Name);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player") return;
        if(OnPowerUpTaken != null)
            OnPowerUpTaken(this);
        _skill.Execute(collider.gameObject.GetComponent<Participant>());
        Destroy(gameObject);
    }
}
