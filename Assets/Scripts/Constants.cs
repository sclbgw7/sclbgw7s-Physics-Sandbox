using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public Transform CameraTransHere;
    public static Transform CameraTrans = null;
    // Start is called before the first frame update
    void Start()
    {
        CameraTrans = CameraTransHere;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
