using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deforma : MonoBehaviour
{
    Mesh mesh;
    public float minVelocity = 5f;
    public float radiusDeformate = 0.5f;
    public float multiply = 0.05f;

    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    bool free=false;
 
    void Update()
    {
 
        if (GetComponent<Rigidbody>().velocity.magnitude<transform.localScale.x && dead)
        {
            StartCoroutine(WaitForSeconds());
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "obstacles2")
        //{
        //    if (collision.relativeVelocity.magnitude > minVelocity)
        //    {
        //        bool isDeformated = false;
        //        Vector3[] verticles = mesh.vertices;

        //        for (int i = 0; i < mesh.vertexCount; i++)
        //        {
        //            for (int j = 0; j < collision.contacts.Length; j++)
        //            {
        //                Vector3 point = transform.InverseTransformPoint(collision.contacts[j].point);
        //                Vector3 veloscity = transform.InverseTransformVector(collision.relativeVelocity);
        //                float distance = Vector3.Distance(point, verticles[i]);

        //                if (distance < radiusDeformate)
        //                {
        //                    Vector3 deformate = veloscity * (radiusDeformate - distance) * multiply;
        //                    verticles[i] += deformate;
        //                    isDeformated = true;
        //                }
        //            }
        //        }

        //        if (isDeformated)
        //        {
        //            mesh.vertices = verticles;
        //            mesh.RecalculateNormals();
        //            mesh.RecalculateBounds();
        //            GetComponent<MeshCollider>().sharedMesh = mesh;
        //        }
        //    }
        //}
    }

    bool dead = false;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "obstacles")
        {
            // other.gameObject.transform.localScale += Vector3.one;
            other.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.red;
            dead = true;
        }
  
    }

    private IEnumerator WaitForSeconds()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
