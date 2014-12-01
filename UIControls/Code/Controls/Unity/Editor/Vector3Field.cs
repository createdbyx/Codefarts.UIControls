/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls.Unity.Editor
{
    using System;

    using UnityEngine;

    public class Vector3Field : FieldControl
    {
        private Vector3 minValue;
        private Vector3 maxValue;
        private Vector3 value;


        public Vector3Field()
        {
            this.minValue = Vector3.one * float.MinValue;
            this.maxValue = Vector3.one * float.MinValue;
        }

        public Vector3Field(Vector3 minValue, Vector3 maxValue)
            : this()
        {
            if (minValue.x > maxValue.x) throw new ArgumentOutOfRangeException("Minimum x value can not be greater then Maximum value.");
            if (maxValue.x < minValue.x) throw new ArgumentOutOfRangeException("Maximum x value can not be less then Minimum value.");
            if (minValue.y > maxValue.y) throw new ArgumentOutOfRangeException("Minimum y value can not be greater then Maximum value.");
            if (maxValue.y < minValue.y) throw new ArgumentOutOfRangeException("Maximum y value can not be less then Minimum value.");
            if (minValue.z > maxValue.z) throw new ArgumentOutOfRangeException("Minimum z value can not be greater then Maximum value.");
            if (maxValue.z < minValue.z) throw new ArgumentOutOfRangeException("Maximum z value can not be less then Minimum value.");
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public Vector3Field(Vector3 minValue, Vector3 maxValue, Vector3 value)
            : this(minValue, maxValue)
        {
            this.Value = value;
        }

        public Vector3Field(Vector3 value)
            : this()
        {
            this.Value = value;
        }

        public Vector3Field(string text)
            : this()
        {
            this.Text = text;
        }

        public Vector3Field(string text, Vector3 minValue, Vector3 maxValue)
            : this(minValue,maxValue)
        {
            this.Text = text;
        }

        public Vector3Field(string text, Vector3 minValue, Vector3 maxValue, Vector3 value)
            : this(minValue, maxValue)
        {
            this.Value = value;
            this.Text = text;
        }

        public Vector3Field(string text, Vector3 value)
            : this()
        {
            this.Text = text;
            this.Value = value;
        }

        public Vector3 MinValue
        {
            get { return this.minValue; }
            set
            {
                this.minValue = value;
                if (this.minValue.x > this.maxValue.x) this.minValue.x = this.maxValue.x;
                if (this.minValue.y > this.maxValue.y) this.minValue.y = this.maxValue.y;
                if (this.minValue.z > this.maxValue.z) this.minValue.z = this.maxValue.z;
            }
        }

        public Vector3 MaxValue
        {
            get { return this.maxValue; }
            set
            {
                this.maxValue = value;
                if (this.maxValue.x < this.minValue.x) this.maxValue.x = this.minValue.x;
                if (this.maxValue.y < this.minValue.y) this.maxValue.y = this.minValue.y;
                if (this.maxValue.z < this.minValue.z) this.maxValue.z = this.minValue.z;
            }
        }

        public Vector3 Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                if (this.value.x < this.minValue.x) this.value.x = this.minValue.x;
                if (this.value.x > this.maxValue.x) this.value.x = this.maxValue.x;
                if (this.value.y < this.minValue.y) this.value.y = this.minValue.y;
                if (this.value.y > this.maxValue.y) this.value.y = this.maxValue.y;
                if (this.value.z < this.minValue.z) this.value.z = this.minValue.z;
                if (this.value.z > this.maxValue.z) this.value.z = this.maxValue.z;
            }
        }
    }
}