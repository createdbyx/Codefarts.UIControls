namespace Codefarts.UIControls
{
    using Codefarts.UIControls.Models;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Provides extension methods for the <see cref="Markup"/> class.
    /// </summary>
    public static class MarkupExtensionMethods
    {
        public static void SetProperty(this Markup markup, string name, bool conditional, object value)
        {
            if (markup == null)
            {
                throw new ArgumentNullException("markup");
            }

            if (!conditional)
            {
                return;
            }

            markup.Properties[name] = value;
        }

        public static bool GetValue<T>(this Markup markup, string name, Action<T> callback)
        {
            if (markup == null)
            {
                throw new ArgumentNullException("markup");
            }

            object value;
            if (markup.Properties != null && markup.Properties.TryGetValue(name, out value))
            {
                try
                {
#if WINDOWS_UWP
                    var t = typeof(T);
                    var isEnum = t.GetTypeInfo().IsEnum;
#else
                    var isEnum = typeof(T).IsEnum;
#endif
                    if (isEnum)
                    {
                        var convertedValue = (T)Enum.Parse(typeof(T), value.ToString(), true);
                        callback(convertedValue);
                    }
                    else
                    {
                        var convertedValue = (T)Convert.ChangeType(value, typeof(T), null);
                        callback(convertedValue);
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }


        public static bool GetValue<T>(this Markup markup, string name, out T result, T defaultValue)
        {
            if (markup == null)
            {
                throw new ArgumentNullException("markup");
            }

            result = defaultValue;
            object value;
            if (markup.Properties != null && markup.Properties.TryGetValue(name, out value))
            {
                try
                {
                    var convertedValue = (T)Convert.ChangeType(value, typeof(T), null);
                    result = convertedValue;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }


        public static bool GetValue<T>(this Markup markup, IEnumerable<string> names, Action<T> callback)
        {
            if (markup == null)
            {
                throw new ArgumentNullException("markup");
            }

            foreach (var name in names)
            {
                if (GetValue<T>(markup, name, x => callback(x)))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool GetValue<T>(this Markup markup, string name, Func<string, T> valueParser, Action<T[]> callback)
        {
            if (markup == null)
            {
                throw new ArgumentNullException("markup");
            }

            object value;
            if (markup.Properties.TryGetValue(name, out value))
            {
                var stringValue = (string)value;
                var parts = stringValue.Split(',');
                var array = new T[parts.Length];
                for (var i = 0; i < array.Length; i++)
                {
                    array[i] = valueParser(parts[i]);
                }

                callback(array);
                return true;
            }

            return false;
        }

        public static bool GetValue<T>(this Markup markup, string name, Func<string, T> enumParser, Action<T> callback)
        {
            if (markup == null)
            {
                throw new ArgumentNullException("markup");
            }

            object value;
            if (markup.Properties.TryGetValue(name, out value))
            {
                callback(enumParser((string)value));
                return true;
            }

            return false;
        }

        public static bool ParseBrush(this Markup markup, string name, Action<Brush> callback)
        {
            string value = null;
            GetValue<string>(markup, name, x => value = x);
            if (string.IsNullOrEmpty(value) || value.Trim() == string.Empty || value.Trim().ToLower().Replace(" ", string.Empty) == "{x:null}")
            {
                return false;
            }

            // check if names brush
            if (value.StartsWith("#"))
            {
                if (value.Length != 9)
                {
                    return false;
                }

                //#FF00FF00
                var a = value.Substring(1, 2);
                var r = value.Substring(3, 2);
                var g = value.Substring(5, 2);
                var b = value.Substring(7, 2);
                var color = Color.FromArgb(
                    byte.Parse(a, System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(r, System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(g, System.Globalization.NumberStyles.HexNumber),
                    byte.Parse(b, System.Globalization.NumberStyles.HexNumber));
                callback(new SolidColorBrush(color));
                return true;
            }

            var validChars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var index = Array.IndexOf(validChars, value.ToLower()[0]);
            if (index == -1)
            {
                return false;
            }

            // try to convert based on name
            var prop = typeof(Brushes).GetProperty(value, BindingFlags.Static | BindingFlags.Public);
            if (prop == null)
            {
                return false;
            }

            Brush brush;
            try
            {
                brush = (Brush)prop.GetGetMethod().Invoke(null, null);
            }
            catch
            {
                return false;
            }

            callback(brush);
            return true;
        }

        public static void AssignCommonProperties(this Markup markup, Control control)
        {
            // Tooltip
            GetValue<string>(markup, "ToolTip", x => control.ToolTip = x);

            // Tag/Uid
            GetValue<string>(markup, "Tag", x => control.Tag = x);

            // ctrl name
            GetValue<string>(markup, "Name", x => control.Name = x);

            // ctrl visibility
            GetValue<bool>(markup, "IsVisible", x => control.IsVisible = x);

            // ctrl enabled
            GetValue<bool>(markup, "IsEnabled", x => control.IsEnabled = x);

            // ctrl ClipToBounds
            GetValue<bool>(markup, "ClipToBounds", x => control.ClipToBounds = x);

            // ctrl opacity
            GetValue<float>(markup, "Opacity", x => control.Opacity = x);

            // ctrl auto size
            GetValue<bool>(markup, "AutoSize", x => control.AutoSize = x);

            // control brushes
            ParseBrush(markup, "Background", x => control.Background = x);
            ParseBrush(markup, "Foreground", x => control.Foreground = x);

            // min/max size
            var size = control.MinimumSize;
            GetValue<float>(markup, "MinWidth", x => size.Width = x);
            GetValue<float>(markup, "MinHeight", x => size.Height = x);
            control.MinimumSize = size;

            size = control.MaximumSize;
            GetValue<float>(markup, "MaxWidth", x => size.Width = x);
            GetValue<float>(markup, "MaxHeight", x => size.Height = x);
            control.MaximumSize = size;
        }

        public static void ProcessHorizontialAndVerticalAlignments(this Markup markup, Control control)
        {
            GetValue(markup, "HorizontalAlignment", x => (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), x, true), x => control.HorizontalAlignment = x);
            GetValue(markup, "VerticalAlignment", x => (VerticalAlignment)Enum.Parse(typeof(VerticalAlignment), x, true), x => control.VerticalAlignment = x);
        }

        public static void ParseRangeBase(this Markup markup, RangeBase control)
        {
            GetValue<float>(markup, "Minimum", x => control.Minimum = x);
            GetValue<float>(markup, "Maximum", x => control.Maximum = x);
            GetValue<int>(markup, "Precision", x => control.Precision = x);
            GetValue<float>(markup, "Value", x => control.Value = x);
            GetValue<float>(markup, "LargeChange", x => control.LargeChange = x);
            GetValue<float>(markup, "SmallChange", x => control.SmallChange = x);
        }

        public static void ParseGridPosition(this Markup markup, Control control)
        {
            //if (!(control.Parent is Grid))
            //{
            //    return;
            //}

            //var grid = (Grid)control.Parent;
            //var index = grid.Controls.IndexOf(control);
            //if (index == -1)
            //{
            //    // somethings wonky the control was not found in it's parent when it should have been
            //}

            var row = 0;
            var column = 0;
            GetValue<int>(markup, "Grid.Column", x => column = x);
            GetValue<int>(markup, "Grid.Row", x => row = x);
            control.Properties["Grid.Row"] = row;
            control.Properties["Grid.Column"] = column;

            // var newIndex = (row * grid.Columns) + column;
            //  grid.Controls.Move(index, newIndex);
        }

        public static void ParseCanvasPosition(this Markup markup, Control control)
        {
            var top = control.Top;
            var left = control.Left;
            GetValue<float>(markup, "Canvas.Left", x => left = x);
            GetValue<float>(markup, "Canvas.Top", x => top = x);
            control.Properties["Canvas.Top"] = top;
            control.Properties["Canvas.Left"] = left;
        }

        public static void ParsePositionAndSize(this Markup markup, Control control, Control parent)
        {
            ParsePositionAndSize(markup, control, parent, false);
        }

        public static void ParsePositionAndSize(this Markup markup, Control control, Control parent, bool designSizeFallback)
        {
            // process anchor first
            ProcessHorizontialAndVerticalAlignments(markup, control);

            var size = control.Size;
            if (!GetValue<float>(markup, "Width", x => size.Width = x) && designSizeFallback)
            {
                GetValue<float>(markup, "DesignWidth", x => size.Width = x);
            }
            if (!GetValue<float>(markup, "Height", x => size.Height = x) && designSizeFallback)
            {
                GetValue<float>(markup, "DesignHeight", x => size.Height = x);
            }
            control.Size = size;

            GetValue(markup, "Margin", x => float.Parse(x), x =>
                {
                    float top;
                    float left;
                    switch (x.Length)
                    {
                        case 1:
                            left = x[0];
                            if (parent != null)
                            {
                                control.SetBounds(left, left, parent.Width - left, parent.Height - left);
                            }
                            else
                            {
                                //do something
                            }

                            break;

                        case 2:
                            left = x[0];
                            top = x[1];
                            // control.Location = new Point(left, top);
                            if (parent != null)
                            {
                                control.SetBounds(left, top, parent.Width - x[0], parent.Height - x[1]);
                            }
                            else
                            {
                                //do something
                            }
                            break;

                        case 4:
                            left = x[0];
                            top = x[1];
                            control.SetBounds(left, top, x[2] - left, x[3] - top);
                            break;
                    }
                });

            GetValue(markup, "Location", x => float.Parse(x), x =>
            {
                float left;
                switch (x.Length)
                {
                    case 1:
                        left = x[0];
                        control.Location = new Point(left, left);
                        break;

                    case 2:
                        left = x[0];
                        var top = x[1];
                        control.Location = new Point(left, top);
                        break;
                }
            });

            GetValue(markup, "Size", x => float.Parse(x), x =>
            {
                float width;
                switch (x.Length)
                {
                    case 1:
                        width = x[0];
                        control.Size = new Size(width, width);
                        break;

                    case 2:
                        width = x[0];
                        var height = x[1];
                        control.Size = new Size(width, height);
                        break;
                }
            });

            var location = control.Location;
            GetValue<float>(markup, "Left", x => location.X = x);
            GetValue<float>(markup, "Top", x => location.Y = x);
            control.Location = location;
        }

        public static Rectangle ParsePositionAndSize(this Markup markup, bool designSizeFallback, Rectangle? defaultValue)
        {
            // process anchor first
            //   ProcessHorizontialAndVerticalAlignments(markup, control);

            var size = defaultValue.HasValue ? defaultValue.Value.Size : Size.Empty;//= control.Size;
            var position = defaultValue.HasValue ? defaultValue.Value.Location : Point.Empty;
            if (!GetValue<float>(markup, "Width", x => size.Width = x) && designSizeFallback)
            {
                GetValue<float>(markup, "DesignWidth", x => size.Width = x);
            }
            if (!GetValue<float>(markup, "Height", x => size.Height = x) && designSizeFallback)
            {
                GetValue<float>(markup, "DesignHeight", x => size.Height = x);
            }

            GetValue(markup, "Margin", x => float.Parse(x), x =>
            {
                var top = 0f;
                var left = 0f;
                Rectangle parentRect;
                switch (x.Length)
                {
                    case 1:
                        left = x[0];
                        top = left;

                        if (markup.Parent != null)
                        {
                            parentRect = markup.Parent.ParsePositionAndSize(designSizeFallback, null);

                            size.Width = parentRect.Width - left;
                            size.Height = parentRect.Height - left;
                        }

                        //if (parent != null)
                        //{
                        //    control.SetBounds(left, left, parent.Width - left, parent.Height - left);
                        //}
                        //else
                        //{
                        //    //do something
                        //}

                        break;

                    case 2:
                        left = x[0];
                        top = x[1];
                        if (markup.Parent != null)
                        {
                            parentRect = markup.Parent.ParsePositionAndSize(designSizeFallback, null);

                            size.Width = parentRect.Width - left;
                            size.Height = parentRect.Height - top;
                        }
                        // control.Location = new Point(left, top);
                        //if (parent != null)
                        //{
                        //    control.SetBounds(left, top, parent.Width - x[0], parent.Height - x[1]);
                        //}
                        //else
                        //{
                        //    //do something
                        //}
                        break;

                    case 4:
                        left = x[0];
                        top = x[1];
                        size.Width = x[2] - left;
                        size.Height = x[3] - top;
                        //control.SetBounds(left, top, x[2] - left, x[3] - top);
                        break;
                }

                position = new Point(left, top);
            });

            GetValue(markup, "Location", x => float.Parse(x), x =>
            {
                float left;
                switch (x.Length)
                {
                    case 1:
                        left = x[0];
                        // control.Location = new Point(left, left);
                        position = new Point(left, left);
                        break;

                    case 2:
                        left = x[0];
                        var top = x[1];
                        // control.Location = new Point(left, top);
                        position = new Point(left, top);
                        break;
                }
            });

            GetValue(markup, "Size", x => float.Parse(x), x =>
            {
                float width;
                switch (x.Length)
                {
                    case 1:
                        width = x[0];
                        size = new Size(width, width);
                        break;

                    case 2:
                        width = x[0];
                        var height = x[1];
                        size = new Size(width, height);
                        break;
                }
            });

            // var location = control.Location;
            GetValue<float>(markup, "Left", x => position.X = x);
            GetValue<float>(markup, "Top", x => position.Y = x);

            return new Rectangle(position, size);
        }
    }
}