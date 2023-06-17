using System;
using GenericScriptableArchitecture;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace nyy.FG_Case.System_Save
{
    public class PlayerPrefsSave : MonoBehaviour, IMultipleEventsListener<string, IntReactiveProperty>
    {
        #region PROPERTIES

        [Title("Events")] 
        public ScriptableEvent<string, IntReactiveProperty> CheckCollectedCount;
        public ScriptableEvent<string, IntReactiveProperty> SaveCollectedCount;
        #endregion
                
        #region EVENT FUNCTIONS

        private void OnEnable()
        {
            CheckCollectedCount += this;
            SaveCollectedCount += this;
        }

        private void OnDisable()
        {
            CheckCollectedCount -= this;
            SaveCollectedCount -= this;
        }

        private void Start()
        {
            
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        public void OnEventInvoked(IEvent<string, IntReactiveProperty> invokedEvent, string arg0, IntReactiveProperty arg1)
        {
            if (ReferenceEquals(invokedEvent, CheckCollectedCount))
                PlayerPrefs_Check(arg0, arg1);

            if (ReferenceEquals(invokedEvent, SaveCollectedCount))
                PlayerPrefs_Save(arg0, arg1);
        }
        
        #endregion  
        
        #region PUBLIC FUNCTIONS

        public void PlayerPrefs_Check(string key, IntReactiveProperty value)
        {
            if (PlayerPrefs.HasKey(key) == true)
            {
                value.Value = PlayerPrefs.GetInt(key); return;
            }

            PlayerPrefs.SetInt(key, 0);
        }
        
        public void PlayerPrefs_Save(string key, IntReactiveProperty value)
        {
            PlayerPrefs.SetInt(key, value.Value);
        }
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}