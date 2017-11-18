/* ========================================
 *             InputManager.cs
 * ---------------------------------------
 *
 *
 * Create by NuSa
 * ========================================*/

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IM_KeyUI : MonoBehaviour {

    public List<GameObject> slotInput;
    public GameObject WaitingInput;

    private bool WaitKey = false;
    private string WaitingKey;

	// Use this for initialization
	void Start ()
    {
        WaitingInput.SetActive(false);
        LoadKey_UI();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (WaitKey)
        {
            detectKey();
        }
	}

    private void LoadKey_UI()
    {
        for (int i = 0; i < slotInput.Count; i++)
        {
            string strFunction = slotInput[i].name.Split('.')[1];
            slotInput[i].GetComponent<IM_KeyText>().KeyText.GetComponent<Text>().text = ((KeyCode)InputManager.get_Instance().Get_Key(strFunction)).ToString();
        }
    }

    public void ChangeKey(string _functionName)
    {
        if (!WaitKey)
        {
            WaitingKey = _functionName;
            WaitKey = true;
            WaitingInput.GetComponentInChildren<Text>().text = "PRESS A KEY - press escp for not change";
            WaitingInput.SetActive(true);
        }
        else
        {
            return;
        }
    }

    private void detectKey()
    {
        foreach (KeyCode dkKey in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(dkKey) && dkKey != KeyCode.Escape)
            {
                if (InputManager.get_Instance().DetectIfKeyIsUsed(dkKey))
                {
                    WaitingInput.GetComponentInChildren<Text>().text = "KEY <" + dkKey.ToString() + "> IS ALREADY USE, PLEASE PRESS OTHER KEY";
                }
                else
                {
                    KeyDetect(dkKey);
                }
            }
            
            //InputManager.get_Instance()

            if (Input.GetKey(KeyCode.Escape))
            {
                WaitKey = false;
                WaitingKey = "";
                WaitingInput.SetActive(false);
            }
        }
    }

    private void KeyDetect(KeyCode _key)
    {
        IM_KeyInfo newKey = new IM_KeyInfo();
        newKey.Function = WaitingKey;
        newKey.Key = _key;

        InputManager.get_Instance().Set_Key(newKey);

        LoadKey_UI();
        WaitingKey = "";
        WaitKey = false;
        WaitingInput.SetActive(false);
    }
}
