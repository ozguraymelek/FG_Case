using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.ReferenceValue
{
    [CreateAssetMenu(menuName = "Ref Values/int", fileName = "new int", order = 0)]
    public class IntRef : RefValue
    {
        #region PROPERTIES
        
        [ShowInInspector]
        public int Value
        {
            get => _value;

            set
            {
                _value = value;
                
                if (OnValueChanged != null)
                    ValueHasChanged();
            }
        }
        
        private int _value;       
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        #endregion
    }
}