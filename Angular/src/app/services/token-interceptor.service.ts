import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest } from '@angular/common/http';

@Injectable()
export class TokenInterceptorService implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next) {

    if (req.url.includes('coinmarketcap')){
      return next.handle(req);
    } else {
      let tokenizedReq = req.clone( {
        setHeaders: {
          Authorization: 'Bearer ' + sessionStorage.getItem('tacs-token')
        }
      });  
      return next.handle(tokenizedReq);
    }    
  }

}
