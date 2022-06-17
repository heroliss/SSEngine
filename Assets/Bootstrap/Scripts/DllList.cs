using System.Collections.Generic;
using UnityEngine;

namespace BootStrap
{
    [CreateAssetMenu(fileName = "DllList", menuName = "ScriptableObjects/DllList", order = 1)]
    public class DllList : ScriptableObject
    {
        public List<string> List = new List<string>();
    }
}
