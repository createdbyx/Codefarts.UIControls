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
    using System.Collections.Generic;

    public class Window : Control
    {
        public IList<Control> Children { get; set; }
        public string Title { get; set; }
        private static int winID;
        public int ID { get; set; }
        public bool IsDragable { get; set; }

        public Window()
        {
            this.Children = new List<Control>();
            this.ID = Window.NewWindowID();
            this.IsDragable = true;
        }

        public static int NewWindowID()
        {
            winID++;
            if (winID == int.MaxValue)
            {
                winID = int.MinValue;
            }

            return winID;
        }
    }
}