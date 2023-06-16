using System;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using nyy.FG_Case.Utils;
    
namespace nyy.FG_Case.System_Gem
{
    public class GemListItem : MonoBehaviour
    {
        #region PROPERTIES
        
        [TabGroup("A","Settings")] 
        public string Type;
        
        [TabGroup("A","Settings")] 
        public int StartingSalePrice;
        
        [TabGroup("A","Settings")] 
        public IntReactiveProperty CollectedCount;
        
        [TabGroup("B","Components")] 
        public Image Icon;
        
        [Header("UI Components")]
        [TabGroup("C","UI Components")] 
        public TMP_Text textType;
        
        [TabGroup("C","UI Components")] 
        public TMP_Text textStartingSalePrice;
        
        [TabGroup("C","UI Components")] 
        public TMP_Text textCollectedCount;

        #endregion
                
        #region EVENT FUNCTIONS

        private void Start()
        {
            CollectedCount.SubscribeToText(textCollectedCount);
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS

        public void SetData()
        {
            textType.text = Type;
            textStartingSalePrice.text = StartingSalePrice.ToString();
            textCollectedCount.text = CollectedCount.ToString();
        }

        public void IncreaseCount()
        {
            CollectedCount.Value ++;
        }
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}