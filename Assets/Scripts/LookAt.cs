using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public string objectTag;
    public Transform objectTransform;
    private Transform _targetTransform;
    public bool canRotateX;
    public bool canRotateY;
    public bool canRotateZ;
    private Vector3 _rotationModifier;
    
    void Awake()
    {
        if (objectTransform == null)
        {
            _targetTransform = GameObject.FindGameObjectWithTag(objectTag).GetComponent<Transform>();
        }

        _rotationModifier.x = canRotateX ? 1 : 0;
        _rotationModifier.y = canRotateY ? 1 : 0;
        _rotationModifier.z = canRotateZ ? 1 : 0;
    }

    private void Update()
    {
        transform.LookAt(_targetTransform);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x * _rotationModifier.x, transform.rotation.eulerAngles.y * _rotationModifier.y, transform.eulerAngles.z * _rotationModifier.z);
    }
}
