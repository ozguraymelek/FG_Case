using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenericScriptableArchitecture;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace nyy.FG_Case.PlayerSc
{
    public class Stack : MonoBehaviour
    {
        #region PROPERTIES

        public ScriptableEvent IncreaseCountEvent;
        
        [TabGroup("References")] 
        public GemGenerator GemGenerator;
        
        [TabGroup("Gem Item List")]
        public List<GemListItem> GemListItems;

        [TabGroup("Components")] 
        public Transform stackHolder;
        
        #endregion
                
        #region EVENT FUNCTIONS

        private void Awake()
        {
            GemListItems = GemGenerator.TempGemListItems;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IStackable gem) == false) return;
            if (gem.IsStacked() == true) return;
            if (gem.CanStackable() == false) return;
                
            gem.Stack(stackHolder);

            FindGemList(gem);
            
            gem.SetGrowable(false);
        }
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS

        private void FindGemList(IStackable gem)
        {
            // if (gem.GetSc() == GemListItems[0].Prefab)
            // {
            //     GemListItems[0].CollectedCount += 1;
            // }

            var tempGemEnumerable = from gemListItem in GemListItems
                where gemListItem.Type == gem.GetName()
                select gemListItem;

            IncreaseCountEvent.Invoke();
        }
        #endregion

        public void OnEventInvoked()
        {
            
        }
    }
}