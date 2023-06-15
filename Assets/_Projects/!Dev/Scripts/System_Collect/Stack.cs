using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.PlayerSc
{
    public class Stack : MonoBehaviour
    {
        #region PROPERTIES

        [TabGroup("Components")] 
        public Transform stackHolder;
        
        #endregion
                
        #region EVENT FUNCTIONS
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IStackable gem) == false) return;
            if (gem.IsStacked() == true) return;
            if (gem.CanStackable() == false) return;
                
            gem.Stack(stackHolder);
            // LastStackedGem = other.GetComponent<Gem>();
            gem.SetGrowable(false);
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