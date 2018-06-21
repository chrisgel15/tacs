import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UnauthGuardGuard implements CanActivate {
  constructor(private router: Router) {

  }
  
  canActivate() {
    console.log(sessionStorage.getItem('access_token'));
    if(sessionStorage.getItem('tacs-token') == null)
      return true;
    else {
      this.router.navigate(['auth']);
      return false;
    }
  }
}
