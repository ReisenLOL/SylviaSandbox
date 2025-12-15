using System;
using UnityEngine;

public class DebugEffect : MonoBehaviour
{
    public float length;

    private void Start()
    {
        Destroy(gameObject, length);
    }
}
