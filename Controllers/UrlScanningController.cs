using Microsoft.AspNetCore.Mvc;
using Urlscan;

namespace url_scanner.Controllers;

[ApiController]
[Route ("[controller]")]
public class UrlScanningController : ControllerBase {
    private readonly UrlscanClient client;

    public UrlScanningController () {
        client = new ("[API_KEY]"); // replace with API key from https://urlscan.io/docs/api/
    }

    [HttpGet ("currentUser")]
    public async Task<User> GetCurrentUser () {
        return await client.GetCurrentUser ();
    }

    [HttpPost ("scan")]
    public async Task<IEnumerable<UrlScanResult>> Scan (UrlScanRequest urlScanRequest) {
        var submission = await client.Scan (new ScanParameters () {
            Url = urlScanRequest.Url,
                Country = ScanCountry.FI,
                UserAgent = "My-Custom-Scanner/1.0.0",
                OverrideSafety = false,
                Referer = "https://google.com",
                Visibility = Visibility.Public
        });
        Result scanResults = await client.Poll (submission);
        var finalizedResults = new List<UrlScanResult> ();
        var verdicts = scanResults.Verdicts;
        finalizedResults.Add (new UrlScanResult ("UrlScan", urlScanRequest.IncludeVerdictScores ? verdicts.Urlscan.Score : null, verdicts.Urlscan.Malicious));
        finalizedResults.Add (new UrlScanResult ("Community", urlScanRequest.IncludeVerdictScores ? verdicts.Community.Score : null, verdicts.Community.Malicious));
        finalizedResults.Add (new UrlScanResult ("Overall", urlScanRequest.IncludeVerdictScores ? verdicts.Overall.Score : null, verdicts.Overall.Malicious));
        return finalizedResults;
    }
}