using UnityEngine;
using System.Collections;

 interface IBehaviour  {

	void SetTarget (Transform target);

	void AddVisibleObject (Transform obj);

	void RemoveVisibleObject (Transform obj);

	void TakeHarm(float damage,Vector3 position);

	void Die ();

	void LookAt(Vector3 position);

}
