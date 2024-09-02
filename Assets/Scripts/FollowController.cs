using UnityEngine;


    public class FollowController : MonoBehaviour
    {
        [SerializeField] private Transform mainCamera;
        [SerializeField] private float distance = 1.25f;
        [SerializeField, Range(0f, 1f)] private float lerpSpeed = 0.05f;

        private void Start()
        {
            SetPose();
        }

        private void SetPose()
        {
            Ray pose = CalculatePose();
            transform.position = pose.origin;
            transform.forward = pose.direction;
        }

        private void Update()
        {
            SetPose();
        }

        private Ray CalculatePose()
        {
            Vector3 cameraPosition = mainCamera.position;
            Vector3 cameraForward = new Vector3(mainCamera.forward.x, 0, mainCamera.forward.z).normalized;
            Vector3 lookBackDirection = new Vector3(transform.position.x - cameraPosition.x, 0,
                transform.position.z - cameraPosition.z).normalized;
            Vector3 position = cameraPosition + cameraForward * distance;
            return new Ray(position, lookBackDirection);
        }
    }
