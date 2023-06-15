using System;
using System.Collections.Generic;
using nyy.FG_Case.System_Gem;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.System_Data
{
    [CreateAssetMenu(menuName = "Data/Gem", fileName = "new Gem Data")]
    public class GemObjectData : SerializedScriptableObject
    {
        #region PROPERTIES
        private bool _hide = true;
        [BoxGroup][Title("Gems")] [DisableIf("_hide")]
        public List<GemProperties> GemDataList;
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        #endregion 
    }
    
    [Serializable]
    public class GemProperties
    {
        [Title("Properties")] 
        public string Name;
        public int StartingSalePrice;
        public Sprite Icon;
        public Gem Prefab;
    }
}