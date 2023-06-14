using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.ReferenceValue
{
    [CreateAssetMenu(menuName = "Ref Values/float3", fileName = "new float3", order = 0)]
    public class Vector3Ref : RefValue
    {
        #region PROPERTIES
        
        [ShowInInspector]
        public Vector3 Value
        {
            get => _value;

            set
            {
                _value = value;
                
                if (OnValueChanged != null)
                    ValueHasChanged();
            }
        }
        
        private Vector3 _value;
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        #endregion
    }
}