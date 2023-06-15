using System;
using TMPro;
using UniRx;

namespace nyy.FG_Case.Utils
{
    public static class UniRxUIExtentions
    {
        #region PROPERTIES
/************** PROPERTIES **************/  
        
/****************************************/    
        #endregion
        
        #region EVENT FUNCTIONS
/************** EVENT FUNCTIONS **************/   
        
/*********************************************/ 
        #endregion
        
        #region IMPLEMENTED FUNCTIONS
/************** IMPLEMENTED FUNCTIONS **************/   
        
/*********************************************/ 
        #endregion  

        #region PUBLIC FUNCTIONS
/************** PUBLIC FUNCTIONS **************/   
            public static IDisposable SubscribeToText(this IObservable<string> source, TMP_Text text)
            {
                    return source.SubscribeWithState(text, (x, t) => t.text = x);
            }
 
            public static IDisposable SubscribeToText<T>(this IObservable<T> source, TMP_Text text)
            {
                    return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
            }
/*********************************************/ 
        #endregion  
        
        #region PRIVATE FUNCTIONS
/************** PRIVATE FUNCTIONS **************/   
        
/*********************************************/
        #endregion
    }
}