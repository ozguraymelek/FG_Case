using nyy.FG_Case.Input;
using nyy.FG_Case.PlayerSc;
using UnityEngine;

namespace nyy.FG_Case.FSMImplement
{
    [CreateAssetMenu(menuName = "Finite State Machine/Decision/Walk/to Idle", fileName = "new Walk to Idle Data")]
    public class PlayerWalkToIdleDecision : PlayerDecision
    {
        #region PROPERTIES
         
        #endregion
                
        #region EVENT FUNCTIONS
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public override bool Decide(Player ctx)
        {
            var magnitudeOfMovementVector = InputController.Magnitude?.Invoke();
            var decide = magnitudeOfMovementVector is 0f;
                
            return decide;
        }             
         
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}