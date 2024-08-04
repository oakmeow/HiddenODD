using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OddCore : MonoBehaviour
{
    // private Link
    private GameManager gm;// { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }
    private GameObject bingo;

    //[Header("SoundFX Objects")]
    //public GameObject[] soundFX;

    // isClick
    Vector2 origin;
    private float t = 0.0f;
    //private float x1, y1;

    // Config
    /*private float CONFIG_CLICK_RANGE = 10.0f;
    private float CONFIG_CLICK_INTERVAL = 0.5f;
    private bool isClick
    {
        get
        {
            if (t < CONFIG_CLICK_INTERVAL)
                return true;
            return false;
        }
    }
    private bool isOnRange
    {
        get
        {
            if (Mathf.Abs(x1 - Input.mousePosition.x) < CONFIG_CLICK_RANGE && Mathf.Abs(y1 - Input.mousePosition.y) < CONFIG_CLICK_RANGE)
                return true;
            return false;
        }
    }
    private bool isOnUI
    {
        get
        {
            if (gm.isOnUI || gm.isBlock)
                return true;
            return false;
        }
    }
    private Vector3 position{
        get {
            return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }*/

    // Odd Data
    public int objIndex { get { return transform.GetSiblingIndex(); } }
    public string oddID { get { return gm.stageID + "." + (objIndex + 1); } }
    public int oddFound {
        get { return PlayerPrefs.GetInt("odd_found" + oddID); }
        set { PlayerPrefs.SetInt("odd_found" + oddID, value); PlayerPrefs.Save(); }
    }



    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bingo = transform.Find("Bingo").gameObject;
    }

    private void Update()
    {
        if(t > 0)
            t += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if (!GetComponent<OddCore>().enabled)
            return;

        // เก็บค่ากดครั้งแรก
        origin = Input.mousePosition;
        //x1 = Input.mousePosition.x;
        //y1 = Input.mousePosition.y;

        //gm.isOddClick = true;

        // เริ่มจับเวลาได้!!!
        t = 0.01f;
    }

    private void OnMouseUp()
    {
        if (!GetComponent<OddCore>().enabled)
            return;

        if(gm.IsZahClick(origin, t))
        {
            // เลื่อน TargetBar ไปที่ตำแหน่งทุกครั้ง
            gm.targetBar.SetIndexStory(objIndex);

            // ถ้าเจอครั้งแรกให้  และแสดง Bingo
            if (oddFound == 0)
            {
                // ให้ PlayerPrefs.odd_found = 1 และแสดง Bingo
                oddFound = 1;
                bingo.SetActive(true);

                // ถ้าหาเจอครบหมดแล้ว
                if (gm.isComplete)
                    gm.complete.SetComplete();
            }
        }

        t = 0;
        //gm.isOddClick = false;


        // ถ้าอยู่ในวินาทีคลิ๊ก และไม่เกินระยะ และไม่อยู่บน UI ใดๆ ให้เล่น ClickFX และ SoundFX
        /*if (isClick && isOnRange && !isOnUI)
        {
            // เลื่อน TargetBar ไปที่ตำแหน่งทุกครั้ง
            gm.targetBar.SetIndexStory(objIndex);

            // ถ้าเจอครั้งแรกให้ PlayerPrefs.odd_found = 1 และแสดง Bingo
            if (oddFound == 0)
            {
                oddFound = 1;
                bingo.SetActive(true);

                // ถ้าหาเจอครบหมดแล้ว
                if (gm.isComplete)
                    gm.complete.SetComplete();
            }

            // สุ่มเล่นเสียง soundFX
            if (soundFX.Length > 0)
            {
                int random = Random.Range(0, soundFX.Length);
                if (soundFX[random])
                {
                    GameObject soundInst = Instantiate(soundFX[random], position, Quaternion.identity, gm.effectCanvas.transform);
                    soundInst.GetComponent<AudioSource>().PlayDelayed(0);
                    Object.Destroy(soundInst, 1.0f);
                }
            }
        }*/

        // t = 0;
        //gm.isOddLandClick = false;
    }
}
