using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class FieldView : MonoBehaviour {

	List<Transform> visibleObjects = new List<Transform> ();
	IBehaviour behaviourScript;




	void OnTriggerStay(Collider otherCol)
	{
		if (otherCol.transform.root != null) {
			if (otherCol.transform.root.GetComponent<Stats> () != null) {
//		Debug.Log ("Collider "+otherCol.name + " is visible for " + transform.root.name + " now!");	
				if (!visibleObjects.Contains (otherCol.transform.root)) {
					visibleObjects.Add (otherCol.transform.root);
					behaviourScript.AddVisibleObject (otherCol.transform.root);
				}
			}
		}
	}

	void OnTriggerExit(Collider otherCol)
	{
		if (otherCol.transform.root != null) {
			if (visibleObjects.Contains (otherCol.transform.root)) {
				visibleObjects.Remove (otherCol.transform.root);
				behaviourScript.RemoveVisibleObject (otherCol.transform.root);
//				Debug.Log ("Collider " + otherCol.name + " is NOT visible for " + transform.root.name + " now!");	

		
			}
		}
	}
	// Use this for initialization
	void Start () {
		 behaviourScript = transform.root.GetComponent<IBehaviour> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
