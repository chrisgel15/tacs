import { Injectable } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';

@Injectable()
export class TokenInterceptorService implements HttpInterceptor {

  intercept(req, next) {
    let tokenizedReq = req.clone( {
      setHeaders: {
        Authorization: 'Bearer ' + sessionStorage.getItem('tacs-token')
      }
    })

    return next.handle(tokenizedReq);
  }

}
