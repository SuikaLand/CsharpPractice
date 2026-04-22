using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace CsharpPractice;

public class Solution
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        var intList = new List<int>();
        var result = new ListNode();
        var curNode = result;
        var tempVal = 0;
        while (l1 != null || l2 != null || tempVal != 0)
        {
            var val1 = 0;
            var val2 = 0;
            if (l1 != null)
            {
                val1 = l1.val;
                l1 = l1.next;
            }
            if (l2 != null)
            {
                val2 = l2.val;
                l2 = l2.next;
            }
            if (curNode == null)
            {
                Console.WriteLine("123");
            }
            tempVal = val1 + val2 + tempVal;
            intList.Add(tempVal % 10);
            tempVal /= 10;
        }
        intList.Reverse();
        foreach (var item in intList)
        {
            var tempLN = new ListNode(item);
            if (result != null)
            {
                tempLN.next = result;
            }
            result = tempLN;
        }
        return result;
    }

    public int LengthOfLongestSubstring(string s)
    {
        var result = 0;
        var tempString = "";
        foreach (var item in s)
        {
            if (!tempString.Contains(item))
            {
                tempString = tempString + item;
            }
            else
            {
                if (result < tempString.Length)
                {
                    result = tempString.Length;
                }
                var index = tempString.IndexOf(item);
                tempString = tempString.Substring(index + 1) + item;
            }
        }
        if (result < tempString.Length)
        {
            result = tempString.Length;
        }
        return result;
    }

    public int RomanToInt(string s)
    {
        var sum = 0;
        var romanDict = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        for (int i = 0; i < s.Length; i++)
        {
            var s1 = romanDict[s[i]];
            var s2 = 0;
            if (i + 1 < s.Length)
            {
                s2 = romanDict[s[i + 1]];
            }

            if (s1 < s2)
            {
                sum = sum + s2 - s1;
                i = i + 1;
            }
            else
            {
                sum = sum + s1;
            }
        }
        return sum;
    }

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        var arr = new int[nums1.Length + nums2.Length];
        nums1.CopyTo(arr, 0);
        nums2.CopyTo(arr, nums1.Length);

        arr = arr.Order().ToArray();

        var mIndex = (arr.Length - 1) / 2;
        var m2Index = arr.Length % 2 == 0 ? mIndex + 1 : 0;

        return m2Index == 0 ? arr[mIndex] : (double)(arr[mIndex] + arr[m2Index]) / 2;
    }

    public string Convert(string s, int numRows)
    {
        if (numRows == 1)
        {
            return s;
        }
        var stringDict = new Dictionary<int, string>();
        var lineIndex = 0;
        var isReturn = false;
        for (int i = 0; i < numRows; i++)
        {
            stringDict.Add(i, "");
        }
        foreach (var item in s)
        {
            stringDict[lineIndex] += $"{item}";
            
            lineIndex = isReturn ? lineIndex - 1 : lineIndex + 1;
            if (lineIndex == numRows - 1)
            {
                isReturn = true;
            }
            if (lineIndex == 0)
            {
                isReturn = false;
            }
        }
        var result = "";
        foreach (var item in stringDict)
        {
            result += item.Value;
        }
        return result;
    }

    #region IsMatch
    // public bool IsMatch(string s, string p) {
    //     // 長度不一樣又沒有*，回傳false
    //     if (s.Length != p.Length && p.FirstOrDefault(f => f == '*') == '\0')
    //     {
    //         return false;
    //     }
    //     for (int i = 0; i < p.Length; i++)
    //     {
    //         if (i + 1 == p.Length)
    //         {
    //             break;
    //         }
    //         while (p[i] == '*' && p[i + 1] == '*')
    //         {
    //             p = p.Remove(i + 1, 1);
    //         }
    //     }
        
    //     var regex = new Regex($"^{p}$");
    //     var result = regex.IsMatch(s);
    //     return result;
    // }
    Dictionary<(int sIndex, int pIndex), bool> dp;
    public bool IsMatch(string s, string p) {
        dp = new();
        return FindMatch(s, p, 0, 0);
    }
    private bool FindMatch(string s, string p, int sIndex, int pIndex)
    {
        if(sIndex >= s.Length && pIndex >= p.Length)
        {
            return true;
        }
        if(pIndex >= p.Length)
        {
            return false;
        }
        if(dp.ContainsKey((sIndex, pIndex)))
        {
            return dp[(sIndex, pIndex)];
        }
        bool result = false;
        bool isMatch = (sIndex < s.Length && (s[sIndex] == p[pIndex] || p[pIndex] == '.'));
        if(pIndex + 1 < p.Length && p[pIndex + 1] == '*')
        {
            result = (isMatch && FindMatch(s, p, sIndex + 1, pIndex)) || FindMatch(s, p, sIndex, pIndex + 2);
        }
        else if(isMatch)
        {
            result = FindMatch(s, p, sIndex + 1, pIndex + 1);
        }
        dp[(sIndex, pIndex)] = result;
        return result;
    }
    #endregion
    

}

public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}