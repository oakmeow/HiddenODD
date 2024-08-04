using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[ExecuteInEditMode]
//[ExecuteAlways]
public class TalentTest : MonoBehaviour
{
    private Animator animator;

    public bool once;
    [Tooltip("Skip frame 1 after round 2")]
    public bool skip1;
    public float accelator;

    public string prefixStateName;
    public bool colliderAsStateName;

    [System.Serializable]
    public class Order
    {
        public enum Action { Idle, Walk }
        public Action action;
        public string stateName;
        public float waitTime;
        public float speed;
        public Vector2 distance;
        public PolygonCollider2D colliderState;
    }
    public Order[] order;

    private string chat;
    private bool isMale;
    public string Chat { 
        get
        {
            var title = isMale ? "Mr." : "Ms.";
            //return $"{title}{chat}";
            return string.Concat(title, chat);
        }
    }

    //private static int DIRECT_HASH = Animator.StringToHash("DIRECT");

    private void Start()
    {
        String[] arr2 = { "Mahesh Chand ", "Chris Love ", "Dave McCarter ", "Praveen Kumar " };
        String seperator2 = ", ";
        string result2 = "First Author, ";
        result2 += String.Join(seperator2, arr2, 1, 2);
        //Console.WriteLine($"Result: {result2}");
        //Debug.Log($"Result: {result2}");
        Debug.Log(String.Format("{0} , {1} , {2} , {3}", arr2[3], arr2[2], arr2[1], arr2[0]));

        /*isMale = false;
        chat = "Palit";
        Debug.Log(Chat);*/

        //int count = line.positionCount;

        //Debug.Log(count + "Count = " + count);
        /*for (int i = 0; i < count; i++)
        {
            //Debug.Log("Local = " + line.GetPosition(i) + " , World = " + transform.TransformPoint(line.GetPosition(i)));
        }*/


        //line.positionCount += 1;
        //a = line.positionCount;
        //Debug.Log("start = " + a);
        //line.SetPosition(5, new Vector3(10,0,0));

        //Action taa = () => { Debug.Log($"zzz = test"); };
        //taa();
        //Debug.Log($"Where b = " + n);

        /*int a = 10;
        int b = a;
        b = 20;
        Debug.Log($"a = {a} , b = {b}");

        string s = "Start";
        string t = s;
        t = "End";
        Debug.Log($"s = {s} , t = {t}");
        
        GameManager z = GetComponent<GameManager>();
        Debug.Log(z.GetType());

        chat = "First";
        Chat = "Secend";
        Debug.Log($"chat = {chat} , Chat = {Chat}");*/
    }

    private void OnValidate()
    {
        //Debug.Log("Valid = " + a);
        //line.positionCount = a;
    }

    
    private void Update()
    {
        
        //a++;

        //line.positionCount = 10;



        //GetComponent<Animator>().Play("Scout_1_Reading",0);
        //Debug.Log(GetComponent<Animator>().HasState(0, Animator.StringToHash("Scout_1_Reading")));
        //Debug.Log(GetComponent<Animator>().HasState(0, Animator.StringToHash("Scout_2_Reading")));


        //int direct = GetComponent<Animator>().GetInteger(DIRECT_HASH) + 1;
        //if (direct)
        //Debug.Log(    GetComponent<Animator>().runtimeAnimatorController.animationClips.Length     );

        /*if (GetComponent<Animator>().GetBool("isUp"))
            GetComponent<Animator>().SetBool("isUp", false);
        else
            GetComponent<Animator>().SetBool("isUp", true);*/
        String[] s = { "sass", "abcd", "efg" };
        
    }

    //public GameObject obj;
    //public Vector3 spawnPoint;


    /*public void BuildObject()
    {
        Instantiate(obj, spawnPoint, Quaternion.identity);
    }*/



    public class Ttt<T>
    {
        
        
    }

}

