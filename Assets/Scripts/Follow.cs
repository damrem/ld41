using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject target;
    public float factor = 0.5f;

	void FixedUpdate ()
    {
        float prevZ = transform.position.z;
        Vector3 nextPosition = Vector3.Lerp(transform.position, target.transform.position, factor);
        nextPosition.z = prevZ;
        transform.position = nextPosition;
	}
}
