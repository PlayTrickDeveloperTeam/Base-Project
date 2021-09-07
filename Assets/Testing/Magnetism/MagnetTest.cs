using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using System.Linq;

public class MagnetTest : MonoBehaviour
{
    Camera Cam;
    Vector3 ClickPos;
    [SerializeField] LayerMask GroundMask;
    List<Collider> cols;
    List<Magnetized> magnets;

    [SerializeField] float Range = 5;

    private void Start()
    {
        Cam = Camera.main;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cols = new List<Collider>();
            ClickPos = Input.mousePosition.GetWorldPosition(Cam, GroundMask);
            cols = Physics.OverlapSphere(ClickPos, Range).ToList();
        }
        if (Input.GetMouseButton(0))
        {
            ClickPos = Input.mousePosition.GetWorldPosition(Cam, GroundMask);
            cols = Physics.OverlapSphere(ClickPos, Range).ToList();
            if (cols == null) return;
            magnets = cols.Where(t => t.GetComponent<Magnetized>() != null).Select(t => t.GetComponent<Magnetized>()).ToList();
            magnets.ForEach(t => t.Activate(ClickPos));
        }
    }
}
