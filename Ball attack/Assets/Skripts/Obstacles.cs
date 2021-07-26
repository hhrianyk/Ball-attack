using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sphere")
        {
            StartCoroutine(WaitForSeconds(Random.Range(0, 4)));

        }
    }

    void OnCollisionEnter(Collision other)
    {
 
        if (other.gameObject.tag == "Sphere")
        {
            StartCoroutine(WaitForSeconds(Random.Range(0,1)));
            
        }
    }

    private IEnumerator WaitForSeconds(float time)
    {
       // yield return new WaitForSeconds(time);
     
       // GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
