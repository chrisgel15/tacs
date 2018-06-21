import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const api = 'https://tacscripto.azurewebsites.net/api';
//const api = 'http://localhost:51882/api';

@Injectable()
export class WalletService {
  constructor(private http: HttpClient) { }

  getWallet() {
    return this.http.get(api + "/user/wallets");
  }
}