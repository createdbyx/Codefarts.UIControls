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
    using System.Collections.Generic;

    using Codefarts.UIControls.Code;
    using Codefarts.UIControls.Unity;

    public class ContainerControl : CustomControl
    {
        public IList<Control> Children { get; set; }

        public ContainerControl()
        {
            this.Children = new List<Control>();
        }

        public override void OnDraw(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
            }

        public override void OnUpdate(ControlRendererManager manager, float elapsedGameTime, float totalGameTime)
        {
        }
    }
}