using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.ReferenceValue
{
    [CreateAssetMenu(menuName = "Ref Values/bool/un special", fileName = "new un special bool", order = 0)]
    public class UnSpBoolRef : BoolRef
    {
        #region PROPERTIES
        
        [ShowInInspector]
        public override bool Value
        {
            get => _value;
            
            set
            {
                _value = value;
                
                if (OnValueChanged != null)
                    ValueHasChanged();
            }
        }
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        #endregion
    }
}