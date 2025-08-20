import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultModel } from '../models/resultModel';
import { environment } from '../../../environments/environments';
import { BusModel } from '../models/busModel';

@Injectable({
  providedIn: 'root'
})
export class BusService {
  private baseUrl = environment.apiUrlBase + "bus";

  constructor(private http: HttpClient) {}

  getBusesAll(): Observable<ResultModel<BusModel[]>> {
    return this.http.get<ResultModel<BusModel[]>>(
      `${this.baseUrl}`
    );
  }
}