using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool HaveGravitation = false;
    public Material DefaultMaterial;
    public Transform CameraTrans;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10) {
            Destroy(gameObject);
        }
       
    }

    void OnMouseOver() {
        if(GeneralController.CurrentMode == GameMode.Free) {
            if(Input.GetMouseButtonDown(0)) {
                if(HaveGravitation) {
                    foreach (Transform child in transform) {
                        if(child.gameObject.activeInHierarchy)
                            child.gameObject.SetActive(false);
                        else
                            child.gameObject.SetActive(true);
                    }
                }
            }
        }
        if(GeneralController.CurrentMode == GameMode.Create) {
            if(Input.GetMouseButtonDown(1)) {
                //if(gameObject.GetComponent<Collider>().isTrigger == false)
                    Destroy(gameObject);
            }
        }
    }

    void OnMouseDrag()
    {
        if(GeneralController.CurrentMode == GameMode.Free) {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4f);
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objectPosition;
        }
    }
}
