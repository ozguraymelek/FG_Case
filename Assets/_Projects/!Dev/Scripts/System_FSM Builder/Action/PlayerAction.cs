using nyy.FG_Case.PlayerSc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.FSMBuilder
{
    public abstract class PlayerAction : SerializedScriptableObject
    {
        #region PUBLIC FUNCTIONS
        
        public virtual void Onset(Player ctx){}
        public virtual void Updating(Player ctx){}
        
        #endregion 
    }
}