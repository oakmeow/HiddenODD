using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bingo : MonoBehaviour
{
    // Child Link
    public GameObject correct;
    // Sound SFX
    public GameObject BingoSFX1;
    public GameObject BingoSFX2;
    // Key
    private float t = 0.0f;
    // Config
    private float CONFIG_CORRECT_SHOW = 1.4f;
    private float CONFIG_BINGO_HIDE = 7.0f;
    //private float CONFIG_COMPLETE_SHOW = 3.0f;

    private void Awake()
    {
        BingoSFX1 = Instantiate(BingoSFX1, transform);
        BingoSFX2 = Instantiate(BingoSFX2, transform);
    }

    private void OnEnable()
    {
        if(BingoSFX1.activeInHierarchy)
          BingoSFX1.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        // ปรับขนาดของ Bingo ตามการ zoom ของกล้อง
        float size = Camera.main.orthographicSize;
        float cal = 100 - ((size - 10) * 5);
        GetComponent<Image>().pixelsPerUnitMultiplier = cal;

        // แสดง Correct ตามเวลาที่ตั้ง CONFIG_CORRECT_SHOW
        if (t >= CONFIG_CORRECT_SHOW)
        {
            if (!correct.activeInHierarchy)
            {
                correct.SetActive(true);
                BingoSFX2.GetComponent<AudioSource>().Play();
            }
        }

        // ตั้งเวลาให้ Bingo ปิดตามเวลาของ CONFIG_BINGO_HIDE
        if (t >= CONFIG_BINGO_HIDE)
        {
            correct.SetActive(false);
            this.gameObject.SetActive(false);
            t = 0.0f;
        }

        t += Time.deltaTime;
    }
}
