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

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// The GUI designer window.
    /// </summary>
    public class GUIDesignerWindow : EditorWindow
    {
        public ScrollViewer DesignArea;

        private static GUIDesignerWindow singleton;

        //  private ControlRendererManager manager;

        public static GUIDesignerWindow Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new GUIDesignerWindow();
                    singleton.title = "GUI Designer";
                }

                return singleton;
            }
        }


        public GUIDesignerWindow()
        {
            this.DesignArea = new ScrollViewer();
            //     this.manager = new ControlRendererManager();
        }

        /// <summary>
        /// The initializes the window and shows it.
        /// </summary>
        [MenuItem("Window/Codefarts/GUI Designer/Show Designer")]
        private static void Init()
        {
            // get the window, show it, and hand it focus
            try
            {
                var window = GUIDesignerWindow.Instance;
                window.Show();
                window.Focus();
                window.Repaint();
                GUIDesignerPropertiesWindow.Instance.Show();
                GUIDesignerToolboxWindow.Instance.Show();
                GUIDesignerHierarchyWindow.Instance.Show();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Renders the GUI
        /// </summary>
        private void OnGUI()
        {
            ControlRendererManager.Instance.DrawControl(this.DesignArea, Time.deltaTime, Time.realtimeSinceStartup);
        }

        //public ControlRendererManager Manager
        //{
        //    get
        //    {
        //        return this.manager;
        //    }
        //}
    }
}