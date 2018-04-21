using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject target;

	void LateUpdate () {

		Vector2 delta = target.transform.position - transform.position;

		Rigidbody2D body = GetComponent<Rigidbody2D> ();
        if(delta.magnitude>float.Epsilon)
		    body.velocity = delta;
	}
}
