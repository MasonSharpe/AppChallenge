using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpHandler : MonoBehaviour
{
    static public XpHandler instance;

    private void Awake()
    {
        instance = this;
    }



}
