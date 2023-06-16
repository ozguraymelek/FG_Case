using System;
using DG.Tweening;
using GenericScriptableArchitecture;
using nyy.FG_Case.Observers;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace nyy.FG_Case.System_Sale
{
    public class SaleObserver : MonoBehaviour, IEventListener<Gem,Transform>
    {
        #region PROPERTIES
        
        // [TabGroup("animation","UI Animation Components")] 
        // public Image moneyIcon;
        // [TabGroup("animation","UI Animation Components")] 
        // public Image moneyPanel;
        [TabGroup("animation","UI Animation Components")] 
        public TextMeshPro textEarnedMoneyPopUp;
        
        [TabGroup("animation","Components")] 
        public Camera camera;
        
        [Title("Events")] 
        public ScriptableEvent<Gem,Transform> SaleAnimationEvent;
        
        [Title("DoTween Settings")] 
        [SerializeField] private AnimationDoTweenSettings animationDoTweenSettings;
        
        #endregion

        #region EVENT FUNCTIONS

        private void OnEnable()
        {
            SaleAnimationEvent += this;
        }
        
        private void OnDisable()
        {
            SaleAnimationEvent -= this;
        }

        #endregion

        #region IMPLEMENTED FUNCTIONS

        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS
        
        private void Animation(Gem gem, Transform transform)
        {
            // var pos = gem.transform.position;
            // var viewportPoint = Camera.main.WorldToScreenPoint(pos);
            //
            // var icon = Instantiate(moneyIcon, viewportPoint,
            //     Quaternion.identity, moneyPanel.transform.parent);

            // var tweenMoney = icon.transform.DOMove(moneyPanel.transform.position, animationDoTweenSettings.MoneyMoveDuration);
            // tweenMoney.OnComplete(() => Destroy(icon.gameObject));
            
            var popUpText = Instantiate(textEarnedMoneyPopUp, transform);
            popUpText.text = $"+ {Sale.CalculatePrice(gem)}";
            popUpText.rectTransform.localPosition = new Vector3(0, -.2f, 0f);
            
            var tweenPopUpMove = popUpText.rectTransform.DOLocalMove(animationDoTweenSettings.PopUpMoveTargetPosition,
                animationDoTweenSettings.PopUpMoveDuration);
            
            var tweenPopUpRotate =
                popUpText.transform.DORotate(animationDoTweenSettings.PopUpRotateTargetRotation, 
                    animationDoTweenSettings.PopUpRotateDuration, RotateMode.FastBeyond360);
            
            tweenPopUpRotate.OnComplete(() => Destroy(popUpText.gameObject));
        }
        
        #endregion

        public void OnEventInvoked(Gem arg0, Transform arg1)
        {
            Animation(arg0, arg1);
        }
    }
    
    [Serializable]
    public struct AnimationDoTweenSettings
    {
        [TabGroup("Money")] 
        public float MoneyMoveDuration;
            
        [Title("Move")]
        [TabGroup("Pop-up")] 
        public Vector3 PopUpMoveTargetPosition;
        [TabGroup("Pop-up")]
        public float PopUpMoveDuration;
        [Title("Rotate")]
        [TabGroup("Pop-up")]
        public Vector3 PopUpRotateTargetRotation;
        [TabGroup("Pop-up")]
        public float PopUpRotateDuration;
    }
}