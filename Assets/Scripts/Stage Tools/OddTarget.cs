using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class OddTarget : MonoBehaviour
{
    [Header("Child Link Object")]
    public GameObject oddImage;
    public GameObject bingo;
    public GameObject clickTargetSFX;

    // public Information
    public string ODD_ID;

    // Get Data
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }
    public int objIndex { get { return transform.GetSiblingIndex(); } }
    public string oddID { get { return gm.stageID + "." + (objIndex + 1); } }
    public int oddFound { get { return PlayerPrefs.GetInt("odd_found" + oddID); } }    

    private void Start()
    {
        // แสดงข้อมูลของ Odd ตัวนี้
        ODD_ID = oddID;
    }

    private void Update()
    {
        // สำหรับเช็คการแสดง Bingo Image ถ้าเจอแล้วให้แสดง
        if (oddFound == 1)
            bingo.SetActive(true);
        else
            bingo.SetActive(false);
    }

    // Events Click
    public void EventOnClick()
    {
        // เมื่อคลิ๊กให้เลื่อน Target ไปยังตำแหน่ง
        gm.targetBar.SetIndexStory(objIndex);

        clickTargetSFX.GetComponent<AudioSource>().Play();
    }

}
