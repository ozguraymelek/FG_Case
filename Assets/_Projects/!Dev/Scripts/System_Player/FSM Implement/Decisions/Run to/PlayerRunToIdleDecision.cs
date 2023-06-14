using nyy.FG_Case.Input;
using nyy.FG_Case.PlayerSc;
using UnityEngine;

namespace nyy.FG_Case.FSMImplement
{
    [CreateAssetMenu(menuName = "Finite State Machine/Decision/Run/to Idle", fileName = "new Run to Idle Data")]
    public class PlayerRunToIdleDecision : PlayerDecision
    {
        #region IMPLEMENTED FUNCTIONS
        
        public override bool Decide(Player ctx)
        {
            var magnitudeOfMovementVector = InputController.Magnitude?.Invoke();
            var decide = magnitudeOfMovementVector is 0f;
                
            return decide;
        }             
         
        #endregion  
    }
}