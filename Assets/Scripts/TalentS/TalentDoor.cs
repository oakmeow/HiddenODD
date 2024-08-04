using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentDoor : MonoBehaviour
{
    // Game Manager
    private GameManager gm;
    // Link
    [Header("Close Door")]
    public Sprite spriteClose;
    public GameObject closeSFX;
    [Header("Open Door")]
    public Sprite spriteOpen;
    public GameObject openSFX;
    [Header("Object Appear")]
    public GameObject appearObject;

    // Key
    private int frameNO = 0;
    private Vector3 position
    {
        get
        {
            return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }

    // Click FXCore
    Vector2 origin;
    private float t = 0.0f;



    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // ปิดการปรากฎกายก่อน
        if(appearObject)
            appearObject.SetActive(false);
    }
    private void Update()
    {
        if (t > 0)
            t += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        // Click FXCore
        origin = Input.mousePosition;
        t = 0.01f;
    }

    private void OnMouseUp()
    {
        if (gm.IsZahClick(origin, t))
        {
            if (frameNO == 0)
            {
                GetComponent<SpriteRenderer>().sprite = spriteOpen;
                gm.CreateSFX(openSFX, position);
                frameNO = 1;

                // ปรากฎกายหลังเปิดประตู
                if (appearObject)
                    appearObject.SetActive(true);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = spriteClose;
                gm.CreateSFX(closeSFX, position);
                frameNO = 0;
            }
        }
    }
}
