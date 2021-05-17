using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    public float sensitivity;
    public float LineSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.K)) {
            Vector3 rotateScale= new Vector3(0, 1, 0);
            transform.Rotate(rotateScale * Time.unscaledDeltaTime * sensitivity);
        }
        if(Input.GetKey(KeyCode.J)) {
            Vector3 rotateScale= new Vector3(0, -1, 0);
            transform.Rotate(rotateScale * Time.unscaledDeltaTime * sensitivity);
        }
        float moveUp = Input.GetAxisRaw("Vertical");
        float moveRight = Input.GetAxisRaw("Horizontal");
        float UpOrForward = 1;
        if(Input.GetKey(KeyCode.LeftShift)) {
            UpOrForward = 0;
        }

        float offset = (float)Math.Sqrt(moveUp * moveUp + moveRight * moveRight);
        Transform TR = gameObject.GetComponent<Transform>();

        //移动
        if(offset < 1)offset = 1;
        Vector3 Displacement = new Vector3(0, moveUp * (1-UpOrForward), 0);
        Displacement += moveRight * TR.right;
        Displacement += moveUp * TR.forward * UpOrForward;
        transform.position += Displacement * LineSpeed * Time.unscaledDeltaTime / offset;
    }
}
