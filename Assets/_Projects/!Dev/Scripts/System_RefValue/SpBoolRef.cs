using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.ReferenceValue
{
    [CreateAssetMenu(menuName = "Ref Values/bool/special", fileName = "new special bool", order = 0)]
    public class SpBoolRef : BoolRef
    {
        #region PROPERTIES

        [InfoBox("If set to true, the 'OnValueChanged' event will invoke if 'Value' is true")]
        [BoxGroup]
        public bool EventActivateCondition;
        
        [ShowInInspector][BoxGroup]
        public override bool Value
        {
            get => _value;
            
            set
            {
                _value = value;
                
                if (OnValueChanged != null && _value == EventActivateCondition)
                    ValueHasChanged();
            }
        }
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        #endregion
    }
}