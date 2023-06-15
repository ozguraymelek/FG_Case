using nyy.FG_Case.FSMBuilder;
using nyy.FG_Case.PlayerSc;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.FSMImplement
{
    [CreateAssetMenu(menuName = "Finite State Machine/Action/Idle", fileName = "new Idle Data")]
    public class PlayerIdleAction : PlayerAction
    {
        #region PROPERTIES
        private static readonly int IDWalk = Animator.StringToHash("IsWalking");
        private static readonly int IDRun = Animator.StringToHash("IsRunning");
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public override void Onset(Player ctx)
        {
            ctx.animator.SetBool(IDWalk, false); 
            ctx.animator.SetBool(IDRun, false); 
            
            Idle(ctx);
        }

        public override void Updating(Player ctx)
        {
            
        }        
         
        #endregion  
                
        #region PRIVATE FUNCTIONS
        private void Idle(Player ctx)
        {
            ctx.rb.velocity = Vector3.zero;
            // Physics.defaultMaxDepenetrationVelocity = 10f;
        }
        
        #endregion
    }
}