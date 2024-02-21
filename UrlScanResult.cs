namespace url_scanner;

public class UrlScanResult {
    public UrlScanResult (string source, int? score, bool isMalicious) {
        Source = source;
        Score = score;
        IsMalicious = isMalicious;
    }

    public string Source { get; init; }
    public int? Score { get; init; }
    public bool IsMalicious { get; init; }
}