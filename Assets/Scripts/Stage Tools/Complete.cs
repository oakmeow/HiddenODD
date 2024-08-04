using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Complete : MonoBehaviour
{
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    [Header("Child Link Objects")]
    public GameObject completeMain;
    public TextMeshProUGUI stageTitle;

    [Header("SFX")]
    public GameObject completeSFX;
    public GameObject homeSFX;
    public GameObject nextSFX;
    public GameObject staySFX;

    // Key
    private float t = 0.0f;
    // Config
    private float delayTime = 3f;      // ตั้งค่าแสดง CompleteMenu หลังจากกี่วินาที


    private void Update()
    {
        if (t > 0)
        {
            t += Time.deltaTime;

            // ถ้าเวลามากกว่า Delay ให้แสดง CompleteMain
            if (t >= delayTime)
            {
                // ถ้า completeMain ยังไม่เคยแสดงมาก่อน ให้เล่นเสียง
                if (!completeMain.activeInHierarchy)
                {
                    completeSFX.GetComponent<AudioSource>().Play();
                    completeMain.SetActive(true);
                    gm.AudioPause();

                    t = 0.0f;
                }
            }
            
        }

        if (completeMain.activeInHierarchy)
        {
            
            stageTitle.text = gm.stageTitle;
        }
    }

    // สำหรับรัน Complete
    public void SetComplete() { t = 0.01f; }

    // Event Click
    public void EventOnMainMenu(){ homeSFX.GetComponent<AudioSource>().Play(); gm.SceneWelcome(); }
    public void EventOnNextStage() { nextSFX.GetComponent<AudioSource>().Play(); gm.SceneNextStage(); }
    public void EventOnStayHere()
    { 
        staySFX.GetComponent<AudioSource>().Play();
        gm.AudioPlay(0);
        completeMain.SetActive(false);
    }
}
