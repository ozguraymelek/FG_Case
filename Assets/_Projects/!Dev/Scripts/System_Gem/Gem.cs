using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GenericScriptableArchitecture;
using Lean.Pool;
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
        
        [TabGroup("X", "Data")] 
        public PlayerData PlayerData;
        [TabGroup("X", "Data")] 
        public GemProperties GemProperties;
        
        
        [TabGroup("A", "Components")] [SerializeField]
        public BoxCollider boxCollider;
        public Material material;

        [Title("Particle Effect Components")] 
        public GameObject grewEffectPrefab;
        public GameObject grewEffect;
        
        [Title("Sound Effect Components")] 
        public GameObject collectSoundPrefab;
        public GameObject collectSound;
        public GameObject saleSoundPrefab;
        public GameObject saleSound;
        
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
        
        #endregion

        #region IMPLEMENTED FUNCTIONS

        public void SetGrowable(bool state)
        {
            canGrow = state;
        }

        public int GetCollectedAllGemAmountValue()
        {
            return CollectedAllGemAmount.Value;
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

        public void Stack(Transform target)
        {
            boxCollider.enabled = false;

            if (PlayerData.soundEffects)
            {
                collectSound = LeanPool.Spawn(collectSoundPrefab, transform);
                DOVirtual.DelayedCall(1f, () => { LeanPool.Despawn(collectSound); });
            }
            
            transform.DOFollow(target,
                target.localPosition + new Vector3(0f,
                    gemDoTweenProperties.DoFollowStackSpace * CollectedAllGemAmount.Value, 0f),
                gemDoTweenProperties.DoFollowDuration).OnComplete(() =>
            {
                SetParent(target);
                Processes(this);
            });
        }

        private void SetParent(Transform target)
        {
            transform.parent = target;
            transform.localPosition = new Vector3(0,
                gemDoTweenProperties.DoFollowStackSpace * CollectedAllGemAmount.Value, 0);
            transform.localEulerAngles = Vector3.zero;
            
            CollectedAllGemAmount.Value += 1;
        }

        private void Processes(Gem gem)
        {
            isStacked = true;

            CollectedGemSet.Add(gem);
        }

        private void GemSituationalActions()
        {
            this.ObserveEveryValueChanged(_ => isGrew).Where(_ => isGrew)
                .Subscribe(unit =>
                {
                    Growing.StopGrowing();
                    
                    grewEffect = LeanPool.Spawn(grewEffectPrefab, transform);

                    SetParticleSystemColor();
                });

            this.ObserveEveryValueChanged(_ => isStacked).Where(_ => isStacked)
                .Subscribe(unit =>
                {
                    isGrew = false;
                    
                    Growing.StopGrowing();

                    if (grewEffect == null) return;
                    
                    grewEffect.transform.parent = null;
                    LeanPool.Despawn(grewEffect);
                });
        }

        private void SetParticleSystemColor()
        {
            var psMain = grewEffect.GetComponent<ParticleSystem>();
                    
            var mainMainPsModule = psMain.main;
                    
            material = GetComponentInChildren<Renderer>().material;
                    
            mainMainPsModule.startColor = new ParticleSystem.MinMaxGradient(material.color);
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
        void SetGemListRef(GemListItem gemListItem);

        int GetCollectedAllGemAmountValue();
        bool IsStacked();
        bool CanStackable();

        string GetName();
        
    }
}