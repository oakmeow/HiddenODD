using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentClickPlayStop : MonoBehaviour
{
    // Game Manager
    private GameManager gm;
    private Animator anim;

    [SerializeReference]
    public string stageName;
    [SerializeReference]
    public float speed;
    [SerializeReference]
    public bool backToFirstFrame;

    // Click FXCore
    Vector2 origin;
    private float t = 0.0f;

    private float randomSpeed { get { return Random.Range(speed - 0.1f, speed + 0.1f); } }


    private void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    private void Update()
    {
        if (t > 0)
            t += Time.deltaTime;

        if (anim.speed > 0)
        {
            float _speed = anim.speed - (Time.deltaTime * randomSpeed);
            if (_speed <= 0)
            {
                _speed = 0;

                if(backToFirstFrame)
                    anim.Play(stageName, 0, 0);
            }

            anim.speed = _speed;
        }
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
            anim.speed = 2;
        }
    }
}
