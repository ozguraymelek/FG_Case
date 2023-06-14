using nyy.FG_Case.PlayerSc;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nyy.FG_Case.FSMBuilder
{
    public class PlayerBaseState : SerializedScriptableObject
    {
        #region PUBLIC FUNCTIONS
        
        public virtual void Onset(Player ctx){} //event func. Start()
        public virtual void Updating(Player ctx){} //event func. Update()
        
        #endregion
    }
}