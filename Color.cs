namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Describes a color in terms of alpha, red, green, and blue channels.
    /// </summary>
    public struct Color : IEquatable<Color>
    {
        /// <summary>
        /// Represents a color that is null.
        /// </summary>
        public readonly static Color Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <param name="a">The red channel, <see cref="Color.A" />, of the new color.</param>
        /// <param name="r">The red channel, <see cref="Color.R" />, of the new color.</param>
        /// <param name="g">The green channel, <see cref="Color.G" />, of the new color.</param>
        /// <param name="b">The blue channel, <see cref="Color.B" />, of the new color.</param>
        public Color(float a, float r, float g, float b)
        {
            this.A = a;
            this.B = b;
            this.G = g;
            this.R = r;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <param name="r">The red channel, <see cref="Color.R" />, of the new color.</param>
        /// <param name="g">The green channel, <see cref="Color.G" />, of the new color.</param>
        /// <param name="b">The blue channel, <see cref="Color.B" />, of the new color.</param>
        public Color(float r, float g, float b)
            : this(1, r, g, b)
        {
        }

        /// <summary>
        /// Gets or sets the alpha channel value of the color.
        /// </summary>
        /// <returns>
        /// The alpha channel value of the color.
        /// </returns>
        public float A { get; set; }

        /// <summary>
        /// Gets or sets the blue channel value of the color.
        /// </summary>
        /// <returns>
        /// The blue channel value of the current <see cref="Color" /> structure.
        /// </returns>
        public float B { get; set; }

        /// <summary>
        /// Gets or sets the green channel value of the color.
        /// </summary>
        /// <returns>
        /// The green channel value of the current <see cref="Color" /> structure.
        /// </returns>
        public float G { get; set; }

        /// <summary>
        /// Gets or sets the red channel value of the color.
        /// </summary>
        /// <returns>
        /// The red channel value of the current <see cref="Color" /> structure.
        /// </returns>
        public float R { get; set; }

        /// <summary>Adds two <see cref="Color" /> structures. </summary>
        /// <returns>A new <see cref="Color" /> structure whose color values are the results of the addition operation.</returns>
        /// <param name="color1">The first <see cref="Color" /> structure to add.</param>
        /// <param name="color2">The second <see cref="Color" /> structure to add.</param>
        public static Color Add(Color color1, Color color2)
        {
            return color1 + color2;
        }

        /// <summary>Compares two <see cref="Color" /> structures for fuzzy equality. </summary>
        /// <returns>true if <paramref name="color1" /> and <paramref name="color2" /> are nearly identical; otherwise, false.</returns>
        /// <param name="color1">The first color to compare.</param>
        /// <param name="color2">The second color to compare.</param>
        public static bool AreClose(Color color1, Color color2)
        {
            return color1.IsClose(color2);
        }

        /// <summary>
        /// Gets the hue-saturation-brightness (HSB) brightness value for this <see cref="Color" /> structure.
        /// </summary>
        /// <returns>
        /// The brightness of this <see cref="Color" />. The brightness ranges from 0.0 through 1.0, where 0.0 represents black and 1.0 represents white.
        /// </returns>
        public float GetBrightness()
        {
            var r = this.R;
            var g = this.G;
            var b = this.B;
            var single = r;
            var single1 = r;
            if (g > single)
            {
                single = g;
            }
            if (b > single)
            {
                single = b;
            }
            if (g < single1)
            {
                single1 = g;
            }
            if (b < single1)
            {
                single1 = b;
            }
            return (single + single1) / 2f;
        }

        /// <summary>
        /// Gets the hue-saturation-brightness (HSB) hue value, in degrees, for this <see cref="Color" /> structure.
        /// </summary>
        /// <returns>
        /// The hue, in degrees, of this <see cref="Color" />. The hue is measured in degrees, ranging from 0.0 through 360.0, in HSB color space.
        /// </returns>
        public float GetHue()
        {
            var r = this.R;
            var g = this.G;
            var b = this.B;
            if (Math.Abs(r - g) < float.Epsilon && Math.Abs(g - b) < float.Epsilon)
            {
                return 0f;
            }
            var single = 0f;
            var single1 = r;
            var single2 = r;
            if (g > single1)
            {
                single1 = g;
            }
            if (b > single1)
            {
                single1 = b;
            }
            if (g < single2)
            {
                single2 = g;
            }
            if (b < single2)
            {
                single2 = b;
            }
            var single3 = single1 - single2;
            if (Math.Abs(r - single1) < float.Epsilon)
            {
                single = (g - b) / single3;
            }
            else if (Math.Abs(g - single1) < float.Epsilon)
            {
                single = 2f + (b - r) / single3;
            }
            else if (Math.Abs(b - single1) < float.Epsilon)
            {
                single = 4f + (r - g) / single3;
            }
            single = single * 60f;
            if (single < 0f)
            {
                single = single + 360f;
            }
            return single;
        }

        /// <summary>
        /// Gets the hue-saturation-brightness (HSB) saturation value for this <see cref="Color" /> structure.
        /// </summary>
        /// <returns>
        /// The saturation of this <see cref="Color" />. The saturation ranges from 0.0 through 1.0, where 0.0 is grayscale and 1.0 is the most saturated.
        /// </returns>
        public float GetSaturation()
        {
            var r = this.R;
            var g = this.G;
            var b = this.B;
            var single = 0f;
            var single1 = r;
            var single2 = r;
            if (g > single1)
            {
                single1 = g;
            }
            if (b > single1)
            {
                single1 = b;
            }
            if (g < single2)
            {
                single2 = g;
            }
            if (b < single2)
            {
                single2 = b;
            }
            if (Math.Abs(single1 - single2) > float.Epsilon)
            {
                single = (single1 + single2) / 2f > 0.5 ? (single1 - single2) / (2f - single1 - single2) : (single1 - single2) / (single1 + single2);
            }
            return single;
        }

        /// <summary>
        /// Sets the color channels of the color to within the gamut of 0 to 1, if they are outside that range.
        /// </summary>
        public void Clamp()
        {
            if (this.A < 0f)
            {
                this.A = 0f;
            }
            else
            {
                this.A = (this.A > 1f ? 1f : this.A);
            }

            if (this.R < 0f)
            {
                this.R = 0f;
            }
            else
            {
                this.R = (this.R > 1f ? 1f : this.R);
            }

            if (this.G < 0f)
            {
                this.G = 0f;
            }
            else
            {
                this.G = (this.G > 1f ? 1f : this.G);
            }

            if (this.B < 0f)
            {
                this.B = 0f;
            }
            else
            {
                this.B = (this.B > 1f ? 1f : this.B);
            }
        }

        /// <summary>Tests whether two <see cref="Color" /> structures are identical. </summary>
        /// <returns>true if <paramref name="color1" /> and <paramref name="color2" /> are exactly identical; otherwise, false.</returns>
        /// <param name="color1">The first <see cref="Color" /> structure to compare.</param>
        /// <param name="color2">The second <see cref="Color" /> structure to compare.</param>
        public static bool Equals(Color color1, Color color2)
        {
            return color1 == color2;
        }

        /// <summary>Tests whether the specified <see cref="Color" /> structure is identical to the current color.</summary>
        /// <returns>true if the specified <see cref="Color" /> structure is identical to the current <see cref="Color" /> structure; otherwise, false.</returns>
        /// <param name="color">The <see cref="Color" /> structure to compare to the current <see cref="Color" /> structure.</param>
        public bool Equals(Color color)
        {
            return this == color;
        }

        /// <summary>Tests whether the specified object is a <see cref="Color" /> structure and is equivalent to the current color. </summary>
        /// <returns>true if the specified object is a <see cref="Color" /> structure and is identical to the current <see cref="Color" /> structure; otherwise, false.</returns>
        /// <param name="o">The object to compare to the current <see cref="Color" /> structure.</param>
        public override bool Equals(object o)
        {
            if (!(o is Color))
            {
                return false;
            }

            return this == (Color)o;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var value = this;
                var hashCode = value.A.GetHashCode();
                hashCode = (hashCode * 397) ^ value.B.GetHashCode();
                hashCode = (hashCode * 397) ^ value.G.GetHashCode();
                hashCode = (hashCode * 397) ^ value.R.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>Creates a new <see cref="Color" /> structure by using the specified sRGB alpha channel and color channel values. </summary>
        /// <returns>A <see cref="Color" /> structure with the specified values.</returns>
        /// <param name="a">The alpha channel, <see cref="Color.A" />, of the new color.</param>
        /// <param name="r">The red channel, <see cref="Color.R" />, of the new color.</param>
        /// <param name="g">The green channel, <see cref="Color.G" />, of the new color.</param>
        /// <param name="b">The blue channel, <see cref="Color.B" />, of the new color.</param>
        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Color() { A = a / 255f, R = r / 255f, G = g / 255f, B = b / 255f };
        }

        /// <summary>Creates a new <see cref="Color" /> structure by using the specified sRGB alpha channel and color channel values. </summary>
        /// <returns>A <see cref="Color" /> structure with the specified values.</returns>
        /// <param name="a">The alpha channel, <see cref="Color.A" />, of the new color.</param>
        /// <param name="r">The red channel, <see cref="Color.R" />, of the new color.</param>
        /// <param name="g">The green channel, <see cref="Color.G" />, of the new color.</param>
        /// <param name="b">The blue channel, <see cref="Color.B" />, of the new color.</param>
        public static Color FromArgb(float a, float r, float g, float b)
        {
            return new Color() { A = a, R = r, G = g, B = b };
        }

        /// <summary>Creates a new <see cref="Color" /> structure by using the specified color channel values. </summary>
        /// <returns>A <see cref="Color" /> structure with the specified values and an alpha channel value of 255.</returns>
        /// <param name="r">The red channel, <see cref="Color.R" />, of the new color.</param>
        /// <param name="g">The green channel, <see cref="Color.G" />, of the new color.</param>
        /// <param name="b">The blue channel, <see cref="Color.B" />, of the new color.</param>
        public static Color FromRgb(byte r, byte g, byte b)
        {
            return Color.FromArgb(255, r, g, b);
        }

        /// <summary>
        /// Creates a <see cref="Color"/> from a ARGB integer.
        /// </summary>
        /// <param name="argb">The integer representing the ARGB color data.</param>
        /// <returns>Returns a color.</returns>
        public static Color FromArgb(int argb)
        {
            var color = new Color();
            color.A = (byte)((argb & -16777216) >> 24) / 255f;
            color.R = (byte)((argb & 16711680) >> 16) / 255f;
            color.G = (byte)((argb & 65280) >> 8) / 255f;
            color.B = (byte)(argb & 255) / 255f;
            return color;
        }

        public bool IsClose(Color color)
        {
            var value = this.AreClose(this.A, color.A);
            value = value && this.AreClose(this.R, color.R);
            value = value && this.AreClose(this.G, color.G);
            value = value && this.AreClose(this.B, color.B);
            return value;
        }

        private bool AreClose(float a, float b)
        {
            if (Math.Abs(a - b) < float.Epsilon)
            {
                return true;
            }

            var single = (Math.Abs(a) + Math.Abs(b) + 10f) * float.Epsilon;
            var single1 = a - b;
            if (-single >= single1)
            {
                return false;
            }

            return single > single1;
        }

        /// <summary>Multiplies the alpha, red, blue, and green channels of the specified <see cref="Color" /> structure by the specified value. </summary>
        /// <returns>A new <see cref="Color" /> structure whose color values are the results of the multiplication operation.</returns>
        /// <param name="color">The <see cref="Color" /> to be multiplied.</param>
        /// <param name="coefficient">The value to multiply by.</param>
        public static Color Multiply(Color color, float coefficient)
        {
            return color * coefficient;
        }

        /// <summary>Adds two <see cref="Color" /> structures. </summary>
        /// <returns>A new <see cref="Color" /> structure whose color values are the results of the addition operation.</returns>
        /// <param name="color1">The first <see cref="Color" /> structure to add.</param>
        /// <param name="color2">The second <see cref="Color" /> structure to add.</param>
        public static Color operator +(Color color1, Color color2)
        {
            return new Color() { A = color1.A + color2.A, R = color1.R + color2.R, G = color1.G + color2.G, B = color1.B + color2.B };
        }

        /// <summary>Tests whether two <see cref="Color" /> structures are identical. </summary>
        /// <returns>true if <paramref name="color1" /> and <paramref name="color2" /> are exactly identical; otherwise, false.</returns>
        /// <param name="color1">The first <see cref="Color" /> structure to compare.</param>
        /// <param name="color2">The second <see cref="Color" /> structure to compare.</param>
        public static bool operator ==(Color color1, Color color2)
        {
            if (Math.Abs(color1.R - color2.R) > float.Epsilon)
            {
                return false;
            }

            if (Math.Abs(color1.G - color2.G) > float.Epsilon)
            {
                return false;
            }

            if (Math.Abs(color1.B - color2.B) > float.Epsilon)
            {
                return false;
            }

            if (Math.Abs(color1.A - color2.A) > float.Epsilon)
            {
                return false;
            }

            return true;
        }

        /// <summary>Tests whether two <see cref="Color" /> structures are not identical. </summary>
        /// <returns>true if <paramref name="color1" /> and <paramref name="color2" /> are not equal; otherwise, false.</returns>
        /// <param name="color1">The first <see cref="Color" /> structure to compare.</param>
        /// <param name="color2">The second <see cref="Color" /> structure to compare.</param>
        public static bool operator !=(Color color1, Color color2)
        {
            return !(color1 == color2);
        }

        /// <summary>Multiplies the alpha, red, blue, and green channels of the specified <see cref="Color" /> structure by the specified value. </summary>
        /// <returns>A new <see cref="Color" /> structure whose color values are the results of the multiplication operation.</returns>
        /// <param name="color">The <see cref="Color" /> to be multiplied.</param>
        /// <param name="coefficient">The value to multiply by.</param>
        public static Color operator *(Color color, float coefficient)
        {
            var newColor = new Color()
            {
                A = color.A * coefficient,
                R = color.R * coefficient,
                G = color.G * coefficient,
                B = color.B * coefficient
            };

            return newColor;
        }

        /// <summary>Subtracts a <see cref="Color" /> structure from a <see cref="Color" /> structure. </summary>
        /// <returns>A new <see cref="Color" /> structure whose color values are the results of the subtraction operation.</returns>
        /// <param name="color1">The <see cref="Color" /> structure to be subtracted from.</param>
        /// <param name="color2">The <see cref="Color" /> structure to subtract from <paramref name="color1" />.</param>
        public static Color operator -(Color color1, Color color2)
        {
            return new Color() { A = color1.A - color2.A, R = color1.R - color2.R, G = color1.G - color2.G, B = color1.B - color2.B };
        }

        /// <summary>Subtracts a <see cref="Color" /> structure from a <see cref="Color" /> structure. </summary>
        /// <returns>A new <see cref="Color" /> structure whose color values are the results of the subtraction operation.</returns>
        /// <param name="color1">The <see cref="Color" /> structure to be subtracted from.</param>
        /// <param name="color2">The <see cref="Color" /> structure to subtract from <paramref name="color1" />.</param>
        public static Color Subtract(Color color1, Color color2)
        {
            return color1 - color2;
        }

        /// <summary>Creates a string representation of the color using the ScRGB channels. </summary>
        /// <returns>The string representation of the color.</returns>
        public override string ToString()
        {
            var color = this;
            color.Clamp();
            var a = (byte)(this.A * 255);
            var r = (byte)(this.R * 255);
            var g = (byte)(this.G * 255);
            var b = (byte)(this.B * 255);
            var strA = a.ToString("X2");
            var strR = r.ToString("X2");
            var strG = g.ToString("X2");
            var strB = b.ToString("X2");
            return "#" + strA + strR + strG + strB;
        }


#if UNITY_5 || UNITY_2017
        public static implicit operator UnityEngine.Color(Color color)
        {
            return new UnityEngine.Color(color.R, color.G, color.B, color.A);
        }

        public static implicit operator Color(UnityEngine.Color color)
        {
            return new Color(color.a, color.b, color.g, color.r);
        }    
#endif
    }
}