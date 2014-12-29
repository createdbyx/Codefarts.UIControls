﻿namespace Codefarts.UIControls
{
    using System;

    public static class PropertyCollectionExtensions
    {
        public static T Get<T>(this PropertyCollection propertiesCollection, string name)
        {
            if (propertiesCollection == null)
            {
                throw new ArgumentNullException("propertiesCollection");
            }

            return (T)propertiesCollection.Get(name);
        }

        public static T Get<T>(this PropertyCollection propertiesCollection, string name, T defaultValue)
        {
            T value;
            if (TryGet(propertiesCollection, name, out value, defaultValue))
            {
                return value;
            }

            return defaultValue;
        }

        public static bool TryGet<T>(this PropertyCollection propertiesCollection, string name, out T value)
        {
            return TryGet(propertiesCollection, name, out value, default(T));
        }

        public static bool TryGet<T>(this PropertyCollection propertiesCollection, string name, out T value, T defaultValue)
        {
            if (propertiesCollection == null)
            {
                throw new ArgumentNullException("propertiesCollection");
            }

            if (!propertiesCollection.ContainsName(name))
            {
                value = defaultValue;
                return false;
            }

            value = (T)propertiesCollection.Get(name);
            return true;
        }
    }
}