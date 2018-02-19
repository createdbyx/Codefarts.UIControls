namespace Codefarts.UIControls
{
    using System;
    using System.Globalization;

#if UNITY_5 || UNITY_2017
    using UnityEngine;
#endif

    /// <summary>Stores an ordered pair of integers, which specify a <see cref="Size.Height" /> and <see cref="Size.Width" />.</summary>
#if !(SILVERLIGHT || WINDOWS_PHONE || NETFX_CORE || PORTABLE)
  [Serializable]
#endif
    public struct Size
    {
        /// <summary>Gets a <see cref="Size" /> structure that has a <see cref="Size.Height" /> and <see cref="Size.Width" /> value of 0. </summary>
        /// <returns>A <see cref="Size" /> that has a <see cref="Size.Height" /> and <see cref="Size.Width" /> value of 0.</returns>
        public readonly static Size Empty;

        private float width;

        private float height;

        /// <summary>Gets or sets the vertical component of this <see cref="Size" /> structure.</summary>
        /// <returns>The vertical component of this <see cref="Size" /> structure, typically measured in pixels.</returns>
        public float Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
            }
        }

        /// <summary>Tests whether this <see cref="Size" /> structure has width and height of 0.</summary>
        /// <returns>This property returns true when this <see cref="Size" /> structure has both a width and height of 0; otherwise, false.</returns>
        public bool IsEmpty
        {
            get
            {
                if (Math.Abs(this.width) > float.Epsilon)
                {
                    return false;
                }

                return Math.Abs(this.height) < float.Epsilon;
            }
        }

        /// <summary>Gets or sets the horizontal component of this <see cref="Size" /> structure.</summary>
        /// <returns>The horizontal component of this <see cref="Size" /> structure, typically measured in pixels.</returns>
        public float Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Size" /> structure from the specified <see cref="Point" /> structure.</summary>
        /// <param name="pt">The <see cref="Point" /> structure from which to initialize this <see cref="Size" /> structure. </param>
        public Size(Point pt)
        {
            this.width = pt.X;
            this.height = pt.Y;
        }

        /// <summary>Initializes a new instance of the <see cref="Size" /> structure from the specified dimensions.</summary>
        /// <param name="width">The width component of the new <see cref="Size" />. </param>
        /// <param name="height">The height component of the new <see cref="Size" />. </param>
        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>Initializes a new instance of the <see cref="Size" /> structure from the specified dimensions.</summary>
        /// <param name="width">The width component of the new <see cref="Size" />. </param>
        /// <param name="height">The height component of the new <see cref="Size" />. </param>
        public Size(float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>Adds the width and height of one <see cref="Size" /> structure to the width and height of another <see cref="Size" /> structure.</summary>
        /// <returns>A <see cref="Size" /> structure that is the result of the addition operation.</returns>
        /// <param name="sz1">The first <see cref="Size" /> structure to add.</param>
        /// <param name="sz2">The second <see cref="Size" /> structure to add.</param>
        public static Size Add(Size sz1, Size sz2)
        {
            return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>Converts the specified <see cref="Size" /> structure to a <see cref="Size" /> structure by rounding the values of the <see cref="Size" /> structure to the next higher integer values.</summary>
        /// <returns>The <see cref="Size" /> structure this method converts to.</returns>
        /// <param name="value">The <see cref="Size" /> structure to convert. </param>
        public static Size Ceiling(Size value)
        {
            return new Size((float)Math.Ceiling(value.Width), (float)Math.Ceiling(value.Height));
        }

        /// <summary>Tests to see whether the specified object is a <see cref="Size" /> structure with the same dimensions as this <see cref="Size" /> structure.</summary>
        /// <returns>true if <paramref name="obj" /> is a <see cref="Size" /> and has the same width and height as this <see cref="Size" />; otherwise, false.</returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        public override bool Equals(object obj)
        {
            if (!(obj is Size))
            {
                return false;
            }

            var size = (Size)obj;
            if (Math.Abs(size.width - this.width) > float.Epsilon)
            {
                return false;
            }

            return Math.Abs(size.height - this.height) < float.Epsilon;
        }


        /// <summary>Returns a hash code for this <see cref="Size" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="Size" /> structure.</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var temp = this;
                return (temp.Width.GetHashCode() * 397) ^ temp.Height.GetHashCode();
            }
        }

        /// <summary>Adds the width and height of one <see cref="Size" /> structure to the width and height of another <see cref="Size" /> structure.</summary>
        /// <returns>A <see cref="Size" /> structure that is the result of the addition operation.</returns>
        /// <param name="sz1">The first <see cref="Size" /> to add. </param>
        /// <param name="sz2">The second <see cref="Size" /> to add. </param>
        public static Size operator +(Size sz1, Size sz2)
        {
            return Add(sz1, sz2);
        }

        /// <summary>Tests whether two <see cref="Size" /> structures are equal.</summary>
        /// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, false.</returns>
        /// <param name="sz1">The <see cref="Size" /> structure on the left side of the equality operator. </param>
        /// <param name="sz2">The <see cref="Size" /> structure on the right of the equality operator. </param>
        public static bool operator ==(Size sz1, Size sz2)
        {
            if (Math.Abs(sz1.Width - sz2.Width) > float.Epsilon)
            {
                return false;
            }

            return Math.Abs(sz1.Height - sz2.Height) < float.Epsilon;
        }

        public static implicit operator Point(Size size)
        {
            return new Point(size.Width, size.Height);
        }

#if UNITY_5 || UNITY_2017
        public static implicit operator Vector2(Size size)
        {
            return new Vector2(size.Width, size.Height);
        }
 
        public static implicit operator Size(Vector2 vector)
        {
            return new Size(vector.x, vector.y);
        }
#endif

        /// <summary>Tests whether two <see cref="Size" /> structures are different.</summary>
        /// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; false if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
        /// <param name="sz1">The <see cref="Size" /> structure on the left of the inequality operator. </param>
        /// <param name="sz2">The <see cref="Size" /> structure on the right of the inequality operator. </param>
        public static bool operator !=(Size sz1, Size sz2)
        {
            return !(sz1 == sz2);
        }

        /// <summary>Subtracts the width and height of one <see cref="Size" /> structure from the width and height of another <see cref="Size" /> structure.</summary>
        /// <returns>A <see cref="Size" /> structure that is the result of the subtraction operation.</returns>
        /// <param name="sz1">The <see cref="Size" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="Size" /> structure on the right side of the subtraction operator. </param>
        /// <filterpriority>3</filterpriority>
        public static Size operator -(Size sz1, Size sz2)
        {
            return Subtract(sz1, sz2);
        }

        /// <summary>Converts the specified <see cref="Size" /> structure to a <see cref="Size" /> structure by rounding the values of the <see cref="Size" /> structure to the nearest integer values.</summary>
        /// <returns>The <see cref="Size" /> structure this method converts to.</returns>
        /// <param name="value">The <see cref="Size" /> structure to convert. </param>
        public static Size Round(Size value)
        {
            return new Size((float)Math.Round(value.Width), (float)Math.Round(value.Height));
        }

        /// <summary>Subtracts the width and height of one <see cref="Size" /> structure from the width and height of another <see cref="Size" /> structure.</summary>
        /// <returns>A <see cref="Size" /> structure that is a result of the subtraction operation.</returns>
        /// <param name="sz1">The <see cref="Size" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="Size" /> structure on the right side of the subtraction operator. </param>
        public static Size Subtract(Size sz1, Size sz2)
        {
            return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        /// <summary>Creates a human-readable string that represents this <see cref="Size" /> structure.</summary>
        /// <returns>A string that represents this <see cref="Size" />.</returns>
        public override string ToString()
        {
            return string.Concat("{Width=", this.width.ToString(CultureInfo.CurrentCulture), ", Height=", this.height.ToString(CultureInfo.CurrentCulture), "}");
        }

        /// <summary>Converts the specified <see cref="Size" /> structure to a <see cref="Size" /> structure by truncating the values of the <see cref="Size" /> structure to the next lower integer values.</summary>
        /// <returns>The <see cref="Size" /> structure this method converts to.</returns>
        /// <param name="value">The <see cref="Size" /> structure to convert. </param>
        public static Size Truncate(Size value)
        {
            return new Size((int)value.Width, (int)value.Height);
        }
    }
}
