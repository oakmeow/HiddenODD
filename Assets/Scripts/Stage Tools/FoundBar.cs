using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class FoundBar : MonoBehaviour
{
    [Header("Primary Objects")]
    public GameManager gm;
    [Header("Link Child Objects")]
    public TextMeshProUGUI foundText;


    public void Update()
    {
        // Update Found Bar
        foundText.text = gm.foundCount + "/" + gm.stageTotal;
    }
}
