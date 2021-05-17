using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    public float ForceMass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other) {
        if(other.gameObject.GetComponent<PhysicsObject>() == null)
            return;
        Vector3 disVec = other.gameObject.GetComponent<Transform>().position
                            - gameObject.transform.parent.position;
        other.gameObject.GetComponent<Rigidbody>().AddForce(disVec * ForceMass * Time.deltaTime);
        gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(-disVec * ForceMass * Time.deltaTime);
    }
}
