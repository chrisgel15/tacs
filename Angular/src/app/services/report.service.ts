import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { apiTacs } from '../config';

@Injectable()
export class ReportService {

  constructor(private http: HttpClient) { }

  getReports() {
    return this.http.get(apiTacs + "/admin/reporte");
  }

}
