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
    using System.Linq;

    using CBX.Controls;

    using UnityEditor;

    using UnityEngine;

    public class GUIDesignerPropertiesWindow : EditorWindow
    {
        public object SelectedObject { get; set; }
        private static GUIDesignerPropertiesWindow singleton;

        public static GUIDesignerPropertiesWindow Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = GetWindow<GUIDesignerPropertiesWindow>();
                    singleton.title = "Properties";
                }

                return singleton;
            }
        }

        public GUIDesignerPropertiesWindow()
        {
            GUIDesignerHierarchyWindow.Instance.Tree.SelectionChanged += Tree_SelectionChanged;
        }

        void Tree_SelectionChanged(object sender, EventArgs e)
        {
            var tree = (CBX.Controls.Unity.TreeView)sender;
            this.SelectedObject = tree.SelectedNode.Value;
        }

        public void OnGUI()
        {
            if (GUILayout.Button("search"))
            {
                var items = Shader.FindObjectsOfTypeIncludingAssets(typeof(Shader)) as Shader[];
                var str = string.Join( "\r\n", items.Select(x => x.name).ToArray());
                Debug.Log(str);
                Debug.Log(items.Length);
            }
        }

        /// <summary>
        /// The initializes the window and shows it.
        /// </summary>
        [MenuItem("Window/Codefarts/GUI Designer/Show Properties")]
        private static void Init()
        {
            // get the window, show it, and hand it focus
            try
            {
                var window = GUIDesignerPropertiesWindow.Instance;
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