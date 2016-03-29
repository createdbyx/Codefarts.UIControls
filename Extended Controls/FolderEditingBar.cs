#if UNITY_5
namespace Codefarts.UIControls.Controls
{
    using UnityEditor;

    public class FolderEditingBar : BaseEditingBar
    {
        #region Overrides of BaseEditingBar

        protected override string ShowOpenPathDialog(string title, string path)
        {
            return EditorUtility.OpenFolderPanel(title, path, null);
        }

        protected override string ShowSavePathDialog(string title, string path)
        {
            return EditorUtility.SaveFolderPanel(title, path, null);
        }

        #endregion
    }
} 
#endif