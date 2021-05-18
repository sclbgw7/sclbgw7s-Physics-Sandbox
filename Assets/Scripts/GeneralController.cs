using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode {
    Create,
    Free
}

public class GeneralController : MonoBehaviour
{
    public static GameMode CurrentMode = GameMode.Free;
    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitTriggers = false;
    }

    // Update is called once per frame
    private bool isPausing = false;
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab)) {
           Time.timeScale = 0.2f;
        }
        else {
            Time.timeScale = 1f;
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            isPausing = !isPausing;
        }
        Time.timeScale *= (isPausing || GeneralController.CurrentMode == GameMode.Create) ? 0f : 1f;
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if(CurrentMode == GameMode.Free)
                CurrentMode = GameMode.Create;
            else
                CurrentMode = GameMode.Free;
        }
    }
}
