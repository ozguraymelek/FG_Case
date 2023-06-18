using System;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

namespace nyy.FG_Case
{
    [CreateAssetMenu(menuName = "Data/Player", fileName = "new Player Data")]
    public class PlayerData : SerializedScriptableObject
    {
        #region PROPERTIES

        [Title("Settings")]
        public int PlayerCoin;

        [Title("Game Settings")] 
        public bool soundEffects = true;

        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS

        #endregion
    }
}