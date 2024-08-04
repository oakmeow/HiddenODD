using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetContent : MonoBehaviour
{
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }
    //[Header("Child Link Objects")]
    //public GameObject landOfOdd;
    [Header("Clone Link Objects")]
    public GameObject oddTarget;

    [System.Serializable]
    public class Avatar
    {
        public GameObject targetImage;
        public Animator animator;
        public Vector2 scale;
    }
    public Avatar[] avatar;

    private void Start()
    {
        for (int i = 0; i < avatar.Length; i++)
        {
            // สร้าง Clone จาก OddLand มาเป็น OddTarget ภายใต้ TargetContent
            string oddID = gm.stageID + (i + 1);
            GameObject gameObject = Instantiate(oddTarget, transform);
            gameObject.name = "oddTarget" + oddID;

            // ให้ภาพ Sprite ของทั้ง OddLand และ OddTarget มี Sprite เหมือนกัน
            gameObject.GetComponent<OddTarget>().oddImage.GetComponent<Image>().sprite = avatar[i].targetImage.GetComponent<Image>().sprite;

            // ปรับ Size ให้เท่ากัน
            if(avatar[i].scale.x > 0 && avatar[i].scale.y > 0)
            {
                gameObject.GetComponent<OddTarget>().oddImage.transform.localScale = avatar[i].scale;
            }
            else
            {
                gameObject.GetComponent<OddTarget>().oddImage.transform.localScale = avatar[i].targetImage.transform.localScale;
            }

            // ถ้า OddLand มีการเปิดให้ Animator ก็ให้ OddTarget เปิดด้วยเช่นกัน
            if (avatar[i].animator)
            {
                gameObject.GetComponent<OddTarget>().oddImage.GetComponent<Animator>().enabled = true;

                gameObject.GetComponent<OddTarget>().oddImage.GetComponent<Animator>().runtimeAnimatorController
                    = avatar[i].animator.runtimeAnimatorController;
            }
        }


    }


    /*public void Prepare()
    {
        int count = landOfOdd.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            // สร้าง Clone จาก OddLand มาเป็น OddTarget ภายใต้ TargetContent
            string oddID = gm.stageID + (i + 1);
            GameObject gameObject = Instantiate(oddTarget, transform);
            gameObject.name = "oddTarget" + oddID;

            // ให้ภาพ Sprite ของทั้ง OddLand และ OddTarget มี Sprite เหมือนกัน
            gameObject.GetComponent<OddTarget>().oddImage.GetComponent<Image>().sprite 
                = landOfOdd.transform.GetChild(i).GetComponent<Image>().sprite;
            // ปรับ Size ให้เท่ากัน
            gameObject.transform.localScale = landOfOdd.transform.GetChild(i).transform.localScale;

            // ถ้า OddLand มีการเปิดให้ Animator ก็ให้ OddTarget เปิดด้วยเช่นกัน
            if (landOfOdd.transform.GetChild(i).GetComponent<Animator>())
            {
                bool enable = landOfOdd.transform.GetChild(i).GetComponent<Animator>().enabled;
                if (enable)
                {
                    gameObject.GetComponent<OddTarget>().oddImage.GetComponent<Animator>().enabled = enable;

                    gameObject.GetComponent<OddTarget>().oddImage.GetComponent<Animator>().runtimeAnimatorController
                        = landOfOdd.transform.GetChild(i).GetComponent<Animator>().runtimeAnimatorController;
                }
            }
        }
    }*/



    
}
