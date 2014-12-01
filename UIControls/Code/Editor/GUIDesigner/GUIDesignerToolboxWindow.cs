/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/
namespace CBX.Unity.Editor.GUIDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Codefarts.UIControls;
    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;

    using UnityEditor;

    using UnityEngine;

    public class GUIDesignerToolboxWindow : EditorWindow
    {
        public List<Control> Controls;

        private Vector2 scrollPos;
        private static GUIDesignerToolboxWindow singleton;

        public static GUIDesignerToolboxWindow Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = GetWindow<GUIDesignerToolboxWindow>();
                    singleton.title = "Toolbox";
                }

                return singleton;
            }
        }


        public GUIDesignerToolboxWindow()
        {
            this.Controls = new List<Control>();

            // register for callback to set localization names
            this.PopulateControls();
        }

        public void OnGUI()
        {
            this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, false, false);
            var manager = ControlRendererManager.Instance;
            foreach (var control in this.Controls)
            {
                manager.DrawControl(control, Time.deltaTime, Time.realtimeSinceStartup);
            }
            GUILayout.EndScrollView();
        }

        public void OnEnable()
        {
            this.PopulateControls();
        }

        private void PopulateControls()
        {
            this.Controls.Clear();
          //  var service = UnityControlRenderingService.Instance;
            var manager = ControlRendererManager.Instance;
            foreach (var type in manager.GetControlTypes().OrderBy(x => x.Name))
            {
                var btn = new Button() { Text = type.Name };       
              //  btn.Extenders = new CBX.Common.ValuesBase<string>();
               // btn.Extenders.SetValue("type", type);
                btn.Click += this.btn_Click;
                this.Controls.Add(btn);
            }

            this.Controls.Add(new FlexibleSpace());
        }

        void btn_Click(object sender, EventArgs e)
        {
            return;
            /*
            var btn = sender as Button;
            var type = btn.Extenders.GetValue<Type>("type");
            var co = type.GetConstructor(Type.EmptyTypes);
            var obj = co.Invoke(new object[0]) as Control;
            obj.Left = 100;
            obj.Top = 100;
            obj.Width = 400;
            obj.Height = 50;
            obj.Name = type.Name;
            var newNode = new TreeViewNode() { Value = new ControlData() { Name = obj.Name, Type = type, Reference = obj } };
            var treeWin = GUIDesignerHierarchyWindow.Instance;
            var designWin = GUIDesignerWindow.Instance;
            if (treeWin.Tree.SelectedNode != null)
            {
                var data = treeWin.Tree.SelectedNode.Value as ControlData;
                if (data != null && data.Reference is ContainerControl)
                {
                    var container = data.Reference as ContainerControl;
                    container.Children.Add(obj);
                    treeWin.Tree.SelectedNode.Nodes.Add(newNode);
                }
                else
                {
                    designWin.DesignArea.Children.Add(obj);
                    treeWin.Tree.Nodes.Add(newNode);
                }
            }
            else
            {
                designWin.DesignArea.Children.Add(obj);
                treeWin.Tree.Nodes.Add(newNode);
            }

            var propertiesWin = GUIDesignerPropertiesWindow.Instance;
            propertiesWin.Repaint();
            treeWin.Repaint();
             */
        }

        /// <summary>
        /// The initializes the window and shows it.
        /// </summary>
        [MenuItem("Window/Codefarts/GUI Designer/Show Toolbox")]
        private static void Init()
        {
            // get the window, show it, and hand it focus
            try
            {
                var window = GUIDesignerToolboxWindow.Instance;
                window.Show();
                window.Focus();
                window.Repaint();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}