using System;

namespace BitSerializer.Utils
{
    public abstract class ArrayUtils
    {
        /// <summary>
        /// Convert 1D array index to multi-dimensiomal array indices.
        /// </summary>
        public static int[] ArrayIndexToIndices(Array arr, int index)
        {
            int[] indices = new int[arr.Rank];
            for (int d = arr.Rank - 1; d >= 0; d--)
            {
                var len = arr.GetLength(d);
                indices[d] = index % len;
                index /= len;
            }
            return indices;
        }
    }
}
