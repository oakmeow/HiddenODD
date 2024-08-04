using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentFall : MonoBehaviour
{
    // Game Manager
    private GameManager gm;// { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    // Click Range
    Vector2 origin;
    private float t = 0.0f;

    // Fall Key
    private bool isFirst = true;
    private bool isFloor = false;


    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // เริ่มมาตรึงไม่ให้ Fall
        GetComponent<Rigidbody2D>().isKinematic = true;

        // ถ้ามี OddCore ให้ disable ไปก่อนพอตกลงพื้นค่อยให้ OddCore ทำงาน
        if(GetComponent<OddCore>())
            GetComponent<OddCore>().enabled = false;
    }

    private void Update()
    {
        if(t > 0.0f)
            t += Time.deltaTime;

        // ถ้า Kinematic = false เริ่มร่วง
        if (!GetComponent<Rigidbody2D>().isKinematic)
        {
            // velocity.y ตอนกดปล่อยเสี้ยววินาทีแรกจะยังเป็น 0 อยู่ จึงต้องดักให้ velocity.y ให้มากกว่า 0 ก่อนครั้งนึงถึงจะเริ่มนับจริงจัง
            if (isFirst)
                if (GetComponent<Rigidbody2D>().velocity.y > 0)
                    isFirst = false;

            // ถ้าหยุดร่วงแล้ว
            if (!isFirst && GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                // ให้แช่แข็งการร่วง
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<FXCore>().enabled = false;
                if (GetComponent<OddCore>())
                    GetComponent<OddCore>().enabled = true;
                GetComponent<TalentFall>().enabled = false;
            }
        }
    }



    private void OnMouseDown()
    {
        if (!GetComponent<TalentFall>().enabled)
            return;

        // เก็บค่ากดครั้งแรก
        origin = Input.mousePosition;

        // เริ่มจับเวลาได้!!!
        t = 0.01f;
    }
    private void OnMouseUp()
    {
        if (!GetComponent<TalentFall>().enabled)
            return;

        if (gm.IsZahClick(origin, t) && !isFloor)
        {
            // ถ้ายังไม่เคยตกถึง Floor มาก่อน ให้เริ่ม Fall
            GetComponent<Rigidbody2D>().isKinematic = false;
        }

        t = 0;
    }
}
