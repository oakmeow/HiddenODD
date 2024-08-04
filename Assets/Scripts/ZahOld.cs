using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZahOld : MonoBehaviour
{
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    [Header("Click Effect")]
    public GameObject clickEffect;

    private float x1, y1;
    private bool isOnRange { 
        get {
            float errorValue = 10.0f;   // Config Default=20;

            if (Mathf.Abs(x1 - Input.mousePosition.x) < errorValue && Mathf.Abs(y1 - Input.mousePosition.y) < errorValue)
                return true;
            return false;
        } }

    /*private bool isOnRange {
        get { float errorValue = 10.0f;
            if (Mathf.Abs(x1 - Input.mousePosition.x) < errorValue && Mathf.Abs(y1 - Input.mousePosition.y) < errorValue)
                return true;
            return false;
        } 
    }*/





    private void OnMouseDown()
    {
        // ดักจับการกดครั้งแรก
        x1 = Input.mousePosition.x;
        y1 = Input.mousePosition.y;

        Debug.Log(gameObject.name);
    }

    private void OnMouseUp()
    {
        // เช็คว่าไม่เกิน error ที่รับได้ && clickEffect ต้องไม่ null
        if (isOnRange && clickEffect)
        {
            // ตรวจสอบว่าไม่ได้กดบน UI หรือ Blocking
            if (gm.isOnUI || gm.isBlocking)
                return;

            // Click Effect
            Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            clickEffect.GetComponent<ClickEffect>().Create(position);
        }
    }
}
