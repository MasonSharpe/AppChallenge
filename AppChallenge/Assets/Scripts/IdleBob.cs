using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBob : MonoBehaviour
{
    public Transform affected;
    private float offset;


    private void Start()
    {
        offset = Random.Range(0.5f, 1.5f);
    }
    private void Update()
    {
        affected.localPosition = new Vector3(0, 0.3f * Mathf.Sin(Time.frameCount / 165f * offset), 0);
    }


}