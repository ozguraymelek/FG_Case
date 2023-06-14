using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.System_Gem
{
    public class Gem : MonoBehaviour, IStackable
    {
        #region PROPERTIES

        [BoxGroup] 
        public GemObjectData GemData;
        
        [TabGroup("X", "Data")] 
        public GemProperties GemProperties;
        
        [TabGroup("A","Components")]
        [SerializeField] public BoxCollider collider;
        
        [Title("Properties")] 
        [TabGroup("B","Stack Settings")]
        public IntRef CollectedAllGemAmount;
        [TabGroup("B","Stack Settings")]
        public bool isStacked = false;
        
        [Title("Properties")] 
        [TabGroup("B","Interact Settings")]
        public bool canGrow;
        [TabGroup("B","Interact Settings")]
        public bool canStackable;
        [TabGroup("B","Interact Settings")]
        public bool isGrew;
        
        [TabGroup("Runtime Set")]
        public RuntimeSet<Gem> CollectedGemSet;

        [TabGroup("C","DoTween Settings")]
        [SerializeField] private GemDoTweenProperties gemDoTweenProperties;
        
        #endregion
                
        #region EVENT FUNCTIONS

        private void OnDisable()
        {
            CollectedGemSet.Clear();
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public async void Stack(Transform parent)
        {
            await UniTask.WhenAll(Move(parent, this.GetCancellationTokenOnDestroy()));
            
            SetParent(parent);
            
            Processes(this);
        }

        public void SetGrowable(bool state)
        {
            canGrow = state;
        }

        public bool IsStacked()
        {
            return isStacked;
        }

        public bool CanStackable()
        {
            return canStackable;
        }
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        protected List<UniTask> Move(Transform target, CancellationToken ct)
        {
            var taskList = new List<UniTask>();

            var seq = DOTween.Sequence();

            seq.Append(transform.DOJump(target.position + new Vector3(0, 0.25f * CollectedAllGemAmount.Value, 0), gemDoTweenProperties.
                    DoJumpPower,
                gemDoTweenProperties.DoJumpNums, gemDoTweenProperties.DoJumpDuration)).OnUpdate(() =>
            {
                
            });
            
            // SpawnedGemFromPool.Remove(this);
                
            taskList.Add(seq.ToUniTask(cancellationToken: ct));

            return taskList;
        }
        
        protected void SetParent(Transform parent)
        {
            transform.parent = parent;
            transform.localPosition = new Vector3(0, 0.25f * CollectedAllGemAmount.Value, 0);
            transform.localEulerAngles = Vector3.zero;
        }
        
        protected void Processes(Gem gem)
        {
            isStacked = true;
            
            CollectedAllGemAmount.Value++;
            
            CollectedGemSet.Add(gem);

            collider.enabled = false;
        }
        
        #endregion
    }

    [Serializable]
    public enum Type
    {
        Green, Yellow, Pink
    }
    
    [Serializable]
    public struct GemDoTweenProperties
    {
        [Title("Properties")] 
        [TabGroup("B","DORotate")]
        public Vector3 DoRotateEndValue;
        [TabGroup("B","DORotate")]
        public float DoRotateDuration;
             
        [Title("Properties")] 
        [TabGroup("B","DOJump")]
        public float DoJumpPower;
        [TabGroup("B","DOJump")]
        public int DoJumpNums;
        [TabGroup("B","DOJump")]
        public float DoJumpDuration;
    }

    public interface IStackable
    {
        void Stack(Transform parent);
        void SetGrowable(bool state);
        bool IsStacked();
        bool CanStackable();
    }
}