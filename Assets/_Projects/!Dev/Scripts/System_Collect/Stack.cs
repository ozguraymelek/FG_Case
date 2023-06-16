using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace nyy.FG_Case.PlayerSc
{
    public class Stack : MonoBehaviour
    {
        #region PROPERTIES

        [Title("Properties")] 
        [TabGroup("B", "Stack Data")]
        public IntRef CollectedAllGemAmount;
        
        [TabGroup("References")] 
        public GemGenerator GemGenerator;
        
        [TabGroup("Gem Item List")][ShowInInspector]
        public RuntimeSet<GemListItem> GemListItems;
        public List<GemListItem> GemList;

        [TabGroup("Components")] 
        public Transform stackHolder;

        #endregion
                
        #region EVENT FUNCTIONS

        [Button]
        private void Set()
        {
            GemList = GemListItems.ToList();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IStackable gem) == false) return;
            if (gem.IsStacked() == true) return;
            if (gem.CanStackable() == false) return;
                
            print("dsadas");
            
            gem.Stack(stackHolder);
            
            CollectedAllGemAmount.Value += 1;
            
            FindCorrectGemItemList(gem);
            
            gem.SetGrowable(false);
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS

        private void FindCorrectGemItemList(IStackable gem)
        {
            foreach (var gemListItem in GemList)
            {
                if (gemListItem.Type == gem.GetName())
                {
                    gem.SetGemListRef(gemListItem);
                    gemListItem.IncreaseCount();
                }
            }
        }
        
        #endregion
    }
}