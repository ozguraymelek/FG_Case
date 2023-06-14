using nyy.FG_Case.Input;
using nyy.FG_Case.PlayerSc;
using UnityEngine;

namespace nyy.FG_Case.FSMImplement
{
    [CreateAssetMenu(menuName = "Finite State Machine/Decision/Idle/to Run", fileName = "new Idle to Run Data")]
    public class PlayerIdleToRunDecision : PlayerDecision
    {
        #region IMPLEMENTED FUNCTIONS
        
        public override bool Decide(Player ctx)
        {
            var magnitudeOfMovementVector = InputController.Magnitude?.Invoke();
            var decide = magnitudeOfMovementVector is > .71f and <= 1f;
            
            return decide;
        }             
         
        #endregion
    }
}