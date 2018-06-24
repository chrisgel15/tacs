import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})

export class AuthComponent  implements OnInit  {
  isAdmin: boolean;

  constructor(private router: Router) {
  }

  ngOnInit() {
    this.isAdmin = (sessionStorage.getItem('admin') == 'SI');
    console.log(this.isAdmin);
  }

  signOut() {
    sessionStorage.removeItem('tacs-token');
    this.router.navigate(['']);
  }
}
