using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VRTarget : MonoBehaviour {
	private static VRTarget mInstance;
	public static VRTarget instance {get{return mInstance;}}
	public SpriteRenderer targetSprite;

	// Use this for initialization
	void Start () {
		if (mInstance == null) {
			mInstance = this;
		}
	}

	public void TargetChosen(bool choosen) {
		if (choosen) {
			targetSprite.color = Color.red;
		} else {
			targetSprite.color = new Color(75/255f,125/255f,193/255f,1f);
		}
	}
}
