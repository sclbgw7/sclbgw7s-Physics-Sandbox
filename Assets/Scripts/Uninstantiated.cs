using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uninstantiated : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer MR;
    //private Material TransparentGrey;
    //private Material TransparentRed;
    void Start()
    {
        MR = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    public int TriggerStayNum = 0;
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.isTrigger == false)
            ++TriggerStayNum;
        Debug.Log(TriggerStayNum);
    }

    void OnTriggerExit(Collider other) {
        if(other.isTrigger == false)
            --TriggerStayNum;
    }
}
