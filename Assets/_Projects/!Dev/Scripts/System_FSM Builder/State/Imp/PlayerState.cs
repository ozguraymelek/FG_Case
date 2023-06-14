using System.Collections.Generic;
using nyy.FG_Case.PlayerSc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.FSMBuilder
{
    [CreateAssetMenu(menuName = "Finite State Machine/States/State", fileName = "new Player State")]
    public class PlayerState : PlayerBaseState
    {
        #region PROPERTIES
        
        [Title("All actions for this state")]
        public List<PlayerAction> Actions = new List<PlayerAction>();
        
        [Title("All transitions for this state")]
        public List<PlayerTransition> Transitions = new List<PlayerTransition>();
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public override void Onset(Player ctx)
        {
            foreach (var action in Actions)
            {
                action.Onset(ctx);
            }
        }

        public override void Updating(Player ctx)
        {
            foreach (var action in Actions)
            {
                action.Updating(ctx);
            }
            
            foreach (var transition in Transitions)
            {
                transition.Execute(ctx);
            }
        }
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}