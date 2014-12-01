/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls.Unity
{
    using System;

    using Codefarts.UIControls;
    using Codefarts.UIControls.Code;

    using UnityEngine;

    public class Table<T> : CustomControl
    {
        private string tableName;

        public bool isVisible = true;

        public delegate void PressedAddButton();
        public delegate void PressedRemoveButton();

        public PressedAddButton addButton;
        public PressedRemoveButton removeButton;

        public Vector2 ScrollPosition { get; set; }

        public ITableModel<T> Model { get; set; }

        public Table(string _tableName)
            : this(_tableName, null)
        {
            this.tableName = _tableName;
        }

        public Table(ITableModel<T> model)
            : this(string.Empty, model)
        {
            this.Model = model;
        }

        public Table(String _tableName, ITableModel<T> _model)
        {
            this.tableName = _tableName;
            this.Model = _model;
        }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            if (!this.isVisible)
            {
                return;
            }

            GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            if (this.tableName.Length != 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(this.tableName);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            this.ScrollPosition = GUILayout.BeginScrollView(this.ScrollPosition, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));


            if (this.Model != null)// && model.GetRowCount() != 0)
            {
                GUILayout.BeginHorizontal();
                for (var column = 0; column < this.Model.ColumnCount; column++)
                {
                    GUILayout.BeginVertical("box");
                    if (this.Model.UseHeaders)
                    {
                        GUILayout.BeginHorizontal("box");
                        GUILayout.Label(this.Model.GetColumnName(column));
                        GUILayout.EndHorizontal();
                    }

                    for (var row = 0; row < this.Model.RowCount; row++)
                    {
                        var obj = this.Model.GetValue(row, column);

                        // Display of edit functionality for the different supported data types (here only string (non-editable) and GameObject).
                        if (obj.GetType().IsValueType)
                        {
                            GUILayout.Label(obj as string);
                        }
                        else if (obj is Control)
                        {
                            manager.DrawControl(obj as Control, elapsedGameTime, totalGameTime);
                            // var service = UnityControlRenderingService.Instance;
                            //   service.Render(obj as Control);
                        }
                    }

                    GUILayout.EndVertical();
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            if (this.addButton != null && this.addButton.GetInvocationList().Length != 0)
            {
                if (GUILayout.Button("Add"))
                {
                    this.addButton();
                }
            }
            if (this.removeButton != null && this.removeButton.GetInvocationList().Length != 0)
            {
                if (GUILayout.Button("Remove"))
                {
                    this.removeButton();
                }
            }
            GUILayout.EndHorizontal();


            GUILayout.EndVertical();
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}
