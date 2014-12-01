/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using System;
using UnityEngine;

namespace Codefarts.UIControls.Unity.Editor
{
    public class Vector2Field : FieldControl
    {
        private Vector2 minValue;
        private Vector2 maxValue;
        private Vector2 value;


        public Vector2Field()
        {
            this.minValue = Vector2.one * float.MinValue;
            this.maxValue = Vector2.one * float.MinValue;
        }

        public Vector2Field(Vector2 minValue, Vector2 maxValue)
            : this()
        {
            if (minValue.x > maxValue.x) throw new ArgumentOutOfRangeException("Minimum x value can not be greater then Maximum value.");
            if (maxValue.x < minValue.x) throw new ArgumentOutOfRangeException("Maximum x value can not be less then Minimum value.");
            if (minValue.y > maxValue.y) throw new ArgumentOutOfRangeException("Minimum y value can not be greater then Maximum value.");
            if (maxValue.y < minValue.y) throw new ArgumentOutOfRangeException("Maximum y value can not be less then Minimum value.");
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public Vector2Field(Vector2 minValue, Vector2 maxValue, Vector2 value)
            : this(minValue, maxValue)
        {
            this.Value = value;
        }

        public Vector2Field(Vector2 value)
            : this()
        {
            this.Value = value;
        }

        public Vector2Field(string text)
            : this()
        {
            this.Text = text;
        }

        public Vector2Field(string text, Vector2 minValue, Vector2 maxValue)
            : this()
        {
            if (minValue.x > maxValue.x) throw new ArgumentOutOfRangeException("Minimum x value can not be greater then Maximum value.");
            if (maxValue.x < minValue.x) throw new ArgumentOutOfRangeException("Maximum x value can not be less then Minimum value.");
            if (minValue.y > maxValue.y) throw new ArgumentOutOfRangeException("Minimum y value can not be greater then Maximum value.");
            if (maxValue.y < minValue.y) throw new ArgumentOutOfRangeException("Maximum y value can not be less then Minimum value.");
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Text = text;
        }

        public Vector2Field(string text, Vector2 minValue, Vector2 maxValue, Vector2 value)
            : this(minValue, maxValue)
        {
            this.Value = value;
            this.Text = text;
        }

        public Vector2Field(string text, Vector2 value)
            : this()
        {
            this.Text = text;
            this.Value = value;
        }

        public Vector2 MinValue
        {
            get { return this.minValue; }
            set
            {
                this.minValue = value;
                if (this.minValue.x > this.maxValue.x) this.minValue.x = this.maxValue.x;
                if (this.minValue.y > this.maxValue.y) this.minValue.y = this.maxValue.y;
            }
        }

        public Vector2 MaxValue
        {
            get { return this.maxValue; }
            set
            {
                this.maxValue = value;
                if (this.maxValue.x < this.minValue.x) this.maxValue.x = this.minValue.x;
                if (this.maxValue.y < this.minValue.y) this.maxValue.y = this.minValue.y;
            }
        }

        public Vector2 Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                if (this.value.x < this.minValue.x) this.value.x = this.minValue.x;
                if (this.value.x > this.maxValue.x) this.value.x = this.maxValue.x;
                if (this.value.y < this.minValue.y) this.value.y = this.minValue.y;
                if (this.value.y > this.maxValue.y) this.value.y = this.maxValue.y;
            }
        }
    }
}