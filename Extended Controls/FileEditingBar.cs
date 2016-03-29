#if UNITY_5
namespace Codefarts.UIControls.Controls
{
    using UnityEditor;

    public class FileEditingBar : BaseEditingBar
    {
        #region Overrides of BaseEditingBar

        protected override string ShowOpenPathDialog(string title, string path)
        {
            return EditorUtility.OpenFilePanel(title, path, this.FileExtension);
        }

        protected override string ShowSavePathDialog(string title, string path)
        {
            return EditorUtility.SaveFilePanel(title, path, null, this.FileExtension);
        }

        #endregion
    }
} 
#endif