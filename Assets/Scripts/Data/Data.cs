using UnityEngine;
using UnityEngine.Localization.Settings;

public class Data : MonoBehaviour
{
    // Link Objects
    public TextAsset stageAsset;
    public TextAsset oddAsset;

    // System Variable
    private int langIndex
    {
        get
        {
            string langCode = LocalizationSettings.SelectedLocale.Identifier.Code;
            int langCount = LocalizationSettings.AvailableLocales.Locales.Count;

            string[] langData = new string[langCount];

            for (int i = 0; i < langCount; i++)
                langData[i] = LocalizationSettings.AvailableLocales.Locales[i].Identifier.Code;
            int index = System.Array.IndexOf(langData, langCode); ;

            return index;
        }
    }



    /*
     * Stage Variable
     */
    private string[] stageCSV { get { return stageAsset.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None); } }
    private int stageColumn { get { return 1 + LocalizationSettings.AvailableLocales.Locales.Count; } }
    public int stageLength { get { return stageCSV.Length / stageColumn - 1; } }

    // Stage Methods
    public string StageGetID(int stageIndex) { return stageCSV[stageColumn * (stageIndex + 1)]; }
    public string StageGetTitle(int stageIndex) { return stageCSV[stageColumn * (stageIndex + 1) + 1 + langIndex]; }
    
    // Found & Total
    public int StageGetTotal(int stageIndex) { return OddCount(StageGetID(stageIndex)); }
    public int StageFoundCount(int stageIndex)
    {
        string stageID = StageGetID(stageIndex);
        int total = StageGetTotal(stageIndex);
        int sum = 0;

        for (int i = 1; i <= total; i++)
        {
            string oddID = stageID + "." + i;
            sum += PlayerPrefs.GetInt("odd_found" + oddID);
        }
        return sum;
    }
    // Unlock
    public int StageGetUnlock(int stageIndex) { return PlayerPrefs.GetInt("stage_unlock" + StageGetID(stageIndex)); }
    public void StageSetUnlock(int stageIndex, int unlock) { PlayerPrefs.SetInt("stage_unlock" + StageGetID(stageIndex), unlock); PlayerPrefs.Save(); }




    /*
     * Odd Variable
     */
    private string[] oddCSV { get { return oddAsset.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None); } }
    private int oddColumn { get { return 1 + LocalizationSettings.AvailableLocales.Locales.Count; } }
    public int oddLength { get { return oddCSV.Length / oddColumn - 1; } }
    // Odd Methods
    public string OddGetID(int oddIndex) { return oddCSV[stageColumn * (oddIndex + 1)]; }
    public int OddCount(string stageID)
    {
        int count = 0;
        string[] id = new string[3];
        for (int i = 0; i < oddLength; i++)
        {
            id = OddGetID(i).Split(new string[] { "." }, System.StringSplitOptions.None);
            string tempID = id[0] + "." + id[1];
            if (tempID.Contains(stageID))
                count++;
        }
        return count;
    }
    
    public string OddGetStory(string oddID)
    {
        int oddIndex = OddIDtoIndex(oddID);
        return oddCSV[stageColumn * (oddIndex + 1) + 1 + langIndex];
    }

    // แปลงค่า Odd จาก oddID ไปเป็น oddIndex
    public int OddIDtoIndex(string oddID)
    {
        string[] oddData = new string[oddLength];
        for (int i = 0; i < oddLength; i++)
            oddData[i] = OddGetID(i);

        return System.Array.IndexOf(oddData, oddID);
    }






    /*
     * PlayerPrefs
     */
    public static void DefaultStage(string stageID, int stageAmount, bool isUnlock)
    {
        // Reset Unlock stage
        int unlock = (isUnlock ? 1 : 0);
        PlayerPrefs.SetInt($"stage_unlock{stageID}", unlock);

        // Reset Found ODDs
        for (int i = 1; i <= stageAmount; i++)
        {
            PlayerPrefs.SetInt($"odd_found{stageID}.{i}", 0);
        }
        
        // Reset lasting
        PlayerPrefs.DeleteKey($"last_camera_zoom{stageID}");
        PlayerPrefs.DeleteKey($"last_camera_x{stageID}");
        PlayerPrefs.DeleteKey($"last_camera_y{stageID}");
        PlayerPrefs.DeleteKey($"last_target_index{stageID}");
        PlayerPrefs.DeleteKey($"last_stage{stageID}");

        PlayerPrefs.Save();
    }

    [ContextMenu("Default Stage1.1")]
    public void DefaultStage11()
    {
        PlayerPrefs.SetInt("stage_unlock1.1", 1);
        PlayerPrefs.SetInt("odd_found1.1.1", 0);
        PlayerPrefs.SetInt("odd_found1.1.2", 0);
        PlayerPrefs.SetInt("odd_found1.1.3", 0);
        PlayerPrefs.SetInt("odd_found1.1.4", 0);
        PlayerPrefs.SetInt("odd_found1.1.5", 0);

        PlayerPrefs.DeleteKey("last_camera_zoom1.1");
    }
    [ContextMenu("Default Stage1.2")]
    public void DefaultStage12()
    {
        PlayerPrefs.SetInt("stage_unlock1.2", 0);
        PlayerPrefs.SetInt("odd_found1.2.1", 0);
        PlayerPrefs.SetInt("odd_found1.2.2", 0);
        PlayerPrefs.SetInt("odd_found1.2.3", 0);
        PlayerPrefs.SetInt("odd_found1.2.4", 0);
        PlayerPrefs.SetInt("odd_found1.2.5", 0);
        PlayerPrefs.SetInt("odd_found1.2.6", 0);
        PlayerPrefs.SetInt("odd_found1.2.7", 0);
        PlayerPrefs.SetInt("odd_found1.2.8", 0);

        PlayerPrefs.DeleteKey("last_camera_zoom1.2");
    }
}
