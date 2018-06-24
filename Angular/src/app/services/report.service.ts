import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

const api = 'https://tacscripto.azurewebsites.net/api';
//const api = 'http://localhost:51882/api'

@Injectable()
export class ReportService {

  constructor(private http: HttpClient) { }

  getReports() {
    return this.http.get(api + "/admin/reporte");
  }

}
