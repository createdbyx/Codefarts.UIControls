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

    using Codefarts.UIControls;
    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;

    using UnityEditor;

    using UnityEngine;

    public class GUIDesignerHierarchyWindow : EditorWindow
    {
        public TreeView Tree;

        private static GUIDesignerHierarchyWindow singleton;

        public static GUIDesignerHierarchyWindow Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = GetWindow<GUIDesignerHierarchyWindow>();
                    singleton.title = "Hierarchy";
                }

                return singleton;
            }
        }

        public GUIDesignerHierarchyWindow()
        {
            this.Tree = new TreeView();
        }

        public void OnGUI()
        {
            ControlRendererManager.Instance.DrawControl(this.Tree, Time.deltaTime, Time.realtimeSinceStartup);
        }

        /// <summary>
        /// The initializes the window and shows it.
        /// </summary>
        [MenuItem("Window/Codefarts/GUI Designer/Show Hierarchy")]
        private static void Init()
        {
            // get the window, show it, and hand it focus
            try
            {
                var window = GUIDesignerHierarchyWindow.Instance;
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