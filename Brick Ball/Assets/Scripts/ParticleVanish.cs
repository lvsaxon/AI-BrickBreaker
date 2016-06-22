using UnityEngine;
using System.Collections;

public class ParticleVanish : MonoBehaviour {

	public float lifeTime;

	void Start(){
        Destroy(gameObject, lifeTime);
	}
}
