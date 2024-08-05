using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    
    [Header("Link Child Objects")]
    public GameObject button;
    public GameObject lockImage;
    public GameObject clearImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI quantityText;
    [Header("SFX")]
    public GameObject selectSFX;
    public GameObject disableSFX;
    [Header("Keys")]
    public int stageIndex;
    public string stageID;
    public string title;
    public int found;
    public int total;
    public int unlock;

    // Data
    private Data data;

    private void Start()
    {
        // ดึงคอมโพเน้น Data จาก Parent(StageContent) มาใส่ในตัวแปร data
        data = transform.parent.GetComponent<StageContent>().data;

        // ใส่ค่าตัวแปร
        stageIndex = transform.GetSiblingIndex();
        stageID = data.StageGetID(stageIndex);
    }

    private void Update()
    {
        // ใส่ค่าตัวแปร
        title = data.StageGetTitle(stageIndex);
        total = data.StageGetTotal(stageIndex);
        found = data.StageFoundCount(stageIndex);
        unlock = data.StageGetUnlock(stageIndex);

        // ใส่ค่า Text UI
        titleText.text = title;
        quantityText.text = found + "/" + total;

        // สถานะของแต่ละด่านว่า Unlock , Lock หรือ Complete
        if (unlock == 1)
        {
            if (found == total)
                StageComplete();
            else
                StageUnlock();
        }
        else
            StageLock();
    }


    // Events Click Functions
    public void EventOnClickStage()
    {
        // ถ้ายัง lock อยู่ให้เล่นเสียง disableSFX แล้ว return ออกเลย
        if (unlock == 0)
        {
            disableSFX.GetComponent<AudioSource>().PlayDelayed(0);
            return;
        }



        // ถ้า unlock แล้วให้เล่นเสียง selectSFX แล้วโหลดเปลี่ยนฉาก
        selectSFX.GetComponent<AudioSource>().PlayDelayed(0);
        // ดึงคอมโพเน้น Loading จาก Parent(StageContent) มาใส่ในตัวแปร loading
        Loading loading = transform.parent.GetComponent<StageContent>().loading;
        loading.LoadScene(stageIndex + 1);  // index ของ Scene จะมากกว่าของ stageIndex เลยต้อง +1
    }

    // ฟังก์ชั่นเปลี่ยนสีปุ่มและ disable ปุ่ม ตามสถานะของ Unlock
    private void StageLock()
    {
        lockImage.SetActive(true);
        clearImage.SetActive(false);

        button.GetComponent<Image>().color = new Color(255, 0, 0);
        //button.GetComponent<Button>().enabled = false;
    }
    private void StageUnlock()
    {
        lockImage.SetActive(false);
        clearImage.SetActive(false);

        button.GetComponent<Image>().color = new Color(255, 255, 255);
        //button.GetComponent<Button>().enabled = true;
    }
    private void StageComplete()
    {
        lockImage.SetActive(false);
        clearImage.SetActive(true);

        button.GetComponent<Image>().color = new Color(0, 255, 0);
        //button.GetComponent<Button>().enabled = true;
    }
}
