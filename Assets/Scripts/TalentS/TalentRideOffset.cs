using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentRideOffset : MonoBehaviour
{
    private Animator animator;

    public TalentMove talentMove;
    [System.Serializable]
    public class Offset
    {
        public int orderNO;
        public string stateName;
        public Vector2 offset;
    }
    public Offset[] offset;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (offset.Length <= 0)
            return;

        int orderNO = talentMove.orderNO;

        animator.Play(offset[orderNO].stateName, 0);
        transform.localPosition = new Vector3(offset[orderNO].offset.x, offset[orderNO].offset.y, transform.localPosition.z);
    }
}
