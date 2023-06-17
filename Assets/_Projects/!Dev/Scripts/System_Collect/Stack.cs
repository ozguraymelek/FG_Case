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
        
        [TabGroup("Gem Item List")]
        public RuntimeSet<GemListItem> GemListItemSetRuntime;

        [TabGroup("Components")] 
        public Transform stackHolder;

        #endregion
                
        #region EVENT FUNCTIONS

        private void Start()
        {
            FindGemListItems();
        }
        
        private void FindGemListItems()
        {
            var tempGemListItems = FindObjectsOfType<GemListItem>().ToList();
            
            foreach (var tempGemListItem in tempGemListItems)
            {
                GemListItemSetRuntime.Add(tempGemListItem);
            }
        }

        private void OnDisable()
        {
            GemListItemSetRuntime.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IStackable gem) == false) return;
            if (gem.IsStacked() == true) return;
            if (gem.CanStackable() == false) return;
            
            gem.Stack(stackHolder);
            
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
            foreach (var gemListItem in GemListItemSetRuntime)
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