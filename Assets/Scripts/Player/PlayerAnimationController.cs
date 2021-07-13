using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public ThirdPersonController _playerController;
    public Animator _animator;
    public float IdleIdleMinimumTriggerTime;
    public float IdleIdleMaximumTriggerTime;
    public float IdleIdleCooldownTime;
    private float idleIdleTimer;
    private float idleIdleTriggerTime;
    private bool canIdleIdle;
    void Start()
    {
        idleIdleTimer = 0;
        idleIdleTriggerTime = 0;
        canIdleIdle = true;
    }
    
    void Update()
    {
        UpdateWalk();
        UpdateIdle();
    }

    void UpdateIdle()
    {
        if (_playerController.Velocity <= 0.02f && canIdleIdle)
        {
            if (idleIdleTriggerTime == 0)
            {
                idleIdleTriggerTime = Random.Range(IdleIdleMinimumTriggerTime,IdleIdleMaximumTriggerTime);
            }
            
            idleIdleTimer += Time.deltaTime;

            if (idleIdleTimer >= idleIdleTriggerTime)
            {
                canIdleIdle = false;
                idleIdleTriggerTime = 0;
                idleIdleTimer = 0;
                StartCoroutine(DelayIdleIdle());
                _animator.SetTrigger("Idle_Idle");
            }
        }
    }

    void UpdateWalk()
    {
        _animator.SetFloat("Velocity", _playerController.Velocity);
    }

    IEnumerator DelayIdleIdle()
    {
        yield return new WaitForSeconds(IdleIdleCooldownTime);
        canIdleIdle = true;
        yield return null;
    }

    public void PointWeapon()
    {
        _animator.SetTrigger("PointWeapon");
    }

    public void HolsterWeapon()
    {
        _animator.SetTrigger("HolsterWeapon");
    }
}
