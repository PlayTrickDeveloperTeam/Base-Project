using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Dreamteck.Splines;
using Lean.Touch;
using UnityEngine;

public class ParentFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _followingObject;
    public float delay;
    private SplineFollower _splineFollower;
    private SplineFollower _childFollower;
    public float RotationSpeed;
    void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _childFollower = _followingObject.GetComponent<SplineFollower>();
        _splineFollower.followSpeed = 0;
    }
    
    void Start()
    {
        _followingObject = transform.GetChild(0);
    }
    
    void Update()
    {
        if(!B_GM_GameManager.instance.IsGamePlaying()) return;
        _splineFollower.followSpeed = _childFollower.followSpeed;
        RunInfinite();
        Rotate();
        
    }
    private void RunInfinite()
    {
        _splineFollower.motion.offset = Vector3.Lerp(_splineFollower.motion.offset, _childFollower.motion.offset, Time.deltaTime * delay);
    }
    
    private void Rotate()
    {

        var leanFinger = LeanTouch.Fingers;
        var x = 0f;
            
        if (leanFinger.Count > 0)
        {
            x = leanFinger[0].ScreenDelta.x;
        }
            
        _splineFollower.motion.rotationOffset = 
            Vector3.Lerp(_splineFollower.motion.rotationOffset,
                new Vector3(0,x * RotationSpeed,0), Time.fixedDeltaTime);

    }

    private void LateUpdate()
    {
        var pos = _childFollower.motion.offset;
        pos.x = _splineFollower.motion.offset.x;
        _splineFollower.motion.offset = pos;
    }
}
