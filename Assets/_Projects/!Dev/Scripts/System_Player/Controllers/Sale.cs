using System;
using DG.Tweening;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.PlayerSc
{
    public class Sale : MonoBehaviour
    {
        #region PROPERTIES

        [TabGroup("Data")] 
        public IntRef PlayerCoin;
        [TabGroup("Data")] 
        public IntRef CollectedAllGemAmount;
        
        [TabGroup("Stack Data")] 
        public RuntimeSet<Gem> CollectedGemSet;
        
        [TabGroup("Settings")]
        public bool canSale;
        
        [Title("Events")] 
        public ScriptableEvent<Gem,Transform> SaleAnimationEvent;
        public ScriptableEvent<Gem> OnCoinMove3Dto2D;
        
        [TabGroup("C","DoTween Settings")]
        [SerializeField] private SaleDoTweenProperties saleDoTweenProperties;
        #endregion

        #region EVENT FUNCTIONS

        #endregion

        #region IMPLEMENTED FUNCTIONS

        #endregion

        #region PUBLIC FUNCTIONS

        public void SaleGem(Transform targetPos)
        {
            var currentGem = CollectedGemSet[^1];
            currentGem.transform.parent = null;

            var tween = currentGem.transform.DOJump(targetPos.position + RandomPos(targetPos)
                , saleDoTweenProperties.DoJumpPower, saleDoTweenProperties.DoJumpNums,
                saleDoTweenProperties.DoJumpDuration);
            
            CollectedGemSet.Remove(currentGem);
            
            tween.OnComplete(() =>
            {
                CollectedAllGemAmount.Value -= 1;
                Price(currentGem, targetPos);
            });
        }
        #endregion

        #region PRIVATE FUNCTIONS

        private void Price(Gem gem, Transform transform)
        {
            PlayerCoin.Value += CalculatePrice(gem);
            
            SaleAnimationEvent.Invoke(gem, transform);
            
            OnCoinMove3Dto2D.Invoke(gem);
        }

        public static int CalculatePrice(Gem gem)
        {
            var earnedMoney = Mathf.RoundToInt(gem.GemProperties.StartingSalePrice +
                                          gem.transform.localScale.x * 100);

            return earnedMoney;
        }
        
        private Vector3 RandomPos(Transform targetPos)
        {
            var randX = UnityEngine.Random.Range(-targetPos.transform.localScale.x, targetPos.transform.localScale.x);
            var randZ = UnityEngine.Random.Range(-targetPos.transform.localScale.x, targetPos.transform.localScale.z);

            var randomPos = new Vector3(randX, 0f, randZ);

            return randomPos;
        }
        
        #endregion
    }
    
    [Serializable]
    public struct SaleDoTweenProperties
    {
        [Title("Properties")] 
        [TabGroup("B","DOJump")]
        public float DoJumpPower;
        [TabGroup("B","DOJump")]
        public int DoJumpNums;
        [TabGroup("B","DOJump")]
        public float DoJumpDuration;
    }
}