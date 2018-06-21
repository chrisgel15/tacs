import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Authorization } from './authorization';

const api = 'https://tacscripto.azurewebsites.net/api';

@Injectable()
export class TransactionService {

  constructor(private http: HttpClient, private auth: Authorization) {}

  Comprar(payload, callbackOk, callbackError){
    const token = this.auth.getAutorization();
    console.log(token);
    console.log(payload.data);
    const headers = new HttpHeaders();
    headers.append('Authorization', token);
    headers.append('Content-Type', 'application/json');
    this.http
      .post(`${api}/user/wallets/${payload.moneda}/transactions`, payload.data, { observe: 'response', headers })
      .subscribe(callbackOk, callbackError);
  }

  Vender(payload, callback){

  }

}
