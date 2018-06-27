import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../../services/report.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  transacciones;

  constructor(private reportService: ReportService) { 
    document.body.style.background = "linear-gradient(to left, #76b852, #8DC26F)";
  }

  ngOnInit() {
    this.reportService.getReports().subscribe(x => this.transacciones = x);
  }

  ngOnDestroy(){
    document.body.style.background = "whiite";
  }
}
