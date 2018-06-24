import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { apiTacs } from '../config' ;

@Injectable()
export class AdminService {

  constructor(private http: HttpClient) { }


  getUsers(callback){
    this.http.get(apiTacs + '/admin/users').subscribe(callback);
  }

  signOut(callbackOk, callbackError){
    this.http
      .delete(apiTacs + '/token', { observe: 'response' })
      .subscribe(res => {
        if (res.status === 200){
          callbackOk();
        } else {
          callbackError();
        }
      }, err => callbackError())
  }
}
