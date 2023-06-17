using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.ReferenceValue
{
    public class ValueSetter : MonoBehaviour
    {
        #region PROPERTIES
        
        [SerializeField] private SetExecution Execution;
        [SerializeField, Required] private RefValue Reference;

        [SerializeField, ShowIf("@Reference is FloatRef"), LabelText("Value"), Indent]
        private float FloatValue;

        [SerializeField, ShowIf("@Reference is BoolRef"), LabelText("Value"), Indent]
        private bool BoolValue;
        
        [SerializeField, ShowIf("@Reference is IntRef"), LabelText("Value"), Indent]
        private int IntValue;
        
        #endregion
                
        #region EVENT FUNCTIONS
        
        private void Awake()
        {
            if (Execution != SetExecution.Awake)
                SetValue();
        }

        private void Start()
        {
            if(Execution == SetExecution.Start)
                SetValue();
        }

        private void OnEnable()
        {
            if(Execution == SetExecution.OnEnable)
                SetValue();
        }
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        private void SetValue()
        {
            switch (Reference)
            {
                case FloatRef floatRef:
                    floatRef.Value = FloatValue;
                    break;
                case BoolRef boolRef:
                    boolRef.Value = BoolValue;
                    break;
                case IntRef intRef:
                    intRef.Value = IntValue;
                    break;
            }
        }
        #endregion
    }
    
    public enum SetExecution
    {
        Awake,
        Start,
        OnEnable
    }
}