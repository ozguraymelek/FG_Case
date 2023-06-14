using nyy.FG_Case.PlayerSc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case
{
    public abstract class PlayerDecision : SerializedScriptableObject
    {
        #region PUBLIC FUNCTIONS
        
        public abstract bool Decide(Player ctx);
        
        #endregion  
    }
}