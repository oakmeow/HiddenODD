using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentSwitchSpriteOnce : MonoBehaviour
{
    // Game Manager
    private GameManager gm;// { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    [Header("End Effect Objects")]
    public GameObject clickFX;
    public GameObject soundFX;
    [Header("Link Sprite Frame")]
    public GameObject[] spriteFrame;

    // Key
    private int frame = -1;
    private Vector3 position
    {
        get
        {
            return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }

    // Click FXCore
    Vector2 origin;
    private float t = 0.0f;



    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // ถ้ามี OddCore ให้ disable ไปก่อนพอถึงเฟรมสุดท้ายค่อยให้ OddCore ทำงาน
        if (GetComponent<OddCore>())
            GetComponent<OddCore>().enabled = false;
    }

    private void Update()
    {
        if (t > 0)
            t += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        // เก็บค่ากดครั้งแรก
        origin = Input.mousePosition;

        // เริ่มจับเวลาได้!!!
        t = 0.01f;
    }
    private void OnMouseUp()
    {
        if (gm.IsZahClick(origin, t))
        {
            if(frame < spriteFrame.Length - 1)
            {
                frame++;
                SetSprite(this.gameObject, GetSprite(spriteFrame[frame]));
                GetComponent<PolygonCollider2D>().points = spriteFrame[frame].GetComponent<PolygonCollider2D>().points;

                if(frame >= spriteFrame.Length - 1)
                {
                    if (clickFX)
                        GetComponent<FXCore>().disableEffect = true;
                    if (soundFX)
                        GetComponent<FXCore>().disableSound = true;
                    if (GetComponent<OddCore>())
                        GetComponent<OddCore>().enabled = true;
                }
            }
            else
            {
                if (clickFX)
                {
                    GameObject clickInst = Instantiate(clickFX, position, Quaternion.identity, gm.effectCanvas.transform);
                    Object.Destroy(clickInst, 1.0f);
                }

                if (soundFX)
                {
                    GameObject soundInst = Instantiate(soundFX, position, Quaternion.identity, gm.effectCanvas.transform);
                    soundInst.GetComponent<AudioSource>().PlayDelayed(0);
                    Object.Destroy(soundInst, 1.0f);
                }
            }

            // Reset
            t = 0;
        }
    }


    // Function Get/Set Sprite for unknow SpriteRenderer or Image
    private Sprite GetSprite(GameObject gameObject)
    {
        if (gameObject.GetComponent<SpriteRenderer>())
            return gameObject.GetComponent<SpriteRenderer>().sprite;
        else
            return gameObject.GetComponent<Image>().sprite;
    }
    private void SetSprite(GameObject targetObject, Sprite sprite)
    {
        if (targetObject.GetComponent<SpriteRenderer>())
            targetObject.GetComponent<SpriteRenderer>().sprite = sprite;
        else
            targetObject.GetComponent<Image>().sprite = sprite;
    }
}
