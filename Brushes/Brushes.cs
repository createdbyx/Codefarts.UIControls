namespace Codefarts.UIControls
{
    using System.Collections.Generic;

#if UNITY_5
    using UnityEngine;
#endif

    /// <summary>
    /// Implements a set of predefined <see cref="SolidColorBrush" /> objects.
    /// </summary>
    public sealed class Brushes
    {
        /// <summary>
        /// The cached brushes dictionary used to hold onto brush references that are returned from the properties in the <see cref="Brushes"/> class.
        /// </summary>
        private static Dictionary<int, SolidColorBrush> cachedBrushes;

        /// <summary>
        /// Initializes the <see cref="Brushes"/> class.
        /// </summary>
        static Brushes()
        {
            cachedBrushes = new Dictionary<int, SolidColorBrush>();
        }

        /// <summary>
        /// Creates a <see cref="SolidColorBrush"/> from a ARGB integer.
        /// </summary>
        /// <param name="argb">The integer representing the ARGB color data.</param>
        /// <returns>Returns a solid color brush reference.</returns>
        private static SolidColorBrush SolidColorBrushFromArgb(int argb)
        {
            SolidColorBrush solidColorBrush;
            lock (Brushes.cachedBrushes)
            {
                if (!Brushes.cachedBrushes.TryGetValue(argb, out solidColorBrush))
                {
                    solidColorBrush = new SolidColorBrush(Color.FromArgb(argb), true);
                    Brushes.cachedBrushes[argb] = solidColorBrush;
                }
            }

            return solidColorBrush;
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF0F8FF. </summary>
        /// <returns>A frozen <see cref="SolidColorBrush" /> with a <see cref="SolidColorBrush.Color" /> of #FFF0F8FF.</returns>
        public static SolidColorBrush AliceBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-984833);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFAEBD7. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush AntiqueWhite
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-332841);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF00FFFF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Aqua
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16711681);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF7FFFD4. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Aquamarine
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8388652);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF0FFFF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Azure
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-983041);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF5F5DC. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Beige
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-657956);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFE4C4. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Bisque
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-6972);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF000000. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Black
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16777216);
            }
        }

        /// <summary> Gets the solid fill color that has a hexadecimal value of #FFFFEBCD. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush BlanchedAlmond
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5171);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF0000FF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Blue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16776961);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF8A2BE2. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush BlueViolet
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7722014);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFA52A2A. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Brown
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5952982);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFDEB887. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush BurlyWood
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2180985);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF5F9EA0. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush CadetBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-10510688);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF7FFF00. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Chartreuse
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8388864);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFD2691E. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Chocolate
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2987746);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF7F50. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Coral
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-32944);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF6495ED. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush CornflowerBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-10185235);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFF8DC. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Cornsilk
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1828);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFDC143C. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Crimson
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2354116);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF00FFFF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Cyan
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16711681);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF00008B. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16777077);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF008B8B. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkCyan
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16741493);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFB8860B. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkGoldenrod
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-4684277);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFA9A9A9. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkGray
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5658199);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF006400. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16751616);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFBDB76B. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkKhaki
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-4343957);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF8B008B. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkMagenta
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7667573);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF556B2F. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkOliveGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-11179217);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF8C00. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkOrange
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-29696);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF9932CC. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkOrchid
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-6737204);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF8B0000. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkRed
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7667712);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFE9967A. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkSalmon
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1468806);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF8FBC8F. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkSeaGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7357297);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF483D8B. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkSlateBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-12042869);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF2F4F4F. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkSlateGray
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-13676721);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF00CED1. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkTurquoise
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16724271);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF9400D3. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DarkViolet
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7077677);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF1493. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DeepPink
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-60269);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF00BFFF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DeepSkyBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16728065);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF696969. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DimGray
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-9868951);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF1E90FF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush DodgerBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-14774017);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFB22222. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Firebrick
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5103070);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFFAF0. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush FloralWhite
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1296);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF228B22. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush ForestGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-14513374);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF00FF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Fuchsia
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-65281);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFDCDCDC. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Gainsboro
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2302756);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF8F8FF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush GhostWhite
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-460545);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFD700. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Gold
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-10496);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFDAA520. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Goldenrod
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2448096);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF808080. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Gray
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8355712);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF008000. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Green
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16744448);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFADFF2F. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush GreenYellow
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5374161);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF0FFF0. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Honeydew
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-983056);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF69B4. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush HotPink
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-38476);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFCD5C5C. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush IndianRed
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-3318692);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF4B0082. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Indigo
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-11861886);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFFFF0. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Ivory
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF0E68C. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Khaki
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-989556);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFE6E6FA. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Lavender
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1644806);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFF0F5. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LavenderBlush
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-3851);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF7CFC00. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LawnGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8586240);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFFACD. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LemonChiffon
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1331);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFADD8E6. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5383962);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF08080. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightCoral
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1015680);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFE0FFFF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightCyan
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2031617);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFAFAD2. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightGoldenrodYellow
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-329006);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFD3D3D3. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightGray
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2894893);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF90EE90. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7278960);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFB6C1. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightPink
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-18751);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFA07A. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightSalmon
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-24454);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF20B2AA. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightSeaGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-14634326);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF87CEFA. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightSkyBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7876870);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF778899. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightSlateGray
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8943463);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFB0C4DE. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightSteelBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5192482);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFFFE0. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LightYellow
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-32);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF00FF00. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Lime
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16711936);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF32CD32. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush LimeGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-13447886);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFAF0E6. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Linen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-331546);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF00FF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Magenta
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-65281);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF800000. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Maroon
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8388608);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF66CDAA. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumAquamarine
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-10039894);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF0000CD. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16777011);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFBA55D3. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumOrchid
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-4565549);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF9370DB. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumPurple
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7114533);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF3CB371. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumSeaGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-12799119);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF7B68EE. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumSlateBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8689426);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF00FA9A. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumSpringGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16713062);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF48D1CC. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumTurquoise
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-12004916);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFC71585. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MediumVioletRed
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-3730043);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF191970. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MidnightBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-15132304);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF5FFFA. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MintCream
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-655366);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFE4E1. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush MistyRose
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-6943);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFE4B5. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Moccasin
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-6987);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFDEAD. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush NavajoWhite
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8531);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF000080. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Navy
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16777088);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFDF5E6. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush OldLace
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-133658);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF808000. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Olive
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8355840);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF6B8E23. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush OliveDrab
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-9728477);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFA500. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Orange
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-23296);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF4500. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush OrangeRed
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-47872);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFDA70D6. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Orchid
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2461482);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFEEE8AA. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush PaleGoldenrod
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1120086);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF98FB98. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush PaleGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-6751336);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFAFEEEE. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush PaleTurquoise
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5247250);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFDB7093. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush PaleVioletRed
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2396013);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFEFD5. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush PapayaWhip
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-4139);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFDAB9. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush PeachPuff
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-9543);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFCD853F. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Peru
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-3308225);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFC0CB. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Pink
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16181);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFDDA0DD. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Plum
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2252579);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFB0E0E6. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush PowderBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-5185306);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF800080. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Purple
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-8388480);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF0000. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Red
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-65536);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFBC8F8F. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush RosyBrown
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-4419697);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF4169E1. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush RoyalBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-12490271);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF8B4513. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SaddleBrown
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7650029);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFA8072. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Salmon
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-360334);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF4A460. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SandyBrown
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-744352);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF2E8B57. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SeaGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-13726889);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFF5EE. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SeaShell
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2578);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFA0522D. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Sienna
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-6270419);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFC0C0C0. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Silver
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-4144960);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF87CEEB. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SkyBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-7876885);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF6A5ACD. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SlateBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-9807155);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF708090. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SlateGray
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-9404272);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFFAFA. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Snow
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1286);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF00FF7F. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SpringGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16711809);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF4682B4. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush SteelBlue
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-12156236);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFD2B48C. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Tan
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2968436);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF008080. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Teal
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-16744320);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFD8BFD8. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Thistle
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-2572328);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFF6347. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Tomato
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-40121);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #00FFFFFF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Transparent
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(16777215);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF40E0D0. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Turquoise
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-12525360);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFEE82EE. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Violet
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1146130);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF5DEB3. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Wheat
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-663885);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFFFFF. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush White
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-1);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFF5F5F5. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush WhiteSmoke
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-657931);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FFFFFF00. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush Yellow
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-256);
            }
        }

        /// <summary>Gets the solid fill color that has a hexadecimal value of #FF9ACD32. </summary>
        /// <returns>A solid fill color.</returns>
        public static SolidColorBrush YellowGreen
        {
            get
            {
                return Brushes.SolidColorBrushFromArgb(-6632142);
            }
        }

        private Brushes()
        {
        }
    }
}