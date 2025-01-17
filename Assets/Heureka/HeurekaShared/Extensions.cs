﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace HeurekaGames
{
    public static class Extensions
    {
        #region Vector3

        public static Vector2 YZ(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }

        public static Vector2[] YZ(this Vector3[] v)
        {
            Vector2[] new2DArray = new Vector2[v.Length];
            for (int i = 0; i < v.Length; i++)
            {
                new2DArray[i] = new Vector2(v[i].x, v[i].z);
            }
            return new2DArray;
        }

        #endregion

        #region Float

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        #endregion

        #region String

        public static string ToCamelCase(this string camelCaseString)
        {
            return System.Text.RegularExpressions.Regex.Replace(camelCaseString, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ").Trim();
        }

        #endregion

        #region GameObject

        public static void SetComponentRecursively<T>(this GameObject gameObject, bool tf) where T : Component
        {
            T[] comps = gameObject.GetComponentsInChildren<T>();
            foreach (T comp in comps)
            {

                try
                {
                    System.Reflection.PropertyInfo pi = (typeof(T)).GetProperty("enabled");
                    if (null != pi && pi.CanWrite)
                    {
                        pi.SetValue(comp, tf, null);
                    }
                    else
                    {
                        Console.WriteLine("Property does not exist, or cannot write");
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("The property does not exist in MyClass." + e.Message);
                }
            }
        }

        #endregion

        #region List
        public static void CastList<T>(this List<T> targetList)
        {
            targetList = targetList.Cast<T>().ToList();
        }

        #endregion

        #region Enums and flags

        public static bool Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }


        public static bool Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }


        public static T Add<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }


        public static T Remove<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }
        #endregion

        #region Color

        public static Color ModifiedAlpha(this Color color, float alpha)
        {
            Color modifiedColor = color;
            modifiedColor.a = alpha;

            return modifiedColor;
        }

        #endregion
    }
}