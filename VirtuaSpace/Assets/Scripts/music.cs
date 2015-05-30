using UnityEngine;
using System.Collections;

public class music: MonoBehaviour {

	//An AudioSource object so the music can be played
	private AudioSource aSource;
	//A float array that stores the audio samples
	public float[] samples = new float[256];
	public ParticleSystem m_currentParticleEffect;
	private ParticleSystem.Particle[] ParticleList;
	private float endTime;
	private GameObject titleCard;


	void Awake () {
		//Get and store a reference to the following attached components:
		//AudioSource
		this.aSource = GetComponent<AudioSource> ();
		ParticleSystem m_currentParticleEffect = (ParticleSystem)GameObject.Find("Fireflies").GetComponent("ParticleSystem");
		endTime = Mathf.Infinity;
		titleCard = GameObject.Find ("TitleCard");
	}

	// Update is called once per frame
	void Update () {
		if (!aSource.isPlaying) {
			Debug.Log(endTime);
			if (Time.time < endTime) {
				endTime = Time.time;
			}
			if (Time.time - endTime >= 2) {
				endTime = Mathf.Infinity;
				titleCard.SetActive (true);
				this.gameObject.SetActive (false);
			}
		}

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
		m_currentParticleEffect.emissionRate = (int)rms*30 + 10; 	//number of particles increase

		float vari = rmsBeat*1500+200;
		for(int i = 0; i < ParticleList.Length; ++i)
		{
			if (i < ParticleList.Length/2) {
				ParticleList[i].size = rmsBeat*650+130;						//size increase
				ParticleList[i].velocity = new Vector3(rms*4000+200,Random.Range(-vari,vari),Random.Range(-vari,vari)); 	//speed increase
			} else {
				ParticleList[i].size = rms*650+130;						//size increase
				ParticleList[i].velocity = new Vector3(rms*4000+200,Random.Range(-vari,vari),Random.Range(-vari,vari)); 	//speed increase
			}
		}        
		m_currentParticleEffect.SetParticles(ParticleList, m_currentParticleEffect.particleCount);

		//Molten Halo Music Effect
		//GameObject.Find ("MoltenHalo").GetComponent<Light>().range = rms * 300 + 1200;

	}
}
