using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace nyy.FG_Case.System_UI
{
    public class ButtonObserver : MonoBehaviour
    {
        #region PROPERTIES

        [Title("Components")] public TMP_Text textStatus;
        
        #endregion
                
        #region EVENT FUNCTIONS

        private void Start()
        {
            Time.timeScale = 0;
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS

        public void Button_Play()
        {
            Time.timeScale = 1;
        }
        
        public void Button_Pause()
        {
            if (Time.timeScale != 0)
            {
                textStatus.gameObject.SetActive(true);
                textStatus.text = "PAUSED";
                Time.timeScale = 0;
            }
            else
            {
                textStatus.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}