using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCon : MonoBehaviour
{
    // Game Manager
    private GameManager gm { get { return GameObject.Find("Game Manager").GetComponent<GameManager>(); } }

    public GameObject oddTarget;    // Original Clone
    public GameObject[] oddLand;    // Odd On Land
    
    [System.Serializable]
    public class Avatar
    {
        public Image Image;
        public Animator animator;
    }
    [SerializeField]
    public Avatar[] avatar;



    private void Start()
    {
        if (oddLand.Length <= 0)
            return;

        for (int i = 0; i < oddLand.Length; i++)
        {
            // สร้าง Clone จาก OddLand มาเป็น OddTarget ภายใต้ TargetContent
            string oddID = gm.stageID + (i + 1);
            GameObject gameObject = Instantiate(oddTarget, transform);
            gameObject.name = "oddTarget" + oddID;

            // ให้ภาพ Sprite ของทั้ง OddLand และ OddTarget มี Sprite เหมือนกัน
            gameObject.GetComponent<OddTarget>().oddImage.GetComponent<Image>().sprite
                = oddLand[i].GetComponent<Image>().sprite;
        }
    }
}
