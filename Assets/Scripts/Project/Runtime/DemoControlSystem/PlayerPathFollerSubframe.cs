using Lean.Touch;
using PathCreation;
using System.Threading.Tasks;
using UnityEngine;

namespace Base
{
    [System.Serializable]
    public class PlayerPathFollerSubframe
    {

        [SerializeField] private PathCreator pathCreator;
        [SerializeField] private EndOfPathInstruction endOfPathInstruction;
        [SerializeField] private float speed = 0.7f;
        [SerializeField] private float SwipeSpeed;
        [SerializeField] private float distanceTravelled = 0f;
        [SerializeField] private float xOffset, yOffset;
        [SerializeField] private Vector3 XMovementClamp = new Vector3(-2.1f, 2.1f);
        private Vector3 OriginalXClamp;
        [SerializeField] private Vector3 desiredPoint;

        PlayerMovementFrame Parent;
        Transform Body;

        private float move;


        public void Setup(Transform body, PlayerMovementFrame parent)
        {
            this.Body = body;
            this.Parent = parent;
            this.OriginalXClamp = XMovementClamp;
        }

        public void MoveBody()
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

                Body.rotation = Quaternion.Lerp(Body.rotation, rot, Time.deltaTime * 35);

                xOffset += move * Time.deltaTime * SwipeSpeed;

                Body.position = desiredPoint;

                xOffset = Mathf.Clamp(xOffset, XMovementClamp.x, XMovementClamp.y);
                desiredPoint = Body.TransformPoint(xOffset, yOffset, 0);

                Body.position = desiredPoint;
            }

            if (pathCreator.path.GetClosestTimeOnPath(Body.position) > 0.99f)
            {
                Parent.EndReached?.Invoke(true);
            }
        }

        public async void ClampMovement(Vector2 newClamp, float Duration = 0)
        {
            this.XMovementClamp = newClamp;
            if (Duration > 0)
            {
                await Task.Delay((int)Duration * 1000);
                this.XMovementClamp = OriginalXClamp;
            }
        }


    }
}