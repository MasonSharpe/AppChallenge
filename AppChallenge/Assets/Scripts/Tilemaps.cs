using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemaps : MonoBehaviour
{
    public static Tilemap[] tilemaps;
    private void Awake()
    {
        tilemaps = GetComponentsInChildren<Tilemap>();
    }
}
