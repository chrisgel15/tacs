import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { apiTacs } from '../config';

@Injectable()
export class WalletService {
  constructor(private http: HttpClient) { }

  getWallet() {
    return this.http.get(apiTacs + "/user/wallets");
  }
}