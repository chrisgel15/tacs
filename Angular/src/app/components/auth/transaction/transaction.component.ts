import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { TransactionService } from './../../../services/transaction.service';


declare var $: any;
//const coinmarket = 'https://api.coinmarketcap.com/v2';
const coinmarket = 'https://api.coinmarketcap.com/v1';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  public coins: Array<any>;

  public precio: number;
  public cantidad: number;
  public moneda: string;

  constructor(private http: HttpClient, private transac: TransactionService) { 
    document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
    this.http.get<any>(coinmarket + '/ticker').subscribe(resp => {
      // this.coins = Object.values(resp.data).map((coin: any) => {
        // return {
        //   id: coin.id,
        //   code: coin.website_slug,
        //   name: coin.name,
        //   symbol: coin.symbol,
        //   price: coin.quotes["USD"].price,
        //   rank: coin.rank,
        //   last_update: coin.last_updated
        // };
      this.coins = resp.map((coin: any) => {
        return {
          id: coin.id,
          name: coin.name,
          symbol: coin.symbol,
          rank: coin.rank,
          price: coin.price_usd,
          last_update: coin.last_updated
        }
      });
      setTimeout(() => { $('.selectpicker').selectpicker('refresh') }, 200);
    })

  }

  ngOnInit() {
  }

  ngOnDestroy(){
    document.body.style.background = "white";
  }

  getPrecio(moneda: string){
    const coin = this.coins.find(c => c.name === moneda);
    this.precio = coin.price;
    this.cantidad = 1;
    this.moneda = moneda;
  }

  CalcularCantidad(monto: number){
    const price = this.coins.find(c => c.name === this.moneda).price;
    this.cantidad = monto/parseFloat(price.toString());
  }

  CalcularPrecio(cantidad: number){
    const price = this.coins.find(c => c.name === this.moneda).price
    this.precio = cantidad * price;
  }

  Comprar(){
    $('btn-compra').button('loading');
    const payload = {
      data: { type: 'compra', amount: this.cantidad },
      moneda: this.coins.find(c => c.name === this.moneda).id
    };

    this.transac.Comprar(payload, res => {
      console.log('Ok: ' + res.status);
    }, err => {
      console.log('Error: ' + err.status);
    });

    $('btn-compra').button('reset');
  }

}
