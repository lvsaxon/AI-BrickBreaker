using UnityEngine;
using System.Collections;

public class BallBouncing : MonoBehaviour {

	public float speed;
    Rigidbody rigidbody;
	float xDir, zDir;

	void Start(){
		xDir = Random.Range(1, 2);
		zDir = Random.Range(-2, 2);
        
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(xDir*speed, 0f, zDir);
	}
}
