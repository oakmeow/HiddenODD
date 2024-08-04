using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentClickSwitchSprite : MonoBehaviour
{
    // Game Manager
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    [Header("After End Once Effect Objects")]
    public GameObject clickFX;
    public GameObject soundFX;

    // public Config
    public enum Properties { Once, Loop, Reverse }
    public Properties properties;

    public GameObject[] selectSprite;

    public class Point { public Vector2[] points; }
    private Point[] point;
    private Sprite[] sprite;

    private Vector3 position {
        get {
            return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
    Vector2 origin;
    private int frame = 0;
    private int number = 1;
    private float t = 0.0f;

    private int count { get { return transform.childCount + 1; } }



    private Sprite GetSprite(GameObject gameObject)
    {
        if (GetComponent<SpriteRenderer>())
            return GetComponent<SpriteRenderer>().sprite;
        else
            return GetComponent<Image>().sprite;
    }

    private void Start()
    {
        point = new Point[count];
        sprite = new Sprite[count];

        point[0] = new Point();
        point[0].points = GetComponent<PolygonCollider2D>().points;

        if(GetComponent<SpriteRenderer>())
            sprite[0] = GetComponent<SpriteRenderer>().sprite;
        else if (GetComponent<Image>())
            sprite[0] = GetComponent<Image>().sprite;

        for (int i = 1; i < count; i++)
        {
            point[i] = new Point();
            point[i].points = transform.GetChild(i - 1).GetComponent<PolygonCollider2D>().points;
            sprite[i] = transform.GetChild(i - 1).GetComponent<SpriteRenderer>().sprite;
        }
    }


    private void Update()
    {
        if(t > 0)
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
            if (properties.GetHashCode() == 0)  // Once
            {
                number = 1;
                if (frame + 1 >= count)
                    number = 0;
                if (frame + 1 >= count - 1)
                {
                    //GetComponent<Entity>().disableSound = true;

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
            }
            else if (properties.GetHashCode() == 1)   // Loop
            {
                number = 1;
                if (frame + 1 >= count)
                    frame = -1;
            }
            else if (properties.GetHashCode() == 2)   // Reverse
            {
                if (frame + 1 >= count)
                    number = -1;
                else if(frame -1 < 0)
                    number = 1;
            }

            frame = frame + number;
            GetComponent<SpriteRenderer>().sprite = sprite[frame];
            GetComponent<PolygonCollider2D>().points = point[frame].points;
        }
    }
}
