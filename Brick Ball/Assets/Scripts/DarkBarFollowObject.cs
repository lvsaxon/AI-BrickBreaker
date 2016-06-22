using UnityEngine;
using System.Collections;

public class DarkBarFollowObject : MonoBehaviour{

    public bool blacksBall, blacksBall2;
    public float paddleSpeed, ballSpeed;
    public BallObject ballPrefab;

    MoveBlack_PaddleBar movePaddle;
    BallBouncing ballBouncing;
    GameObject ball, ball2, ballSpawn;

    Transform followBall;
    Vector3 barAiming_Position, origin;
    bool isFollowingBall, idlePosition, returned;

    BrickDetection brickDetection;

    void Start(){
        isFollowingBall = false;
        followBall = null;

        returned = false;
        blacksBall = true;
        idlePosition = true;

        ball = ballPrefab.ball;
        ball2 = ballPrefab.ball2;

        //Follow Ball Trigger
        GameObject FollowBallCollider = GameObject.FindGameObjectWithTag("Follow_Collider(Black AI)");
        movePaddle = FollowBallCollider.GetComponent<MoveBlack_PaddleBar>();

        //Getting Brick Detections inside Collider
        GameObject BrickDetection_Objects = GameObject.FindGameObjectWithTag("Brick Detection");
        brickDetection = BrickDetection_Objects.GetComponent<BrickDetection>();

        origin = transform.position; //Paddle Bar's original position when starting game
    }


    void Update(){

        if (ballPrefab.ball2 != null || ballPrefab.ball != null){
            ball = GameObject.FindWithTag("Ball");
            ball2 = GameObject.FindWithTag("Ball2");

            MovePaddle_OnTriggerEnter();

            //If true, smoothly transition to follow ball's position
            if (isFollowingBall == true) {
                DistanceTo_GoalLine(ball.transform, ball2.transform);
                barAiming_Position = new Vector3(transform.position.x, 0f, Mathf.Clamp(followBall.position.z, -7f, 6.6f));
                transform.position = Vector3.Lerp(transform.position, barAiming_Position, paddleSpeed * Time.deltaTime);

                //Return to idlePosition when is Retured
                if(returned == true)
                   idlePosition = true;

            } else if (idlePosition == true)
                transform.position = Vector3.Lerp(transform.position, new Vector3(origin.x, 0f, origin.z), (paddleSpeed / 2) * Time.deltaTime);

        }else
            return;
    }


    /** Move PaddleBar when FollowCollider Tigger is Active **/
    void MovePaddle_OnTriggerEnter() {

        if (movePaddle.followBall == true)
            isFollowingBall = true;

        if (movePaddle.followBall == false && movePaddle.count == 0){
            isFollowingBall = false;
            idlePosition = true;
        }
    }


    /** Determine if the Brick is With the Paddle's Field of View **/
    Vector3 BrickAngleDetection(){
        float fieldOfView_Angle = 180;
        Vector3 barAiming_Position = new Vector3();
        
        Vector3 direction = brickDetection.BrickPosition() - transform.position;
        float brickAngle = Vector3.Angle(direction, -transform.right);
        
        if(brickAngle <= (fieldOfView_Angle/2))
           barAiming_Position = direction;
        
        return barAiming_Position;
    }


    /** Calculate Ball Distance from the Goal & chose the one Closest to the Goal Line **/
    Transform DistanceTo_GoalLine(Transform ball, Transform ball2){
        GameObject blackGoal = GameObject.FindGameObjectWithTag("Black Goal");

        //Ball distance from BlackGoal
        float distance = blackGoal.transform.position.x - ball.position.x;
        float ballDist = distance;

        //Ball2 distance from BlackGoal
        float distance2 = blackGoal.transform.position.x - ball2.position.x;
        float ball2Dist = distance2;

        if (ballDist < ball2Dist)
            followBall = ball;

        else if (ballDist > ball2Dist)
            followBall = ball2;

        return followBall;
    }


    /** Play audio onCollision when ball collids **/
    void OnCollisionEnter(Collision collision){
        WhiteBarFollowObject barFollow;
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        GameObject whiteBar = GameObject.FindGameObjectWithTag("White Bar");
        barFollow = whiteBar.GetComponent<WhiteBarFollowObject>();

        if (collision.collider.tag == "Ball"){
            blacksBall = true;
            barFollow.whitesBall = false;

        }else if (collision.collider.tag == "Ball2"){
            blacksBall2 = true;
            barFollow.whitesBall2 = false;
        }
    }


    /** Aiming Angle function within the onTrigger Function **/
    void OnCollisionExit(Collision collision) {
        
        if(collision.collider.tag == "Ball") {
           returned = true;
           if(ball.transform.position.x <= 14)
              ball.GetComponent<Rigidbody>().velocity = BrickAngleDetection() * ballSpeed;

        }else if(collision.collider.tag == "Ball2"){
           returned = true;
           if(ball2.transform.position.x <= 14)
              ball2.GetComponent<Rigidbody>().velocity = BrickAngleDetection() * ballSpeed;
        }
    }
}




