import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../services/admin.service';
import * as moment from 'moment';

declare var $: any;

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public usuarios: Array<any>;
  public criteria = { nombre: '', fecha: '' };
  public comparacion = [];
  public message = { isOk: false, msg: null };

  constructor(private service: AdminService) {
    $(function () {
      $('[data-toggle="tooltip"]').tooltip()
    })
  }

  ngOnInit() {
    this.service.getUsers(data => {
      this.usuarios = data;
      // this.usuarios = data.map(u => {
      //   let fechaInt = moment(u.LastAccess,'DD/MM/YYYY').format('MM/DD/YYYY');
      //   return Object.assign({}, u, { LastAccess: Date.parse(fechaInt) });
      // })
    });
  }

  filtrarPorParametros(user){
    const nombre = this.criteria.nombre;
    const fecha = this.criteria.fecha;
    console.log(fecha);
    if (nombre === '' && fecha === '') {
      return true;
    } else if (nombre !== '' && fecha !== '') {
      return user.Name.includes(nombre) || Date.parse(user.LastAccess) === Date.parse(fecha);
    } else if (nombre !== '') {
      return user.Name.includes(nombre);
    } else if (fecha !== '') {
      return Date.parse(user.LastAccess) === Date.parse(fecha);
    } else {
      return false;
    }
  }

  seleccionarUsuario(id){
    if (this.comparacion.includes(id)){
      let i = this.comparacion.shift();
      document.getElementById('usuario-'+i).style.backgroundColor = 'white';
    } else {
      let l = this.comparacion.length;
      if(l === 2){
        let i = this.comparacion.shift();
        document.getElementById('usuario-'+i).style.backgroundColor = 'white';
        this.comparacion.push(id);
        document.getElementById('usuario-'+id).style.backgroundColor = '#ade28f';  
      } else {
        this.comparacion.push(id);
        document.getElementById('usuario-'+id).style.backgroundColor = '#ade28f';  
      }
    }
    console.log(this.comparacion);
  }

  compararDosUsuarios(){
    this.message = { isOk: false, msg: null };
    if(this.comparacion.length < 2){
      this.message = { isOk: false, msg: 'Elija 2 usuarios para comparar.' };
      $('#modalResponse').modal('show');
    } else {
      let u1 = this.usuarios.find(u => u.Id === this.comparacion[0]).Name;
      let u2 = this.usuarios.find(u => u.Id === this.comparacion[1]).Name;
      this.service.compareUsers(u1, u2, res => {
        const data = res.body.Result;
        let resultado;
        if(data.Patrimonio1 === data.Patrimonio2){
          resultado = 'Ambos usuarios tiene la misma cantidad de capital.';
        } else if(data.Patrimonio1 > data.Patrimonio2){
          resultado = `El usuario \"${u1}\" tiene mayor capital que el usuario \"${u2}\".`;
        } else {
          resultado = `El usuario \"${u2}\" tiene mayor capital que el usuario \"${u1}\".`;
        }        
        this.message = { isOk: true, msg: resultado };
        $('#modalResponse').modal('show');
      }, err => {
        if(err.status >= 400){
          console.log(err);
          this.message = {
            isOk: false, 
            msg: 'Ocurrio un error al realizar la comparacion'
          };
          $('#modalResponse').modal('show');
        }
      });
    }
  }
}
