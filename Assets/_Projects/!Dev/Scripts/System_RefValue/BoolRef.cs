using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.ReferenceValue
{
    public abstract class BoolRef : RefValue
    {
        #region PROPERTIES
        
        public abstract bool Value { get; set; }
        
        protected bool _value;
        
        #endregion
        
        #region PUBLIC FUNCTIONS
        
        #endregion
    }
}