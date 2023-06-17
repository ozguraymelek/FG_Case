using System;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.Input
{
    public class InputController : MonoBehaviour, IEventListener
    {
        #region PROPERTIES
        [Title("Delegates")]
        public delegate float Calculator();
        public static Calculator Magnitude;
        
        [TabGroup("A","Events")]
        [InfoBox("Joystick takes the active input value the moment it is triggered.")]
        public ScriptableEvent OnJoystickTriggered;
        
        [TabGroup("B","Components")]
        public Joystick joystick;

        [TabGroup("B","Variables")]
        public FloatRef VerticalInput;
        [TabGroup("B","Variables")]
        public FloatRef HorizontalInput;
        [TabGroup("B","Variables")]
        public Vector3Ref MovementVector;
        #endregion
                
        #region EVENT FUNCTIONS
        
        private void OnEnable()
        {
            OnJoystickTriggered += this;
            Magnitude += CalculateMovementVectorMagnitude;
        }

        private void OnDisable()
        {
            OnJoystickTriggered -= this;
            Magnitude -= CalculateMovementVectorMagnitude;
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public void OnEventInvoked()
        {
            SetMovementDirection();
        }
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        private void SetMovementDirection()
        {
            HorizontalInput.Value = joystick.Horizontal;
            VerticalInput.Value = joystick.Vertical;

            MovementVector.Value = new Vector3(HorizontalInput.Value, 0f, VerticalInput.Value);
        }
        
        private float CalculateMovementVectorMagnitude()
        {
            return Mathf.Sqrt(Vector3.Dot(MovementVector.Value, MovementVector.Value));
        }
        
        #endregion

        
    }
}