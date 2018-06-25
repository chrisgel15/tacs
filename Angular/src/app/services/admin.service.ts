import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { apiTacs } from '../config' ;

@Injectable()
export class AdminService {

  constructor(private http: HttpClient) { }


  getUsers(callback){
    this.http.get(apiTacs + '/admin/users').subscribe(callback);
  }

  compareUsers(user1, user2, callbackOk, callbackError){
    const params = new HttpParams().set('userName1', user1).set('userName2', user2);
    this.http
      .get(apiTacs + '/admin/compare', { params: params, observe: 'response' })
      .subscribe(callbackOk, callbackError);
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
