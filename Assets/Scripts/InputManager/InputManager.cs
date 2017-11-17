using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static InputManager myInstance;

	// Use this for initialization
	void Start () {
        myInstance = this;

        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static InputManager get_Instance()
    {
        if (myInstance != null)
        {
#if UNITY_EDITOR
            Debug.Log("Return InputManager.myInstance");
#endif
            return myInstance;
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Return InputManager.myInstance = new InputManager();");
#endif
            return myInstance = new InputManager();
        }
    }
}
