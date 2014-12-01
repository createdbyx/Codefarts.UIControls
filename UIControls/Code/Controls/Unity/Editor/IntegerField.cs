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
    public class IntegerField : FieldControl
    {
        private int minValue;
        private int maxValue;
        private int value;
        public event EventHandler ValueChanged;

        public IntegerField()
        {
            this.minValue = int.MinValue;
            this.maxValue = int.MaxValue;
        }

        public IntegerField(int minValue, int maxValue)
            : this()
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException("Minimum value can not be greater then Maximum value.");
            if (maxValue < minValue) throw new ArgumentOutOfRangeException("Maximum value can not be less then Minimum value.");
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public IntegerField(int minValue, int maxValue, int value)
            : this(minValue, maxValue)
        {
            this.Value = value;
        }

        public IntegerField(int value)
            : this()
        {
            this.Value = value;
        }

        public IntegerField(string text)
            : this()
        {
            this.Text = text;
        }

        public IntegerField(string text, int minValue, int maxValue)
            : this()
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException("Minimum value can not be greater then Maximum value.");
            if (maxValue < minValue) throw new ArgumentOutOfRangeException("Maximum value can not be less then Minimum value.");
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Text = text;
        }

        public IntegerField(string text, int minValue, int maxValue, int value)
            : this(minValue, maxValue)
        {
            this.Value = value;
            this.Text = text;
        }

        public IntegerField(string text, int value)
            : this()
        {
            this.Text = text;
            this.Value = value;
        }

        public int MinValue
        {
            get { return this.minValue; }
            set
            {
                this.minValue = value;
                if (this.minValue > this.maxValue) this.minValue = this.maxValue;
            }
        }

        public int MaxValue
        {
            get { return this.maxValue; }
            set
            {
                this.maxValue = value;
                if (this.maxValue < this.minValue) this.maxValue = this.minValue;
            }
        }

        public int Value
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
            if (this.ValueChanged != null) this.ValueChanged(this, EventArgs.Empty);
        }
    }
}