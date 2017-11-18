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

    // Save key in PlayerPrefs
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

    public static bool GetKeyDown(string _function)
    {
        return Input.GetKeyDown((KeyCode)get_Instance().Get_Key(_function));
    }

    public static bool GetKey(string _function)
    {
        return Input.GetKey((KeyCode)get_Instance().Get_Key(_function));
    }

    public static bool GetKeyUp(string _function)
    {
        return Input.GetKeyUp((KeyCode)get_Instance().Get_Key(_function));
    }

    /* ==========================================================
     *                     Get_Key(string)
     * ----------------------------------------------------------
     * @_function : its name of input (exemple : Jump)
     *
     * return : if input name is found, function return KeyCode
     * if input is not found, function return KeyCode.None
     * ========================================================= */
    public int Get_Key(string _function)
    {
        for (int i = 0; i < AllKeys.Count; i++)
        {
            if (AllKeys[i].Function == _function)
            {
                return (int)AllKeys[i].Key;
            }
        }

        Debug.LogError("Key for " + _function + " is not setup.");

        return 0;
    }

    /* ===============================================
     *         DetectIfKeyIsUsed(KeyCode)
     * -----------------------------------------------
     * @_key : its Keycode you want check if used
     *
     * return : if Keycode is already used return true
     * if Keycode if not used return false
     * ===============================================*/

    public bool DetectIfKeyIsUsed(KeyCode _key)
    {
        for (int i = 0; i < AllKeys.Count; i++)
        {
            if (AllKeys[i].Key == _key)
            {
                return true;
            }
        }

        return false;
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
