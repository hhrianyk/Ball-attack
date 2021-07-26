using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Transform player;
    public GameObject newBall;
    
    public float increase = 0.2f;

    [HideInInspector]
    public bool freeflight=true;
    [HideInInspector]
    public float Power = 20f;
    [HideInInspector]
    public bool end = false;
    [HideInInspector]
    public bool reduction = false;

    private Vector3 speed;
    private GameObject Nb; 
    private float radius;


    void Start()
    {
        radius = transform.localScale.x;
        
    }
     
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
           // Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
          //  Debug.Log("Did not Hit");
        }
    }

    [ContextMenu("Up")]
    void Update()
    {

        if (Input.GetKey(KeyCode.S))
        {
            transform.localScale += Vector3.one;
        }

        if(freeflight)
        transform.LookAt(new Vector3(player.position.x, player.position.y, player.position.z)); // где x, y - соответственные координаты нужного объекта

        ////////////////////////////////////////
        ///
        if (Nb != null)
        {
            Nb.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * transform.localScale.x / 2+Vector3.up* transform.localScale.x / 3;
        }

        if (freeflight)
        {
            Power = Vector3.Distance(player.position, transform.position) / 4.5f;
            speed = transform.TransformDirection(Vector3.forward) * Power;

            // Trajectory.ShowTrajectory(transform.position-transform.localScale/2, speed);
 
        }
        if (end && freeflight) 
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().AddForce(speed, ForceMode.VelocityChange);///створити прискорення 
            freeflight = false;
        }
        if (reduction)
        {
            transform.localScale -= Vector3.one / 5;
            if (transform.localScale.x < 0.3f)
                Destroy(gameObject);
        }

    }

    [ContextMenu("Rezet")]
    public void Rezet()
    {
        transform.localScale = Vector3.one;
    }
   
    private void OnMouseDown()
    {
        if (transform.localScale.x > radius * 0.2)
        {
            Nb = Instantiate(newBall, transform.position + transform.TransformDirection(Vector3.forward) * transform.localScale.x / 2, transform.rotation);
        }
        else 
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().AddForce(speed, ForceMode.VelocityChange);///створити прискорення 
            freeflight = false;
        }
    }
    private void OnMouseDrag()
    {
        if (transform.localScale.x > radius * 0.2f && freeflight)
        {
            
            Nb.transform.localScale += Vector3.one* increase;
            transform.localScale -= Vector3.one* increase;
        }
    }

    private void OnMouseUp()
    {
        if (Nb != null)
        {
            Nb.GetComponent<Rigidbody>().isKinematic = false;
            Nb.GetComponent<Rigidbody>().AddForce(speed, ForceMode.VelocityChange);///створити прискорення 
            Nb = null;
        }
 
    }

    private IEnumerator WaitForSeconds(float time)
    {
        
        yield return new WaitForSeconds(time);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().AddForce(speed, ForceMode.VelocityChange);///створити прискорення 
        freeflight = false;
    }

}
