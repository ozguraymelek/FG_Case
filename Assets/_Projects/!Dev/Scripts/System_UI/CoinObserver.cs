using System;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using nyy.FG_Case.Utils;

namespace nyy.FG_Case.Observers
{
    public class CoinObserver : MonoBehaviour, IEventListener
    {
        #region PROPERTIES

        [TabGroup("UI Components")] 
        public TMP_Text textPlayerCoin;

        [TabGroup("Data")] 
        public IntRef playerCoin;
        
        [TabGroup("Data")] 
        public ScriptableEvent OnPlayerCoinChanged;

        #endregion

        #region EVENT FUNCTIONS

        private void Start()
        {
            textPlayerCoin.text = playerCoin.Value.ToString();
        }

        private void OnEnable()
        {
            OnPlayerCoinChanged += this;
        }

        private void OnDisable()
        {
            OnPlayerCoinChanged -= this;
        }

        #endregion

        #region IMPLEMENTED FUNCTIONS

        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS

        #endregion

        public void OnEventInvoked()
        {
            textPlayerCoin.text = playerCoin.Value.ToString();
        }
    }
}