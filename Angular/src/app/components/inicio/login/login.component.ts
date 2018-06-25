import { Component, OnInit, Input } from '@angular/core';
import { InicioService } from '../../../services/inicio.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  // atributos
  username: string = '';
  password: string = '';

  public procesando: boolean = false;

  constructor(private servicio: InicioService, private router: Router) {
  }

  ngOnInit() {
  }

  valEnter(event){
    if (event.keyCode === 13) {
      document.getElementById("btn-login").click();
    }
  }

  validarCampos(){
    var validation;
    if (this.username == '' || !/^([a-z0-9]{5,})$/.test(this.username.toLowerCase())) {
      validation = { isError: true, msg: 'Username incorrecto, minimo 5 caracteres alfanumerico.' };
    } else if (this.password == '' || !/^([a-z0-9]{8,})$/.test(this.password.toLowerCase())) {
      validation = { isError: true, msg: 'Password incorrecto, minimo 8 caracteres alfanumerico.' };
    } else {
      validation = { isError: false, msg: null };
    }
    this.servicio.EmitirError(validation);
    return validation.isError;
  }

  login(){
    if (!this.validarCampos()){
      this.servicio.IniciarSesion({username: this.username, password: this.password}, (response) => {
        this.procesando = true;
        if (response.status >= 400){
          this.servicio.EmitirError({ isError: true, msg: 'Credenciales incorrectas' });
        }
        if (response.status >= 200 && response.status < 300) {
          sessionStorage.setItem('tacs-token', response.body['access_token']);
          this.servicio.InfoDelCliente(data => {
              this.router.navigate(['/auth']);
              sessionStorage.setItem('admin', data.EsAdmin);
            // if (data.EsAdmin === "SI"){
            //   this.router.navigate(['/admin/users']);
            // } else {
            //   this.router.navigate(['/auth/wallet']);
            // }
          }, err => {
            this.servicio.EmitirError({ isError: true, msg: 'Credenciales Incorrectos' });
            this.procesando = false;
          });
        }
      });
    }
  }
}
