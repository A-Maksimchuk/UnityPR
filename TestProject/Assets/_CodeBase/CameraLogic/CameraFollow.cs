using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow: MonoBehaviour
    {
        [SerializeField] private float rotationAngleX;
        [SerializeField] private float distance;
        [SerializeField] private float offsetY;
        [SerializeField] private float speed;

        private Transform _following;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            if (_following == null)
            {
                return;
            }

            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();

            _transform.rotation = rotation;
            _transform.position = Vector3.MoveTowards(_transform.position, position, 
                speed*Time.deltaTime*Vector3.Distance(_transform.position, position));
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += offsetY;

            return followingPosition;
        }
    }
}