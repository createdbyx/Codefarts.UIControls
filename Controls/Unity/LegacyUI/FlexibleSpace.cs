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
    using Codefarts.UIControls.Code;

    using UnityEngine;

    public class FlexibleSpace : CustomControl
    {  
        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            GUILayout.FlexibleSpace();
        }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}