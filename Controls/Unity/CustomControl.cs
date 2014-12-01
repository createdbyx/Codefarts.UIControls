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

    public abstract class CustomControl : Control
    {
        public abstract void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime);
        public abstract void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime);
    }
}