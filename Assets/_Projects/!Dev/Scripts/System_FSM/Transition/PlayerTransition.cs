using nyy.FG_Case.PlayerSc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.FSMBuilder
{
    [CreateAssetMenu(menuName = "Finite State Machine/Transition")]
    public sealed class PlayerTransition : SerializedScriptableObject
    {
        #region PROPERTIES
        
        [Title("Select decision for transition")]
        public PlayerDecision Decision;

        [Title("Select state for transition or stay")]
        public PlayerBaseState NewState;
        public PlayerBaseState StayState;
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        public void Execute(Player ctx)
        {
            if (Decision.Decide(ctx) && NewState is not PlayerRemainInState)
                ctx.CurrentState = NewState;
            
            else if (Decision.Decide(ctx) == false && StayState is not PlayerRemainInState)
                ctx.CurrentState = StayState;
        }
        
        #endregion 
    }
}