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
        public RuntimeSet<Gem> SoldGemSet;
        
        [TabGroup("Stack Data")]
        public RuntimeSet<GemListItem> GemListItemSetRuntime;
        
        [Title("Events")] 
        public ScriptableEvent<Gem,Transform> SaleAnimationEvent;
        public ScriptableEvent<Gem> OnCoinMove3Dto2D;
        public ScriptableEvent<Gem> OnGemSold;
        
        [Title("Save Events")] public ScriptableEvent SavePlayerCoin;
        
        [TabGroup("C","DoTween Settings")]
        [SerializeField] private SaleDoTweenProperties saleDoTweenProperties;
        #endregion

        #region EVENT FUNCTIONS

        private void OnDisable()
        {
            SoldGemSet.Clear();
        }

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
            
            // FindCorrectGemItemList(currentGem);
            CollectedGemSet.Remove(currentGem);
            
            tween.OnComplete(() =>
            {
                CollectedAllGemAmount.Value -= 1;
                Price(currentGem, targetPos);
                SoldGemSet.Add(currentGem);

                DestroySoldGem(currentGem);
            });
        }
        #endregion

        #region PRIVATE FUNCTIONS

        private void Price(Gem gem, Transform transform)
        {
            PlayerCoin.Value += CalculatePrice(gem);
            // SavePlayerCoin.Invoke();
            
            SaleAnimationEvent.Invoke(gem, transform);
            
            OnCoinMove3Dto2D.Invoke(gem);
        }

        private void FindCorrectGemItemList(IStackable gem)
        {
            foreach (var gemListItem in GemListItemSetRuntime)
            {
                if (gemListItem.Type == gem.GetName())
                {
                    gem.SetGemListRef(gemListItem);
                    gemListItem.DecreaseCount();
                }
            }
        }

        private void DestroySoldGem(Gem gem)
        {
            OnGemSold.Invoke(gem);
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