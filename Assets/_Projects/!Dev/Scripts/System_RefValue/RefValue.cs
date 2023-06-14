using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.ReferenceValue
{
    [CreateAssetMenu(menuName = "...", fileName = "...")]
    public class RefValue : SerializedScriptableObject
    {
        #region PROPERTIES

        public event Action OnValueChanged;

        #endregion

        #region PUBLIC FUNCTIONS

        public void ValueHasChanged()
        {
            OnValueChanged?.Invoke();
        }
        #endregion
    }
}