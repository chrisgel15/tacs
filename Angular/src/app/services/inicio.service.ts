import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

const api = 'https://tacscripto.azurewebsites.net/api';
//const api = 'http://localhost:51882/api';

@Injectable()
export class InicioService {
  
  private EmitirErrorSubject = new BehaviorSubject<any>({isError: false, msg: null});
  InfoInicio = this.EmitirErrorSubject.asObservable();

  constructor(private http: HttpClient) {
    //this.EmitirError$ = this.EmitirErrorSubject.asObservable();
  }

  IniciarSesion(data, callback){
    this.http.post(api + '/token', data, { observe: 'response' })
      .subscribe(resp => callback(resp), err => callback(err));
  }

  Registrar(data, callback){
    this.http.post(api + '/user', data, { observe: 'response' })
      .subscribe(resp => callback(resp), err => callback(err));
  }

  EmitirError(data) {
    this.EmitirErrorSubject.next(data);
  }
}
