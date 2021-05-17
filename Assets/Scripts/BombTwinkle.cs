using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTwinkle : MonoBehaviour
{
    public Material RedBright;
    public Material PureBlack;
    public float Interval;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float TwinkleTime = 0f;
    private bool IsBlack = true;
    void Update()
    {
        MeshRenderer MR = gameObject.GetComponent<MeshRenderer>();
        TwinkleTime += Time.deltaTime;
        if(TwinkleTime >= Interval) {
            TwinkleTime -= Interval;
            MR.material = IsBlack ? RedBright : PureBlack;
            IsBlack = !IsBlack;
        }
        
    }
}
