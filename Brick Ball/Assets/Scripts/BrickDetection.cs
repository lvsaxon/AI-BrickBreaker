using UnityEngine;
using System.Collections;

public class BrickDetection : MonoBehaviour {

    Vector3 brickPosition;

    void OnTriggerStay(Collider collid) {
        Vector3 pos = new Vector3();

        if(collid.tag == "Red Brick") {
            pos = collid.transform.position;
        
        }else if(collid.tag == "Purple Brick") {
            pos = collid.transform.position;
        
        }else if(collid.tag == "Blue Brick") {
            pos = collid.transform.position;
        }

        brickPosition = pos;
    }

    public Vector3 BrickPosition() {

        return brickPosition;
    }
}
