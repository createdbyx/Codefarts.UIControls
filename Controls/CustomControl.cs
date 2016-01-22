/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls
{
    public abstract class CustomControl : Control
    {
        public abstract void OnDraw(IControlRendererManager manager, float elapsedGameTime, float totalGameTime);
        public abstract void OnUpdate(ControlRenderingArgs args);
    }
}