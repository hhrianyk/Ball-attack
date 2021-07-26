using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField]
    public GameObject Ball;
    public TrajectoryRender Trajectory;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Ball.transform.position;
        transform.position -= Vector3.up * (transform.position.y - 6);
        GetComponent<LineRenderer>().startWidth = Ball.transform.localScale.x;

        if (Physics.Raycast(transform.position + Vector3.up * 10, Vector3.down* 10, out var hit, 200,
          LayerMask.GetMask("Ground")))
        {
           
            transform.position = hit.point - hit.normal * .1f;

            //Vector3 normal = hit.normal;

            //Vector3 fwd = Vector3.Cross(normal, Vector3.right).normalized;
            //fwd = Quaternion.AngleAxis(Random.Range(0, 360), normal) * fwd;
             transform.rotation = Ball.transform.rotation;
        }

       

  
    }
    Vector3 speed;
     
    void FixedUpdate()
    {
       
        //d  transform.position = new Vector3(0, Ball.transform.position.y);
        if (Ball.GetComponent<BallController>().freeflight)
        {
            GetComponent<LineRenderer>().startWidth = Ball.transform.localScale.x;
            transform.rotation = Ball.transform.rotation;
            speed = transform.TransformDirection(Vector3.forward) * Ball.GetComponent<BallController>().Power;
        }
        Trajectory.ShowTrajectory(transform.position - transform.localScale / 2, speed);
    }
}
