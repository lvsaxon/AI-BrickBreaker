using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public Transform wRespawn, bRespawn;
	public GUIText DarkScoreText, WhiteScoreText;
	int bScore, wScore;

	Transform ball, ball2;
	GameObject blackGoal, whiteGoal;
    BallSpawn spawnBall;


	void Start(){
        bScore = 0;
        wScore = 0;

		blackGoal = GameObject.FindGameObjectWithTag("Black Goal");
		whiteGoal = GameObject.FindGameObjectWithTag("White Goal");
	}


	/* Set score for the Dark paddleBar */
	public void SetBlackScore(int points){

		bScore += points;
		DarkScoreText.text = "Score: " + bScore.ToString();
	}

	/* Set score for the White paddleBar */
	public void SetWhiteScore(int points){

		wScore += points;
		WhiteScoreText.text = "Score: " + wScore.ToString();
	}


	/* When the AI Paddle has scored, the ball respawns on the Opponent AI Paddle's side */
	void OnTriggerExit(Collider collid){
		GameObject spawning = GameObject.FindGameObjectWithTag("Manager");
		spawnBall = spawning.GetComponent<BallSpawn>();

		ball = GameObject.FindWithTag("Ball").transform; 
		ball2 = GameObject.FindWithTag("Ball2").transform;

		//White Goal Condition
		if(ball.position.x < whiteGoal.transform.position.x){
		   SetBlackScore(25);
			
		   ball.rotation = wRespawn.rotation;
		   ball.position = wRespawn.position;
		   spawnBall.Spawning_Ball1(ball);

		}else if(ball2.position.x < whiteGoal.transform.position.x){
		         SetBlackScore(25);

			     ball2.rotation = wRespawn.rotation;
				 ball2.position = wRespawn.position;
			     spawnBall.Spawning_Ball2(ball2);
		}

		//Black Goal Condition
		if(ball.position.x > blackGoal.transform.position.x){
			SetWhiteScore(25);

			ball.rotation = bRespawn.rotation;
			ball.position = bRespawn.position;
			spawnBall.Spawning_Ball1(ball);

		}else if(ball2.position.x > blackGoal.transform.position.x){
			     SetWhiteScore(25);

			     ball2.rotation = bRespawn.rotation;
			     ball2.position = bRespawn.position;
			     spawnBall.Spawning_Ball2(ball2);
		}
	}
}
