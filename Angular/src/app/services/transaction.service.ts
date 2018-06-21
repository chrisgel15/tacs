import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const api = 'https://tacscripto.azurewebsites.net/api';

@Injectable()
export class TransactionService {

  constructor(private http: HttpClient) {}

  Comprar(payload, callbackOk, callbackError){

    this.http
      .post(`${api}/user/wallets/${payload.moneda}/transactions`, payload.data, { observe: 'response' })
      .subscribe(callbackOk, callbackError);
  }

  Vender(payload, callback){

  }

}
