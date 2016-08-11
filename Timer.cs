using UnityEngine;
using System.Collections;

public class Timer:MonoBehaviour  {
	public float countDown;
	public bool end;
	float value;

	// Use this for initialization
	void Start() {

	}

	public void SetCount(float countDownValue)
	{
//		Debug.Log ("Timer created with value " + countDownValue);
		value = countDownValue;
		countDown = countDownValue;
		end = false;
	}

	public void RestartTimer()
	{
		countDown = value;
		end = false;

	}


	// Update is called once per frame
	void FixedUpdate () {
//		if (value != 0f) {
//			if (!end)
//				countDown -= Time.deltaTime;
//			if (countDown < 0) {
//				Debug.Log ("Timer is end!");
//				end = true;
//
//			}
//	
//		}
	}
}
