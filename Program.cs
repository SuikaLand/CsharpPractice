using CsharpPractice;

var solution = new Solution();

var solutions = new Dictionary<int, (string Name, Action Run)>
{
    {
        1, ("AddTwoNumbers", () =>
        {
            var input1 = new ListNode(2, new ListNode(4, new ListNode(3)));
            var input2 = new ListNode(5, new ListNode(6, new ListNode(4)));
            var result = solution.AddTwoNumbers(input1, input2);
            var resultString = "root -> ";
            while (result != null)
            {
                resultString += result.val + " -> ";
                result = result.next;
            }
            Console.WriteLine(resultString);
        })
    },
    {
        2, ("LengthOfLongestSubstring", () =>
        {
            var input = "dvdf";
            var result = solution.LengthOfLongestSubstring(input);
            Console.WriteLine($"Input: \"{input}\", Result: {result}");
        })
    },
    {
        3, ("RomanToInt", () =>
        {
            var input = "MDCXCV";
            var result = solution.RomanToInt(input);
            Console.WriteLine($"Input: \"{input}\", Result: {result}");
        })
    },
    {
        4, ("FindMedianSortedArrays", () =>
        {
            var input1 = new int[] { 1, 4 };
            var input2 = new int[] { 2, 3 };
            var result = solution.FindMedianSortedArrays(input1, input2);
            Console.WriteLine($"Input: [{string.Join(", ", input1)}] and [{string.Join(", ", input2)}], Result: {result}");
        })
    },
    {
        5, ("Convert", () =>
        {
            var input = "PAYPALISHIRING";
            var numRows = 3;
            var result = solution.Convert(input, numRows);
            Console.WriteLine($"Input: \"{input}\", numRows: {numRows}, Result: \"{result}\"");
        })
    },
    {
        6, ("IsMatch (Regular Expression Matching)", () =>
        {
            var testCases = new List<(string s, string p, bool expected)>
            {
                ("abc", "a***abc", true),
                ("aa", "a*", true),
                ("ab", ".*", true),
                ("mississippi", "mis*is*p*.", false),
            };
            foreach (var (s, p, expected) in testCases)
            {
                var result = solution.IsMatch(s, p);
                Console.WriteLine($"s=\"{s}\", p=\"{p}\", Result: {result}, Match Expected: {expected == result}");
            }
        })
    },
    {
        7, ("SecurityDemo (GitHub Security 測試)", () =>
        {
            var demo = new SecurityDemo();

            Console.WriteLine("\n[--- Secret Scanning 範例 ---]");
            demo.HardcodedGitHubToken();
            demo.HardcodedAwsKey();
            demo.HardcodedDbConnectionString();

            Console.WriteLine("\n[--- Code Scanning (CodeQL) 範例 ---]");
            demo.SqlInjectionDemo("1 OR 1=1; DROP TABLE Users;--");
            demo.PathTraversalDemo(@"..\..\..\..\Windows\System32\drivers\etc\hosts");
            demo.CommandInjectionDemo("file.txt & whoami & net user hacker P@ss /add");
        })
    },
};

while (true)
{
    Console.WriteLine();
    Console.WriteLine("===== CsharpPractice Solution Menu =====");
    foreach (var kv in solutions)
    {
        Console.WriteLine($"  {kv.Key}. {kv.Value.Name}");
    }
    Console.WriteLine("  0. Exit");
    Console.WriteLine("=========================================");
    Console.Write("請輸入數字選擇要執行的 Solution: ");

    var input = Console.ReadLine();
    if (!int.TryParse(input, out var choice))
    {
        Console.WriteLine("無效的輸入，請輸入數字。");
        continue;
    }

    if (choice == 0)
    {
        Console.WriteLine("Bye!");
        break;
    }

    if (!solutions.TryGetValue(choice, out var selected))
    {
        Console.WriteLine("找不到對應的 Solution，請重新輸入。");
        continue;
    }

    Console.WriteLine($"\n--- 執行 {selected.Name} ---");
    selected.Run();
    Console.WriteLine("--- 執行完畢 ---");
}

