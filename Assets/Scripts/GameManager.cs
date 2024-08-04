using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Data Link Object")]
    public Data data;

    [Header("System Link Objects")]
    public CameraControl mainCamera;
    public Loading loading;
    public Setting setting;
    public Complete complete;

    [Header("Sound Manager")]
    public GameObject bgm;
    public GameObject foundSFX;

    [Header("Stage Tool Link Objects")]
    public Scroll targetBar;
    public StoryBar storyBar;
    public FoundBar foundBar;
    public InfoBar infoBar;
    public NextBar nextBar;

    [Header("Other Link")]
    public GameObject effectCanvas;

    // Stage Data
    public int stageIndex { get { return SceneManager.GetActiveScene().buildIndex - 1; } }
    public string stageID { get { return data.StageGetID(stageIndex); } }
    public string stageTitle { get { return data.StageGetTitle(stageIndex); } }
    public int foundCount { get { return data.StageFoundCount(stageIndex); } }
    public int stageTotal { get { return data.StageGetTotal(stageIndex); } }
    public int nextUnlock { get { return data.StageGetUnlock(stageIndex + 1); }
                            set { data.StageSetUnlock(stageIndex + 1, 1); }}

    // Stage Pass Grade & complete grade
    public int passMinimium { get { return (int)Mathf.Ceil((stageTotal * 50.0f / 100.0f)); } }
    public int passRemain
    {
        get
        {
            int remain = passMinimium - foundCount;
            if (remain < 0)
                remain = 0;
            return remain;
        }
    }
    public bool isComplete {
        get {
            if (foundCount >= stageTotal) return true;
            else return false;
        }
    }

    // Global Variable
    public bool isCameraDrag { get{ return mainCamera.drag; } }
    public bool isBlocking
    {
        get
        {
            if (setting.settingMenu.activeInHierarchy || complete.completeMain.activeInHierarchy)
                return true;
            else
                return false;
        }
    }
    public bool isOnUI { get { return mainCamera.IsOnDisableEffectArea(); } }
    [System.NonSerialized] public bool isTalenting = false; // สำหรับไม่ให้เลื่อนฉากถ้า Talent-Slide ทำงานอยู่
    //[System.NonSerialized] public bool isOddClick = false; // เช็คว่า OddLand โดนคลิ๊กอยู่รึเปล่า?
    [SerializeReference]
    public bool disableGuideLine;

    // RESET
    private float RESET_CAMERA_SIZE = 10;


    private void Start()
    {
        // คืนค่าภาษา
        //int lastLanguage = PlayerPrefs.GetInt("last_language");
        //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lastLanguage];

        // คืนค่า ความสว่าง และเสียง
        //setting.SetBrightness(PlayerPrefs.GetInt("last_brightness"));
        //setting.SetVolume(PlayerPrefs.GetInt("last_volume"));

        // คืนตำแหน่งการ Zoom แต่กรณีที่ last_camera_zoom ยังไม่เคยสร้างมาก่อนให้ค่าเท่ากับ default_cameraSize
        if (!PlayerPrefs.HasKey("last_camera_zoom" + stageID))
        {
            PlayerPrefs.SetFloat("last_camera_zoom" + stageID, RESET_CAMERA_SIZE);
            PlayerPrefs.Save();
        }
        float cameraSize = PlayerPrefs.GetFloat("last_camera_zoom" + stageID);
        Camera.main.orthographicSize = cameraSize;

        // คืนค่าตำแหน่งกล้อง x, y
        float cameraX = PlayerPrefs.GetFloat("last_camera_x" + stageID);
        float cameraY = PlayerPrefs.GetFloat("last_camera_y" + stageID);
        Camera.main.transform.position = new Vector3(cameraX, cameraY, Camera.main.transform.position.z);

        // คืนค่าตำแหน่ง TargetBar ครั้งสุดท้าย
        targetBar.SetIndex(PlayerPrefs.GetInt("last_target_index" + stageID));

        // เซฟ last_stage_index ที่เข้ามาล่าสุด
        PlayerPrefs.SetInt("last_stage", stageIndex);
        PlayerPrefs.Save();

        // ประกาศ AudioSource หลักของ GameManager เพื่อโหลด BGM
        //audioPlayer = GetComponent<AudioSource>();

        // Info
        //stageIDInfo = stageID;
    }
    private void Update()
    {
        // ถ้า BGM ไม่ได้เล่นเพลง ให้สุ่มเพลงมาเล่น
        /*if (!audioPlayer.isPlaying )
        {
            // สุ่มเพลงเล่น
            int bgmIndex = BGMInfo = Random.Range(0, bgm.transform.childCount);
            audioPlayer = bgm.transform.GetChild(bgmIndex).GetComponent<AudioSource>();
            audioPlayer.Play();
            
            Debug.Log("length = " + audioPlayer.clip.length);
        }
        Debug.Log("pitch = " + audioPlayer.time);*/
    }

    [ContextMenu("Pause")]
    public void AudioPause() { bgm.GetComponent<AudioSource>().Pause(); }
    public void AudioPlay(float delay) { bgm.GetComponent<AudioSource>().PlayDelayed(delay); }
    [ContextMenu("Save Screen")]
    public void SaveScreen()
    {
        Debug.Log("Save Screenshot");
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/screenshots/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png", 4);
        UnityEditor.AssetDatabase.Refresh();
    }


    // Scene Functions
    public int sceneIndex { get { return loading.GetSceneIndex(); } }
    public void SceneNextStage() { loading.LoadScene(sceneIndex + 1); }
    public void SceneWelcome() { loading.LoadScene(0); }

    // Sound Function
    public void CreateSFX(GameObject sfx, Vector3 position)
    {
        GameObject soundInst = Instantiate(sfx, position, Quaternion.identity, effectCanvas.transform);
        soundInst.GetComponent<AudioSource>().Play();
        Destroy(soundInst, 1.0f);
    }


    // Function สำหรับเช็คการคลิ๊กของ Zah ว่าอยู่ในเงื่อนไขรึเปล่า
    // ต้องคลิ๊กภายในเวลา CONFIG_CLICK_INTERVAL , ต้องคลิ๊กภายในระยะทาง CONFIG_CLICK_RANGE
    // ต้องไม่อยู่ในพื้นที่ของ UI - isOnUI และไม่อยู่ในขณะที่เปิด Block - isBlock
    // ต้องไม่คลิ๊กบน OddLand
    //Config
    float CONFIG_CLICK_INTERVAL = 0.5f;
    float CONFIG_CLICK_RANGE = 10.0f;
    public bool IsZahClick(Vector2 origin, float t)
    {
        if ((t < CONFIG_CLICK_INTERVAL) 
            && (Mathf.Abs(origin.x - Input.mousePosition.x) < CONFIG_CLICK_RANGE && Mathf.Abs(origin.y - Input.mousePosition.y) < CONFIG_CLICK_RANGE)
            && !(isOnUI || isBlocking)
            /*&& !isOddClick*/)
        {
            return true;
        }
        return false;
    }
    // ต่างกับ Zah ตรงที่จะไม่ตรวจสอบ isOddLandClick
    /*public bool IsOddClick(Vector2 origin, float t)
    {
        if ((t < CONFIG_CLICK_INTERVAL)
            && (Mathf.Abs(origin.x - Input.mousePosition.x) < CONFIG_CLICK_RANGE && Mathf.Abs(origin.y - Input.mousePosition.y) < CONFIG_CLICK_RANGE)
            && !(isOnUI || isBlock))
        {
            return true;
        }
        return false;
    }*/
}
