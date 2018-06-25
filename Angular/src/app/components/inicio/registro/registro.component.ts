import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { InicioService } from '../../../services/inicio.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})


export class RegistroComponent implements OnInit {

  username: string = '';
  password: string = '';

  procesando: boolean = false;

  constructor(private servicio: InicioService, private router: Router) {}

  ngOnInit() {}

  valEnter(event){
    if (event.keyCode === 13) {
      document.getElementById("signup").click();
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

  registrar(){
    if (!this.validarCampos()){
      this.procesando = true;
      this.servicio.Registrar({username: this.username, password: this.password}, (response) => {
        if (response.status >= 400){
          this.servicio.EmitirError({ isError: true, msg: 'Ocurrio un error, intente de nuevo.' });
        }
        if (response.status >= 200 && response.status < 300) {
          this.servicio.EmitirError({ isError: false, msg: 'Se creo exitosamente el usuario.' });
          this.router.navigate(['/']);
        }
        this.procesando = false;  
      });
    }
  }
}
