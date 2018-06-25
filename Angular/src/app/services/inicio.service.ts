import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { apiTacs } from '../config';

@Injectable()
export class InicioService {
  
  private EmitirErrorSubject = new BehaviorSubject<any>({isError: false, msg: null});
  InfoInicio = this.EmitirErrorSubject.asObservable();

  constructor(private http: HttpClient) {
    //this.EmitirError$ = this.EmitirErrorSubject.asObservable();
  }

  IniciarSesion(data, callback){
    this.http.post(apiTacs + '/token', data, { observe: 'response' })
      .subscribe(resp => callback(resp), err => callback(err));
  }

  Registrar(data, callback){
    this.http.post(apiTacs + '/user', data, { observe: 'response' })
      .subscribe(resp => callback(resp), err => callback(err));
  }

  InfoDelCliente(callbackOk, callbackError){
    this.http.get(apiTacs + '/user', { observe: 'response' })
      .subscribe( res => callbackOk(res.body), err => callbackError(err));
  }

  EmitirError(data) {
    this.EmitirErrorSubject.next(data);
  }
}
