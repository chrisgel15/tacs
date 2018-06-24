import { Component, OnInit } from '@angular/core';
import { WalletService } from '../../../services/wallet.service';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.css']
})
export class WalletComponent implements OnInit {

  wallets: any; 

  constructor(private walletService: WalletService) { }

  ngOnInit() {
    this.walletService.getWallet()
      .subscribe(x => this.wallets = x);
  }

  balance() {
    return this.wallets.reduce( function(total, wallet) {
        return total + (wallet.Cotizacion * wallet.Balance)
      }, 0).toFixed(2);
  }

}
