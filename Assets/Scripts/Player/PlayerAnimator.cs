using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private const string Push = "IsPush";
    private const string Jump = "IsJumping";
    private const string Falling = "IsFalling";
    private const string Grounded = "IsGrounded";
    private const string FlatImpact = "IsFlatImpact";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void DisableAnimation()
    {
        _animator.enabled = false;  
    }

    public void OnPushAnimation(bool value)
    {
        _animator.SetBool(Push, value);
    }

    public void OnJumping(bool value)
    {
        _animator.SetBool(Jump, value);
    }
    
    public void OnGrounded(bool value)
    {
        _animator.SetBool(Grounded, value);
    }

    public void OnFalling(bool value)
    {
        _animator.SetBool(Falling, value);
    }

    public void OnFlatImpact(bool value)
    {
        _animator.SetBool(FlatImpact, value);
    }
}
