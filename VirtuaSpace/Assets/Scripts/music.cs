﻿using UnityEngine;
using System.Collections;

public class music: MonoBehaviour {

	//An AudioSource object so the music can be played
	private AudioSource aSource;
	//A float array that stores the audio samples
	public float[] samples = new float[256];
	public ParticleSystem m_currentParticleEffect;
	private ParticleSystem.Particle[] ParticleList;


	void Awake () {
		//Get and store a reference to the following attached components:
		//AudioSource
		this.aSource = GetComponent<AudioSource> ();
		ParticleSystem m_currentParticleEffect = (ParticleSystem)GameObject.Find("Fireflies").GetComponent("ParticleSystem");

	}

	// Update is called once per frame
	void Update () {
		//Obtain the samples from the frequency bands of the attached AudioSource
		aSource.GetSpectrumData (this.samples, 0, FFTWindow.BlackmanHarris);
		float sum = 0;

		//For each sample
		for (int i=0; i<samples.Length; i++) {
			sum += samples [i] * samples [i];
		}
		float rms = Mathf.Sqrt (sum / 256f) * 50f;

		sum = 0;
		for (int i=0; i<samples.Length/3; i++) {
			sum += samples [i] * samples [i];
		}
		float rmsBeat = Mathf.Sqrt (sum / 256f) * 50f;

		//rms = rmsBeat;

		//Debug.Log (rms*200);

		ParticleSystem.Particle []ParticleList = new ParticleSystem.Particle[m_currentParticleEffect.particleCount];
		m_currentParticleEffect.GetParticles(ParticleList);
		m_currentParticleEffect.emissionRate = (int)rms*30 + 8; 	//number of particles increase
		
		for(int i = 0; i < ParticleList.Length; ++i)
		{
			if (i < ParticleList.Length/2) {
				ParticleList[i].size = rmsBeat*600+230;						//size increase
				ParticleList[i].velocity = new Vector3(rmsBeat*6000+200,0,0); 	//speed increase
			} else {
				ParticleList[i].size = rms*600+230;						//size increase
				ParticleList[i].velocity = new Vector3(rms*6000+200,0,0); 	//speed increase
			}
		}        
		m_currentParticleEffect.SetParticles(ParticleList, m_currentParticleEffect.particleCount);

		//Molten Halo Music Effect
		//GameObject.Find ("MoltenHalo").GetComponent<Light>().range = rms * 300 + 1200;

	}
}
