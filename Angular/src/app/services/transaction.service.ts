import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const coinmarket = 'https://api.coinmarketcap.com/v2';

@Injectable()
export class TransactionService {

  public coins: Array<{
    id: number,
    code: string,
    name: string,
    symbol: string,
    price: number,
    rank: number,
    last_update: number
  }>;

  constructor(private http: HttpClient) {
    
  }

  getCoins(){
  }

}
