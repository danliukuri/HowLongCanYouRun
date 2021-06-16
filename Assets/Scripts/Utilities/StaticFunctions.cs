using UnityEngine;
using System;
using System.Collections;

namespace Utilities
{
    public static class StaticFunctions
    {
        /// <summary>
        /// Invokes a function after a certain amount of time
        /// </summary>
        /// <param name="action">Action to be invoked</param>
        /// <param name="delayTime">Time after which the function will be invoked</param>
        public static IEnumerator Invoke(Action action, float delayTime)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            if (delayTime <= 0f)
                throw new ArgumentException("Value must be greater than zero!", nameof(delayTime));

            yield return new WaitForSeconds(delayTime);
            action.Invoke();
        }

        /// <summary>
        ///  Invokes a function after a certain amount of time (<typeparamref name="T"/>) in a "while" loop
        /// </summary>
        /// <param name="action">Action to be invoked</param>
        /// <param name="conditionPredicate">Condition for invoking an action
        /// If <see langword="true"/>, then the cycle continues
        /// Else if <see langword="false"/>, then the cycle stops
        /// </param>
        public static IEnumerator DoWhile<T>(Action action, Func<bool> conditionPredicate)
            where T : YieldInstruction, new()
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));
            if (conditionPredicate is null)
                throw new ArgumentNullException(nameof(conditionPredicate));

            yield return new WaitUntil(conditionPredicate);
            while (conditionPredicate.Invoke())
            {
                action.Invoke();
                yield return new T();
            }
        }
    }
}