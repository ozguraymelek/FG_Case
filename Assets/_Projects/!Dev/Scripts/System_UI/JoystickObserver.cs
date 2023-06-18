using System;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace nyy.FG_Case.System_UI
{
    public class JoystickObserver : MonoBehaviour, IEventListener
    {
        #region PROPERTIES
        
        [BoxGroup("Variables")] public FloatRef VerticalInput;
        [BoxGroup("Variables")] public FloatRef HorizontalInput;

        public ScriptableEvent JoystickEffect;

        [Header("Ref")] 
        private Image _activeEffect;

        [BoxGroup] public List<Image> effects;
        
        #endregion
                
        #region EVENT FUNCTIONS
        
        private void OnEnable()
        {
            JoystickEffect += this;
        }
        
        private void OnDisable()
        {
            JoystickEffect -= this;
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public void OnEventInvoked()
        {
            Effect();
        }
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS

        private void Effect()
        {
            if(_activeEffect != null) _activeEffect.color = Color.black;
            
            if (HorizontalInput.Value == 0 && VerticalInput.Value == 0)
            {
                foreach (var effect in effects)
                {
                    effect.color = Color.black;
                }
            }
            
            if (HorizontalInput.Value > 0 && VerticalInput.Value > 0)
            {
                _activeEffect = effects[0]; //top right
                _activeEffect.color = Color.blue;
            }
            
            else if (HorizontalInput.Value < 0 && VerticalInput.Value > 0)
            {
                _activeEffect = effects[1]; //top left
                _activeEffect.color = Color.blue;
            }
            
            else if (HorizontalInput.Value > 0 && VerticalInput.Value < 0)
            {
                _activeEffect = effects[2]; //down right
                _activeEffect.color = Color.blue;
            }
            
            else if (HorizontalInput.Value < 0 && VerticalInput.Value < 0)
            {
                _activeEffect = effects[3]; //down left
                _activeEffect.color = Color.blue;
            }
        }
        
        #endregion
    }
}