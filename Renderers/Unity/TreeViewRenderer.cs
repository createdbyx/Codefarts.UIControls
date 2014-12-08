namespace Codefarts.UIControls.Code.Renderers
{
    using System;
    using System.Collections.Generic;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Unity;

    using UnityEngine;

    public class TreeViewRenderer : IControlRenderer
    {
        private static GUIStyle selectionStyle;

        private GUIStyle normalStyle;

        public TreeViewRenderer()
        {
            // auto register this control type with the control drawing service
            // var service = UnityControlRenderingService.Instance;
            //  service.Register<TreeView>(ControlDrawingHelpers.RenderCustomControl);
            if (selectionStyle == null)
            {
                selectionStyle = new GUIStyle("label");
                selectionStyle.name = "TreeViewSelection";
            }

            this.Inset = 8;
        }

        public Type ControlType
        {
            get
            {
                return typeof(TreeView);
            }
        }

        public float Inset { get; set; }

        public void Draw(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var tree = control as TreeView;
            var stack = new Stack<TreeViewNode>();
            try
            {
                var scroll = GUILayout.BeginScrollView(new Vector2(tree.HorizontialOffset, tree.VerticalOffset), true, true, ControlDrawingHelpers.StandardDimentionOptions(tree));
                tree.HorizontialOffset = scroll.x;
                tree.VerticalOffset = scroll.y;
                foreach (var node in tree.Nodes)
                {
                    this.DrawNode(tree, manager, stack, node, elapsedGameTime, totalGameTime);
                }
            }
            finally
            {
                GUILayout.EndScrollView();
            }
        }

        private void DrawNode(TreeView tree, ControlRendererManager manager, Stack<TreeViewNode> stack, TreeViewNode node, float elapsedGameTime, float totalGameTime)
        {
            if (!node.IsVisible)
            {
                return;
            }

            if (selectionStyle == null)
            {
                selectionStyle = new GUIStyle(GUI.skin.label);
                selectionStyle.normal.textColor = GUI.skin.settings.selectionColor;
                selectionStyle.wordWrap = false;
            }

            if (this.normalStyle != GUI.skin.label)
            {
                this.normalStyle = new GUIStyle(GUI.skin.label) { wordWrap = false };
            }

            var nodeList = node.Nodes;
            try
            {
                GUILayout.BeginHorizontal();

                GUILayout.Space(stack.Count * (node.Nodes != null && node.Nodes.Count == 0 ? this.Inset * 2 : this.Inset));
                stack.Push(node);

                if (nodeList != null && nodeList.Count > 0 && GUILayout.Button(node.IsExpanded ? "-" : "+", this.normalStyle))
                {
                    node.IsExpanded = !node.IsExpanded;
                }


                if (node.Value is Control)
                {
                    manager.DrawControl(node.Value as Control, elapsedGameTime, totalGameTime);
                }                          
                else
                {
                    var label = tree.SelectedNode == node ? selectionStyle : normalStyle;
                    var button = GUILayout.Button(node.Value.ToString(), label);
                    tree.SelectedNode = button ? node : tree.SelectedNode;
                }

                GUILayout.FlexibleSpace();
            }
            finally
            {
                GUILayout.EndHorizontal();
            }

            if (node.IsExpanded && nodeList != null)
            {
                foreach (var child in nodeList)
                {
                    this.DrawNode(tree, manager, stack, child, elapsedGameTime, totalGameTime);
                }
            }

            stack.Pop();
        }


        public void Update(ControlRendererManager manager, Control control, float elapsedGameTime, float totalGameTime)
        {
            var tree = control as TreeView;
            var stack = new Stack<TreeViewNode>();
            try
            {
                var scroll = GUILayout.BeginScrollView(new Vector2(tree.HorizontialOffset, tree.VerticalOffset), true, true, ControlDrawingHelpers.StandardDimentionOptions(tree));
                tree.HorizontialOffset = scroll.x;
                tree.VerticalOffset = scroll.y;
                foreach (var node in tree.Nodes)
                {
                    this.DrawNode(tree, manager, stack, node, elapsedGameTime, totalGameTime);
                }
            }
            finally
            {
                GUILayout.EndScrollView();
            }
        }
    }
}