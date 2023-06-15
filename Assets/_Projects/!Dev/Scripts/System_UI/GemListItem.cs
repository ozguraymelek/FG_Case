using System;
using GenericScriptableArchitecture;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using nyy.FG_Case.Utils;
    
namespace nyy.FG_Case
{
    public class GemListItem : MonoBehaviour
    {
        #region PROPERTIES
        
        public ScriptableEvent IncreaseCountEvent;
        
        [TabGroup("A","Settings")] 
        public string Type;
        
        [TabGroup("A","Settings")] 
        public int StartingSalePrice;
        
        [TabGroup("A","Settings")] 
        public int CollectedCount;
        
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
            // CollectedCount.SubscribeToText(textCollectedCount);
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS

        [Button]
        public void SetData()
        {
            textType.text = Type;
            textStartingSalePrice.text = StartingSalePrice.ToString();
            textCollectedCount.text = CollectedCount.ToString();
        }

        [Button]
        public void IncreaseCount()
        {
            print("b");
            CollectedCount = 31;
            print("a");
        }
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}