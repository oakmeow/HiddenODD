using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXCore : MonoBehaviour
{
    // Game Manager
    private GameManager gm;// { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    [Header("Effect Objects")]
    public GameObject clickFX;
    public GameObject[] soundFX;

    [Header("Animator")]
    [SerializeReference] public string stateName;
    public PolygonCollider2D startCollider;

    // Key
    Vector2 origin;
    private float t = 0.0f;
    private Vector3 position
    {
        get
        {
            return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
    [System.NonSerialized] public bool disableSound;
    [System.NonSerialized] public bool disableEffect;

    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // ถ้ามี Component<Animator> จริง ให้ตรวจสอบว่า StateName มีอยู่จริงจึงจะเล่น Animation
        if(GetComponent<Animator>())
            if (GetComponent<Animator>().HasState(0, Animator.StringToHash(stateName)))
                GetComponent<Animator>().Play(stateName, 0);

        // ถ้า startCollider ไม่ null ก็อปมาใช้
        if (startCollider)
            GetComponent<PolygonCollider2D>().points = startCollider.points;
    }

    private void Update()
    {
        if(t > 0.0f)
            t += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if (!GetComponent<FXCore>().enabled)
            return;

        // เก็บค่ากดครั้งแรก
        origin = Input.mousePosition;

        // เริ่มจับเวลาได้!!!
        t = 0.01f;
    }

    private void OnMouseUp()
    {
        if (!GetComponent<FXCore>().enabled)
            return;

        if (gm.IsZahClick(origin, t))
        {
            // เช็คว่า clickFX ไม่เท่ากับ null แล้วให้คัดลอก clickFX มาสร้าง clickInst บน effectCanvas หลังจากนั้นก็ทำลายใน 1 วิ
            if (!disableEffect && clickFX)
            {
                GameObject clickInst = Instantiate(clickFX, position, Quaternion.identity, gm.effectCanvas.transform);
                Object.Destroy(clickInst, 1.0f);
            }

            // สุ่มเล่นเสียง soundFX
            if(!disableSound && soundFX.Length > 0)
            {
                int random = Random.Range(0, soundFX.Length);
                if (soundFX[random])
                {
                    GameObject soundInst = Instantiate(soundFX[random], position, Quaternion.identity, gm.effectCanvas.transform);
                    soundInst.GetComponent<AudioSource>().PlayDelayed(0);
                    Object.Destroy(soundInst, 1.0f);
                }
            }
        }

        t = 0;
    }
}
