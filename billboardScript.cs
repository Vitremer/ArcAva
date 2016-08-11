using UnityEngine;
using System.Collections;

public class billboardScript : MonoBehaviour {
	Animation anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
	}
	void OnAwake()
	{
		
		anim.Play ();
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt (Camera.main.transform);
//		if(!anim.IsPlaying(anim[2]))
//			Destroy (gameObject, 1.0f);
	}
}
