using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform playerTransform;
    void Awake()
    {
        playerTransform = FindObjectOfType<ThirdPersonController>().transform;
        enabled = false;
    }

    private void Update()
    {
        transform.LookAt(playerTransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
