using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VRTarget : MonoBehaviour {
	private static VRTarget mInstance;
	public static VRTarget instance {get{return mInstance;}}
	public SpriteRenderer targetSprite;
	public SpriteRenderer animSprite;
	public static bool reticleMode = true;
	public static bool reticleModeToggle = true;
	private float timer;
	private float t = 0;
	private Color targetColor = new Color(75/255f,125/255f,193/255f,1f);
	private Color animColor = Color.white;
	private Color hoverColor = Color.white;

	// Use this for initialization
	void Awake () {
		if (mInstance == null) {
			mInstance = this;
		}
		// Color Adjustments
		targetSprite.color = targetColor;
		animSprite.transform.localScale = new Vector3(0,0,0);
		animSprite.color = animColor;

		timer = TeleportLegacyUI.timerMax;    //get transition time from other script
	}

	void Update() {
		if (Input.GetKeyUp(KeyCode.H)) { 
			gameObject.SetActive(false);
		}
		if (Cardboard.SDK.CardboardTriggered && reticleMode) {
			reticleModeToggle = !reticleModeToggle;
			if (!reticleModeToggle) {
				targetSprite.gameObject.SetActive(false);
			} else {
				targetSprite.gameObject.SetActive(true);
			}
		}

	}

	public void TargetChosen(bool choosen) {
		if (choosen) {
			targetSprite.color = hoverColor;
			if (reticleMode && reticleModeToggle) {
				animSprite.transform.localScale = Vector3.Lerp(new Vector3(0,0,0),new Vector3(1,1,1), t);
				t += Time.deltaTime/timer;
			}
			//targetSprite.color = new Color(75/255f,125/255f,193/255f,1f);
		} else {
			targetSprite.color = targetColor;
			animSprite.transform.localScale = new Vector3(0,0,0);
			t = 0;
		}
	}
}
