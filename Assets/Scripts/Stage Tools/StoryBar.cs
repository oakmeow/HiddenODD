using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryBar : MonoBehaviour
{
    [Header("Primary Link Object")]
    public GameManager gm;
    [Header("Child Link Objects")]
    public GameObject storyMain;
    public GameObject lockButton;
    public TextMeshProUGUI storyText;
    // SFX
    public GameObject clickSFX;
    private GameObject sfx;

    // Key
    public bool isActive { get { return storyMain.activeInHierarchy; } }
    public int isLock { get { return PlayerPrefs.GetInt("last_story_lock"); } }
    // Key Event Mouse
    [System.NonSerialized]
    public bool isOver = false;

    private bool dontclose = false;
    private int objIndex;



    public void Start()
    {
        clickSFX = Instantiate(clickSFX, transform);
    }

    private void Update()
    {
        // ถ้ายังไม่ Active ไม่ต้องทำงาน
        if (!isActive)
            return;

        // ถ้า CameraControl กำลัง Drag อยู่ ให้ปิดหน้าต่าง StoryBar
        if (!dontclose && gm.isCameraDrag && isLock == 0)
            storyMain.SetActive(false);
        
        // เปลี่ยนรูปตาม isLock
        lockButton.GetComponent<Image>().sprite = lockButton.transform.GetChild(isLock).GetComponent<Image>().sprite; // sp_lock[isLock];

        // แปลง oddIndex เป็น oddID แล้วดึงค่า OddStory มาใส่ใน StoryText
        string oddID = gm.stageID + "." + (objIndex + 1);
        storyText.text = gm.data.OddGetStory(oddID);

        dontclose = false;
    }

    public void SetStory(int objIndex)
    {
        dontclose = true;
        storyMain.SetActive(true);
        this.objIndex = objIndex;
    }

    // Event Clic
    public void EventOnClickLock()
    {
        // ถ้ากดแล้ว ล็อค(1) อยู่ให้เปลี่ยนเป็น ไม่ล็อค(0) แต่ถ้าเป็น ไม่ล็อค(0) อยู่ให้สลับเป็น ล็อค(1) พร้อมเซฟใส่ PlayerPrefs
        if (isLock == 1)
            PlayerPrefs.SetInt("last_story_lock", 0);
        else
            PlayerPrefs.SetInt("last_story_lock", 1);
        PlayerPrefs.Save();

        clickSFX.GetComponent<AudioSource>().Play();
    }

    // Event Check Mouse สำหรับ MainCamera
    public void EventCheckEnter() { isOver = true; }
    public void EventCheckExit() { isOver = false; }
}
