using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class JukeBox : MonoBehaviour {

    public enum SOUNDS { BloodyTime, CyanoTime, Dead, Impact, LaserShoot, LemonidasTime, LetMeThink, Misile, PinkyTime, Teleport, Welcome, YouWin, Speed, Invencible, ExplosionDie, LemoDies, PinkyDies, CyaniDies, BloodyDies }
    public static JukeBox Instance;
    private AudioSource _audioSource;
    public AudioClip BloodyTime, CyanoTime, Dead, Impact, LaserShoot, LemonidasTime, LetMeThink, Misile, PinkyTime, Teleport, Welcome, YouWin, Speed, Invencible, ExplosionDie, LemoDies, PinkyDies, CyaniDies, BloodyDies;
    public Dictionary<SOUNDS, AudioClip> _loadedSounds;
    public Slider VolumeSlider;
    public static float MasterVolume = 1f;


    void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        _loadedSounds = new Dictionary<SOUNDS, AudioClip>()
        {
            { SOUNDS.BloodyTime, BloodyTime },
            { SOUNDS.CyanoTime, CyanoTime },
            { SOUNDS.Dead, Dead },
            { SOUNDS.Impact, Impact },
            { SOUNDS.LaserShoot, LaserShoot },
            { SOUNDS.LemonidasTime, LemonidasTime },
            { SOUNDS.LetMeThink, LetMeThink },
            { SOUNDS.Misile, Misile },
            { SOUNDS.PinkyTime, PinkyTime },
            { SOUNDS.Teleport, Teleport },
            { SOUNDS.Welcome, Welcome },
            { SOUNDS.YouWin, YouWin },
            { SOUNDS.Invencible, Invencible },
            { SOUNDS.Speed, Speed },
            { SOUNDS.ExplosionDie, ExplosionDie },
            { SOUNDS.LemoDies, LemoDies },
            { SOUNDS.PinkyDies, PinkyDies },
            { SOUNDS.CyaniDies, CyaniDies },
            { SOUNDS.BloodyDies, BloodyDies },
        };
	}
	
    public void PlaySound(SOUNDS sound, float volume = 0.3f)
    {
        _audioSource.PlayOneShot(_loadedSounds[sound], volume * MasterVolume);
    }

    public void OnVolumeChange()
    {
        MasterVolume = VolumeSlider.value;
        Debug.Log("Volume Changed" + MasterVolume);
    }
}
