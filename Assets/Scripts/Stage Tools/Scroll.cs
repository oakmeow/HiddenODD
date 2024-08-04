using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    [Header("Primary Link Object")]
    public GameManager gm;
    [Header("Child Link Objects")]
    public GameObject mask;
    public GameObject content;
    public GameObject oddTarget;

    //[System.NonSerialized]
    public bool isOn;

    // Key
    private bool isHorizontal { get { return mask.GetComponent<ScrollRect>().horizontal; } }
    private bool isReady { get{ if (GetSnapPosition(0) == 0) return true; return false; } }
    private float anchPos
    {
        get {
            if (isHorizontal) return content.GetComponent<RectTransform>().anchoredPosition.x;
            else return content.GetComponent<RectTransform>().anchoredPosition.y;
        }
    }
    private int itemSize
    {
        get
        {
            if (isHorizontal)
            {
                return (int)oddTarget.GetComponent<RectTransform>().sizeDelta.x;
                //return (int)content.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            }
            else
                return (int)content.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;

        }
    }
    private int itemCount { get{ return content.transform.childCount; } }
    private int index;
    private bool moving = false;
    private float x, y;
    private float fromA, gotoB;

    // Config
    private bool snap = true;
    private float speed = 0.1f;
    private float stop = 3.0f;

    [System.NonSerialized]
    public bool showStory = false;

    private void Start()
    {
        PreparePadding();
    }

    private void Update()
    {
        if (!isReady)
            return;

        Moving();
    }



    public void SetIndex(int index)
    {
        this.index = index;
        moving = true;

        // เซฟค่า TargetBar ล่าสุด
        PlayerPrefs.SetInt("last_target_index" + gm.stageID, index);
        PlayerPrefs.Save();
    }
    public void SetIndexStory(int index)
    {
        showStory = true;
        SetIndex(index);
    }

    

    private void Moving()
    {
        // Don't move if index < 0
        if (!isReady || !moving)
            return;

        gotoB = GetSnapPosition(index);

        // Moving
        fromA = anchPos;
        y = Mathf.Lerp(fromA, gotoB, speed);
        x = 0;
        if (isHorizontal)
        {
            x = Mathf.Lerp(fromA, gotoB, speed);
            y = 0;
        }
        content.GetComponent<RectTransform>().anchoredPosition = new Vector2(x , y);

        // Break
        if (Distance(fromA, gotoB) <= stop)
        {
            if (isHorizontal)
                content.GetComponent<RectTransform>().anchoredPosition = new Vector2(gotoB, 0f);
            else
                content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, gotoB);

            mask.GetComponent<ScrollRect>().StopMovement();
            moving = false;

            // Story
            if (showStory)
                gm.storyBar.SetStory(index);

            showStory = false;
        }
    }


    // Snap Function
    private float GetSnapPosition(int index)
    {
        if (content.transform.childCount <= 0)
            return 0;

        if (index >= content.transform.childCount)
            index = 0;

        float pos;
        if (isHorizontal)
        {
            pos = content.transform.GetChild(index).GetComponent<RectTransform>().localPosition.x * -1;
            return pos + (GetPaddingHead() + (itemSize / 2));
        }
        else
        {
            pos = content.transform.GetChild(index).GetComponent<RectTransform>().localPosition.y * -1;
            return pos -(GetPaddingHead() + (itemSize / 2));
        }
    }
    public void EventDragEnd()
    {
        if (!snap) return;

        float[] distance = new float[itemCount];
        for (int i = 0; i < itemCount; i++)
            distance[i] = Distance(anchPos, GetSnapPosition(i));

        int snapIndex = System.Array.IndexOf(distance, Mathf.Min(distance));

        mask.GetComponent<ScrollRect>().StopMovement();
        moving = false;

        SetIndexStory(snapIndex);
    }

    // Padding
    private float GetPaddingHead()
    {
        float paddingHead;

        paddingHead = (Screen.height / 2) - Mathf.Abs(mask.GetComponent<RectTransform>().offsetMax.y) - (itemSize / 2);
        if (isHorizontal)
            paddingHead = (Screen.width / 2) - Mathf.Abs(mask.GetComponent<RectTransform>().offsetMax.x) - (itemSize / 2);

        return paddingHead;
    }
    private float GetPaddingFoot()
    {
        float paddingFoot;

        paddingFoot = (Screen.height / 2) - Mathf.Abs(mask.GetComponent<RectTransform>().offsetMin.y) - (itemSize / 2);
        if (isHorizontal)
            paddingFoot = (Screen.width / 2) - Mathf.Abs(mask.GetComponent<RectTransform>().offsetMin.x) - (itemSize / 2);

        return paddingFoot;
    }
    private void PreparePadding()
    {
        if (isHorizontal)
        {
            content.GetComponent<LayoutGroup>().padding.left = (int)GetPaddingHead();
            content.GetComponent<LayoutGroup>().padding.right = (int)GetPaddingFoot();
        }
        else
        {
            content.GetComponent<LayoutGroup>().padding.top = (int)GetPaddingHead();
            content.GetComponent<LayoutGroup>().padding.bottom = (int)GetPaddingFoot();
        }
    }

    // Math Function
    private float Distance(float fromA, float gotoB) { return Mathf.Abs(fromA - gotoB); }

    // Event เช็คว่าเมาส์อยู่บน Object นี้หรือไม่
    public void EventOnEnter() { isOn = true; }
    public void EventOnExit() { isOn = false; }
}
