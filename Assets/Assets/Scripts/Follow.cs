using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject target;

	void LateUpdate () {

		Vector2 delta = target.transform.position - transform.position;

		Rigidbody body = GetComponent<Rigidbody> ();
		body.velocity = delta;
	}
}
