using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TalentMove))]
public class _TalentMove : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create & Update Draft Line"))
        {
            TalentMove talentMove = (TalentMove)target;
            if (!talentMove.GetComponent<LineRenderer>())
                talentMove.gameObject.AddComponent<LineRenderer>();
            LineRenderer line = talentMove.GetComponent<LineRenderer>();

            //line.tag = "EditorOnly";
            line.useWorldSpace = true;
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
            line.positionCount = talentMove.order.Length + 1;
            


            /*Vector3 origin = talentMove.transform.position;
            line.SetPosition(0, origin);
            origin += (Vector3)talentMove.order[0].distance;
            line.SetPosition(1, origin);*/


            Vector3 origin = talentMove.transform.position;
            line.SetPosition(0, origin);
            for (int i = 1; i < line.positionCount; i++)
            {
                origin += (Vector3)talentMove.order[i - 1].distance;
                line.SetPosition(i, origin);
            }



            //line.SetPosition(0, );
            //float z = talentMove.transform.position.z;
            //line.SetPosition(0, talentMove.transform.TransformPoint(talentMove.transform.position));
                /*for (int i = 1; i < line.positionCount; i++)
                {
                    Vector3 distance = new Vector3(talentMove.order[i - 1].distance.x, talentMove.order[i - 1].distance.y, 0);
                    Vector3 position = origin + distance;


                        //talentMove.transform.TransformPoint(talentMove.order[i - 1].distance.x, talentMove.order[i - 1].distance.y, z);
                    line.SetPosition(i, position);
                }*/
        }
    }
}
