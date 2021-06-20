using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class MoveAndRotateToTargetBehaviour : MonoBehaviour
    {
        #region Properties
        public string TargetName => targetName;
        public UnityEvent OnAchieveTargetPosition { get => onAchieveTargetPosition; }
        #endregion

        #region Fields
        [SerializeField] string targetName;
        [Header("Position")]
        [SerializeField] Vector3 targetPosition;
        [SerializeField] Vector3<bool> ignoreTargetPosition;
        [SerializeField] float movementSpeed = 1f;
        [Header("Rotation")]
        [SerializeField] Vector3 targetRotation;
        [Tooltip("The speed of rotation of the object to the target\n" +
            "Measured in degrees per second")]
        [SerializeField] float rotationSpeed = 10f;

        [SerializeField] UnityEvent onAchieveTargetPosition;
        #endregion

        #region Methods
        void Update()
        {
            if (ignoreTargetPosition.X) targetPosition.x = transform.position.x;
            if (ignoreTargetPosition.Y) targetPosition.y = transform.position.y;
            if (ignoreTargetPosition.Z) targetPosition.z = transform.position.z;

            float distanceToObject = Vector3.Distance(transform.position, targetPosition);
            float angleToObject = Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation));

            float interpolatingMovementValue = movementSpeed / distanceToObject * Time.deltaTime;
            float interpolatingRotationValue = rotationSpeed / angleToObject * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetPosition, interpolatingMovementValue);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), interpolatingRotationValue);

            if (distanceToObject == 0f && angleToObject == 0f)
            {
                onAchieveTargetPosition.Invoke();
                enabled = false;
            }
        }
        #endregion

        #region Classes
        [Serializable]
        class Vector3<T>
        {
            [SerializeField] T x;
            [SerializeField] T y;
            [SerializeField] T z;
            public T X => x;
            public T Y => y;
            public T Z => z;
        }
        #endregion
    }
}