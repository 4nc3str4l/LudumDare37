using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Skills;
using UnityEngine.SceneManagement;

public class Participant : MonoBehaviour {

    public GameObject InvencibilityHalo;
    public string Name;
    public const float SPEED = 5f;
    public const float ASSASSIN_PENALIZATION_SPPED = 1.5f;
    public const float MAX_HEALTH = 100f;
    public Cannon Cannon;
    public Minigun Minigun;
    public Color PlayerColor;
    public float Health = MAX_HEALTH;
    public GameObject ParticlePrefab;

    public delegate void ParticipantMethod(Participant participant);
    public delegate void ParticipantHURT(Participant participant, float ammount);
    public static event ParticipantMethod OnPlayerDead;
    public static event ParticipantHURT OnPlayerHurt;
    private float _actualSpeed;
    private float _nextParticleTrow = 0;
    private Participant _target;
    private bool _isAsssasin = false;

    private List<PlayerBonus> _passiveBonus;
    private Skill _principalSkill, _secondarySkill, _specialSkill;

    private bool _invencible;

    public RadialParticleEmmiter SpeedEmmiter;

    public float _recuperationSpeed = 10f;

    public bool IsThePlayer = false;
    public float CooldownBonus = 1f;
    public float DamageBonus = 1f;
    public bool isVisible = true;
    private SpriteRenderer _spriteRenderer;

    public GameObject Halo;

    public bool IsAssasin
    {
        get
        {
            return _isAsssasin;
        }
    }

    public bool Invencible
    {
        get { return _invencible; }
    }

    void Awake()
    {
        _passiveBonus = new List<PlayerBonus>();
        PlayerColor = this.GetComponent<SpriteRenderer>().color;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
        UIController.Instance.CreateHealthController(this);
        MapController.OnAssasinChange += OnAssasinChange;
        _specialSkill = new TeleportSkill(this);
    }
	
    public void SetVisible(bool visibility)
    {
        if (!visibility) UIController.Instance.CreateFloatingText("Invisible!", Color.white, transform);
        Halo.SetActive(visibility);
        _spriteRenderer.enabled = visibility;
        isVisible = visibility;
    }

	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (_principalSkill != null)
            _principalSkill.Update();

        if (_secondarySkill != null)
            _secondarySkill.Update();

        if (_specialSkill != null)
            _specialSkill.Update();

        for(int i = _passiveBonus.Count - 1; i >= 0;  i--)
        {
            _passiveBonus[i].CheckExpiration();
        }
           

        if (_nextParticleTrow < Time.time)
            CreateParticle();

        SpeedEmmiter.isActive = _actualSpeed > SPEED;
        InvencibilityHalo.SetActive(_invencible);
    }

    public void Heal(float ammount)
    {
        if(Health + ammount > 100)
        {
            Health = 100;
        }
        else
        {
            Health += ammount;
        }
        
        OnPlayerHurt(this, -ammount);
        UIController.Instance.CreateFloatingText("+" + ammount.ToString() + " Health", Color.magenta, transform);
    }

    public void AddBonus(PlayerBonus newBonus)
    {
        _passiveBonus.Add(newBonus);
    }

    public void PrincipalSkill()
    {
        if(_principalSkill != null)
            _principalSkill.Execute(this);
    }

    public void SecondarySkill()
    {
        if (_secondarySkill != null)
            _secondarySkill.Execute(this);
    }

    public bool SpecialSkill()
    {
        return _specialSkill.Execute(this);
    }

    public void ShootCannon()
    {
        Cannon.Shoot();
    }

    public void ShootMinigun()
    {
        Minigun.Shoot();
    }

    public void TurnLeft()
    {
        transform.Rotate(new Vector3(0, 0, 1) * 360 * Time.deltaTime);
    }

    public void TurnRight()
    {
        transform.Rotate(new Vector3(0, 0, -1) * 360 * Time.deltaTime);
    }

    public void MoveForward()
    {
        transform.Translate(Vector3.right * _actualSpeed * Time.deltaTime);
    }

    public void MoveBack()
    {
        transform.Translate(Vector3.left * _actualSpeed * Time.deltaTime);
    }

    public void GetHurt(float dmg)
    {
        if (_invencible) return;
        Health -= dmg;
        UIController.Instance.CreateFloatingText("-" + dmg.ToString(), Color.red, transform);
        JukeBox.Instance.PlaySound(JukeBox.SOUNDS.Impact);
        if (OnPlayerHurt != null)
            OnPlayerHurt(this, dmg);
        if (Health <= 0)
            Dead();
    }

    public void Dead()
    {
        if (IsThePlayer)
        {
            GameController.Instance.GameOverLogic();
        }

        SpeedEmmiter.CreateSomeParticles(transform.position, 100, PlayerColor, 10);
        JukeBox.Instance.PlaySound(JukeBox.SOUNDS.ExplosionDie, 0.5f);
        switch (Name)
        {
            case "Bloody":
                JukeBox.Instance.PlaySound(JukeBox.SOUNDS.BloodyDies, 1f);
                UIController.Instance.CreateFadingPanel("Bloody Dies...", PlayerColor, UIController.Instance.Bloody);
                break;
            case "Pinkie":
                JukeBox.Instance.PlaySound(JukeBox.SOUNDS.PinkyDies, 1f);
                UIController.Instance.CreateFadingPanel("Pinkie Dies...", PlayerColor, UIController.Instance.Pinky);
                break;
            case "Cyanisaurious":
                JukeBox.Instance.PlaySound(JukeBox.SOUNDS.CyaniDies, 1f);
                UIController.Instance.CreateFadingPanel("Cyanisaurious Dies...", PlayerColor, UIController.Instance.Cyano);
                break;
            case "Lemonidas":
                JukeBox.Instance.PlaySound(JukeBox.SOUNDS.LemoDies, 1f);
                UIController.Instance.CreateFadingPanel("Lemonidas Dies...", PlayerColor, UIController.Instance.Lemon);
                break;
        }

        if (OnPlayerDead != null)
            OnPlayerDead(this);
        Destroy(gameObject);
    }

    public void OnAssasinChange(Participant participant)
    {
        if (participant == null)
        {
            SetPacificSkills();
            _isAsssasin = false;
            Cannon.CanFire = false;
            Minigun.CanFire = false;
            return;
        }
        Cannon.CanFire = participant.Equals(this);
        Minigun.CanFire = participant.Equals(this);

        int dificulty = PlayerPrefs.GetInt("Difficulty");

        if (dificulty == 5)
        {
            _actualSpeed = participant.Equals(this) ? SPEED + ASSASSIN_PENALIZATION_SPPED : SPEED;
        }
        else if(dificulty == 4 || dificulty == 3)
        {
            _actualSpeed = SPEED;
        }
        else
        {
            _actualSpeed = participant.Equals(this) ? SPEED - ASSASSIN_PENALIZATION_SPPED : SPEED;
        }
        
        _isAsssasin = participant.Equals(this);
        if (_isAsssasin)
        {
            SetAgressiveSkills();
        }
        else
        {
            SetPacificSkills();
        }
    }
   
    private void SetPacificSkills()
    {
        _principalSkill = new SprintSkill(this);
        _secondarySkill = new InvencibilitySkill(this);
        if(Name == "Cyanisaurious")
        {
            TrackSkills();
        }
    }

    private void SetAgressiveSkills()
    {
        _principalSkill = new ShootSkill(this);
        _secondarySkill = new MinigunSkill(this);
        UIController.Instance.CreateFloatingText("Killer", Color.yellow, transform);
        if (Name == "Cyanisaurious")
        {
            TrackSkills();
        }
    }

    public void TrackSkills()
    {
        UIController.Instance.Skill1.SetTrackingSkill(_principalSkill);
        UIController.Instance.Skill2.SetTrackingSkill(_secondarySkill);
        UIController.Instance.Special.SetTrackingSkill(_specialSkill);
    }



    void CreateParticle()
    {
        if (!isVisible) return;

        GameObject particle = GameObject.Instantiate(ParticlePrefab,
            new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f),
                        transform.position.y + Random.Range(-0.1f, 0.1f),
                        transform.position.z), transform.rotation) as GameObject;
        particle.GetComponent<ProjectileParticle>().Initialize(PlayerColor, 0.6f);
        _nextParticleTrow = Time.time + Random.Range(0.01f, 0.03f);
    }

    public void RestartSpeed()
    {
        _actualSpeed = SPEED;
    }

    public void ChangeSpeed(float newSpeed)
    {
        _actualSpeed = newSpeed;
    }

    public void RemoveBonus(PlayerBonus bonus)
    {
        _passiveBonus.Remove(bonus);
    }

    public void SetInvencible(bool invencible)
    {
        _invencible = invencible;
        if(invencible)
            UIController.Instance.CreateFloatingText("Invencible", Color.white, transform);
    }

    public void ReduceCooldowns(float reductionAmmount)
    {
        UIController.Instance.CreateFloatingText("Cooldown reduction", Color.white, transform);
        CooldownBonus = reductionAmmount;
    }

    public void ResetCoooldownBonus()
    {
        CooldownBonus = 1f;
    }
}
