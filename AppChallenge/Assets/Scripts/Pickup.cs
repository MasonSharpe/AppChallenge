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

    private void Start()
    {
        switch (type){
            case PickupType.Health:
                spriteRenderer.color = Color.green; break;
            case PickupType.Armor:
                spriteRenderer.color = Color.grey; break;

        }
    }


}