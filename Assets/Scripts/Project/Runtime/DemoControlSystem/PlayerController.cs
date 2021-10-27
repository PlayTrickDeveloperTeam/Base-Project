using Lean.Touch;
using PathCreation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    [SerializeField] private float speed = 0.7f;
    [SerializeField] private float SwipeSpeed;
    [SerializeField] private float distanceTravelled = 0f;
    [SerializeField] private float xOffset, yOffset;
    [SerializeField] private float maxDistance = 2.1f;
    [SerializeField] private Vector3 desiredPoint;

    private float move;

    void Update()
    {
        var fingers = LeanTouch.Fingers;

        if (fingers.Count > 0)
        {
            if (!fingers[0].IsOverGui) move = fingers[0].ScreenDelta.x;
        }
        else move = 0;

        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            desiredPoint = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);

            var rot = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction) * Quaternion.Euler(0, 0, 90);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 35);

            xOffset += move * Time.deltaTime * SwipeSpeed;

            transform.position = desiredPoint;

            xOffset = Mathf.Clamp(xOffset, -maxDistance, maxDistance);
            desiredPoint = transform.TransformPoint(xOffset, yOffset, 0);

            transform.position = desiredPoint;



            //YOL BITERSE
            // if (pathCreator.path.GetClosestTimeOnPath(transform.position) > 0.99f) 
            // {
            //     
            // }

        }

    }


}