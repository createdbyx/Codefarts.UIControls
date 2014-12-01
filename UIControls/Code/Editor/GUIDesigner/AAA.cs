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
    using Codefarts.UIControls;
    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;

    using UnityEngine;

    public class AAA : CustomControl
    {
        public string Text { get; set; }

        public Texture2D Texture { get; set; }


        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            var rect = GUILayoutUtility.GetLastRect();// this.GetScreenRectangle();
            this.Text = rect.ToString();
            var content = new GUIContent(this.Text, this.Texture);

            if (this.Left == 0 && this.Top == 0)
            {
                if (GUILayout.Button(content, ControlDrawingHelpers.StandardDimentionOptions(this)))
                {

                }
            }
            else
            {
                if (GUI.Button(new Rect(this.Left + rect.x, this.Top + rect.y, this.Width, this.Height), content))
                {

                }
            }
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}