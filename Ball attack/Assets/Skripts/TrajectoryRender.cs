using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRender : MonoBehaviour
{
    private LineRenderer lineComponentRender;
    
    // Start is called before the first frame update
    void Start()
    {
        lineComponentRender = GetComponent<LineRenderer>();
    }

    
    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[50];
        lineComponentRender.positionCount = points.Length;
         
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;


            points[i] = origin + speed * time  ;

            if (points[0].y < -5) 
            {
                lineComponentRender.positionCount = i + 1;
                break;
            }
        }

        lineComponentRender.SetPositions(points);
    }
 
}
