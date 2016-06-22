using UnityEngine;
using System.Collections;

public class BrickDestroyed : MonoBehaviour {

    public GameObject brickParticles;
	public bool blacksBall, whitesBall;

	Score score;
	DarkBarFollowObject darkFollowObject;
	WhiteBarFollowObject whiteFollowObject;

	void Start(){

		//Get Goal score component
		GameObject scoring = GameObject.FindGameObjectWithTag("Manager");
		score = scoring.GetComponent<Score>();

        GameObject darkPaddle = GameObject.FindGameObjectWithTag("Black Bar");
		darkFollowObject = darkPaddle.GetComponent<DarkBarFollowObject>();

		GameObject whitePaddle = GameObject.FindGameObjectWithTag("White Bar");
		whiteFollowObject = whitePaddle.GetComponent<WhiteBarFollowObject>();
	}

	public void Contact(bool _wBall, bool _bBall){

		blacksBall = _bBall;
		whitesBall = _wBall;
	}


    /** When Ball destroys Brick; give points to the owner **/
	void OnCollisionEnter(Collision collision){

        Instantiate(brickParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
		if((collision.collider.tag == "Ball" && darkFollowObject.blacksBall) || (collision.collider.tag == "Ball2" && darkFollowObject.blacksBall2)){

			if(gameObject.tag == "Blue Brick")
			   score.SetBlackScore(5);
			   
			if(gameObject.tag == "Purple Brick")
			   score.SetBlackScore(10);
			   
			else if(gameObject.tag == "Red Brick")
			   score.SetBlackScore(15);
		}


		if((collision.collider.tag == "Ball" && whiteFollowObject.whitesBall) || (collision.collider.tag == "Ball2" && whiteFollowObject.whitesBall2)){
			  
			if(gameObject.tag == "Blue Brick")
			   score.SetWhiteScore(5);
			
			if(gameObject.tag == "Purple Brick")
			   score.SetWhiteScore(10);
			
			else if(gameObject.tag == "Red Brick")
			   score.SetWhiteScore(15);
	    }
	}
}
