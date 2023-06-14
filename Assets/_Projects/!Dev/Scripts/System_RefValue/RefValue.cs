using System;
using GenericScriptableArchitecture;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.ReferenceValue
{
    public class RefValue : SerializedScriptableObject
    {
        #region PROPERTIES

        [InfoBox("You can add an event that will be triggered when this reference value changes")]
        public ScriptableEvent OnValueChanged;

        #endregion

        #region PUBLIC FUNCTIONS

        protected void ValueHasChanged()
        {
            OnValueChanged.Invoke();
        }
        
        #endregion
    }
}