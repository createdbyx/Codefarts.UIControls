/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/
namespace CBX.Unity.Editor.Windows
{
    using Codefarts.UIControls;
    using Codefarts.UIControls.Code;

    using UnityEngine;

    public class ControlLayoutDesigner : ScrollViewer
    {
       // public ControlLayoutDesigner()
     //   {
            // auto register this control type with the control drawing service
         //   var service = UnityControlRenderingService.Instance;
           // service.Register<ControlLayoutDesigner>(ControlDrawingHelpers.RenderCustomControl);
      //  }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            // GUILayout.Box(string.Empty, GUI.skin.verticalScrollbarDownButton);
         //   var service = UnityControlRenderingService.Instance;
          //  service.Render(this.Children);  
            foreach (var child in this.Children)
            {
                manager.DrawControl(child,elapsedGameTime,totalGameTime);
            }
        }
    }
}