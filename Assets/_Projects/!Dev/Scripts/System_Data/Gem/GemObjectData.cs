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
        [BoxGroup("Settings")][InlineButton("AddGem")][InlineButton("ResetGem")]
        public int NumberOfGemToAdd;
        
        [BoxGroup][Title("Gems")] 
        public List<GemProperties> GemDataList;
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        public void AddGem()
        {
            for(int i=0;i<NumberOfGemToAdd;i++)
            {
                GemDataList.Add(new GemProperties());
            }
        }
        
        public void ResetGem()
        {
            GemDataList.Clear();
        }
        
        #endregion 
    }
    
    [Serializable]
    public class GemProperties
    {
        [Title("Properties")]
        [field: SerializeField]
        public string Name { get; set; }
        [field: SerializeField]
        public int StartingSalePrice { get; set; }
        [field: SerializeField]
        public Sprite Icon { get; set; }
        [field: SerializeField] 
        public Gem Prefab { get; set; }
    }
}