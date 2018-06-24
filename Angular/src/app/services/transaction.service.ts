import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { apiTacs } from '../config';

const api = 'https://tacscripto.azurewebsites.net/api';
//const api = 'http://localhost:51882/api'

@Injectable()
export class TransactionService {

  constructor(private http: HttpClient) {}

  CrearTransaccion(payload, callbackOk, callbackError){
    this.http
      .post(`${apiTacs}/user/wallets/${payload.moneda}/transactions`, payload.data, { observe: 'response' })
      .subscribe(callbackOk, callbackError);
  }

  GetMyCoins(callbackOk, callbackError){
    this.http
      .get(`${apiTacs}/user/wallets`, { observe: 'response' })
      .subscribe(callbackOk, callbackError);
  }

}