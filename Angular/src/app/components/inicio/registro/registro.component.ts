import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})

export class RegistroComponent implements OnInit {

  nombre: string = '';
  email: string = '';
  password: string = '';

  err_nombre: boolean = false;
  err_email: boolean = false;
  err_password: boolean = false;

  msg_nombre: string = '';
  msg_email: string = '';
  msg_password: string = '';

  constructor() { }

  ngOnInit() {
  }

  validarCampos(){
    if (this.nombre == '') {
      this.err_nombre = true;
      this.msg_nombre = 'El nombre no es correcto.';
    } else {
      this.err_nombre = false;
      this.msg_nombre = '';
    }
    if (this.email == '') {
      this.err_email = true;
      this.msg_email = 'El email no es correcto.';
    } else {
      this.err_email = false;
      this.msg_email = '';
    }
    if (this.password == '') {
      this.err_password = true;
      this.msg_password = 'El password no es correcto.';
    } else {
      this.err_password = false;
      this.msg_password = '';
    }

    return this.err_nombre && this.err_email && this.err_password;
  }

  registrar(){
    if (this.validarCampos()){

    } else {
      
    }
  }

}
