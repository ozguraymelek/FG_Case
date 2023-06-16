using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GenericScriptableArchitecture;
using nyy.FG_Case.Extensions;
using nyy.FG_Case.PlayerSc;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Data;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace nyy.FG_Case.System_Gem
{
    public class Gem : MonoBehaviour, IStackable
    {
        #region PROPERTIES

        [BoxGroup] public GemObjectData GemData;
        [BoxGroup] public Growing Growing;

        [TabGroup("X", "Data")] public GemProperties GemProperties;

        [TabGroup("A", "Components")] [SerializeField]
        public BoxCollider collider;

        [Title("Properties")] [TabGroup("B", "Stack Settings")]
        public IntRef CollectedAllGemAmount;

        [TabGroup("B", "Stack Settings")] public bool isStacked = false;

        [Title("Properties")] [TabGroup("B", "Interact Settings")]
        public bool canGrow;

        [TabGroup("B", "Interact Settings")] public bool canStackable;
        [TabGroup("B", "Interact Settings")] public bool isGrew;

        [TabGroup("Runtime Set")] public RuntimeSet<Gem> CollectedGemSet;

        [TabGroup("C", "DoTween Settings")] [SerializeField]
        private GemDoTweenProperties gemDoTweenProperties;

        public GemListItem gemListItemRef;
        
        #endregion

        #region EVENT FUNCTIONS

        private void Start()
        {
            Growing.CheckGrowing(this);

            GemSituationalActions();
        }

        private void OnDisable()
        {
            CollectedGemSet.Clear();
            // CollectedAllGemAmount.Value = 0;
        }

        #endregion

        #region IMPLEMENTED FUNCTIONS

        public void Stack(Transform target)
        {
            // await UniTask.WhenAll(Move(target, this.GetCancellationTokenOnDestroy()));
            Move(target).OnComplete(() =>
            {
                SetParent(target);
                Processes(this);
            });
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
        
        public void SetGemListRef(GemListItem gemListItem)
        {
            gemListItemRef = gemListItem;
        }

        public string GetName()
        {
            return GemProperties.Name;
        }

        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS

        // private List<UniTask> Move(Transform target, CancellationToken ct)
        // {
        //     var taskList = new List<UniTask>();
        //
        //     var seq = DOTween.Sequence();
        //
        //     seq.Append(transform.DOLocalMove(target.position + gemDoTweenProperties.DoMoveUpEndValue, gemDoTweenProperties.DoMoveUpDuration));
        //
        //     // SpawnedGemFromPool.Remove(this);
        //         
        //     taskList.Add(seq.ToUniTask(cancellationToken: ct));
        //
        //     return taskList;
        // }

        private Tween Move(Transform target)
        {
            var seq = DOTween.Sequence();

            // seq.Append(transform.DOMove(target.position + gemDoTweenProperties.DoMoveUpEndValue, gemDoTweenProperties.DoMoveUpDuration));
            seq.Append(transform.DOPunchPosition(gemDoTweenProperties.DoMoveUpEndValue,
                gemDoTweenProperties.DoMoveUpDuration));

            // SpawnedGemFromPool.Remove(this);

            return seq;
        }

        private void SetParent(Transform target)
        {
            transform.DOFollow(target,
                target.localPosition + new Vector3(0f,
                    gemDoTweenProperties.DoFollowStackSpace * CollectedAllGemAmount.Value, 0f),
                gemDoTweenProperties.DoFollowDuration).OnComplete(() =>
            {
                transform.parent = target;
                transform.localPosition = new Vector3(0,
                    gemDoTweenProperties.DoFollowStackSpace * CollectedAllGemAmount.Value, 0);
                transform.localEulerAngles = Vector3.zero;
            });

            CollectedAllGemAmount.Value++;
        }

        private void Processes(Gem gem)
        {
            isStacked = true;

            CollectedGemSet.Add(gem);

            collider.enabled = false;
        }

        public void GemSituationalActions()
        {
            this.ObserveEveryValueChanged(_ => isGrew).Where(_ => isGrew)
                .Subscribe(unit => { Growing.StopGrowing(); });

            this.ObserveEveryValueChanged(_ => isStacked).Where(_ => isStacked)
                .Subscribe(unit => { Growing.StopGrowing(); });
        }

        #endregion
    }

    [Serializable]
    public struct GemDoTweenProperties
    {
        [Title("Properties")] [TabGroup("B", "DOFollow")]
        public float DoFollowDuration;

        [TabGroup("B", "DOFollow")] [InfoBox("Space coefficient of the stacked objects")]
        public float DoFollowStackSpace;

        [Title("Properties")] [TabGroup("B", "DOMove")]
        public Vector3 DoMoveUpEndValue;

        [TabGroup("B", "DOMove")] public float DoMoveUpDuration;
    }

    public interface IStackable
    {
        void Stack(Transform parent);
        void SetGrowable(bool state);
        bool IsStacked();
        bool CanStackable();

        string GetName();
        void SetGemListRef(GemListItem gemListItem);
    }
}