import { Component, OnInit, Input } from '@angular/core';
import { InicioService } from '../../../services/inicio.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  username: string = '';
  password: string = '';

  constructor(private servicio: InicioService, private router: Router) {
  }

  ngOnInit() {
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
        if (response.status >= 400){
          this.servicio.EmitirError({ isError: true, msg: 'Credenciales incorrectas' });
        }
        if (response.status >= 200 && response.status < 300) {
          this.servicio.EmitirError({ isError: false, msg: 'Sesion Iniciada' });
          sessionStorage.setItem('tacs-token', response.body['access_token']);
          console.log(response.body); // sacar en produccion!!!
          //this.router.navigate(['/auth/wallet']);
          this.router.navigate(['/admin']);
        }
      });
    }
  }


}
