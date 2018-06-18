import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class TransactionService {

  public coins: {
    id: number,
    code: string,
    name: string,
    symbol: string,
    price: number,
    rank: number,
    last_update: number
  }

  constructor(private http: HttpClient) {
    //this.http.get
  }

  getCoins(){

  }

}
