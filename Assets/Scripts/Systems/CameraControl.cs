using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Master Link
    //[Header("Primary Link Object")]
    //public GameManager gm;
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    // Config Move Camera
    [Header("Config: Camera Movment")]
    public float minX;   // Default=-16
    public float maxX;    //  Default=16
    public float minY;   // Default=-10
    public float maxY;    //  Default=10
    public float speed;    // Default=1

    // Config Zoom Camera
    [Header("Config: Camera Zoom")]
    public float zoomChange;    // Default=50// 100
    public float smoothChange;  // Default=2
    public float minZoom;       // Default=5
    public float maxZoom;       // Default=15

    // Link Add Component
    private ClickEvent clickEvent;

    // Key Camera Move
    private Vector3 origin;
    private Vector3 difference;
    [System.NonSerialized]
    public bool drag = false;

    // Block
    private bool block = false;


    private void Start()
    {
        clickEvent = gameObject.AddComponent<ClickEvent>();
    }

    private void LateUpdate()
    {
        // ถ้า Setting กำลังแสดงอยู่ ไม่ให้เลื่อนจอหรือซูมใดๆเลย
        if (gm.isBlocking || gm.isTalenting)
            return;

        
        if (Input.GetMouseButtonDown(0))
        {
            // ตรวจจับการกดใน TargetBar และใน StoryBar ถ้าอยู่ใน Area ให้บล็อคการ Drag แผนที่
            if (gm.storyBar.isOver || clickEvent.IsOnArea(gm.targetBar.transform.GetChild(0).gameObject))
                block = true;
        }

        if(!block)
            Move();

        Zoom();

        if (Input.GetMouseButtonUp(0))
        {
            // ปล็ดบล็อค
            block = false;
        }
            
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            // Camera Drag
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (drag == false)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            Camera.main.transform.position = origin - difference * speed;
            Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, minX, maxX)
                , Mathf.Clamp(Camera.main.transform.position.y, minY, maxY)
                , transform.position.z);
        }

        // เซฟการเลื่อนจอล่าสุด
        if (Input.GetMouseButtonUp(0))
        {
            PlayerPrefs.SetFloat("last_camera_x" + gm.stageID, Camera.main.transform.position.x);
            PlayerPrefs.SetFloat("last_camera_y" + gm.stageID, Camera.main.transform.position.y); 
        }
    }

    private void Zoom()
    {
        if (Input.mouseScrollDelta.y > 0)
            Camera.main.orthographicSize -= zoomChange * Time.deltaTime * smoothChange;
        if (Input.mouseScrollDelta.y < 0)
            Camera.main.orthographicSize += zoomChange * Time.deltaTime * smoothChange;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

        // Save Last Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            PlayerPrefs.SetFloat("last_camera_zoom" + gm.stageID, Camera.main.orthographicSize);
            PlayerPrefs.Save();
        }
    }


    public bool IsOnDisableEffectArea()
    {
        // Disable Effect click on Setting Icon Area
        if (clickEvent.IsOnArea(gm.setting.transform.Find("Setting Icon").gameObject))
            return true;
        // Disable Effect click on TargetBar Area
        if (clickEvent.IsOnArea(gm.targetBar.transform.GetChild(0).gameObject))
            return true;
        // Disable Effect click on foundBar Area
        if (clickEvent.IsOnArea(gm.foundBar.transform.GetChild(0).gameObject))
            return true;
        // Disable Effect click on infoBar Area
        if (clickEvent.IsOnArea(gm.infoBar.transform.GetChild(0).gameObject))
            return true;
        // Disable Effect click on NextBar Area
        if (clickEvent.IsOnArea(gm.nextBar.transform.GetChild(0).gameObject))
            return true;
        // Disable Effect click on StoryBar Area
        if (gm.storyBar.isOver)
            return true;

        return false;
    }


}
