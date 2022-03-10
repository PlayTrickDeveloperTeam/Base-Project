using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum AnimList
    {
        Idle,
        Die,
        Walk,
        Run,
        TakeDamage,
        Attack
    }
    
    
    private Animator _animator;
    private SplineFollower _splineFollower;
    public static Action<AnimList> PlayAnim;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _splineFollower = FindObjectOfType<RelativeFollower>().GetComponent<SplineFollower>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed",_splineFollower.followSpeed);
    }
    
    private void PlayOverideAnim(AnimList anim)
    {
        switch (anim)
        {
            // case AnimList.Idle:
            //     break;
            case AnimList.Die:
                _animator.SetBool("Die",true);
                break;
            // case AnimList.Walk:
            //     break;
            // case AnimList.Run:
            //     break;
            case AnimList.TakeDamage:
                _animator.SetTrigger("TakeDamage");
                break;
            case AnimList.Attack:
                _animator.SetTrigger("Attack");
                break;
        }
    }
    
}
