namespace Codefarts.UIControls
{
    using System;
    using System.Globalization;

#if UNITY_5
    using UnityEngine;
#endif

    /// <summary>
    /// Stores a set of four floating-point numbers that represent the location and size of a rectangle.
    /// </summary>
#if !(SILVERLIGHT || WINDOWS_PHONE || NETFX_CORE || PORTABLE)
    [Serializable]
#endif
    public struct Rectangle
    {
        /// <summary>
        /// Represents an instance of the <see cref="Rectangle" /> class with its members uninitialized.
        /// </summary>
        public readonly static Rectangle Empty;

        /// <summary>
        /// The backing field for the <see cref="X"/> property.
        /// </summary>
        private float x;

        /// <summary>
        /// The backing field for the <see cref="Y"/> property.
        /// </summary>
        private float y;

        /// <summary>
        /// The backing field for the <see cref="Width"/> property.
        /// </summary>
        private float width;

        /// <summary>
        /// The backing field for the <see cref="Height"/> property.
        /// </summary>
        private float height;

        /// <summary>
        /// Gets or sets the x-coordinate of the bottom edge of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// The y-coordinate that is the sum of <see cref="Rectangle.Y" /> and <see cref="Rectangle.Height" /> of this <see cref="Rectangle" /> structure.
        /// </returns>
        public float Bottom
        {
            get
            {
                return this.y + this.height;
            }

            set
            {
                this.height = (value - this.y);
            }
        }

        /// <summary>
        /// Gets or sets the height of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// The height of this <see cref="Rectangle" /> structure. The default is 0.
        /// </returns>
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

        /// <summary>
        /// Tests whether the <see cref="Rectangle.Width" /> or <see cref="Rectangle.Height" /> property of this <see cref="Rectangle" /> has a value of zero.
        /// </summary>
        /// <returns>
        /// This property returns true if the <see cref="Rectangle.Width" /> or <see cref="Rectangle.Height" /> property of this <see cref="Rectangle" /> has a value of zero; otherwise, false.
        /// </returns>
        public bool IsEmpty
        {
            get
            {
                return this.Width <= 0f || this.Height <= 0f;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the left edge of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// The x-coordinate of the left edge of this <see cref="Rectangle" /> structure. 
        /// </returns>
        public float Left
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

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// A <see cref="Point" /> that represents the upper-left corner of this <see cref="Rectangle" /> structure.
        /// </returns>
        public Point Location
        {
            get
            {
                return new Point(this.x, this.y);
            }

            set
            {
                this.x = value.X;
                this.y = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate that is the sum of <see cref="Rectangle.X" /> and <see cref="Rectangle.Width" /> of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// The x-coordinate that is the sum of <see cref="Rectangle.X" /> and <see cref="Rectangle.Width" /> of this <see cref="Rectangle" /> structure. 
        /// </returns>
        public float Right
        {
            get
            {
                return this.x + this.width;
            }

            set
            {
                this.width = (value - this.x);
            }
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="Rectangle" />.
        /// </summary>
        /// <returns>
        /// A <see cref="Size" /> that represents the width and height of this <see cref="Rectangle" /> structure.
        /// </returns>
        public Size Size
        {
            get
            {
                return new Size(this.width, this.height);
            }

            set
            {
                this.width = value.Width;
                this.height = value.Height;
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the top edge of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// The y-coordinate of the top edge of this <see cref="Rectangle" /> structure.
        /// </returns>
        public float Top
        {
            get
            {
                return this.Y;
            }

            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// The width of this <see cref="Rectangle" /> structure. The default is 0.
        /// </returns>
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

        /// <summary>
        /// Gets or sets the x-coordinate of the upper-left corner of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// The x-coordinate of the upper-left corner of this <see cref="Rectangle" /> structure. The default is 0.
        /// </returns>
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

        /// <summary>
        /// Gets or sets the y-coordinate of the upper-left corner of this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// The y-coordinate of the upper-left corner of this <see cref="Rectangle" /> structure. The default is 0.
        /// </returns>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> class with the specified location and size.
        /// </summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle. </param>
        /// <param name="width">The width of the rectangle. </param>
        /// <param name="height">The height of the rectangle. </param>
        public Rectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> class with the specified location and size.
        /// </summary>
        /// <param name="location">A <see cref="Point" /> that represents the upper-left corner of the rectangular region. </param>
        /// <param name="size">A <see cref="Size" /> that represents the width and height of the rectangular region. </param>
        public Rectangle(Point location, Size size)
        {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.Width;
            this.height = size.Height;
        }

        /// <summary>
        /// Determines if the specified point is contained within this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// This method returns true if the point defined by <paramref name="positionX" /> and <paramref name="positionY" /> 
        /// is contained within this <see cref="Rectangle" /> structure; otherwise false.
        /// </returns>
        /// <param name="positionX">The x-coordinate of the point to test.</param>
        /// <param name="positionY">The y-coordinate of the point to test.</param>
        public bool Contains(float positionX, float positionY)
        {
            if (this.x > positionX || positionX >= this.x + this.width || this.y > positionY)
            {
                return false;
            }

            return positionY < this.y + this.height;
        }

        /// <summary>
        /// Determines if the specified point is contained within this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// This method returns true if the point represented by the <paramref name="point" /> parameter is contained 
        /// within this <see cref="Rectangle" /> structure; otherwise false.
        /// </returns>
        /// <param name="point">The <see cref="Point" /> to test. </param>
        public bool Contains(Point point)
        {
            return this.Contains(point.X, point.Y);
        }

        /// <summary>
        /// Determines if the rectangular region represented by <paramref name="rectangle" /> is entirely contained within 
        /// this <see cref="Rectangle" /> structure.
        /// </summary>
        /// <returns>
        /// This method returns true if the rectangular region represented by <paramref name="rectangle" /> is entirely 
        /// contained within the rectangular region represented by this <see cref="Rectangle" />; otherwise false.
        /// </returns>
        /// <param name="rectangle">The <see cref="Rectangle" /> to test. </param>
        public bool Contains(Rectangle rectangle)
        {
            if (this.X > rectangle.X || rectangle.X + rectangle.Width > this.X + this.Width || this.Y > rectangle.Y)
            {
                return false;
            }

            return rectangle.Y + rectangle.Height <= this.Y + this.Height;
        }

        /// <summary>
        /// Tests whether <paramref name="obj" /> is a <see cref="Rectangle" /> with the same location and size of this <see cref="Rectangle" />.
        /// </summary>
        /// <returns>
        /// This method returns true if <paramref name="obj" /> is a <see cref="Rectangle" /> and its X, Y, Width, and Height 
        /// properties are equal to the corresponding properties of this <see cref="Rectangle" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object" /> to test. </param>
        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
            {
                return false;
            }

            var rectangle = (Rectangle)obj;
            if (Math.Abs(rectangle.X - this.X) > float.Epsilon || Math.Abs(rectangle.Y - this.Y) > float.Epsilon || Math.Abs(rectangle.Width - this.Width) > float.Epsilon)
            {
                return false;
            }

            return Math.Abs(rectangle.Height - this.Height) < float.Epsilon;
        }

        /// <summary>
        /// Creates a <see cref="Rectangle" /> structure with upper-left corner and lower-right corner at the specified locations.
        /// </summary>
        /// <returns>
        /// The new <see cref="Rectangle" /> that this method creates.
        /// </returns>
        /// <param name="left">The x-coordinate of the upper-left corner of the rectangular region. </param>
        /// <param name="top">The y-coordinate of the upper-left corner of the rectangular region. </param>
        /// <param name="right">The x-coordinate of the lower-right corner of the rectangular region. </param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of the rectangular region. </param>
        public static Rectangle FromLTRB(float left, float top, float right, float bottom)
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Gets the hash code for this <see cref="Rectangle" /> structure. 
        /// </summary>
        /// <returns>
        /// The hash code for this <see cref="Rectangle" />.
        /// </returns>
        public override int GetHashCode()
        {
            return (int)((uint)this.X ^ ((uint)this.Y << 13 |
                    (uint)this.Y >> 19) ^ ((uint)this.Width << 26 |
                    (uint)this.Width >> 6) ^ ((uint)this.Height << 7 |
                    (uint)this.Height >> 25));
        }

        /// <summary>
        /// Enlarges this <see cref="Rectangle" /> structure by the specified amount.
        /// </summary>
        /// <param name="positionX">The amount to inflate this <see cref="Rectangle" /> structure horizontally. </param>
        /// <param name="positionY">The amount to inflate this <see cref="Rectangle" /> structure vertically. </param>
        public void Inflate(float positionX, float positionY)
        {
            this.x = this.x - positionX;
            this.y = this.y - positionY;
            this.width = this.width + 2f * positionX;
            this.height = this.height + 2f * positionY;
        }

        /// <summary>
        /// Enlarges this <see cref="Rectangle" /> by the specified amount.
        /// </summary>
        /// <param name="size">The amount to inflate this rectangle. </param>
        public void Inflate(Size size)
        {
            this.Inflate(size.Width, size.Height);
        }

        /// <summary>
        /// Creates and returns an enlarged copy of the specified <see cref="Rectangle" /> structure. 
        /// The copy is enlarged by the specified amount and the original rectangle remains unmodified.
        /// </summary>
        /// <returns>
        /// The enlarged <see cref="Rectangle" />.
        /// </returns>
        /// <param name="rect">The <see cref="Rectangle" /> to be copied. This rectangle is not modified. </param>
        /// <param name="x">The amount to enlarge the copy of the rectangle horizontally. </param>
        /// <param name="y">The amount to enlarge the copy of the rectangle vertically. </param>
        public static Rectangle Inflate(Rectangle rect, float x, float y)
        {
            var rectangle = rect;
            rectangle.Inflate(x, y);
            return rectangle;
        }

        /// <summary>
        /// Replaces this <see cref="Rectangle" /> structure with the intersection of itself and the specified <see cref="Rectangle" /> structure.
        /// </summary>
        /// <param name="rect">The rectangle to intersect. </param>
        public void Intersect(Rectangle rect)
        {
            var rectangle = Rectangle.Intersect(rect, this);
            this.x = rectangle.X;
            this.y = rectangle.Y;
            this.width = rectangle.Width;
            this.height = rectangle.Height;
        }

        /// <summary>
        /// Returns a <see cref="Rectangle" /> structure that represents the intersection of two rectangles. 
        /// If there is no intersection, and empty <see cref="Rectangle" /> is returned.
        /// </summary>
        /// <returns>
        /// A third <see cref="Rectangle" /> structure the size of which represents the overlapped area of the two specified rectangles.
        /// </returns>
        /// <param name="a">A rectangle to intersect. </param>
        /// <param name="b">A rectangle to intersect. </param>
        public static Rectangle Intersect(Rectangle a, Rectangle b)
        {
            var positionX = Math.Max(a.X, b.X);
            var positionRight = Math.Min(a.X + a.Width, b.X + b.Width);
            var positionY = Math.Max(a.Y, b.Y);
            var positionBottom = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if (positionRight < positionX || positionBottom < positionY)
            {
                return Rectangle.Empty;
            }

            return new Rectangle(positionX, positionY, positionRight - positionX, positionBottom - positionY);
        }

        /// <summary>
        /// Determines if this rectangle intersects with <paramref name="rectangle" />.
        /// </summary>
        /// <returns>
        /// This method returns true if there is any intersection.
        /// </returns>
        /// <param name="rectangle">The rectangle to test. </param>
        public bool IntersectsWith(Rectangle rectangle)
        {
            if (rectangle.X >= this.X + this.Width || this.X >= rectangle.X + rectangle.Width || rectangle.Y >= this.Y + this.Height)
            {
                return false;
            }

            return this.Y < rectangle.Y + rectangle.Height;
        }

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="position">The amount to offset the location. </param>
        public void Offset(Point position)
        {
            this.Offset(position.X, position.Y);
        }

        /// <summary>
        /// Adjusts the location of this rectangle by the specified amount.
        /// </summary>
        /// <param name="positionX">The amount to offset the location horizontally. </param>
        /// <param name="positionY">The amount to offset the location vertically. </param>
        public void Offset(float positionX, float positionY)
        {
            this.x = this.x + positionX;
            this.y = this.y + positionY;
        }

        /// <summary>
        /// Tests whether two <see cref="Rectangle" /> structures have equal location and size.
        /// </summary>
        /// <returns>
        /// This operator returns true if the two specified <see cref="Rectangle" /> structures have equal 
        /// <see cref="Rectangle.X" />, <see cref="Rectangle.Y" />, <see cref="Rectangle.Width" />, and
        /// <see cref="Rectangle.Height" /> properties.
        /// </returns>
        /// <param name="left">The <see cref="Rectangle" /> structure that is to the left of the equality operator. </param>
        /// <param name="right">The <see cref="Rectangle" /> structure that is to the right of the equality operator. </param>
        /// <remarks>Compares values using <see cref="float.Epsilon"/>.</remarks>
        public static bool operator ==(Rectangle left, Rectangle right)
        {
            if (Math.Abs(left.X - right.X) > float.Epsilon || Math.Abs(left.Y - right.Y) > float.Epsilon || Math.Abs(left.Width - right.Width) > float.Epsilon)
            {
                return false;
            }

            return Math.Abs(left.Height - right.Height) < float.Epsilon;
        }

        /// <summary>
        /// Tests whether two <see cref="Rectangle" /> structures differ in location or size.
        /// </summary>
        /// <returns>
        /// This operator returns true if any of the <see cref="Rectangle.X" />, <see cref="Rectangle.Y" />, <see cref="Rectangle.Width" />, 
        /// or <see cref="Rectangle.Height" /> properties of the two <see cref="Rectangle" /> structures are unequal; otherwise false.
        /// </returns>
        /// <param name="left">The <see cref="Rectangle" /> structure that is to the left of the inequality operator. </param>
        /// <param name="right">The <see cref="Rectangle" /> structure that is to the right of the inequality operator. </param>
        /// <remarks>Compares values using <see cref="float.Epsilon"/>.</remarks>
        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Converts the Location and <see cref="Size" /> of this <see cref="Rectangle" /> to a human-readable string.
        /// </summary>
        /// <returns>
        /// A string that contains the position, width, and height of this <see cref="Rectangle" /> structure. 
        /// For example, "{X=20, Y=20, Width=100, Height=50}".
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{X={0}, Y={1}, Width={2}, Height={3}}}", this.x, this.y, this.width, this.height);
        }

        /// <summary>
        /// Creates the smallest possible third rectangle that can contain both of two rectangles that form a union.
        /// </summary>
        /// <returns>
        /// A third <see cref="Rectangle" /> structure that contains both of the two rectangles that form the union.
        /// </returns>
        /// <param name="a">A rectangle to union. </param>
        /// <param name="b">A rectangle to union. </param>
        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            var positionX = Math.Min(a.X, b.X);
            var positionRight = Math.Max(a.X + a.Width, b.X + b.Width);
            var positionY = Math.Min(a.Y, b.Y);
            var positionBottom = Math.Max(a.Y + a.Height, b.Y + b.Height);
            return new Rectangle(positionX, positionY, positionRight - positionX, positionBottom - positionY);
        }


#if UNITY_5
        public static implicit operator Rect(Rectangle rectangle)
        {
            return new Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static implicit operator Rectangle(Rect rectangle)
        {
            return new Rectangle(rectangle.x, rectangle.y, rectangle.width, rectangle.height);
        }
#endif
    }
}