using UnityEngine;
using System;

namespace Utilities
{
    public static class StaticFunctions
    {
        /// <summary>
        /// Invokes a function after a certain amount of time
        /// </summary>
        /// <param name="action">Action to be invoked using "action?.Invoke();"</param>
        /// <param name="delayTime">Time after which the function will be invoked</param>
        public static System.Collections.IEnumerator Invoke(Action action, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            action?.Invoke();
        }
    }
}