using nyy.FG_Case.PlayerSc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.FSMBuilder
{
    public abstract class PlayerAction : SerializedScriptableObject
    {
        #region PUBLIC FUNCTIONS
        
        public abstract void Onset(Player ctx);
        public abstract void Updating(Player ctx);
        
        #endregion 
    }
}