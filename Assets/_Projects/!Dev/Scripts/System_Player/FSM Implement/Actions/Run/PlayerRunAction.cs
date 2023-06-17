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

        private static readonly int ID = Animator.StringToHash("Speed");

        [BoxGroup("Variables")] public FloatRef VerticalInput;
        [BoxGroup("Variables")] public FloatRef HorizontalInput;

        #endregion

        #region IMPLEMENTED FUNCTIONS
        
        public override void Updating(Player ctx)
        {
            Run(ctx);
            Rotate(ctx);

            SetAnimation(ctx);
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
            if (HorizontalInput.Value == 0 && VerticalInput.Value == 0) return;

            var direction = (Vector3.forward * VerticalInput.Value + Vector3.right * HorizontalInput.Value).normalized;

            var rotGoal = Quaternion.LookRotation(direction);

            ctx.transform.rotation = Quaternion.Slerp(ctx.transform.rotation, rotGoal,
                ctx.rotationSpeed * .5f * Time.deltaTime);
        }

        private void SetAnimation(Player ctx)
        {
            if (Mathf.Abs(VerticalInput.Value) > Mathf.Abs(HorizontalInput.Value))
                ctx.animator.SetFloat(ID, Mathf.Abs(VerticalInput.Value));

            if (Mathf.Abs(HorizontalInput.Value) > Mathf.Abs(VerticalInput.Value))
                ctx.animator.SetFloat(ID, Mathf.Abs(HorizontalInput.Value));
        }

        #endregion
    }
}