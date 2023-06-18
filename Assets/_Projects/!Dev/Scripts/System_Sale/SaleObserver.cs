using System;
using DG.Tweening;
using GenericScriptableArchitecture;
using nyy.FG_Case.Observers;
using nyy.FG_Case.PlayerSc;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace nyy.FG_Case.System_Sale
{
    public class SaleObserver : MonoBehaviour, IEventListener<Gem,Transform>
    {
        #region PROPERTIES
        
        [TabGroup("animation","UI Animation Components")] 
        public TextMeshPro textEarnedMoneyPopUp;
        
        [Title("Events")] 
        public ScriptableEvent<Gem,Transform> SaleAnimationEvent;
        
        [Title("DoTween Settings")] 
        [SerializeField] private PopUpDoTweenSettings popUpDoTweenSettings;
        
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
        
        public void OnEventInvoked(Gem arg0, Transform arg1)
        {
            Observer(arg0, arg1);
        }
        
        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS
        
        private void Observer(Gem gem, Transform transform)
        {
            var popUpText = Instantiate(textEarnedMoneyPopUp, transform);
            popUpText.text = $"+ {Sale.CalculatePrice(gem)}";
            // popUpText.faceColor = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1), 1);
            popUpText.rectTransform.localPosition = new Vector3(0, -.2f, 0f);
            
            var tweenPopUpMove = popUpText.rectTransform.DOLocalMove(popUpDoTweenSettings.PopUpMoveTargetPosition,
                popUpDoTweenSettings.PopUpMoveDuration);
            
            var tweenPopUpRotate =
                popUpText.transform.DORotate(popUpDoTweenSettings.PopUpRotateTargetRotation, 
                    popUpDoTweenSettings.PopUpRotateDuration, RotateMode.FastBeyond360);

            tweenPopUpRotate.OnComplete(() => Destroy(popUpText.gameObject));
        }
        
        #endregion
    }
    
    [Serializable]
    public struct PopUpDoTweenSettings
    {
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