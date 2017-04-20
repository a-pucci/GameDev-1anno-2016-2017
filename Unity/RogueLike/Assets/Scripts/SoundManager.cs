using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
	public AudioSource fxSource;
	public AudioSource musicSource;

	public static SoundManager Instance = null;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	void Awake ()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle(AudioClip clip)
	{
		this.fxSource.pitch = 1f;
		this.fxSource.clip = clip;
		this.fxSource.Play ();
	}

	public void RandomizeFx (params AudioClip[] clip)
	{
		int randomIndex = Random.Range (0, clip.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		this.fxSource.pitch = randomPitch;
		this.fxSource.clip = clip [randomIndex];
		this.fxSource.Play ();
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
