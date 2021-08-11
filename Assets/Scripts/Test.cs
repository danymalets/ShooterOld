using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test: MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
        Application.targetFrameRate = 30;
    }
}