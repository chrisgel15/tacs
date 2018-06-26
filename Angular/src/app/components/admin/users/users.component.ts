import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../services/admin.service';
import * as moment from 'moment';
import { access } from 'fs';

declare var $: any;

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public usuarios: Array<any>;
  public filtrados: Array<any>;
  public criteria = { nombre: '', fecha: '' };
  public comparacion = [];
  public comparando = false;
  public message = { isOk: false, msg: null };

  constructor(private service: AdminService) {
    document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
    $(function () {
      $('[data-toggle="tooltip"]').tooltip()
    })
  }

  ngOnInit() {
    this.service.getUsers(data => {
      this.usuarios = data;
      this.filtrados = data;
    });
  }

  ngOnDestroy(){
    document.body.style.background = "whiite";
  }

  buscarUsuariosPorCriterio(){
    this.filtrados = this.usersByCriteria();
  }

  limpiarFiltros(){
    this.criteria.fecha = '';
    this.criteria.nombre = '';
    this.filtrados = this.usuarios;
    this.comparacion.map(i => {
      document.getElementById('usuario-'+i).style.background = "white";
    })
    this.comparacion = [];
  }

  usersByCriteria(){
    const nombre = this.criteria.nombre;
    const fecha = moment(this.criteria.fecha,'YYYY-MM-DD').format('MM/DD/YYYY');
    if (nombre === '' && fecha === '') {
      return this.usuarios;
    } else {
      return this.usuarios.filter(u => {
        if (nombre !== '' && fecha !== '') {
          let access = moment(u.LastAccess,'DD/MM/YYYY').format('MM/DD/YYYY')
          return u.Name.includes(nombre) || Date.parse(access) === Date.parse(fecha);
        } else if (nombre !== '') {
          return u.Name.includes(nombre);
        } else if (fecha !== '') {
          let access = moment(u.LastAccess,'DD/MM/YYYY').format('MM/DD/YYYY')
          return Date.parse(access) === Date.parse(fecha);
        } else {
          return false;
        }
      });
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
    this.comparando = true;
    this.message = { isOk: false, msg: null };
    if(this.comparacion.length < 2){
      this.message = { isOk: false, msg: 'Elija 2 usuarios para comparar.' };
      $('#modalResponse').modal('show');
      this.comparando = false;
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
        this.comparando = false;
        this.comparacion.map(i => {
          document.getElementById('usuario-'+i).style.background = "white";
        });
        this.comparacion = [];
      }, err => {
        if(err.status >= 400){
          console.log(err);
          this.message = {
            isOk: false, 
            msg: 'Ocurrio un error al realizar la comparacion'
          };
          $('#modalResponse').modal('show');
        }
        this.comparando = false;
      });
    }
  }
}
