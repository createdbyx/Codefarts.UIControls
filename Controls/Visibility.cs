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
    /// <summary>Specifies the display state of an element. </summary>
    public enum Visibility : byte
    {
        /// <summary>Display the element.</summary>
        Visible,
        /// <summary>Do not display the element, but reserve space for the element in layout.</summary>
        Hidden,
        /// <summary>Do not display the element, and do not reserve space for it in layout.</summary>
        Collapsed
    }
}