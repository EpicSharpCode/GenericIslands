using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericIslands
{
    public static class GI_Utilities
    {
        public static float Map(float value, float from, float to, float from2, float to2)
        {
            if (value <= from2) { return from; }
            else if (value >= to2) { return to; }
            else
            {
                return (to - from) * ((value - from2) / (to2 - from2)) + from;
            }
        }
    }
}
