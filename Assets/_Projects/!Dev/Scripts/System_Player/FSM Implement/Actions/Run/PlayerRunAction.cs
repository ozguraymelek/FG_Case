using nyy.FG_Case.FSMBuilder;
using nyy.FG_Case.PlayerSc;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.FSMImplement
{
    [CreateAssetMenu(menuName = "Finite State Machine/Action/Run", fileName = "new Run Data")]
    public class PlayerRunAction : PlayerAction
    {
        #region PROPERTIES
        
        private static readonly int IDRun = Animator.StringToHash("IsRunning");
        private static readonly int IDWalk = Animator.StringToHash("IsWalking");
         
        [BoxGroup("Variables")] public FloatRef VerticalInput;
        [BoxGroup("Variables")] public FloatRef HorizontalInput;
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public override void Onset(Player ctx)
        {
            ctx.animator.SetBool(IDRun, true); 
            ctx.animator.SetBool(IDWalk, false); 
        }

        public override void Updating(Player ctx)
        {
                Run(ctx);
                Rotate(ctx);
        }        
         
        #endregion   
                
        #region PRIVATE FUNCTIONS
        
        private void Run(Player ctx)
        {
            ctx.rb.velocity = ctx.MovementVector.Value
                                                      * (ctx.runSpeed * Time.fixedDeltaTime);
        }
            
        private void Rotate(Player ctx)
        {
            var direction = (Vector3.forward * VerticalInput.Value + Vector3.right * HorizontalInput.Value).normalized;
                    
            var rotGoal = Quaternion.LookRotation(direction);

            ctx.transform.rotation = Quaternion.Slerp(ctx.transform.rotation, rotGoal,
                ctx.rotationSpeed * .5f * Time.deltaTime);
        }
        
        #endregion
    }
}