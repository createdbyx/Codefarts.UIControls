/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using System;

namespace Codefarts.UIControls.Unity.Editor
{
    public class FloatField : FieldControl
    {
        private float minValue;
        private float maxValue;
        private float value;
        public event EventHandler ValueChanged;

        public FloatField()
        {
            this.minValue = float.MinValue;
            this.maxValue = float.MaxValue;
        }

        public FloatField(float minValue, float maxValue)
            : this()
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException("Minimum value can not be greater then Maximum value.");
            if (maxValue < minValue) throw new ArgumentOutOfRangeException("Maximum value can not be less then Minimum value.");
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public FloatField(float minValue, float maxValue, float value)
            : this(minValue, maxValue)
        {
            this.Value = value;
        }

        public FloatField(float value)
            : this()
        {
            this.Value = value;
        }

        public FloatField(string text)
            : this()
        {
            this.Text = text;
        }

        public FloatField(string text, float minValue, float maxValue)
            : this()
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException("Minimum value can not be greater then Maximum value.");
            if (maxValue < minValue) throw new ArgumentOutOfRangeException("Maximum value can not be less then Minimum value.");
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Text = text;
        }

        public FloatField(string text, float minValue, float maxValue, float value)
            : this(minValue, maxValue)
        {
            this.Value = value;
            this.Text = text;
        }

        public FloatField(string text, float value)
            : this()
        {
            this.Text = text;
            this.Value = value;
        }

        public float MinValue
        {
            get { return this.minValue; }
            set
            {
                this.minValue = value;
                if (this.minValue > this.maxValue) this.minValue = this.maxValue;
            }
        }

        public float MaxValue
        {
            get { return this.maxValue; }
            set
            {
                this.maxValue = value;
                if (this.maxValue < this.minValue) this.maxValue = this.minValue;
            }
        }

        public float Value
        {
            get { return this.value; }
            set
            {
                if (value < this.minValue) value = this.minValue;
                if (value > this.maxValue) value = this.maxValue;
                var changed = this.value != value;
                this.value = value;
                if (changed) this.OnValueChanged();
            }
        }

        private void OnValueChanged()
        {
            var handler = this.ValueChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}