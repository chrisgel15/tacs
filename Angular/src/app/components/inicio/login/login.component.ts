import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  err_name: boolean = false;
  err_pass: boolean = false;
  msg_name: string = '';
  msg_pass: string = '';

  name: string = '';
  pass: string = '';

  constructor() { }

  ngOnInit() {
  }

  validarCampos(){
    if (this.name == '') {
      this.err_name = true;
      this.msg_name = 'El nombre no debe ser vacio.';
    } else {
      this.err_name = false;
      this.msg_name = '';
    }
    if (this.pass == '') {
      this.err_pass = true;
      this.msg_pass = 'El password no debe ser vacio.';
    } else {
      this.err_pass = false;
      this.msg_pass = '';
    }

    return this.err_name && this.err_pass;
  }

  login(){
    this.validarCampos();
  }


}
