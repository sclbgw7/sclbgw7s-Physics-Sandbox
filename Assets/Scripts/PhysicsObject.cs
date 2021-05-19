using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool HaveGravitation = false;
    public Material DefaultMaterial;
    public Transform CameraTrans;
    private Rigidbody RB;
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CameraTrans == null)
            CameraTrans = Constants.CameraTrans;
        if(transform.position.y < -10) {
            Destroy(gameObject);
        }
       
    }

    void OnMouseOver() {
        if(GeneralController.CurrentMode == GameMode.Create) {
            if(Input.GetMouseButtonDown(1)) {
                //if(gameObject.GetComponent<Collider>().isTrigger == false)
                    Destroy(gameObject);
            }
        }
    }

    private float clickTime;
    private float distance;
    void OnMouseDown()
    {
        if(GeneralController.CurrentMode == GameMode.Free) {
            distance = (transform.position - CameraTrans.position).magnitude;
            if(HaveGravitation) {
                clickTime = Time.unscaledTime;
            }
        }
    }

    void OnMouseUp()
    {
        if(GeneralController.CurrentMode == GameMode.Free) {
            if(HaveGravitation) {
                if(Time.unscaledTime - clickTime < 0.1f) {
                    foreach (Transform child in transform) {
                        if(child.gameObject.activeInHierarchy)
                            child.gameObject.SetActive(false);
                        else
                            child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void OnMouseDrag()
    {
        if(GeneralController.CurrentMode == GameMode.Free) {
            if(Time.unscaledTime - clickTime > 0.1f) {
                if(Input.GetKey(KeyCode.UpArrow))
                    distance += Time.unscaledTime * 0.001f;
                if(Input.GetKey(KeyCode.DownArrow))
                    distance -= Time.unscaledTime * 0.001f;
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = objectPosition;
                RB.velocity = new Vector3(0,0,0);
            }
        }
    }
}
