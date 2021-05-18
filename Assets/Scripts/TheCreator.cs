using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCreator : MonoBehaviour
{
    public GameObject[] Sample;
    public Material TransparentGrey;
    public Material TransparentRed;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer MR;
        Collider CL;
        for(int i = 0; i < Sample.Length; i++) {
            MR = Sample[i].GetComponent<MeshRenderer>();
            CL = Sample[i].GetComponent<Collider>();
            MR.material = TransparentGrey;
            CL.isTrigger = true;
        }
    }

    // Update is called once per frame
    private int currentObject = 0;
    void Update()
    {
        if(GeneralController.CurrentMode != GameMode.Create) {
            if(Sample[currentObject].activeInHierarchy)
                Sample[currentObject].SetActive(false);
            return;
        }
        //select object
        float scrollVal = Input.GetAxis("Mouse ScrollWheel");
        if(scrollVal != 0) {
            Sample[currentObject].SetActive(false);
            if(scrollVal > 0) 
                ++currentObject;
            else 
                currentObject += Sample.Length - 1;
            currentObject %= Sample.Length;
        }
        Sample[currentObject].SetActive(true);
        //detect
        bool isOnTrigger = detectCollide();
        if(isOnTrigger) {
            Sample[currentObject].GetComponent<MeshRenderer>().material = TransparentRed;
            return;
        }
        else {
            Sample[currentObject].GetComponent<MeshRenderer>().material = TransparentGrey;
        }
        //instantiate
        if(Input.GetMouseButtonDown(0)) {
            GameObject newObject = Instantiate(Sample[currentObject], Sample[currentObject].transform.position, Sample[currentObject].transform.rotation);
            newObject.GetComponent<MeshRenderer>().material = newObject.GetComponent<PhysicsObject>().DefaultMaterial;
            newObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    private bool detectCollide() {
        Collider[] others;
        BoxCollider BCL;
        SphereCollider SCL;
        CapsuleCollider CCL;
        switch (currentObject)
        {
            case 0:
            case 4:
                SCL = Sample[currentObject].GetComponent<SphereCollider>();
                others = Physics.OverlapSphere(SCL.transform.position, SCL.radius * SCL.transform.localScale.x);
                break;
            case 1:
            case 2:
                BCL = Sample[currentObject].GetComponent<BoxCollider>();
                others = Physics.OverlapBox(BCL.transform.position, BCL.transform.localScale / 2, BCL.transform.rotation);
                break;
            case 3:// TO BE FIXED!!!
                CCL = Sample[currentObject].GetComponent<CapsuleCollider>();
                float offset = CCL.height * 0.5f;
                float radius = CCL.radius;
                float scale = CCL.transform.localScale.x;
                Vector3 pointTop = CCL.transform.position + CCL.transform.up * (radius - offset) * scale;
                Vector3 pointBottom = CCL.transform.position * 2 - pointTop;
    
                others = Physics.OverlapCapsule(pointTop, pointBottom, radius * scale);
                break;
            default:
                others = null;
                break;
        }
        /*foreach(Collider other in others) {
            Debug.Log(other.isTrigger);
            if(other.isTrigger == false)
                return true;
        }*/
        if(others.Length != 0)
            return true;
        return false;
    }
}
