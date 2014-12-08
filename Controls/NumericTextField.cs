namespace Codefarts.UIControls
{
    using System;
                                     
    public class NumericTextField : TextBox
    {
        private float minimum;
        private float maximum = 1;

        private int precsion;

        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = this.SanitizeValue(value, this.minimum, this.maximum);
            }
        }

        private string SanitizeValue(string value, float min, float max)
        {
            //  var acceptedValue = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.' };
            var i = 0;
            var periodIndex = -1;
            var newValue = value.Trim();
            while (i < newValue.Length)
            {
                switch (newValue[i])
                {
                    case '-':
                        if (i > 0)
                        {
                            newValue = newValue.Remove(i, 1);
                            continue;
                        }

                        break;

                    case '0':
                        if (i == 0)
                        {
                            newValue = newValue.Remove(i, 1);
                            continue;
                        }

                        break;

                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        // do nothing
                        break;

                    case '.':
                        periodIndex = periodIndex == -1 ? i : periodIndex;
                        if (periodIndex != i)
                        {
                            newValue = newValue.Remove(i, 1);
                            continue;
                        }

                        break;

                    default:
                        newValue = newValue.Remove(i, 1);
                        break;
                }

                i++;
            }

            float result;
            if (float.TryParse(newValue, out result))
            {
                if (result < min)
                {
                    newValue = Math.Round(min, this.Precsion).ToString();
                }
                else if (result > max)
                {
                    newValue = Math.Round(max, this.Precsion).ToString();
                }
                else
                {
                    newValue = Math.Round(result, this.precsion).ToString();
                }
            }
            else
            {
                newValue = value;
            }

            return newValue;
        }

        public virtual int Precsion
        {
            get
            {
                return this.precsion;
            }

            set
            {
                if (value < 0)
                {
                    this.precsion = 0;
                }
                else if (value > 28)
                {
                    this.precsion = 28;
                }
                else
                {
                    this.precsion = value;
                }
            }
        }

        /// <summary>
        ///  Gets or sets a minimum rotation angle in degrees. 
        /// </summary>
        public float Minimum
        {
            get
            {
                return this.minimum;
            }

            set
            {
                if (value > this.maximum)
                {
                    throw new ArgumentOutOfRangeException("Minimum value can not be greater then Maximum value.");
                }

                this.minimum = value;
            }
        }

        /// <summary>
        /// Gets or sets a maximum rotation angle in degrees. 
        /// </summary>
        public float Maximum
        {
            get
            {
                return this.maximum;
            }

            set
            {
                if (value < this.minimum)
                {
                    throw new ArgumentOutOfRangeException("Maximum value can not be less then Minimum value.");
                }

                this.maximum = value;
            }
        }
    }
}