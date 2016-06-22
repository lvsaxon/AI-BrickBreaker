using UnityEngine;
using System.Collections;


[System.Serializable]
public class BallObject{
	public GameObject ball, ball2;
	public int SpawnLimit;
}


public class BallSpawn : MonoBehaviour {

	public BallObject ballPrefab;
	public float minAngle, maxAngle, timeDuration;
	public Transform blackSpawn, whiteSpawn;

	Transform ballTransform, ball2Transform;
	BallBouncing ballSpeed;
	int count = 0;


	void Update(){

		if((ballPrefab.ball != null || ballPrefab.ball2 != null) && count < ballPrefab.SpawnLimit){
			Instantiate(ballPrefab.ball, blackSpawn.position, transform.rotation);
			Instantiate(ballPrefab.ball2, whiteSpawn.position, transform.rotation);
			count++;
		
        }else if(ballPrefab.ball == null){
			count=0;
		}
    }

	/* After the seconds delay has passed; paddle will launch ball to continue Playing */
	IEnumerator Proceed(float seconds, Transform ballObj){
		float z = 0;
		ballSpeed = ballObj.GetComponent<BallBouncing>();

		ballObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		yield return new WaitForSeconds(seconds);

		if(ballObj.GetComponent<Rigidbody>().velocity == Vector3.zero){
			z = (float)System.Math.Round(UnityEngine.Random.Range(minAngle, maxAngle), 2);
			ballObj.GetComponent<Rigidbody>().velocity = new Vector3(ballSpeed.speed, 0f, z);
		}
	}

	/* Wait before launching Ball */
	public void Spawning_Ball1(Transform _ball){
		StartCoroutine(Proceed(timeDuration, _ball));
	}

	/* Wait before launching Ball2 */
	public void Spawning_Ball2(Transform _ball2){
		StartCoroutine(Proceed(timeDuration, _ball2));
	}

}
	