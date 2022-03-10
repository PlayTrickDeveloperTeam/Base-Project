using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public abstract class Contactable : MonoBehaviour
{
    public int value;
    public Collider collider;
    public bool JustOnce;
    public bool DestoryWhenContact;
    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    public abstract void WhenContact(GameObject _obj);
    public abstract void Effect(GameObject _obj);
    public abstract void WhenExit(GameObject _obj);
    public virtual void DestoryThis()
    {
        Destroy(gameObject);
    }

    public virtual void JustOnceContact()
    {
        collider.enabled = false;
    }
    public virtual bool Requirement(GameObject _obj)
    {
        return _obj.TryGetComponent(out Player player);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Requirement(other.gameObject)) return;
        if(JustOnce) JustOnceContact();
        WhenContact(other.gameObject);
        Effect(other.gameObject);
        
        if (DestoryWhenContact) DestoryThis();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Requirement(other.gameObject)) return;
        WhenExit(other.gameObject);
    }
}
