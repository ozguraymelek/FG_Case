using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace nyy.FG_Case.System_Gem
{
    public class Growing : MonoBehaviour
    {
        #region PROPERTIES
        
        [TabGroup("B","Tweens")]
        [Title("Grow Up Tween")] 
        private Tween _growUpTween;
        
        [TabGroup("C","Growing DoT Settings")]
        [SerializeField] private GrowingSettings growingSettings;
        
        #endregion
                
        #region EVENT FUNCTIONS

        private void Start()
        {
            StartGrowing();
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        public void StopGrowing()
        {
            var uid = Guid.NewGuid();
            _growUpTween.id = uid;

            DOTween.Kill(uid);
        }
        
        public void CheckGrowing(Gem gem)
        {
            this.ObserveEveryValueChanged(_ => transform.localScale)
                .Subscribe(unit =>
                {
                    gem.canStackable = transform.localScale.x >= .25f;
                    gem.isGrew = transform.localScale.x >= 1f;
                    gem.canGrow = transform.localScale.x < 1f;
                });
        }
        
        #endregion  
                
        #region PRIVATE FUNCTIONS

        private void StartGrowing()
        {
            _growUpTween = transform.DOScale(growingSettings.GrowUpMaxValue, growingSettings.GrowUpDuration);
        }
        
        #endregion
    }
    
    [Serializable]
    public struct GrowingSettings
    {
        [Title("Grow Up Properties")] 
        [TabGroup("B","Process Settings")]
        public Vector3 GrowUpMaxValue;
        [TabGroup("B","Process Settings")]
        public float GrowUpDuration;
    }
}