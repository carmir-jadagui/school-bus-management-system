import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultModel } from '../models/resultModel';
import { environment } from '../../../environments/environments';
import { DriverModel } from '../models/driverModel';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  private baseUrl = environment.apiUrlBase + "driver";

  constructor(private http: HttpClient) {}

  getDriversAll(): Observable<ResultModel<DriverModel[]>> {
    return this.http.get<ResultModel<DriverModel[]>>(
      `${this.baseUrl}`
    );
  }
}