import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { Router } from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private service: AdminService, private router: Router) { 
    document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
  }

  ngOnInit() {
    $('ul li a').click(function(){ $('li a').removeClass("active"); $(this).addClass("active"); });
  }

  ngOnDestroy(){
    document.body.style.background = "white";
  }

  signOut() {
    this.service.signOut(
      () => {
        sessionStorage.removeItem('tacs-token');
        this.router.navigate(['']);
      },
      () => {
        alert('Ocurrio un error al desconectarte del sitio.');
      }
    );
  }

}
