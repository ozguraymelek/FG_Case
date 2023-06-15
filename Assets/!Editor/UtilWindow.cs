#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace nyy.FG_Case.Editor
{
    public class UtilWindow : OdinMenuEditorWindow
    {  
        #region IMPLEMENTED FUNCTIONS
        
        #endregion

        #region PRIVATE FUNCTIONS
        
        [MenuItem("nyy/Utils")]
        private static void OpenWindow()
        {
            var window = GetWindow<UtilWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }
        
        #endregion

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true)
            {
                { "Create Gem", GemGenerator.Instance, EditorIcons.Podium },
            };
        
            return tree;
        }
    }
}

#endif