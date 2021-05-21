using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Data
{
    [CreateAssetMenu(fileName = "Slot", menuName = "Config/ New slot information", order = 0)]
    public class SlotInformation : ScriptableObject
    {
        //public var
        [SerializeField] public ProgressionStat StatLevel;
        [TextArea] public string textInformation = "";
        public Sprite spriteBossSlot;

        /*public GameObject GetActualPrefab(int m_actualLevel) {
            return StatLevel.gameObjectsByLevel[m_actualLevel];
        }*/

    }

    [System.Serializable]
    public class ProgressionStat
    {
        public long costsToUnlockedSlot;
        public int levelUnitUnlockedSlot;

        [Space]
        public long costsToUpgradeLevel;
        public int levelUnitUpgradeLevel;
        public int[] countToUpgradeLevelExp;
        public float[] multiplicatorLevels;
        public float[] timeToUpgradeLevel;
        public Mesh[] gameObjectsByLevel;
    }

}
