using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnableActionMap : MonoBehaviour
{
    public InputActionAsset enableActionMap;

    void Awake()
    {
        enableActionMap.Enable();
    }

}
