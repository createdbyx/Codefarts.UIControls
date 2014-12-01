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

    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;

    using UnityEditor;

    using Object = UnityEngine.Object;

    public class ObjectField : CustomControl
    {
        private Object source;
        public Type Type { get; set; }
        public string Text { get; set; }
        public bool AllowSceneObjects { get; set; }
        
        public Object Source
        {
            get { return this.source; }
            set
            {
                this.source = value;
                this.Type = this.source.GetType();
            }
        }                  

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.source = EditorGUILayout.ObjectField(this.source, this.Type, this.AllowSceneObjects, ControlDrawingHelpers.StandardDimentionOptions(this));
            }
            else
            {
                this.source = EditorGUILayout.ObjectField(this.Text, this.source, this.Type, this.AllowSceneObjects, ControlDrawingHelpers.StandardDimentionOptions(this));
            }
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}