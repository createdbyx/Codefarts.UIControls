namespace Codefarts.UIControls.Code.Renderers
{
    using System;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Unity;

    using Smooth.Algebraics;

    using UnityEngine;

    public class NumericTextFieldRenderer : IControlRenderer
    {
        public Type ControlType
        {
            get
            {
                return typeof(NumericTextField);
            }
        }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var textField = control as NumericTextField;
            // var controlId = GUIUtility.GetControlID(FocusType.Passive) + 1;
            var value = GUILayout.TextField(string.IsNullOrEmpty(textField.Text) ? string.Empty : textField.Text, ControlDrawingHelpers.StandardDimentionOptions(control));
            if (control.IsEnabled)
            {
                //textField.Text = this.SanitizeValue(value, textField.Minimum, textField.Maximum);
                textField.Text = value;
            }
        }

        //private string SanitizeValue(string value, float min, float max)
        //{
        //    //  var acceptedValue = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.' };
        //    var i = 0;
        //    var periodIndex = -1;
        //    var newValue = value;
        //    while (i < newValue.Length)
        //    {
        //        switch (newValue[i])
        //        {
        //            case '0':
        //                if (i == 0)
        //                {
        //                    newValue = newValue.Remove(i, 1);
        //                    continue;
        //                }

        //                break;

        //            case '1':
        //            case '2':
        //            case '3':
        //            case '4':
        //            case '5':
        //            case '6':
        //            case '7':
        //            case '8':
        //            case '9':
        //                // do nothing
        //                break;

        //            case '.':
        //                periodIndex = periodIndex == -1 ? i : periodIndex;
        //                if (periodIndex != i)
        //                {
        //                    newValue = newValue.Remove(i, 1);
        //                    continue;
        //                }

        //                break;

        //            default:
        //                newValue = newValue.Remove(i, 1);
        //                break;
        //        }

        //        i++;
        //    }

        //    float result;
        //    if (float.TryParse(newValue, out result))
        //    {
        //        if (result < min)
        //        {
        //            newValue = min.ToString();
        //        }
        //        else if (result > max)
        //        {
        //            newValue = max.ToString();
        //        }
        //        else
        //        {
        //            newValue = result.ToString();
        //        }
        //    }
        //    else
        //    {
        //        newValue = value;
        //    }

        //    return newValue;
        //}

        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}