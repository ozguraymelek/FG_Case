using System;
using DG.Tweening;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using nyy.FG_Case.Utils;
using UnityEngine.UI;

namespace nyy.FG_Case.Observers
{
    public class CoinObserver : MonoBehaviour, IEventListener, IEventListener<Gem>
    {
        #region PROPERTIES

        [TabGroup("animation","UI Animation Components")] 
        public Image coinIconPrefab;
        [TabGroup("animation","UI Animation Components")] 
        public Image coinPanel;
        
        [TabGroup("animation","Components")] 
        public Camera camera;
        
        [TabGroup("UI Components")] 
        public TMP_Text textPlayerCoin;
        
        [TabGroup("Data")] 
        public IntRef playerCoin;
        
        [Title("Events")] 
        public ScriptableEvent OnPlayerCoinChanged;
        public ScriptableEvent<Gem> OnCoinMove3Dto2D;
        
        [Title("DoTween Settings")] 
        [SerializeField] private CoinMoveDoTweenSettings coinMoveDoTweenSettings;
        
        #endregion

        #region EVENT FUNCTIONS

        private void Start()
        {
            textPlayerCoin.text = playerCoin.Value.ToString();
        }

        private void OnEnable()
        {
            OnPlayerCoinChanged += this;
            OnCoinMove3Dto2D += this;
        }

        private void OnDisable()
        {
            OnPlayerCoinChanged -= this;
            OnCoinMove3Dto2D -= this;
        }

        #endregion

        #region IMPLEMENTED FUNCTIONS
        
        public void OnEventInvoked()
        {
            textPlayerCoin.text = playerCoin.Value.ToString();
        }

        public void OnEventInvoked(Gem argument)
        {
            CoinMoveAnimation(argument);
        }
        
        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS

        private void CoinMoveAnimation(Gem gem)
        {
            var pos = gem.transform.position;
            var viewportPoint = camera.WorldToScreenPoint(pos);
            
            var icon = Instantiate(coinIconPrefab, viewportPoint,
                Quaternion.identity, coinPanel.transform.parent);

            var tweenCoin = icon.transform.DOMove(coinPanel.transform.position, coinMoveDoTweenSettings.MoneyMoveDuration);
            tweenCoin.OnComplete(() => Destroy(icon.gameObject));
        }
        
        #endregion
    }
    
    [Serializable]
    public struct CoinMoveDoTweenSettings
    {
        [TabGroup("Money")] 
        public float MoneyMoveDuration;
    }
}