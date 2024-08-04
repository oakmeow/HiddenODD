using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentMoveDirect : MonoBehaviour
{
    public enum Direction { Vertical, Horizontal }
    public Direction direction;
    public int directionIndex { get { return direction.GetHashCode(); } }
    private int direct;

    public float speed;
    public float posMax;
    public int animationFaceStart;
    public int animationFaceA;
    public int animationFaceB;

    private Vector3 origin;
    private float posOrigin {
        get {  
            if (directionIndex == 1)
                return origin.x;
            return origin.y;
        }
    }
    private float posCurrent {
        get {
            if (directionIndex == 1)
                return transform.position.x;
            return transform.position.y;
        }
        set
        {
            if (directionIndex == 1)
                transform.position += new Vector3(value, 0);
            else
                transform.position += new Vector3(0, value);
        }
    }

    // Static variable
    private static int FACE_HASH = Animator.StringToHash("FACE");

    private void Start()
    {
        origin = transform.position;

        // กำหนด Direction เริ่มต้น
        direct = directionIndex;

        GetComponent<Animator>().SetInteger(FACE_HASH, animationFaceStart);
    }


    private void Update()
    {
        if(direct == 0)
        {
            if (posCurrent <= posOrigin + posMax)
            {
                posCurrent = Time.deltaTime * speed;
            }
            else
            {
                direct = 1;
                GetComponent<Animator>().SetInteger(FACE_HASH, animationFaceA);
            }
        }
        else if(direct == 1)
        {
            if (posCurrent > posOrigin)
            {
                posCurrent = -(Time.deltaTime * speed);
            }
            else
            {
                direct = 0;
                GetComponent<Animator>().SetInteger(FACE_HASH, animationFaceB);
            }
        }
    }
}
