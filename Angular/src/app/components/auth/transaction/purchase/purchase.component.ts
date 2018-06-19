import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../../../../services/transaction.service';
import { HttpClient } from '@angular/common/http';

const coinmarket = 'https://api.coinmarketcap.com/v2';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit {

  public coins: Array<any>;

  constructor(private http: HttpClient) { 
    this.http.get<any>(coinmarket + '/ticker').subscribe(resp => {
      this.coins = Object.values(resp.data).map((coin: any) => {
        return {
          id: coin.id,
          code: coin.website_slug,
          name: coin.name,
          symbol: coin.symbol,
          price: coin.quotes["USD"].price,
          rank: coin.rank,
          last_update: coin.last_updated
        };
      })
    })
  }

  ngOnInit() {
  }

}
