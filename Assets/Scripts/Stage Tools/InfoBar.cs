using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour
{
    [Header("Primary Link Object")]
    public GameManager gm;
    [Header("Child Link Objects")]
    public GameObject infoIcon;
    public GameObject infoMain;
    public TextMeshProUGUI infoText;

    // Key Color
    private Color32 color_enter = new Color32(255, 255, 255, 255);
    private Color32 color_exit = new Color32(150,150,150,255);


    private bool isActive { get { return infoMain.activeInHierarchy; } }

    private void Update()
    {
        if (!isActive)
            return;

        infoText.text = gm.passRemain + GetLocalString("UI Text", "found_info");
    }



    // Event เมื่อเมาส์อยู่บน(Enter) ให้แสดง InfoBar ถ้าเมาส์ออก(Exit) ให้ปิดการแสดง
    public void EventOnEnter() 
    {
        infoIcon.GetComponent<Image>().color = color_enter;
        infoMain.SetActive(true); 
    }
    public void EventOnExit()
    {
        infoIcon.GetComponent<Image>().color = color_exit;
        infoMain.SetActive(false); 
    }

    // Event สำหรับมือถือ ถ้าเมาส์คลิ๊กให้แสดง InfoBar
    public void EventOnClick()
    {
        infoIcon.GetComponent<Image>().color = color_enter;
        infoMain.SetActive(true);
    }

    public string GetLocalString(string tableName, string keyName)
    {
        return LocalizationSettings.StringDatabase.GetLocalizedString(tableName, keyName);
    }
}
