using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBob : MonoBehaviour
{
    public Transform affected;
    public float strength = 1;
    private float offset;
    private float totalFrameCount = 0;


    private void Start()
    {
        offset = Random.Range(0.5f, 1.5f);
    }
    private void Update()
    {
        totalFrameCount++;
        affected.localPosition = new Vector3(0, 0.3f * Mathf.Sin(totalFrameCount / 165f * offset), 0) * strength;
    }


}