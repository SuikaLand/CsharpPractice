using System.Data.SqlClient;
using System.Diagnostics;

namespace CsharpPractice;

// =====================================================================
// [刻意撰寫的漏洞 - 僅供 GitHub Security 功能測試用途，請勿用於生產環境]
// =====================================================================
public class SecurityDemo
{
    // ---------------------------------------------------------------
    // 漏洞 1：硬編碼的 GitHub Personal Access Token
    // 類型：Secret Scanning（機密掃描）
    // 危害：攻擊者取得原始碼後可直接使用此 Token 存取 GitHub API，
    //       進行未授權的倉庫讀寫、組織成員管理等操作。
    // 正確做法：應使用環境變數或 GitHub Actions Secrets 儲存 Token，
    //           不應將任何憑證直接寫死在程式碼中。
    // ---------------------------------------------------------------
    // [刻意撰寫的漏洞 - 僅供安全測試用途]
    // GitHub Classic PAT 格式：ghp_ 後接剛好 36 個英數字（共 40 字元）
    // 以下符合 ghp_[A-Za-z0-9]{36} 的正規表達式，可觸發 Secret Scanning
    private const string GitHubToken = "ghp_abcdefghijklmnopqrstuvwxyz0123456789";

    public string HardcodedGitHubToken()
    {
        Console.WriteLine($"[Secret Scanning 範例] 硬編碼的 GitHub Token: {GitHubToken}");
        return GitHubToken;
    }

    // ---------------------------------------------------------------
    // 漏洞 2：硬編碼的 AWS Access Key ID 與 Secret Access Key
    // 類型：Secret Scanning（機密掃描）
    // 危害：攻擊者可利用這組金鑰存取 AWS 雲端資源，
    //       造成資料外洩、產生高額費用，或破壞雲端基礎設施。
    // 正確做法：應使用 IAM Role、AWS Secrets Manager 或環境變數，
    //           絕不能將 AWS 金鑰硬編碼在原始碼中。
    // ---------------------------------------------------------------
    // [刻意撰寫的漏洞 - 僅供安全測試用途]
    private const string AwsAccessKeyId = "AKIAFAKEACCESSKEYID12";
    private const string AwsSecretAccessKey = "FakeSecretAccessKey/FakeForTestingOnly+ABCD";

    public string HardcodedAwsKey()
    {
        Console.WriteLine($"[Secret Scanning 範例] AWS Access Key ID: {AwsAccessKeyId}");
        Console.WriteLine($"[Secret Scanning 範例] AWS Secret Access Key: {AwsSecretAccessKey}");
        return $"AccessKeyId={AwsAccessKeyId}";
    }

    // ---------------------------------------------------------------
    // 漏洞 3：含有明文密碼的資料庫連線字串
    // 類型：Secret Scanning（機密掃描）
    // 危害：攻擊者取得原始碼後可直接連線到資料庫，
    //       竊取、竄改或刪除所有資料。
    // 正確做法：應使用 Azure Key Vault、環境變數或加密設定檔，
    //           不應將資料庫帳號密碼硬編碼在程式碼中。
    // ---------------------------------------------------------------
    // [刻意撰寫的漏洞 - 僅供安全測試用途]
    private const string DbConnectionString =
        "Server=prod-db.example.com;Database=AppDB;User Id=admin;Password=SuperSecret@Password123;";

    public string HardcodedDbConnectionString()
    {
        Console.WriteLine($"[Secret Scanning 範例] 資料庫連線字串: {DbConnectionString}");
        return DbConnectionString;
    }

    // ---------------------------------------------------------------
    // 漏洞 4：SQL Injection（SQL 注入）
    // 類型：Code Scanning / CodeQL（OWASP Top 10 A03:2021 - Injection）
    // 危害：攻擊者可透過惡意輸入（例如 "1 OR 1=1"）繞過查詢條件，
    //       讀取所有使用者資料、刪除資料表，甚至執行作業系統命令。
    // 正確做法：應使用參數化查詢（Parameterized Query）或 ORM，
    //           絕不應將使用者輸入直接拼接進 SQL 字串。
    // ---------------------------------------------------------------
    // [刻意撰寫的漏洞 - 僅供安全測試用途]
    public string SqlInjectionDemo(string userInput)
    {
        // 危險：直接將使用者輸入拼接成 SQL，未做任何過濾或參數化
        var dangerousSql = "SELECT * FROM Users WHERE Id = " + userInput;
        Console.WriteLine($"[SQL Injection 範例] 產生的危險 SQL: {dangerousSql}");

        // 以下為實際有漏洞的 ADO.NET 寫法（已註解，不真正執行）
        using var conn = new SqlConnection(DbConnectionString);
        var cmd = new SqlCommand(dangerousSql, conn);   // ← 直接使用拼接字串，有 SQL Injection 風險
        conn.Open();
        var reader = cmd.ExecuteReader();

        // 正確的參數化寫法應為：
        // var cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @id", conn);
        // cmd.Parameters.AddWithValue("@id", userInput);

        return dangerousSql;
    }

    // ---------------------------------------------------------------
    // 漏洞 5：Path Traversal（路徑遍歷）
    // 類型：Code Scanning / CodeQL（OWASP Top 10 A01:2021 - Broken Access Control）
    // 危害：攻擊者可透過 "../../etc/passwd" 等惡意路徑，
    //       讀取伺服器上任意檔案，包含系統設定、密碼檔等機敏資料。
    // 正確做法：應對使用者輸入進行路徑正規化並驗證是否在允許的目錄範圍內，
    //           例如使用 Path.GetFullPath() 後比對是否以允許的根目錄開頭。
    // ---------------------------------------------------------------
    // [刻意撰寫的漏洞 - 僅供安全測試用途]
    public string PathTraversalDemo(string userInput)
    {
        var baseDir = @"C:\App\Files\";

        // 危險：直接將使用者輸入組合為檔案路徑，未做任何驗證
        var dangerousPath = Path.Join(baseDir, userInput);
        Console.WriteLine($"[Path Traversal 範例] 產生的危險路徑: {dangerousPath}");
        Console.WriteLine($"[Path Traversal 範例] 攻擊範例輸入: ..\\..\\..\\Windows\\System32\\drivers\\etc\\hosts");

        // 以下為實際有漏洞的寫法（已註解，不真正執行，避免本機副作用）
        var fileContent = File.ReadAllText(dangerousPath);  // ← 直接讀取，有 Path Traversal 風險

        // 正確的驗證寫法應為：
        // var fullPath = Path.GetFullPath(dangerousPath);
        // if (!fullPath.StartsWith(baseDir, StringComparison.OrdinalIgnoreCase))
        //     throw new UnauthorizedAccessException("禁止存取根目錄以外的檔案");

        return dangerousPath;
    }

    // ---------------------------------------------------------------
    // 漏洞 6：Command Injection（命令注入）
    // 類型：Code Scanning / CodeQL（OWASP Top 10 A03:2021 - Injection）
    // 危害：攻擊者可透過惡意輸入（例如 "file.txt & del /f /s /q C:\\"）
    //       在伺服器上執行任意作業系統命令，取得系統控制權。
    // 正確做法：應避免直接將使用者輸入傳入 shell 命令；
    //           若必須執行外部程式，應使用白名單驗證輸入，
    //           並將輸入作為獨立引數傳遞，不透過 shell 解析。
    // ---------------------------------------------------------------
    // [刻意撰寫的漏洞 - 僅供安全測試用途]
    public string CommandInjectionDemo(string userInput)
    {
        // 危險：直接將使用者輸入拼接為命令列引數，未做任何過濾
        var dangerousArgs = "/c type " + userInput;
        Console.WriteLine($"[Command Injection 範例] 產生的危險命令引數: cmd.exe {dangerousArgs}");
        Console.WriteLine($"[Command Injection 範例] 攻擊範例輸入: file.txt & whoami & net user hacker P@ss /add");

        // 以下為實際有漏洞的寫法（已註解，不真正執行，避免本機副作用）
        var psi = new ProcessStartInfo("cmd.exe", dangerousArgs)  // ← 有 Command Injection 風險
        {
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        var proc = Process.Start(psi);
        var output = proc?.StandardOutput.ReadToEnd();

        // 正確做法：避免使用 shell；若需執行外部程式，直接傳入獨立引數
        // var psi = new ProcessStartInfo("type", userInput)  // 直接傳檔名，不透過 cmd /c 解析
        // {
        //     UseShellExecute = false
        // };

        return dangerousArgs;
    }
}
