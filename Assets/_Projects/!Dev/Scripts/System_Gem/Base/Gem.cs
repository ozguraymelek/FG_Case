using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.System_Gem
{
    public class Gem : MonoBehaviour
    {
        #region PROPERTIES
        
        [TabGroup("Components")]
        [SerializeField] public BoxCollider collider;
        
        [Title("Interact Properties")] 
        [TabGroup("Settings")]
        public bool canGrow;
        [TabGroup("Settings")]
        public bool canStack;
        [TabGroup("Settings")]
        public bool isGrew;
        
        #endregion
                
        #region EVENT FUNCTIONS
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }

    public interface IStackable
    {
        void Stack(Transform parent);
    }
}