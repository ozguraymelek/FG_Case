using GenericScriptableArchitecture;
using nyy.FG_Case.Input;
using nyy.FG_Case.PlayerSc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.FSMImplement
{
    [CreateAssetMenu(menuName = "Finite State Machine/Decision/Idle/to Walk", fileName = "new Idle to Walk Data")]
    public class PlayerIdleToWalkDecision : PlayerDecision
    {
        #region IMPLEMENTED FUNCTIONS
        
        public override bool Decide(Player ctx)
        {
            var magnitudeOfMovementVector = InputController.Magnitude?.Invoke();
            var decide = magnitudeOfMovementVector is < .71f and > 0f;
            
            return decide;
        }             
         
        #endregion
    }
}