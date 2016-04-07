namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Used to group collections of controls.
    /// </summary>
    public class Popup : Panel
    {
        public void Show(Control owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            // get root control
            var root = owner;
            while (root.Parent != null)
            {
                root = root.Parent;
            }

            var ownerOrigin = owner.PointToScreen(Point.Empty);
            this.Location = ownerOrigin + new Point(0, owner.Height) - root.Location;

            // perform checking weather or not popup is within 
            this.IsVisible=true;

            root.Controls.Add(this);
        }
    }
}