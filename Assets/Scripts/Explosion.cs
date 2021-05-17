using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float BombForce;
    public float ExplodeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float nowTime = 0f;
    private bool IsExploding = false;
    void Update()
    {
        nowTime += Time.deltaTime;
        if(nowTime >= ExplodeTime) {
            IsExploding = true;
            Destroy(gameObject.transform.parent.gameObject, 0.1f);
        }
    }

    void OnTriggerStay(Collider other) {
        if(!IsExploding)
            return;
        if(other.gameObject.GetComponent<PhysicsObject>() == null)
            return;
        Vector3 disVec = other.gameObject.GetComponent<Transform>().position
                            - gameObject.transform.parent.position;
        other.gameObject.GetComponent<Rigidbody>().AddForce(disVec * BombForce * Time.deltaTime * 20);
    }
}
