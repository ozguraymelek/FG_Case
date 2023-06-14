using nyy.FG_Case.Input;
using nyy.FG_Case.PlayerSc;
using UnityEngine;

namespace nyy.FG_Case
{
    [CreateAssetMenu(menuName = "Finite State Machine/Decision/Walk/to Run", fileName = "new Walk to Run Data")]
    public class PlayerWalkToRunDecision : PlayerDecision
    {
        #region PROPERTIES
         
        #endregion
                
        #region EVENT FUNCTIONS
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public override bool Decide(Player ctx)
        {
            var magnitudeOfMovementVector = InputController.Magnitude?.Invoke();
            var decide = magnitudeOfMovementVector is > .71f and <= 1f;
            
            return decide;
        }             
         
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}