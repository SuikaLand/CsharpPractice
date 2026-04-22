using CsharpPractice;

var solution = new Solution();

#region AddTwoNumbers
// var input1 = new ListNode()
// {
//     val = 2,
//     next = new ListNode()
//     {
//         val = 4,
//         next = new ListNode(3)
//     }
// };

// var input2 = new ListNode()
// {
//     val = 5,
//     next = new ListNode()
//     {
//         val = 6,
//         next = new ListNode(4)
//     }
// };


// var addTwoNumbersResult = solution.AddTwoNumbers(input1, input2);

// var addTwoNumbersResultString = "root -> ";

// // 只要目前節點還不是空值，就讓節點資料記錄到result字串，並將當前節點current換成下一個節點
// while (addTwoNumbersResult != null)
// {
//     addTwoNumbersResultString += addTwoNumbersResult.val + " -> ";
//     addTwoNumbersResult = addTwoNumbersResult.next;
// }

// Console.WriteLine(addTwoNumbersResultString);
#endregion

#region LengthOfLongestSubstring
// var lengthOfLongestSubstringInput = "dvdf";
// var lengthOfLongestSubstringResult = solution.LengthOfLongestSubstring(lengthOfLongestSubstringInput);
// Console.WriteLine(lengthOfLongestSubstringResult);

#endregion

#region RomanToInt

// var romanToIntInput = "MDCXCV";
// var romanToIntResult = solution.RomanToInt(romanToIntInput);
// Console.WriteLine(romanToIntResult);

#endregion

#region FindMedianSortedArrays

// var findMedianSortedArraysInput1 = new int[]{ 1, 4};
// var findMedianSortedArraysInput2 = new int[]{ 2, 3};
// var findMedianSortedArraysResult = solution.FindMedianSortedArrays(findMedianSortedArraysInput1, findMedianSortedArraysInput2);
// Console.WriteLine(findMedianSortedArraysResult);

#endregion

#region Convert

// var convertInput1 = "PAYPALISHIRING";
// var convertInput1 = "AB";
// var convertInput2 = 1;
// var convertResult = solution.Convert(convertInput1, convertInput2);
// Console.WriteLine(convertResult);

#endregion

#region Regular Expression Matching (IsMatch)

var isMatchInput1List = new List<string>();
var isMatchInput2List = new List<string>();
var isMatchOutputResult = new List<bool>();
// isMatchInput1List.Add("mississippi");
// isMatchInput2List.Add("mis*is*ip*.");
// isMatchOutputResult.Add(true);

// isMatchInput1List.Add("mississippi");
// isMatchInput2List.Add("mis*is*p*.");
// isMatchOutputResult.Add(false);

// isMatchInput1List.Add("aaa");
// isMatchInput2List.Add("ac*ab*a");
// isMatchOutputResult.Add(true);

// isMatchInput1List.Add("aab");
// isMatchInput2List.Add("c*a*b");
// isMatchOutputResult.Add(true);

// isMatchInput1List.Add("aa");
// isMatchInput2List.Add("a*");
// isMatchOutputResult.Add(true);

// isMatchInput1List.Add("ab");
// isMatchInput2List.Add(".*");
// isMatchOutputResult.Add(true);

// isMatchInput1List.Add("ab");
// isMatchInput2List.Add(".*c");
// isMatchOutputResult.Add(false);

// isMatchInput1List.Add("aaa");
// isMatchInput2List.Add("a*a");
// isMatchOutputResult.Add(true);

// isMatchInput1List.Add("aaa");
// isMatchInput2List.Add("ab*a");
// isMatchOutputResult.Add(false);

// isMatchInput1List.Add("aaa");
// isMatchInput2List.Add("ab*a*c*a");
// isMatchOutputResult.Add(true);

// isMatchInput1List.Add("a");
// isMatchInput2List.Add("ab*");
// isMatchOutputResult.Add(true);

// isMatchInput1List.Add("a");
// isMatchInput2List.Add("ab*a");
// isMatchOutputResult.Add(false);

isMatchInput1List.Add("abc");
isMatchInput2List.Add("a***abc");
isMatchOutputResult.Add(true);

for (int i = 0; i < isMatchInput1List.Count; i++)
{
    var isMatchResult = solution.IsMatch(isMatchInput1List[i], isMatchInput2List[i]);
    Console.WriteLine($"Result: {isMatchResult}, Expected: {isMatchOutputResult[i] == isMatchResult}");
}

#endregion

