using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentClickMove : MonoBehaviour
{
    // Game Manager
    private GameManager gm;

    public float speed;
    [Header("Move to Position 1 (Move To)")]  // Click = 0
    public Vector2 move1;
    [Header("Move to Position 2 (Origin)")]  // Click = 1
    public Vector2 move2;

    // Key
    //private bool move;
    private int operatorX, operatorY;
    private int orderNO = 0;
    private Vector2 distance;
    private bool isReady
    {
        get
        {
            if (distance.x <= 0 && distance.y <= 0)
                return true;
            return false;
        }
    }


    // Click FXCore
    Vector2 origin;
    private float t = 0.0f;


    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //Debug.Log($"Speed = {speed} , Move1 = {move1} , {move2}");
    }
    private void Update()
    {
        if (t > 0)
            t += Time.deltaTime;

        // เริ่มเคลื่อนที่
        if (!isReady)
        {
            float speedDelta = speed * Time.deltaTime;
            float speedX = speedDelta * operatorX;
            float speedY = speedDelta * operatorY;
            if (distance.x > 0)
            {
                distance -= new Vector2(speedDelta, 0);
                transform.position += new Vector3(speedX, 0);
            }
            if (distance.y > 0)
            {
                distance -= new Vector2(0, speedDelta);
                transform.position += new Vector3(0, speedY);
            }
        }
    }

    private void OnMouseDown()
    {
        // เก็บค่ากดครั้งแรก
        origin = Input.mousePosition;

        // เริ่มจับเวลาได้!!!
        t = 0.01f;
    }
    private void OnMouseUp()
    {
        if (gm.IsZahClick(origin, t) && isReady)
        {
            if (orderNO == 0)
            {
                operatorX = Direct(move1.x);
                operatorY = Direct(move1.y);
                distance = new Vector2(Mathf.Abs(move1.x), Mathf.Abs(move1.y));
                orderNO = 1;
            }
            else
            {
                operatorX = Direct(move2.x);
                operatorY = Direct(move2.y);
                distance = new Vector2(Mathf.Abs(move2.x), Mathf.Abs(move2.y));
                orderNO = 0;
            }
            
            // Reset
            t = 0.0f;
        }
    }


    private int Direct(float value)
    {
        if (value > 0)
            return 1;
        if (value < 0)
            return -1;
        return 0;
    }
}
