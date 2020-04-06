using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiDllFile.DllFiles;
using KaiDllFile;

public class InputManager : MonoBehaviour
{
    public float throttle;
    public float steer;
    public bool lightON;
    public bool brake;

    ControllerInitializer controller = new ControllerInitializer();

    void Start() {
        controller.Initalise("test", "qwerty");
        
    }


    void Update() {
        throttle = ControllerDatabase.GetQuatZ() * 2.4f;
        Debug.Log(ControllerDatabase.GetQuatZ());
        steer = ControllerDatabase.GetQuatX() * 2.8f;

        lightON = Input.GetKeyDown(KeyCode.L);
        brake = Input.GetKey(KeyCode.Space);
    }
    
}
