using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Est.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/ New Data Item Store 3D", order = 0)]
    public class DataItemsStore3D : ScriptableObject
    {
        public int costItem = 10;
        public int indexCostItem = 1;
        public int multiplicatorToGenerationPerSecond = 2;
    }
}
