import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public usuarios: Array<any>;

  public criteria = {
    nombre: '',
    fecha: ''
  };

  public comparacion = [null, null];

  constructor(private service: AdminService) {}

  ngOnInit() {
    this.service.getUsers((data) => {
      console.log(data);
      this.usuarios = data;
    });
  }

  filtrarPorParametros(user){
    const nombre = this.criteria.nombre;
    const fecha = this.criteria.fecha;
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
    console.log(id);
  }
}
