using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case
{
    [CreateAssetMenu(menuName = "Ref Values/float", fileName = "new float", order = 0)]
    public class FloatRef : RefValue
    {
        #region PROPERTIES
        
        [ShowInInspector]
        public float Value
        {
            get => _value;

            set
            {
                _value = value;
                
                if (OnValueChanged != null)
                    ValueHasChanged();
            }
        }

        private float _value;
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
    }
}