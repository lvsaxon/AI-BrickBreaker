using UnityEngine;
using System.Collections;

public class WhiteBarFollowObject : MonoBehaviour {

	public bool whitesBall, whitesBall2;
	public float speed;
	public BallObject ballPrefab;

	MoveWhite_PaddleBar movePaddle;
	BallBouncing ballBouncing;
	GameObject ball, ball2, ballSpawn;

    Vector3[] hitPositions = new Vector3[3];
	Transform followBall;
	Vector3 returnBall_Position, origin;
	bool isFollowingBall, idlePosition;
    bool returned;


	void Start(){
		isFollowingBall = false;
		followBall = null;
        returned = false;

        whitesBall2 = true;
		idlePosition = true;

		ball = ballPrefab.ball;
		ball2 = ballPrefab.ball2;

		GameObject FollowBallCollider = GameObject.FindGameObjectWithTag("Follow_Collider(White AI)");
		movePaddle = FollowBallCollider.GetComponent<MoveWhite_PaddleBar>();

		origin = transform.position; //Paddle Bar's original position when starting game
	}
	
    void FixedUpdate() {
        Angle();
    }

    void Update(){
		
		if(ball != null || ball2 != null){
			ball = GameObject.FindWithTag("Ball");
			ball2 = GameObject.FindWithTag("Ball2");

			MovePaddle_OnTrigger();
			
            //If true, smoothly transition to follow ball's position
			if(isFollowingBall == true){
				DistanceTo_GoalLine(ball.transform, ball2.transform);
				returnBall_Position = new Vector3(transform.position.x, 0f, Mathf.Clamp(followBall.position.z, -7f, 6.6f));
				transform.position = Vector3.Lerp(transform.position, returnBall_Position, speed*Time.deltaTime);

                //Return to idlePosition when is Retured
                if(returned == true)
                   idlePosition = true;

			}else if(idlePosition == true)
				transform.position = Vector3.Lerp(transform.position, new Vector3(origin.x, 0f, origin.z), (speed/2)*Time.deltaTime);
			
		}else
			return;
	}


    /** Chosen Angle with the highest Value **/
	Vector3 Angle(){
        int maxValue = 0, indx = 0;
        Vector3 barAiming_Position = new Vector3();
		int[] values = new int[3];
        float distanceOut = 25f;

        //Forward Raycast Direction
        values[0] = ForwardRaycastDirection(distanceOut);

        //Left Raycast Direction
        values[1] = LeftRaycastDirection(distanceOut);
       
        //Right Raycast Direction
        values[2] = RightRaycastDirection(distanceOut);
      
        for(int i=0; i < values.Length; i++) {
            if(values[i] > maxValue) { 
               maxValue = values[i]; 
               indx = i;
            }
        }

        barAiming_Position = hitPositions[indx];
        return barAiming_Position;
	}

	
    /** Acquire the current Value from the Forward Raycast **/
    int ForwardRaycastDirection(float distanceOut) {
        int hitValue = 0;
        RaycastHit hitForward;
        Ray centerRay = new Ray(transform.position, Vector3.right);  //forward Pos

        //Forward Raycast Direction
        if(Physics.Raycast(centerRay, out hitForward, distanceOut)){
            Debug.DrawRay(centerRay.origin, centerRay.direction*distanceOut, Color.red);
          
            if (hitForward.collider.gameObject.GetComponent<Collider>().tag == "Blue Brick") 
                hitValue = 5;
                
            if (hitForward.collider.gameObject.GetComponent<Collider>().tag == "Purple Brick") 
                hitValue = 10;

            if (hitForward.collider.gameObject.GetComponent<Collider>().tag == "Red Brick")
                hitValue = 15;
        }

        hitPositions[0] = centerRay.direction;
        return hitValue;
    }


    /** Acquire the current Value from the Left Raycast **/
	int LeftRaycastDirection(float distanceOut) {
        int hitValue = 0;
        RaycastHit hitLeft;
        Ray leftRay = new Ray(transform.position, new Vector3(1, 0, .3f));  //left Pos

        if(Physics.Raycast(leftRay, out hitLeft, distanceOut)) {
            Debug.DrawRay(leftRay.origin, leftRay.direction*distanceOut, Color.green);

            if (hitLeft.collider.gameObject.GetComponent<Collider>().tag == "Blue Brick") 
                hitValue = 5;
                
            if (hitLeft.collider.gameObject.GetComponent<Collider>().tag == "Purple Brick") 
                hitValue = 10;

            if (hitLeft.collider.gameObject.GetComponent<Collider>().tag == "Red Brick")
                hitValue = 15;
        }

        hitPositions[1] = leftRay.direction;
        return hitValue;
    }


    /** Acquire the current Value from the Right Raycast **/
    int RightRaycastDirection(float distanceOut) {
        int hitValue = 0;
        RaycastHit hitRight;
        Ray rightRay = new Ray(transform.position, new Vector3(1, 0, -.3f)); //right Pos

        if(Physics.Raycast(rightRay, out hitRight, distanceOut)) {
            Debug.DrawRay(rightRay.origin, rightRay.direction*distanceOut, Color.blue);

            if (hitRight.collider.gameObject.GetComponent<Collider>().tag == "Blue Brick") 
                hitValue = 5;
                
            if (hitRight.collider.gameObject.GetComponent<Collider>().tag == "Purple Brick") 
                hitValue = 10;

            if (hitRight.collider.gameObject.GetComponent<Collider>().tag == "Red Brick")
                hitValue = 15;
        }

        hitPositions[2] = rightRay.direction;
        return hitValue;
    }

	
    /** Calculate Ball Distance from the Goal & choose the one Closest to the Goal Line **/
	Transform DistanceTo_GoalLine(Transform ball, Transform ball2){
		GameObject whiteGoal = GameObject.FindGameObjectWithTag("White Goal");
		
		//Ball distance from White Goal
		float distance = ball.position.x - whiteGoal.transform.position.x;
		float ballDist = distance;
		
		//Ball2 distance from White Goal
		float distance2 = ball2.position.x - whiteGoal.transform.position.x;
		float ball2Dist = distance2;
		
		if(ballDist < ball2Dist)
		   followBall = ball;

		if(ballDist > ball2Dist)
	       followBall = ball2;
		
		return followBall;
	}
	

    /** Move PaddleBar when FollowCollider Tigger is Active **/
	void MovePaddle_OnTrigger(){

		if(movePaddle.followBall == true)
		   isFollowingBall = true;
		   
		if(movePaddle.followBall == false && movePaddle.count == 0){
		   isFollowingBall = false;
	       idlePosition = true;
		}
	}


	/** Play audio onCollision when Ball collids 
	 * Declare ownership of Ball2 when OnCollision **/
	void OnCollisionEnter(Collision collision){
        AudioSource audio = GetComponent<AudioSource>();

        DarkBarFollowObject barFollow;
		GameObject darkBar = GameObject.FindGameObjectWithTag("Black Bar");
		barFollow = darkBar.GetComponent<DarkBarFollowObject>();


		if(collision.collider.tag == "Ball" || collision.collider.tag == "Ball2")
		   audio.Play();

		if(collision.collider.tag == "Ball"){
			whitesBall = true;
			barFollow.blacksBall = false;
			
		}else if(collision.collider.tag == "Ball"){
			whitesBall2 = true;
			barFollow.blacksBall2 = false;
		}
	}

    
    /** Aiming Angle function within the onTrigger Function **/
    void OnCollisionExit(Collision collision){

        //Changing Ball Directions
        if(collision.collider.tag == "Ball2") {
           if(ball2.transform.position.x >= -13.9f)
              ball2.GetComponent<Rigidbody>().velocity = Angle() * 21f;
            
        } else {
           if(ball.transform.position.x >= -13.9f)
              ball.GetComponent<Rigidbody>().velocity = Angle() * 21f;
        }

        //Returned Ball when CollisionExit & from a certain distance
        if (collision.collider.tag == "Ball")
            returned = true;
        
        if (collision.collider.tag == "Ball2")
            returned = true;

    }
}

