import { Component, OnInit } from '@angular/core';
import { trigger, state, animate, style, transition } from '@angular/animations';
import { InicioService } from '../../services/inicio.service';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css'],
  animations: [
    trigger('fadeMessage', [
      state('shown', style({ opacity:1, display:'block' })),
      state('hidden', style({ opacity:0, display:'none' })),
      transition('shown => hidden', animate('1000ms ease-out')),
      transition('hidden => shown', animate('300ms ease-in')),
    ])
  ]
})
export class InicioComponent implements OnInit {

  public validation: {
    error: { show: boolean, msg: string }, 
    success: { show: boolean, msg: string }
  };

  constructor(private servicio: InicioService) {}

  ngOnInit() {
    this.servicio.InfoInicio.subscribe(result => {
      if (result.isError && result.msg){
        this.validation.error = { show: true, msg: result.msg };
      } else if (!result.isError && result.msg){
        this.validation.success = { show: true, msg: result.msg };
      } else {
        this.validation = { 
          error: { show: false, msg: null },
          success: { show: false, msg: null }
        };
      }
      // si no da clic sobre el aviso, desaparecera en 3s.
      setTimeout(() => { this.toggle() }, 3000);
    });    
  }

  toggle(){
    this.validation.error.show = false;
    this.validation.success.show = false;
  }
}
