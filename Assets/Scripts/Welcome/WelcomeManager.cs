using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class WelcomeManager : MonoBehaviour
{
    [Header("Systems Objects")]
    public StageContent stageContent;
    public GameObject settingIcon;

    public int lastStage
    {   get { return PlayerPrefs.GetInt("last_stage"); }
        set { PlayerPrefs.SetInt("last_stage", value); PlayerPrefs.Save(); }
    }

    private void Start()
    {
        // ค่าเริ่มต้นของการรันเกมครั้งแรก
        Default();

        // Prepare // เปลี่ยนภาษาจาก PlayerPrefs - last_language
        //int lastLanguage = PlayerPrefs.GetInt("last_language");
        //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lastLanguage];

        // ตั้งตำแหน่ง Setting Icon เผื่อลืม
        settingIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, -190);

        // เลื่อตำแหน่ง Scroll Stage ไปที่ PlayerPrefs : last_stage
        stageContent.SetIndex(lastStage);
    }

    public void Default()
    {
        // ถ้ายังไม่เคยสร้าง PlayerPrefs : last_stage มาก่อน ให้ใส่ค่า = 0
        if (!PlayerPrefs.HasKey("last_stage"))
            lastStage = 0;

        // กำหนดให้ด่านที่ 1 : stage1.1 ปลดล็อคตลอดกาล
        PlayerPrefs.SetInt("stage_unlock1.1", 1);




        // Settings
        //data.SwitchLanguage(1);     // Language English(en)
        //data.SaveVolume(50);         // Volume 50%
        //data.SaveBrightness(50);     // Brightness
    }
}
