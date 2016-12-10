using UnityEngine;
using System.Collections.Generic;

public class JukeBox : MonoBehaviour {

    public enum SOUNDS { SHOOT }
    public static JukeBox Instance;
    private AudioSource _audioSource;
    public AudioClip ShootSound;
    public Dictionary<SOUNDS, AudioClip> _loadedSounds;

    void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        _loadedSounds = new Dictionary<SOUNDS, AudioClip>()
        {
            { SOUNDS.SHOOT, ShootSound },
        };
	}
	
    public void PlaySound(SOUNDS sound)
    {
        _audioSource.PlayOneShot(_loadedSounds[sound]);
    }
}
