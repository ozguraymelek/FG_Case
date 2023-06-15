using nyy.FG_Case.FSMBuilder;
using nyy.FG_Case.PlayerSc;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.FSMImplement
{
    [CreateAssetMenu(menuName = "Finite State Machine/Action/Walk", fileName = "new Walk Data")]
    public class PlayerWalkAction : PlayerAction
    {
        #region PROPERTIES
        
        private static readonly int IDWalk = Animator.StringToHash("IsWalking");
        private static readonly int IDRun = Animator.StringToHash("IsRunning");
        
        [BoxGroup("Variables")] public FloatRef VerticalInput;
        [BoxGroup("Variables")] public FloatRef HorizontalInput;
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public override void Onset(Player ctx)
        {
            ctx.animator.SetBool(IDWalk, true);
            ctx.animator.SetBool(IDRun, false);
            
            // Physics.defaultMaxDepenetrationVelocity = ctx.walkSpeed / Mathf.Abs(Physics.gravity.y);
        }

        public override void Updating(Player ctx)
        {
            Walk(ctx);
            Rotate(ctx);
        }        
         
        #endregion  
                
        
        #region PRIVATE FUNCTIONS
        
        private void Walk(Player ctx)
        {
            ctx.rb.velocity = ctx.MovementVector.Value
                              * (ctx.walkSpeed * Time.fixedDeltaTime);
        }
        
        private void Rotate(Player ctx)
        {
            var direction = (Vector3.forward * VerticalInput.Value + Vector3.right * HorizontalInput.Value).normalized;
                    
            var rotGoal = Quaternion.LookRotation(direction);
                    
            ctx.transform.rotation = Quaternion.Slerp(ctx.transform.rotation, rotGoal, ctx.rotationSpeed * Time.deltaTime);
        }
        
        #endregion
    }
}