using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBar : MonoBehaviour
{
    [Header("Primary Object")]
    public GameManager gm;
    [Header("Child Link Objects")]
    public GameObject nextButton;
    public GameObject lockImage;
    public GameObject boomImage;
    [Header("SFX")]
    public GameObject unlockSFX;
    public GameObject nextSFX;

    // Key
    private float t = 0;
    // Config
    private float hideTime = 2.5f;

    private void Update()
    {
        if (gm.nextUnlock > 0)
        {
            Unlock();
        }
        else
        {
            Lock();
            if (gm.passRemain == 0)
            {
                // ถ้ายังไม่ unlock แล้ว boomImage ยังไม่เคยแสดงมาก่อน ให้เล่นเสียง unlockSFX
                if(!boomImage.activeInHierarchy)
                    unlockSFX.GetComponent<AudioSource>().PlayDelayed(2f);

                boomImage.SetActive(true);
            }
        }

        if(boomImage.activeInHierarchy)
        {
            if( t >= hideTime)
            {
                t = 0.0f;
                gm.nextUnlock = 1;
            }

            t += Time.deltaTime;
        }
    }

    // Events
    public void EventOnNext() 
    {
        nextSFX.GetComponent<AudioSource>().Play();
        gm.SceneNextStage(); 
    }

    // State of Lock - Unlock
    private void Lock()
    {
        nextButton.GetComponent<Button>().enabled = false;
        nextButton.GetComponent<Image>().color = new Color32(100,100,100,255);
        lockImage.SetActive(true);
    }
    private void Unlock()
    {
        nextButton.GetComponent<Button>().enabled = true;
        nextButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        lockImage.SetActive(false);
    }
}
