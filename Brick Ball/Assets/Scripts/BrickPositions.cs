using UnityEngine;
using System.Collections;

public class BrickPositions : MonoBehaviour {

	public GameObject[] brick;
	public Transform brickPos;
	public float columns, rows;
	
    float x, z;
	Vector3 space;
    Rigidbody rigidbody;

	void Awake(){

        rigidbody = GetComponent<Rigidbody>();

		//Setting Up Brick Positions
		for(x=0; x>-columns; x-=1.5f){   //vertical
			for(z=0; z>-rows; z-=2.5f){  //horizontal
				space = new Vector3(x, 0.0f, z);
				//Blue Bricks
				if(x==0 || x==-1.5 || x==-10.5 || x==-columns+1)
				   Instantiate(brick[0], brickPos.position+space, Quaternion.identity);

				//Purple Bricks
				if(x==-3 || x==-4.5 || x==-7.5 || x==-9)
				   Instantiate(brick[2], brickPos.position+space, Quaternion.identity);

				//Red Bricks
				if(x==(-columns+1)/2)
				   Instantiate(brick[1], brickPos.position+space, Quaternion.identity);
			}
	    }
	}

    void Update() {

        if(rigidbody.IsSleeping()) {
            rigidbody.WakeUp();
        }
    }
}
