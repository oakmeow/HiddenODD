using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageContent : MonoBehaviour
{
    [Header("Data Link Object")]
    public Data data;

    [Header("Child Link Objects")]
    public GameObject mask;
    public GameObject content;

    [Header("Link Object")]
    public Loading loading;

    [Header("Clone Objects")]
    public GameObject stageSelect;

    // Key
    private bool isReady { get { if (GetSnapPosition(0) == 0) return true; return false; } }
    private bool isHorizontal { get { return mask.GetComponent<ScrollRect>().horizontal; } }
    private int itemSize
    {
        get
        {
            if (isHorizontal)
                return (int)content.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            else
                return (int)content.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;

        }
    }
    private int index = -1;


    private void Start()
    {
        // สร้าง Stage Select ตามจำนวนที่มีใน Data
        int length = data.stageLength;;
        for (int i = 0; i < length; i++)
        {
            Instantiate(stageSelect, transform).name = data.StageGetTitle(i);
        }

        // จัดขนาดของ Padding
        PreparePadding();
    }

    private void Update()
    {
        if (!isReady)
            return;

        if(index > -1)
        {
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, GetSnapPosition(index));
            mask.GetComponent<ScrollRect>().StopMovement();

            index = -1;
        }

    }


    public void SetIndex(int index)
    {
        this.index = index;
    }
    // Snap Function
    private float GetSnapPosition(int index)
    {
            float pos = content.transform.GetChild(index).GetComponent<RectTransform>().localPosition.y * -1;
            return pos - (GetPaddingHead() + (itemSize / 2));
    }
    // Padding
    private float GetPaddingHead() { return (Screen.height / 2) - Mathf.Abs(mask.GetComponent<RectTransform>().offsetMax.y) - (itemSize / 2); }
    private float GetPaddingFoot() { return (Screen.height / 2) - Mathf.Abs(mask.GetComponent<RectTransform>().offsetMin.y) - (itemSize / 2); }
    private void PreparePadding()
    {
        content.GetComponent<LayoutGroup>().padding.top = (int)GetPaddingHead();
        content.GetComponent<LayoutGroup>().padding.bottom = (int)GetPaddingFoot();
    }
}
