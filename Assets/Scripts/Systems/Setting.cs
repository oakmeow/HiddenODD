using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using TMPro;

public class Setting : MonoBehaviour
{
    [Header("Link Objects")]
    public GameObject loading;

    [Header("Child Link Objects")]
    public GameObject settingMenu;
    public GameObject resetOnlyButton;
    public GameObject resetAllButton;
    public GameObject mainMenuButton;

    [Header("Warning Link Objects")]
    public GameObject warningMenu;
    public TextMeshProUGUI warningText;

    [Header("Volume & Brightness")]
    public Slider volumn;
    public Slider brightness;
    public GameObject brightnessScreen;

    [Header("SFX")]
    public GameObject enterSFX;
    public GameObject backSFX;
    public GameObject homeSFX;
    public GameObject switchSFX;
    public GameObject slideSFX;
    public GameObject warnSFX;
    public GameObject confirmSFX;

    private int sceneIndex { get { return SceneManager.GetActiveScene().buildIndex; } }

    // Language Data Array
    private string[] languageData
    {
        get
        {
            int count = LocalizationSettings.AvailableLocales.Locales.Count;
            string[] data = new string[count];

            for (int i = 0; i < count; i++)
            {
                data[i] = LocalizationSettings.AvailableLocales.Locales[i].Identifier.Code;
            }
            return data;
        }
    }

    // Get PlayerPrefs
    public int lastVolume
    {   get { return PlayerPrefs.GetInt("last_volume"); }
        set { PlayerPrefs.SetInt("last_volume", value);
              PlayerPrefs.Save();
        }
    }
    public int lastBrightness
    {   get { return PlayerPrefs.GetInt("last_brightness"); }
        set { PlayerPrefs.SetInt("last_brightness", value);
              PlayerPrefs.Save();
        }
    }
    public int lastLanguage
    {
        get { return PlayerPrefs.GetInt("last_language"); }
        set
        {
            PlayerPrefs.SetInt("last_language", value);
            PlayerPrefs.Save();
        }
    }

    // Time
    private float t = 0.0f;
    private float delayTime = 1f; // ตั้งค่า Delay ให้เปลี่ยนฉากตอน reset

    private void HashPrefs()
    {
        // ถ้ายังไม่เคย PlayerPrefs last_volume มาก่อน
        if (!PlayerPrefs.HasKey("last_volume"))
            lastVolume = 10;

        // ถ้ายังไม่เคย PlayerPrefs last_brightness มาก่อน
        if ((!PlayerPrefs.HasKey("last_brightness")))
            lastBrightness = 10;

        // ถ้ายังไม่เคย PlayerPrefs last_language มาก่อน
        if (!PlayerPrefs.HasKey("last_language"))
            lastLanguage = 1;

    }

    private void Start()
    {
        // เช็ค Hash ถ้าไม่เคยให้กำหนดค่า Default
        HashPrefs();

        // คืนค่า last_language จาก PlayerPrefs
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lastLanguage];

        // ดึงค่าเริ่มต้นของ Volume กับ Brightness โดยดึงค่าจาก PlayerPrefs ถ้ายังไม่เคยตั้งค่ามาก่อนให้ volume กับ Brightness เป็น 10 default
        volumn.value = lastVolume;
        brightness.value = lastBrightness;

        // เงื่อนไขการแสดงปุ่ม Reset ถ้าเป็น WelcomeScene ให้แสดงปุ่ม Reset All ถ้าเป็นอื่นๆแสดงปุ่ม Reset Only
        if (sceneIndex == 0)
        {
            resetOnlyButton.SetActive(false);
            resetAllButton.SetActive(true);
            mainMenuButton.SetActive(false);
        }
        else
        {
            resetOnlyButton.SetActive(false);   // false ยกเลิกปุ่ม Reset Only this Land
            resetAllButton.SetActive(true);     // true ให้โชว์แต่ Reset all land
            mainMenuButton.SetActive(true);
        }
    }

    private void Update()
    {
        // Delay ตอนเปลี่ยนฉากของ Reset
        if (t > 0)
        {
            t += Time.deltaTime;
            if(t > delayTime)
            {
                loading.GetComponent<Loading>().LoadScene(sceneIndex);
                t = 0.0f;
            }
        }
    }


    // Event Volume & Brightness
    public void SetVolume(int value) { AudioListener.volume = value * 0.1f; }
    public void EventOnVolume() 
    {
        SetVolume((int)volumn.value);

        // ต้องให้ SettingMenu Active ถึงจะมีเสียง
        if (settingMenu.activeInHierarchy)
            slideSFX.GetComponent<AudioSource>().Play();

        lastVolume = (int)volumn.value;
    }
    public void SetBrightness(int value)
    {
        int alpha = 200 + (value * -20);
        brightnessScreen.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)alpha);
    }
    public void EventOnBrightness()
    {
        SetBrightness((int)brightness.value);

        // ต้องให้ SettingMenu Active ถึงจะมีเสียง
        if (settingMenu.activeInHierarchy)
            slideSFX.GetComponent<AudioSource>().Play();

        lastBrightness = (int)brightness.value;
    }
    


    // Event Click
    public void EventOnResetOnly()
    {
        warningMenu.SetActive(true);
        warningText.text = GetLocalString("UI Text", "ui_warn_reset_only");
        warnSFX.GetComponent<AudioSource>().Play();
    }
    public void EventOnResetAll()
    {
        warningMenu.SetActive(true);
        warningText.text = GetLocalString("UI Text", "ui_warn_reset_all");
        warnSFX.GetComponent<AudioSource>().Play();
    }
    public void EventOnOpen()
    {
        enterSFX.GetComponent<AudioSource>().Play();
        settingMenu.SetActive(true);
    }
    public void EventOnBack() 
    {
        backSFX.GetComponent<AudioSource>().Play();
        settingMenu.SetActive(false);
    }
    public void EventOnHome()
    {
        homeSFX.GetComponent<AudioSource>().Play();

        loading.GetComponent<Loading>().LoadScene(0);
        settingMenu.SetActive(false);
    }
    

    // Warning Event Click
    public void EventOnWarningBack()
    {
        warningMenu.SetActive(false);
        backSFX.GetComponent<AudioSource>().Play();
    }
    public void EventOnWarningConfirm()
    {
        confirmSFX.GetComponent<AudioSource>().Play();

        warningMenu.SetActive(false);
        settingMenu.SetActive(false);

        t = 0.01f;

        // Reset All Lands
        Data.DefaultStage("1.1", 5, true);
        Data.DefaultStage("1.2", 8, false);

        //loading.GetComponent<Loading>().Show();
        loading.GetComponent<Loading>().LoadScene(0);
    }


    // เปลี่ยนภาษา
    public void EventOnSwitchLanguage()    // เปลี่ยนภาษา +1
    {
        // นับจำนวนของ Language
        int count = LocalizationSettings.AvailableLocales.Locales.Count;
        // แปลง Code ของภาษาปัจจุบันเป็น index
        int index = System.Array.IndexOf(languageData, LocalizationSettings.SelectedLocale.Identifier.Code);

        // ถ้าเพิ่มค่า index แล้วมากกว่าหรือเท่ากับ count แสดงว่าเกินให้ index เริ่มที่ 0 ใหม่
        index++;
        if (index >= count)
            index = 0;

        // เปลี่ยนภาษาตามค่า index
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        // เซฟภาษาที่ last_language = index
        lastLanguage = index;

        // เล่นเสียง
        switchSFX.GetComponent<AudioSource>().Play();
    }
    public string GetLocalString(string tableName, string keyName)
    {
        return LocalizationSettings.StringDatabase.GetLocalizedString(tableName, keyName);
    }
}
