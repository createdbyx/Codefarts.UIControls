#if UNITY3D
namespace Codefarts.UIControls
{
    using UnityEngine;

    public class ButtonCheckBox : CheckBox
    {
        public ButtonCheckBox(Texture2D texture)
            : base(texture)
        {
        }

        public ButtonCheckBox()
            : base()
        {
        }
    }
} 
#endif