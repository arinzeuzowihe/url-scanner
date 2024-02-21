import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  url: string = "";
  scanResults: UrlScanResult[] = [];
  httpClient: HttpClient;
  apiUrl: string;
  isLoading: boolean = false;
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.apiUrl = baseUrl + 'urlscanning/scan';
  }

  scanUrl() {
    this.isLoading = true;

    var request = {
      url: this.url,
      includeVerdictScores: true
    }
    
    this.httpClient.post<UrlScanResult[]>(this.apiUrl, request).subscribe(result => {
      this.scanResults = result;
      this.isLoading = false;
    }, error => console.error(error)); 
  }
}


interface UrlScanResult {
  source: string;
  score: number;
  IsMalicious: boolean;
}
