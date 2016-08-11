using UnityEngine;
using System.Collections;

public class CamPodScript : MonoBehaviour {

	public Transform target;
	Vector3 offset;


	// Use this for initialization
	void Start () {
	
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		transform.position = target.position + offset;
		if (Input.GetMouseButton(2)&&!Input.GetMouseButton(1)) { 
			transform.rotation = Quaternion.Lerp (transform.rotation,target.rotation,5f*Time.deltaTime);
//						transform.rotation = Quaternion.Lerp (transform.rotation, new Quaternion (transform.rotation.x, transform.rotation.y, target.rotation.z, transform.rotation.w), 5f * Time.deltaTime);
//			float eulerZ = target.eulerAngles.z;
////			if(euler.x) euler-=360;
//						transform.eulerAngles = Vector3.Lerp (transform.eulerAngles, Quaternion.Euler (transform.eulerAngles.x, transform.eulerAngles.y, eulerZ), 5f * Time.deltaTime);
////				
				}
		
	}
}
