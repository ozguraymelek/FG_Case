using System;
using DG.Tweening;
using GenericScriptableArchitecture;
using Lean.Pool;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Gem;
using nyy.FG_Case.System_Sale;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace nyy.FG_Case.PlayerSc
{
    public class Sale : MonoBehaviour, IEventListener<Gem>
    {
        #region PROPERTIES

        [TabGroup("Data")] 
        public PlayerData playerData;
        // public IntRef PlayerCoin;
        [TabGroup("Data")] 
        public IntRef CollectedAllGemAmount;
        
        [TabGroup("Stack Data")] 
        public RuntimeSet<Gem> CollectedGemSet;
        [TabGroup("Stack Data")] 
        public RuntimeSet<Gem> SoldGemSet;
        
        [TabGroup("Stack Data")]
        public RuntimeSet<GemListItem> GemListItemSetRuntime;
        
        [Title("Events")] 
        public ScriptableEvent<Gem,Transform> SaleAnimationEvent;
        public ScriptableEvent<Gem> PriceEvent;
        public ScriptableEvent<Gem> OnCoinMove3Dto2D;
        public ScriptableEvent OnPlayerCoinChanged;
        
        [Title("Save Events")] 
        public ScriptableEvent SavePlayerData;
        
        [TabGroup("C","DoTween Settings")]
        [SerializeField] private SaleDoTweenProperties saleDoTweenProperties;
        #endregion

        #region EVENT FUNCTIONS
        
        private void OnEnable()
        {
            PriceEvent += this;
        }

        private void OnDisable()
        {
            PriceEvent -= this;
            SoldGemSet.Clear();
        }

        #endregion

        #region IMPLEMENTED FUNCTIONS
        
        public void OnEventInvoked(Gem argument)
        {
            Price(argument);
        }
        
        #endregion

        #region PUBLIC FUNCTIONS

        public void SaleGem(Transform targetPos)
        {
            var currentGem = CollectedGemSet[^1];
            currentGem.transform.parent = null;
            currentGem.boxCollider.enabled = true;

            var tween = currentGem.transform.DOJump(targetPos.position + RandomPos(targetPos)
                , saleDoTweenProperties.DoJumpPower, saleDoTweenProperties.DoJumpNums,
                saleDoTweenProperties.DoJumpDuration);
            
            CollectedGemSet.Remove(currentGem);
            SoldGemSet.Add(currentGem);
            
            tween.OnComplete(() =>
            {
                CollectedAllGemAmount.Value -= 1;
                
                SaleAnimationEvent.Invoke(currentGem, targetPos);
                
                OnCoinMove3Dto2D.Invoke(currentGem);
                
                DestroySoldGem(currentGem);
            });
        }
        #endregion

        #region PRIVATE FUNCTIONS

        private void Price(Gem gem)
        {
            playerData.PlayerCoin += CalculatePrice(gem);
            OnPlayerCoinChanged.Invoke();
            SavePlayerData.Invoke();
        }
        
        private void DestroySoldGem(Gem gem)
        {
            gem.gameObject.SetActive(false);
            
            gem.transform.localScale = Vector3.zero;
            
            gem.isStacked = false;
            gem.isGrew = false;
            gem.canStackable = false;
            gem.canGrow = false;
            
            LeanPool.Despawn(gem);
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