using System.Collections;
using GenericScriptableArchitecture;
using Lean.Pool;
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
        
        #endregion

        #region IMPLEMENTED FUNCTIONS
        
        #endregion

        #region PUBLIC FUNCTIONS

        private void FixedUpdate()
        {
            if (currentPlantedGem == null) return;
            if (currentPlantedGem.isStacked == false) return;
            
            if (Physics.Raycast(transform.position, transform.up, Vector3.one.y,mask) == false)
            {
                if(currentPlantedGem.isStacked)
                    planted = false;
            }
        }

        #endregion

        #region PRIVATE FUNCTIONS

        private IEnumerator Plant()
        {
            yield return new WaitForSeconds(1f);
            
            currentPlantedGem = null;
            
            var rand = Random.Range(0, GemData.GemDataList.Count);
            
            // var gemClone = LeanPool.Spawn(GemData.GemDataList[rand].Prefab, transform);
            var gemClone = Instantiate(GemData.GemDataList[rand].Prefab, transform);
            
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

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position,transform.up);
        }
    }
}