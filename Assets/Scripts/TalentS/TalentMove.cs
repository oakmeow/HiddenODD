using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentMove : MonoBehaviour
{
    private GameManager gm;
    private Animator animator;
    
    public bool once;
    [Tooltip("Skip frame 1 after round 2")]
    public bool skip1;
    [Tooltip("Default= 0 or 1 and less than 0 to stop")]
    public float accelator;

    public string prefixStateName;
    public bool colliderAsStateName;
    
    [System.Serializable]
    public class Order
    {
        public enum Action { Idle, Walk }
        public Action action;
        public string stateName;
        public float waitTime;
        public float speed;
        public Vector2 distance;
        public PolygonCollider2D colliderState;
    }
    public Order[] order;
    
    // Key
    public int orderNO;
    private int orderCount { get { return order.Length; } }
    private Vector2 distance;
    private Vector3 targetPosition;
    private float t = 0;

    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        /*if (orderCount > 0)
        {
            if (prefixStateName != string.Empty)
                animator.Play(prefixStateName + order[orderNO].stateName, 0);
            else
                animator.Play(order[orderNO].stateName, 0);
        }*/
        // Backup Position
        targetPosition = transform.position;
    }

    private void OnEnable()
    {
        /*if (orderCount > 0)
        {
            if (prefixStateName != string.Empty)
                animator.Play(prefixStateName + order[orderNO].stateName, 0);
            else
                animator.Play(order[orderNO].stateName, 0);
        }*/
    }


    private void Update()
    {
        if (orderCount <= 0)
            return;

        if (gm.disableGuideLine)
        {
            if (GetComponent<LineRenderer>())
                GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            if (GetComponent<LineRenderer>())
                GetComponent<LineRenderer>().enabled = true;
        }

        if (prefixStateName != string.Empty)
            animator.Play(prefixStateName + order[orderNO].stateName, 0);
        else
            animator.Play(order[orderNO].stateName, 0);

        // ถ้า action มีค่าเป็น Walk (1)
        if (order[orderNO].action.GetHashCode() == 1)
        {
            float multiple = GetAccelator(accelator);
            /*if (accelator == 0)
                multiple = 1;
            else if (accelator < 0)
                multiple = 0;
            else
                multiple = accelator;*/

            float speed = order[orderNO].speed * Time.deltaTime * multiple;
            float speedX = speed * Direct(order[orderNO].distance.x);
            float speedY = speed * Direct(order[orderNO].distance.y);
            Vector2 orderDistance = new Vector2(Mathf.Abs(order[orderNO].distance.x), Mathf.Abs(order[orderNO].distance.y));

            // ถ้า distance ยังไม่ถึง order.distance ให้ distance และ position เพิ่มตำแหน่งไปเรื่อยๆ
            if(distance.x < orderDistance.x)
            {
                distance += new Vector2(speed, 0);
                transform.position += new Vector3(speedX, 0, 0);
            }
            if(distance.y < orderDistance.y)
            {
                distance += new Vector2(0, speed);
                transform.position += new Vector3(0, speedY, 0);
            }
            // ถ้า distance >= order.distance ให้ไป orderNO ถัดไป
            if(distance.x >= orderDistance.x && distance.y >= orderDistance.y)
            {
                // เวลาวนไปหลายๆรอบจะชอบมีเศษทำให้เวลาวนไปลายๆรอบจะไม่กลับมาตรงที่เดิม จึงต้องดึงตพแหน่งก่อนย้าย + distance ให้ได้ตำแหน่งแม่นยำ
                transform.position = targetPosition + new Vector3(order[orderNO].distance.x, order[orderNO].distance.y, 0);

                RunOrderNO();
            }
        }
        // ถ้า action มีค่าเป็น Idle (0)
        if (order[orderNO].action.GetHashCode() == 0)
        {
            t += Time.deltaTime * GetAccelator(accelator);

            // ถ้ารอจนครบเวลาแล้วให้ Run Order ต่อไป
            if (t >= order[orderNO].waitTime)
            {
                RunOrderNO();
            }
        }
    }

    private void RunOrderNO()
    {
        if (once == true)
        {
            if (orderNO == orderCount - 1)
                return;
        }

        if (orderNO == orderCount - 1)
        {
            orderNO = 0;
            // ถ้า skip1 = true ให้ข้าม frame1 นับจาก round2
            if (skip1)
                orderNO = 1;
        }
        else
            orderNO++;

        // Change Animate state and Collider
        if (prefixStateName != string.Empty)
            animator.Play(prefixStateName + order[orderNO].stateName, 0);
        else
            animator.Play(order[orderNO].stateName, 0);

        if (colliderAsStateName)
        {
            if (transform.Find(order[orderNO].stateName).GetComponent<PolygonCollider2D>())
                GetComponent<PolygonCollider2D>().points = transform.Find(order[orderNO].stateName).GetComponent<PolygonCollider2D>().points;
        }
        else
        {
            if (order[orderNO].colliderState)
                GetComponent<PolygonCollider2D>().points = order[orderNO].colliderState.points;
        }

        // Backup Position
        targetPosition = transform.position;

        // Reset
        t = 0;
        distance = new Vector2(0, 0);
    }
    
    private int Direct(float value)
    {
        if (value > 0)
            return 1;
        if (value < 0)
            return -1;
        return 0;
    }

    private float GetAccelator(float accelator)
    {
        if (accelator == 0)
            return 1;
        else if (accelator < 0)
            return 0;
        else
            return accelator;
    }
    //private Vector2 Distance(Vector2 origin, Vector2 target){ return target - origin; }
    /*private Vector2 Direction(Vector2 position)
    {
        int x = 0, y = 0;
        if (position.x > 0)
            x = 1;
        if (position.x < 0)
            x = -1;
        if (position.y > 0)
            y = 1;
        if (position.y < 0)
            y = -1;
        return new Vector2(x, y);
    }*/
}
