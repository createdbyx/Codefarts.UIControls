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
    /// <summary>
    /// Specifies the visibility of a ScrollBar for scrollable content.
    /// </summary>
    public enum ScrollBarVisibility
    {
        /// <summary>
        /// A ScrollBar does not appear even when the viewport cannot display all of the content. 
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// A ScrollBar appears and the dimension of the container is applied to the content when the viewport cannot display all of the content.
        /// </summary>
        Auto = 1,

        /// <summary>
        /// A ScrollBar does not appear even when the viewport cannot display all of the content. 
        /// </summary>
        Hidden = 2,

        /// <summary>
        /// A ScrollBar always appears. 
        /// </summary>
        Visible = 3
    }
}