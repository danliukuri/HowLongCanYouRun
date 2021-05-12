using UnityEngine;

namespace Camera
{
    public class CameraMovementToObject : MonoBehaviour
    {
        #region Fields
        [SerializeField] Vector3 targetPosition;
        [SerializeField] Vector3 targetRotation;
        [SerializeField] float movementSpeed = 1f;
        #endregion

        #region Methods
        void Update()
        {
            float distanceToObject = Vector3.Distance(transform.position, targetPosition);
            float interpolatingValue = movementSpeed / distanceToObject * Time.deltaTime;
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, interpolatingValue);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), interpolatingValue);

            if (distanceToObject == 0f)
            {
                GameplayHandler.StartGameplay();
                enabled = false;
            }
        }
        #endregion
    }
}