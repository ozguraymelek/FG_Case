using System;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace nyy.FG_Case.System_Sale
{
    public class SaleArea : MonoBehaviour
    {
        #region PROPERTIES

        [TabGroup("B", "Global")] 
        public FloatRef SalesDelayTime;
        
        [TabGroup("Stack Data")] 
        public RuntimeSet<Gem> CollectedGemSet;
        
        [Title("Settings")] 
        private float _stayedTime;
        #endregion
                
        #region EVENT FUNCTIONS

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Sale player) == false) return;

            player.canSale = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Sale player) == false) return;
            if (CollectedGemSet.Count == 0) return;

            _stayedTime += Time.deltaTime;

            if (_stayedTime < SalesDelayTime.Value)
                return;
            
            player.SaleGem(transform);

            _stayedTime = 0;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Sale player) == false) return;

            player.canSale = false;
            _stayedTime = 0;
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}