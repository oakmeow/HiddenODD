using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OddLandOld : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //[Header("Info")]
    //public string ODD_ID;
    //public int ODD_FOUND;

    // Get Data
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }
    public int objIndex { get { return transform.GetSiblingIndex(); } }
    public string oddID { get { return gm.stageID + "." + (objIndex + 1); } }
    public int oddFound { get { return PlayerPrefs.GetInt("odd_found" + oddID); }
                          set { PlayerPrefs.SetInt("odd_found" + oddID, value); PlayerPrefs.Save(); }}
    //private GameObject bingo { get { return transform.GetChild(0).gameObject; } }
    private GameObject bingo;
    private GameObject sfx1 { get { return gm.foundSFX.transform.GetChild(0).gameObject; } }
    private GameObject sfx2 { get { return gm.foundSFX.transform.GetChild(1).gameObject; } }

    // Key
    private float x1, x2;
    private float y1, y2;
    private float t = 0.0f;
    //private bool completeFirstTime = true;

    // Config
    private float errorValue = 20.0f;       // ตั้งค่าความไม่ตั้งใจในการลากจอแล้วไปโดน Odd โดยค่าที่รับได้ (Error) ไม่เกินกี่ระยะ
    private float hideTime = 7.0f;          // ตั้งค่าให้ Bingo หายหลังจากกี่วินาที
    //private float completeTime = 3.0f;      // ตั้งค่าแสดง CompleteMenu หลังจากกี่วินาที
    private float correctTime = 1.4f;
    

    private void Start()
    {
        bingo = transform.Find("Bingo").gameObject;
    }

    private void Update()
    {
        if (bingo.activeInHierarchy)
        {
            // ปรับขนาดของ Bingo ตามการ zoom ของกล้อง
            float size = Camera.main.orthographicSize;
            float cal = 100 - ((size - 10) * 5);
            bingo.GetComponent<Image>().pixelsPerUnitMultiplier = cal;

            // แสดง Correct
            if (t >= correctTime)
            {
                if (!bingo.transform.GetChild(0).gameObject.activeInHierarchy)
                    sfx2.GetComponent<AudioSource>().PlayDelayed(0);

                bingo.transform.GetChild(0).gameObject.SetActive(true);
            }

            // ตั้งเวลาให้ Bingo ปิดตามเวลาของ hideTime
            if (t >= hideTime)
            {
                bingo.SetActive(false);
                bingo.transform.GetChild(0).gameObject.SetActive(false);
            }

            t += Time.deltaTime;
        }
    }

    // PointerEvents Click
    public void OnPointerDown(PointerEventData eventData)
    {
        x1 = Input.mousePosition.x;
        y1 = Input.mousePosition.y;

        //gm.isOddClick = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        x2 = Input.mousePosition.x;
        y2 = Input.mousePosition.y;
        float x = Mathf.Abs(x1 - x2);
        float y = Mathf.Abs(y1 - y2);

        if (x < errorValue && y < errorValue)
        {
            // เลื่อน TargetBar ไปที่ตำแหน่งทุกครั้ง
            gm.targetBar.SetIndexStory(objIndex);

            // ถ้าเจอครั้งแรกให้ PlayerPrefs.odd_found = 1 และแสดง Bingo
            if (oddFound == 0)
            {
                sfx1.GetComponent<AudioSource>().PlayDelayed(0);

                oddFound = 1;
                bingo.SetActive(true);

                // ถ้าหาเจอครบหมดแล้ว
                if (gm.isComplete)
                    gm.complete.SetComplete();
            }
        }

        //gm.isOddClick = false;
    }
}

