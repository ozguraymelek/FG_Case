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
        private static readonly int ID = Animator.StringToHash("Speed");
        
        [BoxGroup("Variables")] public FloatRef VerticalInput;
        [BoxGroup("Variables")] public FloatRef HorizontalInput;
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public override void Onset(Player ctx)
        {
            Idle(ctx);
        }

        public override void Updating(Player ctx)
        {
            SetAnimation(ctx);
        }        
         
        #endregion  
                
        #region PRIVATE FUNCTIONS
        private void Idle(Player ctx)
        {
            ctx.rb.velocity = Vector3.zero;
        }
        
        private void SetAnimation(Player ctx)
        {
            if (Mathf.Abs(VerticalInput.Value) >= Mathf.Abs(HorizontalInput.Value))
                ctx.animator.SetFloat(ID, Mathf.Abs(VerticalInput.Value));

            if (Mathf.Abs(HorizontalInput.Value) > Mathf.Abs(VerticalInput.Value))
                ctx.animator.SetFloat(ID, Mathf.Abs(HorizontalInput.Value));
        }
        
        #endregion
    }
}