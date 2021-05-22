using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class MoveAndRotateToTargetBehaviour : MonoBehaviour
    {
        #region Fields
        [Header("Position")]
        [SerializeField] Vector3 targetPosition;
        [SerializeField] Vector3<bool> ignoreTargetPosition;
        [SerializeField] float movementSpeed = 1f;
        [Header("Rotation")]
        [SerializeField] Vector3 targetRotation;
        [SerializeField] float rotationSpeed = 1f;

        [SerializeField] UnityEvent callbackFunction;
        #endregion

        #region Methods
        void Update()
        {
            if (ignoreTargetPosition.X) targetPosition.x = transform.position.x;
            if (ignoreTargetPosition.Y) targetPosition.y = transform.position.y;
            if (ignoreTargetPosition.Z) targetPosition.z = transform.position.z;

            float distanceToObject = Vector3.Distance(transform.position, targetPosition);
            float interpolatingMovementValue = movementSpeed / distanceToObject * Time.deltaTime;
            float interpolatingRotationValue = rotationSpeed / distanceToObject * Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetPosition, interpolatingMovementValue);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), interpolatingRotationValue);

            if (distanceToObject == 0f)
            {
                callbackFunction.Invoke();
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