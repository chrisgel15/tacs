import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TransactionService } from './../../../services/transaction.service';

declare var $: any;
const coinmarket = 'https://api.coinmarketcap.com/v1';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  public coins: Array<any>;
  public myCoins: Array<any>;

  public compra = {
    procesando: false,
    habilitado: false,
    cantidad: null,
    precio: null,
    moneda: null
  };

  public venta = {
    procesando: false,
    habilitado: false,
    cantidad: null,
    cantidadMax: null,
    precio: null,
    moneda: null
  };

  public modal = { tipo: '', titulo: '', mensaje: '' };

  constructor(private http: HttpClient, private transac: TransactionService) { 
    document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
  }

  ngOnInit() {
    this.TraerDatosMonedas();
    setTimeout(() => { this.ActualizarBilletera() }, 500);    
  }

  ngOnDestroy(){
    document.body.style.background = "white";
  }

  getDatosCompra(moneda: string){
    this.compra.habilitado = true;
    const coin = this.coins.find(c => c.name === moneda);
    this.compra.precio = coin.price;
    this.compra.cantidad = 1;
    this.compra.moneda = moneda;
  }

  getDatosVenta(moneda: string){
    this.venta.habilitado = true;
    this.venta.cantidadMax = this.myCoins.find(c => c.name === moneda).amount;
    this.venta.precio = this.round10(this.coins.find(c => c.name === moneda).price * this.venta.cantidadMax,-3);
    this.venta.moneda = moneda;
    this.venta.cantidad = this.round10(this.venta.cantidadMax,-5);
  }

  CompraOnKeyUpPrecio(){
    const price = this.coins.find(c => c.name === this.compra.moneda).price;
    this.compra.precio = this.round10(this.compra.precio,-3);
    this.compra.cantidad = this.round10(this.compra.precio/parseFloat(price.toString()), -5);
  }

  CompraOnKeyUpCantidad(){
    const price = this.coins.find(c => c.name === this.compra.moneda).price;
    this.compra.cantidad = this.round10(this.compra.cantidad,-5);
    this.compra.precio = this.round10(this.compra.cantidad * price,-3);
  }

  VentaOnKeyUpPrecio(){
    const price = this.coins.find(c => c.name === this.venta.moneda).price;
    if (this.venta.cantidadMax * price < this.venta.precio){
      this.venta.precio = this.round10(this.venta.cantidadMax * price, -3);
    }
    this.venta.cantidad = this.round10(this.venta.precio/parseFloat(price.toString()), -5);
  }

  VentaOnKeyUpCantidad(){
    const price = this.coins.find(c => c.name === this.venta.moneda).price;
    if (this.venta.cantidadMax < this.venta.cantidad){
      this.venta.cantidad = this.round10(this.venta.cantidadMax,-5);
      this.venta.precio = this.round10(this.venta.cantidadMax * price,-3);
    } else {
      this.venta.precio = this.round10(this.venta.cantidad * price,-3);
      this.venta.cantidad = this.round10(this.venta.cantidad, -5);
    }
  }

  Comprar(){
    if (!this.compra.cantidad && !this.compra.precio && !this.compra.moneda){
      this.mostrarModal('success','Compra', 'Complete los campos faltantes de compra.');
      return;
    }
    // activo el boton de cargando
    this.compra.procesando = true;
    const payload = {
      data: { type: 'compra', amount: this.compra.cantidad },
      moneda: this.coins.find(c => c.name === this.compra.moneda).id
    };
    this.transac.CrearTransaccion(payload, res => {
      this.compra = {
        cantidad: null,
        precio: null,
        procesando: false,
        habilitado: false,
        moneda: null
      };
      this.mostrarModal('success', 'Compra', 'La compra se realizo exitosamente.');
      $('#selec-coin-purchase').val('default');
      $('#selec-coin-purchase').selectpicker('refresh');
    }, err => {
      this.compra.procesando = false;
      this.compra.habilitado = false;
      this.mostrarModal('error', 'Compra', 'Ocurrio un error al momento de realizar la compra.');
    });
  }

  Vender(){
    // activo el boton de cargando
    this.venta.procesando = true;
    const payload = {
      data: { type: 'venta', amount: this.venta.cantidad },
      moneda: this.coins.find(c => c.name === this.venta.moneda).id
    };
    this.transac.CrearTransaccion(payload, res => {
      this.venta = {
        cantidad: null,
        cantidadMax: null,
        precio: null,
        procesando: false,
        habilitado: false,
        moneda: null
      };
      this.mostrarModal('success', 'Venta', 'La venta se realizo exitosamente.');
      $('#selec-coin-sale').val('default');
      $('#selec-coin-sale').selectpicker('refresh');
    }, err => {
      this.venta.procesando = false;
      this.venta.habilitado = false;
      this.mostrarModal('error', 'Venta', 'Ocurrio un error al momento de realizar la venta.');
    });
  }

  ActualizarBilletera(){
    this.transac.GetMyCoins(
      res => {
        this.myCoins = res.body.map(w => {
          let name = this.coins.find(c => c.id === w.NombreMoneda).name;
          return {
            code: w.NombreMoneda,
            name: name,
            amount: w.Balance
          }
        });
        setTimeout(() => { $('#selec-coin-sale').selectpicker('refresh') }, 200);
      },
      err => {
        this.mostrarModal('error', 'Carga de Datos', 'Ocurrio un error al traer las monedas del cliente');
      }
    );
  }

  TraerDatosMonedas(){
    this.http
      .get<any>(coinmarket + '/ticker')
      .subscribe(resp => {
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
        setTimeout(() => { $('#selec-coin-purchase').selectpicker('refresh') }, 200);
      });
  }

  decimalAdjust(type, value, exp) {
    // Si el exp no está definido o es cero...
    if (typeof exp === 'undefined' || +exp === 0) {
      return Math[type](value);
    }
    value = +value;
    exp = +exp;
    // Si el valor no es un número o el exp no es un entero...
    if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0)) {
      return NaN;
    }
    // Shift
    value = value.toString().split('e');
    value = Math[type](+(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp)));
    // Shift back
    value = value.toString().split('e');
    return +(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp));
  }

  round10(value, exp) {
    if (!value){
      return value;
    } else {
      return this.decimalAdjust('round', value, exp);
    }    
  }

  mostrarModal(type, title, message){
    this.modal = {
      tipo: type,
      titulo: title,
      mensaje: message
    }
    $('#modal-popup').modal('show');
    setTimeout(() => { $('#modal-popup').modal('hide'); }, 4000);
  }
}
