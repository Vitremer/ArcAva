using UnityEngine;
using System.Collections;

public class GoPoint : MonoBehaviour {
	Animation anim;
	public AnimationClip animClipOfDestroing;
    public int index = 0;
    public GoPoint nextPoint;
    public GoPoint prevPoint;
	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animation> ();
	}

	public void Destroy()
	{
		Destroy (gameObject);
//
//		anim.PlayQueued (animClipOfDestroing.name);
	}
	// Update is called once per frame
	void Update () {

	}
}
