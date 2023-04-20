using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDistance
{

    class DocDistance
    {

        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>
        /// 

        public static double CalculateDSquare(Dictionary<string, int> dec)
        {
            double res = 0;
            foreach (var it in dec)
            {
                double it_value = it.Value;
                res += it_value * it_value;
            }
            return res;
        }

        public static double CalculateDotProductD(Dictionary<string, int> dec1, Dictionary<string, int> dec2, ref double d1)
        {
            double ans = 0;
            foreach (var it in dec1)
            {
                int val = 0;
                double it_value = it.Value;
                if (dec2.TryGetValue(it.Key, out val))
                {
                    ans += (it_value * Convert.ToDouble(val));
                }
                d1 +=  it_value *it_value;
            }
            return ans;
        }

        public static bool IsAlphaNumber(char ch)
        {
            return ((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'));
        }

        public static void GenerateStringsDictionariesOfFile(string s, Dictionary<string, int> dec)
        {
            string tmp = "";
            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] >= '0' && s[i] <= '9') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= 'A' && s[i] <= 'Z'))
                {
                    tmp += s[i];
                }
                else
                {
                    if (tmp.Length > 0)
                    {
                        if (dec.ContainsKey(tmp)) { dec[tmp]++; }
                        else { dec.Add(tmp, 1); }
                        tmp = "";
                    }
                }
            }
            if (tmp.Length > 0)
            {
                if (dec.ContainsKey(tmp)) { dec[tmp]++; }
                else { dec.Add(tmp, 1); }
                tmp = "";
            }
        }

        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            // TODO comment the following line THEN fill your code here
            // throw new NotImplementedException();

            // Read files to string and make all string char in lower case 
            string file1 = File.ReadAllText(doc1FilePath).ToLower();
            string file2 = File.ReadAllText(doc2FilePath).ToLower();

            // Create 2 dectionaries to calculate the frequancy of strings in each file 
            Dictionary<string, int> mp1 = new Dictionary<string, int>();
            Dictionary<string, int> mp2 = new Dictionary<string, int>();

            // Generate strings and frequancy of them for each file [Can Be Optimized]
            GenerateStringsDictionariesOfFile(file1, mp1);
            GenerateStringsDictionariesOfFile(file2, mp2);

            // Calculate dSquare for each document
            double D1 = 0;
            double D2 = CalculateDSquare(mp2);

            // Calculate D1.D2
            double D1_D2;
            D1_D2 = CalculateDotProductD(mp1, mp2, ref D1);

            // Calculate distance
            double val = D1_D2 / Math.Sqrt(D1 * D2);
            double mycalcInRadians = Math.Acos(val);
            double mycalcInDegrees = mycalcInRadians * (180 / Math.PI);

            // finally return distance
            return mycalcInDegrees;
        }

    }
}