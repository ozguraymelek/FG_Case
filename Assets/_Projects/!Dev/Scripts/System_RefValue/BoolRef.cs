using UnityEngine;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;

namespace nyy.FG_Case
{
    [CreateAssetMenu(menuName = "Ref Values/bool", fileName = "new bool", order = 0)]
    public class BoolRef : RefValue
    {
        #region PROPERTIES
        
        [ShowInInspector]
        public bool Value
        {
            get => _value;

            set
            {
                _value = value;
                
                if (OnValueChanged != null)
                    ValueHasChanged();
            }
        }
        
        private bool _value;
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        #endregion
    }
}