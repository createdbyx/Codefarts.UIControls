namespace Codefarts.UIControls
{
    using System;
    using System.Globalization;

#if UNITY_5 || UNITY_2017
    using UnityEngine;
#endif

    /// <summary>
    /// Represents an ordered pair of floating-point x- and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
#if !(SILVERLIGHT || WINDOWS_PHONE || NETFX_CORE || PORTABLE)
    [Serializable]
#endif
    public struct Point
    {
        /// <summary>Represents a new instance of the <see cref="Point" /> class with member data left uninitialized.</summary>
        public readonly static Point Empty;

        /// <summary>
        /// The value for the <see cref="X"/> property.
        /// </summary>
        private float x;

        /// <summary>
        /// The value for the <see cref="Y"/> property.
        /// </summary>
        private float y;

        /// <summary>Gets a value indicating whether this <see cref="Point" /> is empty.</summary>
        /// <returns>true if both <see cref="Point.X" /> and <see cref="Point.Y" /> are 0; otherwise, false.</returns>
        public bool IsEmpty
        {
            get
            {
                if (Math.Abs(this.x) > float.Epsilon)
                {
                    return false;
                }

                return Math.Abs(this.y) < float.Epsilon;
            }
        }

        /// <summary>Gets or sets the x-coordinate of this <see cref="Point" />.</summary>
        /// <returns>The x-coordinate of this <see cref="Point" />.</returns>
        public float X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        /// <summary>Gets or sets the y-coordinate of this <see cref="Point" />.</summary>
        /// <returns>The y-coordinate of this <see cref="Point" />.</returns>
        public float Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Point" /> class with the specified coordinates.</summary>
        /// <param name="x">The horizontal position of the point. </param>
        /// <param name="y">The vertical position of the point. </param>
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>Translates a given <see cref="Point" /> by the specified <see cref="Size" />.</summary>
        /// <returns>The translated <see cref="Point" />.</returns>
        /// <param name="pt">The <see cref="Point" /> to translate.</param>
        /// <param name="sz">The <see cref="Size" /> that specifies the numbers to add to the coordinates of <paramref name="pt" />.</param>
        public static Point Add(Point pt, Size sz)
        {
            return new Point(pt.X + sz.Width, pt.Y + sz.Height);
        }

        /// <summary>Specifies whether this <see cref="Point" /> contains the same coordinates as the specified <see cref="T:System.Object" />.</summary>
        /// <returns>This method returns true if <paramref name="obj" /> is a <see cref="Point" /> and has the same coordinates as this <see cref="Point" />.</returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        public override bool Equals(object obj)
        {
            if (!(obj is Point))
            {
                return false;
            }
            var point = (Point)obj;
            if (Math.Abs(point.X - this.X) > float.Epsilon || Math.Abs(point.Y - this.Y) > float.Epsilon)
            {
                return false;
            }
            return point.GetType() == this.GetType();
        }

        /// <summary>Returns a hash code for this <see cref="Point" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="Point" /> structure.</returns>
        public override int GetHashCode()
        {
            var temp = this;
            return (temp.X.GetHashCode() * 397) ^ temp.Y.GetHashCode();
        }

        /// <summary>Translates a <see cref="Point" /> by a given <see cref="Size" />.</summary>
        /// <returns>Returns the translated <see cref="Point" />.</returns>
        /// <param name="pt">The <see cref="Point" /> to translate. </param>
        /// <param name="sz">A <see cref="Size" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />. </param>
        public static Point operator +(Point pt, Size sz)
        {
            return Add(pt, sz);
        }

        /// <summary>Compares two <see cref="Point" /> structures. The result specifies whether the values of the <see cref="Point.X" /> and <see cref="Point.Y" /> properties of the two <see cref="Point" /> structures are equal.</summary>
        /// <returns>true if the <see cref="Point.X" /> and <see cref="Point.Y" /> values of the left and right <see cref="Point" /> structures are equal; otherwise, false.</returns>
        /// <param name="left">A <see cref="Point" /> to compare. </param>
        /// <param name="right">A <see cref="Point" /> to compare. </param>
        public static bool operator ==(Point left, Point right)
        {
            if (Math.Abs(left.X - right.X) > float.Epsilon)
            {
                return false;
            }
            return Math.Abs(left.Y - right.Y) < float.Epsilon;
        }

        /// <summary>Determines whether the coordinates of the specified points are not equal.</summary>
        /// <returns>true to indicate the <see cref="Point.X" /> and <see cref="Point.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false. </returns>
        /// <param name="left">A <see cref="Point" /> to compare.</param>
        /// <param name="right">A <see cref="Point" /> to compare.</param>
        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        /// <summary>Translates a <see cref="Point" /> by the negative of a given <see cref="Size" />.</summary>
        /// <returns>The translated <see cref="Point" />.</returns>
        /// <param name="pt">The <see cref="Point" /> to translate.</param>
        /// <param name="sz">The <see cref="Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
        public static Point operator -(Point pt, Size sz)
        {
            return Subtract(pt, sz);
        }

        /// <summary>Translates a <see cref="Point" /> by the negative of a specified size.</summary>
        /// <returns>The translated <see cref="Point" />.</returns>
        /// <param name="pt">The <see cref="Point" /> to translate.</param>
        /// <param name="sz">The <see cref="Size" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
        public static Point Subtract(Point pt, Size sz)
        {
            return new Point(pt.X - sz.Width, pt.Y - sz.Height);
        }

        /// <summary>Converts this <see cref="Point" /> to a human readable string.</summary>
        /// <returns>A string that represents this <see cref="Point" />.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{X={0}, Y={1}}}", this.x, this.y);
        }

        public static implicit operator Size(Point size)
        {
            return new Size(size.X, size.Y);
        }

#if UNITY_5 || UNITY_2017
        public static implicit operator Vector2(Point point)
        {
            return new Vector2(point.X, point.Y);
        }   

        public static implicit operator Point(Vector2 vector)
        {
            return new Point(vector.x, vector.y);
        }
#endif
    }
}