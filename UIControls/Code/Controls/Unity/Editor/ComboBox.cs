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
    using System.Collections.Generic;

    using Codefarts.UIControls.Code;

    using UnityEditor;

    public class ComboBox : CustomControl
    {
        public IList<object> Items { get; set; }
        private int selectedIndex;

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        public void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            var handler = this.SelectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                if (this.Items != null && this.selectedIndex > this.Items.Count - 1)
                {
                    return;
                }

                if (this.Items != null && this.selectedIndex < -1)
                {
                    var local = Localization.LocalizationManager.Instance;
                    throw new ArgumentException(local.Get("ERR_IndexOutOfBounds"));
                }

                var oldValue = this.selectedIndex;
                var changed = value != oldValue;
                this.selectedIndex = value;
                if (changed)
                {
                    this.OnSelectionChanged(new SelectionChangedEventArgs(oldValue, value));
                }
            }
        }

        public object SelectedItem
        {
            get
            {
                return this.Items == null || (this.selectedIndex < 0 && this.selectedIndex > this.Items.Count - 1) ? null : this.Items[this.selectedIndex];
            }

            set
            {
                if (this.Items == null || (this.selectedIndex < 0 && this.selectedIndex > this.Items.Count - 1))
                {
                    return;
                }

                this.Items[this.selectedIndex] = value;
            }
        }

        public ComboBox()
        {
            this.selectedIndex = -1;
            this.Items = new List<object>();
        }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var names = new string[this.Items.Count];
            var i = 0;
            foreach (var item in this.Items)
            {
                names[i++] = item.ToString();
            }

            this.SelectedIndex = EditorGUILayout.Popup(this.SelectedIndex, names, ControlDrawingHelpers.StandardDimentionOptions(this));
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            
        }
    }
}