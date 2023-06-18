using System.Collections;
using GenericScriptableArchitecture;
using Lean.Pool;
using nyy.FG_Case.PlayerSc;
using nyy.FG_Case.System_Data;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace nyy.FG_Case.System_Grid
{
    public class Tile : MonoBehaviour
    {
        #region PROPERTIES
        
        [Title("Data")]
        public GemObjectData GemData;
        
        [TabGroup("Settings")] 
        [SerializeField] private Gem currentPlantedGem;
        public bool planted;

        public LayerMask mask;
        
        #endregion

        #region EVENT FUNCTIONS

        private void Start()
        {
            planted = false;
            
            this.ObserveEveryValueChanged(_ => planted).Where(_ => planted == false)
            .Subscribe(unit =>
            {
                StartCoroutine(Plant());
            });
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color=Color.red;
            
            Gizmos.DrawWireCube(transform.position,transform.localScale*2);
        }
        
        #endregion

        #region IMPLEMENTED FUNCTIONS
        
        #endregion

        #region PUBLIC FUNCTIONS

        private void FixedUpdate()
        {
            if (currentPlantedGem == null) return;
            if (currentPlantedGem.isStacked == false) return;
            
            // if (Physics.SphereCast(transform.position, 1f, transform.forward, out var hit,500f,mask))
            // {
            //     if (hit.collider.TryGetComponent(out Player player) == true)
            //     {
            //         print("xx");
            //         planted = false;
            //     }
            // }

            var colliders = Physics.OverlapBox(transform.position, transform.localScale*2,Quaternion.identity, mask);
            print(colliders.Length);
            
            if (colliders.Length != 0)
                planted = false;
        }

        #endregion

        #region PRIVATE FUNCTIONS

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Plant()
        {
            yield return new WaitForSeconds(1f);
            print("planted");
            currentPlantedGem = null;
            
            var rand = Random.Range(0, GemData.GemDataList.Count);
            
            var gemClone = LeanPool.Spawn(GemData.GemDataList[rand].Prefab, transform);
            
            currentPlantedGem = gemClone;
            
            currentPlantedGem.transform.parent = transform;
            currentPlantedGem.transform.localPosition = Vector3.zero;
            currentPlantedGem.transform.localScale = new Vector3(0f, 0f, 0f);
            
            currentPlantedGem.gameObject.SetActive(true);
                    
            currentPlantedGem.canGrow = true;
            currentPlantedGem.isGrew = false;
            
            planted = true;
        }
        
        #endregion
    }
}