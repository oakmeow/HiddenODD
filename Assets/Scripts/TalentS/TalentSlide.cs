using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TalentSlide : MonoBehaviour
{
    [Header("SoundFX")]
    public GameObject sfx;
    public float delaySFX;
    private GameObject instSFX;

    // Call Link
    private GameManager gm;

    private Vector3 origin;
    private Vector3 mousePosition;
    private float t = 0.0f;

    // Public Config
    public bool axisVertical;
    public float slope;
    public float marginLeft, marginRight;
    public float marginButtom, marginTop;

    private void Start()
    {
        origin = transform.position;

        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (t > 0)
            t += Time.deltaTime;
    }


    private void OnMouseDown()
    {
        // ถ้า isActing = true จะไม่เลื่อฉาก
        gm.isTalenting = true;

        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        t = 0.01f;
    }



    private void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);

        if (axisVertical)
            pos.x = slope * (pos.y - origin.y) + origin.x;
        else
            pos.y = slope * (pos.x - origin.x) + origin.y;


        float x1 = origin.x - marginLeft;
        float x2 = origin.x + marginRight;
        float y1 = origin.y - marginButtom;
        float y2 = origin.y + marginTop;


        if (pos.x < x1)
            pos.x = x1;
        if (pos.x > x2)
            pos.x = x2;

        if (pos.y < y1)
            pos.y = y1;
        if (pos.y > y2)
            pos.y = y2;

        if (pos.x != transform.position.x || pos.y != transform.position.y)
        {
            if (sfx && instSFX == null)
            {
                instSFX = Instantiate(sfx, gm.effectCanvas.transform);
                instSFX.GetComponent<AudioSource>().Play();
                Destroy(instSFX, delaySFX);
            }
        }

        // ตั้งค่าตำแหน่ง
        transform.position = pos;
    }

    private void OnMouseUp()
    {
        gm.isTalenting = false;

        t = 0.0f;
    }
}
