using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MagnetBase : MonoBehaviour, Magnetized
{
    public Action<Vector3> Pulled;
    public MagnetSide MagnetSide;
    Rigidbody RB;

    public int Force;
    int forceSide;


    private void Start()
    {
        Setup();
    }

    void Setup()
    {
        MagnetSide = (MagnetSide)UnityEngine.Random.Range(0, Enum.GetValues(MagnetSide.GetType()).Length);
        Pulled += ForceSide;
        RB = GetComponent<Rigidbody>();
        Color col = new Color();
        switch (MagnetSide)
        {
            case MagnetSide.Positive:
                forceSide = +1;
                col = Color.red;
                break;
            case MagnetSide.Negative:
                forceSide = -1;
                col = Color.green;
                break;
            case MagnetSide.Natural:
                forceSide = 0;
                col = Color.white;
                break;
        }
        GetComponent<MeshRenderer>().material.color = col;
    }

    public void Activate(Vector3 pos)
    {
        Pulled?.Invoke(pos);
    }


    void ForceSide(Vector3 pos)
    {
        RB.AddExplosionForce(forceSide * Force, pos, Mathf.Infinity, 0, ForceMode.Acceleration);
    }
}

public enum MagnetSide { Positive, Negative, Natural }

public interface Magnetized
{
    public void Activate(Vector3 pos);
}
