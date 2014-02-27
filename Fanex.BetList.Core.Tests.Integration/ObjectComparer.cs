// ---------------------------------------------------------------------------------
// <copyright file="ObjectComparer.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.25">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Class ObjectComparer.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    public static class ObjectComparer<T>
    {
        /// <summary>
        /// There're some different between the values in VerticalAlignment enum of NPOI versions
        /// so we use this field as a mapping for old value and new value.
        /// Ex: NPOI 2.0.6.0 does not have enum value 0 for VerticalAlignment, when version 1.2.4.0 does.
        /// </summary>
        private static Dictionary<int, VerticalAlignment> _verticalAlignOldToNew = new Dictionary<int, VerticalAlignment>
        {
            { 0, VerticalAlignment.Bottom }
        };

        /// <summary>
        /// Compares the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns><c>True</c> if equal, <c>false</c> otherwise.</returns>
        public static bool Compare(T source, T destination)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            FieldInfo[] fields = type.GetFields();

            bool propertiesEqual = CompareProperties(properties, source, destination);
            bool fieldsEqual = CompareFields(fields, source, destination);

            if (propertiesEqual && fieldsEqual)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Compares the fields.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns><c>True</c> if equals, <c>false</c> otherwise.</returns>
        private static bool CompareFields(FieldInfo[] fields, T source, T destination)
        {
            int compareValue = 0;
            int fieldCompareFail = 0;

            foreach (FieldInfo field in fields)
            {
                try
                {
                    IComparable sourceVal = field.GetValue(source) as IComparable;
                    if (sourceVal == null)
                    {
                        continue;
                    }

                    object destinationVal = field.GetValue(destination);
                    compareValue = sourceVal.CompareTo(destinationVal);
                    if (compareValue != 0)
                    {
                        return false;
                    }
                }
                catch (TargetInvocationException)
                {
                    fieldCompareFail++;
                    continue;
                }
            }

            if (fieldCompareFail > 1 && fieldCompareFail == fields.Length - 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Compares the properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns><c>True</c> if equals, <c>false</c> otherwise.</returns>
        private static bool CompareProperties(PropertyInfo[] properties, T source, T destination)
        {
            bool sameCellStyle = false;
            int propCompareFail = 0;
            int compareValue = 0;
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    if (property.Name == "CellStyle")
                    {
                        HSSFCellStyle sourceVal = property.GetValue(source, null) as HSSFCellStyle;
                        HSSFCellStyle destinationVal = property.GetValue(destination, null) as HSSFCellStyle;
                        sameCellStyle = CompareCellStyle(sourceVal, destinationVal);

                        if (sameCellStyle == false)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        IComparable sourceVal = property.GetValue(source, null) as IComparable;
                        if (sourceVal == null)
                        {
                            continue;
                        }

                        object destinationVal = property.GetValue(destination, null);
                        compareValue = sourceVal.CompareTo(destinationVal);
                        if (compareValue != 0)
                        {
                            return false;
                        }
                    }
                }
                catch (TargetInvocationException)
                {
                    propCompareFail++;
                    continue;
                }
            }

            if (propCompareFail > 1 && propCompareFail == properties.Length - 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Compares the cell style.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="des">The destination.</param>
        /// <returns><c>True</c> if equal, <c>false</c> otherwise.</returns>
        private static bool CompareCellStyle(HSSFCellStyle src, HSSFCellStyle des)
        {
            return
                src.Alignment == des.Alignment &&
                src.BorderBottom == des.BorderBottom &&
                src.BorderDiagonal == des.BorderDiagonal &&
                src.BorderDiagonalColor == des.BorderDiagonalColor &&
                src.BorderDiagonalLineStyle == des.BorderDiagonalLineStyle &&
                src.BorderLeft == des.BorderLeft &&
                src.BorderRight == des.BorderRight &&
                src.BorderTop == des.BorderTop &&
                src.BottomBorderColor == des.BottomBorderColor &&
                src.DataFormat == des.DataFormat &&

                ////src.FillBackgroundColor == des.FillBackgroundColor &&
                ////src.FillBackgroundColorColor == des.FillBackgroundColorColor &&
                ////src.FillForegroundColor == des.FillForegroundColor &&
                ////src.FillForegroundColorColor == des.FillForegroundColorColor &&

                src.FillPattern == des.FillPattern &&
                src.FontIndex == des.FontIndex &&
                src.Indention == des.Indention &&
                src.Index == des.Index &&
                src.IsHidden == des.IsHidden &&
                src.IsLocked == des.IsLocked &&
                src.LeftBorderColor == des.LeftBorderColor &&
                src.ParentStyle == des.ParentStyle &&
                src.RightBorderColor == des.RightBorderColor &&
                src.Rotation == des.Rotation &&
                src.ShrinkToFit == des.ShrinkToFit &&
                src.TopBorderColor == des.TopBorderColor &&
                src.UserStyleName == des.UserStyleName &&
                src.VerticalAlignment == _verticalAlignOldToNew[(int)des.VerticalAlignment] &&
                src.WrapText == des.WrapText;
        }
    }
}
