/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls.Unity.Editor
{
    using System;

    using Codefarts.UIControls.Unity;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Provides gui helpers methods for drawing controls.
    /// </summary>
    public static class EditorControlRenderHelpers
    {
        /// <summary>
        /// Provides a method for drawing a <see cref="PrefixLabel"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderPrefixLabel(PrefixLabel control)
        {
            EditorGUILayout.PrefixLabel(control.Text);
        }

        /// <summary>
        /// Provides a method for drawing a <see cref="EditorTextField"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderEditorTextField(EditorTextField control)
        {
            string value;
            if (string.IsNullOrEmpty(control.Label))
            {
                value = GUILayout.TextField(control.Text ?? string.Empty, ControlDrawingHelpers.StandardDimentionOptions(control));
            }
            else
            {
                value = EditorGUILayout.TextField(control.Label, control.Text ?? string.Empty, ControlDrawingHelpers.StandardDimentionOptions(control));
            }

            if (control.IsEnabled)
            {
                control.Text = value;
            }      
        }

        /// <summary>
        /// Provides a method for drawing a <see cref="ComboBox"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderComboBox(ComboBox control)
        {
            var names = new string[control.Items.Count]; 
            var i = 0;
            foreach (var item in control.Items)
            {
                names[i++] = item.ToString();
            }

            control.SelectedIndex = EditorGUILayout.Popup(control.SelectedIndex, names, ControlDrawingHelpers.StandardDimentionOptions(control));
        }

        /// <summary>
        /// Provides a method for drawing a <see cref="Expander"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderExpander(Expander control)
        {
            control.IsExpanded = EditorGUILayout.Foldout(control.IsExpanded, control.Header ?? string.Empty);
            if (control.IsExpanded)
            {
                throw new NotImplementedException();
                //ControlDrawingHelpers.DrawControl(control.Children);
            }
        }

        ///// <summary>
        ///// Provides a method for drawing a <see cref="ColorPicker"/> control.
        ///// </summary>
        ///// <param name="control">A reference to the control that should be drawn.</param>
        ///// <remarks>Does not check if the control reference is null.</remarks>
        //public static void RenderColorPicker(ColorPicker control)
        //{
        //    if (string.IsNullOrEmpty(control.Text))
        //    {
        //        control.Color = EditorGUILayout.ColorField(control.Color, ControlDrawingHelpers.StandardDimentionOptions(control));
        //    }
        //    else
        //    {
        //        control.Color = EditorGUILayout.ColorField(control.Text, control.Color, ControlDrawingHelpers.StandardDimentionOptions(control));
        //    }
        //}

        /// <summary>
        /// Provides a method for drawing a <see cref="ObjectField"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderObjectField(ObjectField control)
        {
            if (string.IsNullOrEmpty(control.Text))
            {
                control.Source = EditorGUILayout.ObjectField(control.Source, control.Type, control.AllowSceneObjects, ControlDrawingHelpers.StandardDimentionOptions(control));
            }
            else
            {
                control.Source = EditorGUILayout.ObjectField(control.Text, control.Source, control.Type, control.AllowSceneObjects, ControlDrawingHelpers.StandardDimentionOptions(control));
            }
        }

        /// <summary>
        /// Provides a method for drawing a <see cref="IntegerField"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderIntegerField(IntegerField control)
        {
            control.Value = EditorGUILayout.IntField(control.Text, control.Value, ControlDrawingHelpers.StandardDimentionOptions(control));
        }

        /// <summary>
        /// Provides a method for drawing a <see cref="Vector2Field"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderVector2Field(Vector2Field control)
        {
            control.Value = EditorGUILayout.Vector2Field(control.Text, control.Value, ControlDrawingHelpers.StandardDimentionOptions(control));
        }

        /// <summary>
        /// Provides a method for drawing a <see cref="Vector3Field"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderVector3Field(Vector3Field control)
        {
            control.Value = EditorGUILayout.Vector3Field(control.Text, control.Value, ControlDrawingHelpers.StandardDimentionOptions(control));
        }

        /// <summary>
        /// Provides a method for drawing a <see cref="FloatField"/> control.
        /// </summary>
        /// <param name="control">A reference to the control that should be drawn.</param>
        /// <remarks>Does not check if the control reference is null.</remarks>
        public static void RenderFloatField(FloatField control)
        {
            control.Value = EditorGUILayout.FloatField(control.Text, control.Value, ControlDrawingHelpers.StandardDimentionOptions(control));
        }
    }
}
