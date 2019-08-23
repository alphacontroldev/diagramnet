using System;
using System.Collections;

namespace Dalssoft.DiagramNet.Helpers
{
    public class ArrayHelper
    {
        public static Array Append(Array arr1, Array arr2)
        {
            var arr1Type = arr1.GetType().GetElementType();
            var arr2Type = arr1.GetType().GetElementType();

            if (arr1Type != arr2Type)
                throw new Exception("Arrays isn't the same type");

            var arrNew = new ArrayList(arr1.Length + arr2.Length - 1);
            arrNew.AddRange(arr1);
            arrNew.AddRange(arr2);
            return arrNew.ToArray(arr1Type);
        }

        public static Array Shrink(Array arr, object removeValue)
        {
            var arrNew = new ArrayList(arr.Length - 1);
            foreach (var item in arr)
                if (item != removeValue)
                    arrNew.Add(item);

            arrNew.TrimToSize();
            return arrNew.ToArray(arr.GetType().GetElementType());
        }
    }
}