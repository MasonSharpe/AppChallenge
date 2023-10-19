using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField] SpriteRenderer spriteRenderer;
    public enum PickupType
    {
        Health,
        Armor
    }

    public PickupType type;
    public int ID;



}