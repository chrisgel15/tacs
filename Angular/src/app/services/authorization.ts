// import { Injectable } from '@angular/core';
// import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
// import { Observable } from 'rxjs/Observable';

// @Injectable()
// export class TacsHttpInterceptor implements HttpInterceptor {
  
//   intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
//     const newRequest = req.clone({
//       headers: req.headers
//         .set('Authorization', 'Bearer' + sessionStorage.getItem('tacs-token'))
//     });
    
//     return next.handle(newRequest);
//   }
// }
import { Injectable } from '@angular/core';

@Injectable()
export class Authorization {

  getAutorization(){
    return 'bearer ' + sessionStorage.getItem('tacs-token');
  }
}