import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthGuardGuard implements CanActivate {
  constructor(private router: Router) {

  }

  canActivate() {
    if(sessionStorage.getItem('tacs-token') != null)
      return true;
    else {
      this.router.navigate(['']);
      return false;
    }
  }
}
