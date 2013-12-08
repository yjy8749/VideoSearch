using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoSearch
{
    public class StringSimilarity
    {
        private static int editDistance(string str1, string str2)
        {
            int[,] matrix = new int[str1.Length + 1, str2.Length + 1];
            for (int i = 0; i <= str1.Length; ++i)
                matrix[i, 0] = i;

            for (int i = 0; i <= str2.Length; ++i)
                matrix[0, i] = i;
            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    int addition = str1[i - 1] == str2[j - 1] ? 0 : 1;
                    int min = Math.Min(matrix[i - 1, j - 1] + addition, matrix[i, j - 1] + 1);
                    matrix[i, j] = Math.Min(min, matrix[i - 1, j] + 1);
                }
            }
            return matrix[str1.Length, str2.Length];
        }
        public static float compare(string str1, string str2)
        {
            return 1 - (float)editDistance(str1, str2)/ Math.Max(str1.Length, str2.Length);
        } 

    }
}
