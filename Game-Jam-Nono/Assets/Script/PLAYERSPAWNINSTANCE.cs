using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERSPAWNINSTANCE : MonoBehaviour
{
    public static PLAYERSPAWNINSTANCE instance;

    private void Awake()
    {
        instance = this;
    }
}
