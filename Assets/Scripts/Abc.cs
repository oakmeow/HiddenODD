using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abc : MonoBehaviour
{

    //private bool click = false;


    private void Update()
    {
        //GetComponent<Animator>().Play("Buoy_1", 0, 0.40f);
    }


    private void OnMouseDrag()
    {
        GetComponent<Animator>().Play("Buoy_1", 0, 0.50f);
    }

    private void OnMouseDown()
    {
        //click = true;
    }
    public void OnEventClick()
    {
        
        
    }
}
