using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCreator : MonoBehaviour
{
    public GameObject[] Sample;
    public GameObject LandPoint,LandRay;
    public Material TransparentGrey;
    public Material TransparentRed;
    public Transform CameraTrans;
    private Transform LPTrans,LRTrans;
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
        LPTrans = LandPoint.GetComponent<Transform>();
        LRTrans = LandRay.GetComponent<Transform>();
    }

    // Update is called once per frame
    private int currentObject = 0;
    private float distance = 5f;
    void Update()
    {
        if(CameraTrans == null)
            CameraTrans = Constants.CameraTrans;
        if(GeneralController.CurrentMode != GameMode.Create) {
            distance = 5f;
            if(Sample[currentObject].activeInHierarchy)
                Sample[currentObject].SetActive(false);
            if(LandPoint.activeInHierarchy)
                LandPoint.SetActive(false);
            if(LandRay.activeInHierarchy)
                LandRay.SetActive(false);
            return;
        }
        //get position
        if(Input.GetKey(KeyCode.UpArrow))
            distance += Time.unscaledTime * 0.001f;
        if(Input.GetKey(KeyCode.DownArrow))
            distance -= Time.unscaledTime * 0.001f;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objectPosition;
        //draw ray
        LandPoint.SetActive(true);
        LandRay.SetActive(true);
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, new Vector3(0,-1,0), out hitInfo)){
		    LPTrans.position = transform.position - new Vector3(0, hitInfo.distance, 0);
            LRTrans.position = transform.position - new Vector3(0, hitInfo.distance / 2, 0);
            LRTrans.localScale = new Vector3(0.02f, hitInfo.distance / 2, 0.02f);
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
