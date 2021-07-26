using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneUp : MonoBehaviour
{

    public GameObject tower;
    public GameObject Panel;
    public GameObject Sfera;
    public BallController player;
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }
    bool isUp = false;
   
    // Update is called once per frame
    void Update()
    {

        if (isUp && transform.position.y < tower.GetComponent<MeshFilter>().sharedMesh.bounds.size.z*tower.transform.localScale.z) 
        {
            transform.position += Vector3.up / 10;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            StartCoroutine(WaitForSeconds(collision.gameObject,true));
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sphere")
        {
            if (player != null)
            {
                player.end = true;
            }
          //  StartCoroutine(WaitForSeconds(other.gameObject, false));
        }

    }

    private IEnumerator WaitForSeconds(GameObject obj,bool Up)
    {
 
        if(Up)
        {
            obj.GetComponent<BallController>().reduction=true;
            yield return new WaitForSeconds(1);
            Sfera.GetComponent<ParticleSystem>().Play();
            isUp = true;
            StartCoroutine(End());
        }
        else
        {
            yield return new WaitForSeconds(2);
            Destroy(obj);
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(12);
        Panel.SetActive(true);
    }
}
