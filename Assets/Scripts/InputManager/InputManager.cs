/* ========================================
 *             InputManager.cs
 * ---------------------------------------
 *
 *
 * Create by NuSa
 * ========================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public List<IM_KeyInfo> AllKeys = new List<IM_KeyInfo>();

    private static InputManager myInstance;



    void Awake()
    {
        myInstance = this;
        DontDestroyOnLoad(this);
        Load_Key();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Load key save in PlayerPrefs
    public void Load_Key()
    {
        for (int i = 0; i < AllKeys.Count; i++)
        {
            string KeyPrefix = ("Key." + AllKeys[i].Function);
            AllKeys[i].Key = (KeyCode)PlayerPrefs.GetInt(KeyPrefix, (int)AllKeys[i].Key); //Return Keycode save in PlayerPref, but if never save return by default 2nd argument

#if UNITY_EDITOR
            Debug.Log(KeyPrefix + " = " + AllKeys[i].Key);
#endif
        }

    }

    public void Set_Key(IM_KeyInfo _key)
    {
        for (int i = 0; i < AllKeys.Count; i++)
        {
            if (AllKeys[i].Function == _key.Function)
            {
                AllKeys[i] = _key;
                string KeyPrefix = ("Key." + _key.Function);

                PlayerPrefs.SetInt(KeyPrefix, (int)_key.Key);
#if UNITY_EDITOR
                Debug.Log("Replace key function " + _key.Function + " by " + _key.Key.ToString());
#endif
            }
        }
    }




    /* ==========================================
     *              get_Instance()
     *-------------------------------------------
     *  return unique instance of InputManager
     * ===========================================*/
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
