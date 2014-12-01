/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/
namespace CBX.Controls.Unity
{
    using System.Collections.Generic;
    using System;


    using Codefarts.UIControls;
    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;

    using UnityEngine;

    public class TreeView : CustomControl
    {
        private Vector2 scrollPosition;

        private TreeViewNode selectedNode;

        public List<TreeViewNode> Nodes { get; set; }

        public event EventHandler SelectionChanged;

        public TreeViewNode SelectedNode
        {
            get
            {
                return this.selectedNode;
            }
            private set
            {
                this.selectedNode = value;
                var handler = this.SelectionChanged;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        public TreeView()
        {
            // auto register this control type with the control drawing service
            // var service = UnityControlRenderingService.Instance;
            //  service.Register<TreeView>(ControlDrawingHelpers.RenderCustomControl);

            this.Inset = 8;
            this.Nodes = new List<TreeViewNode>();
        }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var stack = new Stack<TreeViewNode>();
            this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, true, true, ControlDrawingHelpers.StandardDimentionOptions(this));
            foreach (var node in this.Nodes)
            {
                this.DrawNode(manager, stack, node, elapsedGameTime, totalGameTime);
            }
            // GUILayout.FlexibleSpace();
            GUILayout.EndScrollView();
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {   
        }

        private void DrawNode(ControlRendererManager manager, Stack<TreeViewNode> stack, TreeViewNode node, float elapsedGameTime, float totalGameTime)
        {
            if (!node.IsVisible)
            {
                return;
            }

            GUILayout.BeginHorizontal();

            GUILayout.Space(this.Inset * stack.Count);
            stack.Push(node);

            if (GUILayout.Button(node.IsExpanded ? "-" : "+"))
            {
                node.IsExpanded = !node.IsExpanded;
            }


            if (node.Value is Control)
            {
                manager.DrawControl(node.Value as Control, elapsedGameTime, totalGameTime);
            }
            else
            {
                var label = this.SelectedNode == node ? "SelectionRect" : "label";
                var button = GUILayout.Button(node.Value.ToString(), label);
                this.SelectedNode = button ? node : this.SelectedNode;
            }


            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (node.IsExpanded)
            {
                foreach (var child in node.Nodes)
                {
                    this.DrawNode(manager, stack, child, elapsedGameTime, totalGameTime);
                }
            }

            stack.Pop();
        }

        protected float Inset { get; set; }
    }
}