using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Est.Data
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config/ New Config", order = 0)]
    public class Configuration : ScriptableObject
    {
        //public var
        public Sprite typeSpriteCoin;
        public Sprite typeSpriteGenerationCoin;
        public string stringTypeCoin;
        public string stringTypeGenerationCoin;

    }
}
