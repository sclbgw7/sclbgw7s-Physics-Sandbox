using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool HaveGravitation = false;
    public Material DefaultMaterial;
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

    void OnMouseDown() {
        if(GeneralController.CurrentMode == GameMode.Free) {
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
}
