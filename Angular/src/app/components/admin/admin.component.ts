import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor() { 
    document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
  }

  ngOnInit() {
  }

  ngOnDestroy(){
    document.body.style.background = "white";
  }

}
