using UnityEngine;
using System.Collections.Generic;

namespace Utilities
{
    public static class Extentions
    {
        /// <summary>
        /// Changes property "enabled" for each list item
        /// </summary>
        public static void Enabled(this List<Behaviour> behaviours, bool value)
        {
            for (int i = 0; i < behaviours.Count; i++)
                behaviours[i].enabled = value;
        }
    }
}