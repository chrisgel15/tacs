import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InicioService } from '../../../services/inicio.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})

export class AuthComponent  implements OnInit  {
  isAdmin: boolean;

  constructor(private router: Router, private service: InicioService) {
  }

  ngOnInit() {
    this.isAdmin = (sessionStorage.getItem('admin') == 'SI');
    console.log(this.isAdmin);
  }

  signOut() {
    this.service.signOut(
      () => {
        sessionStorage.removeItem('tacs-token');
        this.router.navigate(['']);
      },
      () => {
        alert('Ocurrio un error al desconectarte del sitio.');
      }
    );
  }
}
