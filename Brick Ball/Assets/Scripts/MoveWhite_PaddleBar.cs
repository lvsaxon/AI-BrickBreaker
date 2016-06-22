using UnityEngine;
using System.Collections;

public class MoveWhite_PaddleBar : MonoBehaviour {

	public bool followBall = false, followBall2 = false;
	public int count = 0;
	

	/* AI Paddle follows ball, when ball enters Tigger */
	void OnTriggerEnter(Collider collid){
	
		if(collid.tag == "Ball" || collid.tag == "Ball2"){
		   count++;
		   followBall = true;
		}
	}
	
	void OnTriggerExit(Collider collid){

		if(collid.tag == "Ball" || collid.tag == "Ball2"){
		   count--;
		   followBall = true;
		}

		if(count == 0)
		   followBall = false;
	}
}
