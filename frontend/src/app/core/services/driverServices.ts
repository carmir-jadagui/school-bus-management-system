// Librerias de angular
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
// Otros
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';
// Modelos
import { ResultModel } from '../models/resultModel';
import { ResponseBaseModel } from '../models/responseBaseModel';
import { DriverModel } from '../models/driverModel';

@Injectable({
  providedIn: 'root'
})

export class DriverService {
  private baseUrl = environment.apiUrlBase + "driver";

  constructor(private http: HttpClient) { }

  getDriversAll(): Observable<ResultModel<DriverModel[]>> {
    return this.http.get<ResultModel<DriverModel[]>>(
      `${this.baseUrl}`
    );
  }

  getDriverById(id: number): Observable<ResultModel<DriverModel>> {
    return this.http.get<ResultModel<DriverModel>>(
      `${this.baseUrl}/${id}`
    );
  }

  createDriver(driver: DriverModel): Observable<ResultModel<ResponseBaseModel>> {
    // Para eliminar el campo id, y el back permita la creación del registro
    const driverC: Omit<DriverModel, 'id'> = driver;
    delete (driverC as any).id;

    return this.http.post<ResultModel<ResponseBaseModel>>(
      `${this.baseUrl}`, driverC
    );
  }

  updateDriver(driver: DriverModel): Observable<ResultModel<ResponseBaseModel>> {
    return this.http.put<ResultModel<ResponseBaseModel>>(
      `${this.baseUrl}`, driver
    );
  }

  deleteDriver(id: number): Observable<ResultModel<ResponseBaseModel>> {
    return this.http.delete<ResultModel<ResponseBaseModel>>(
      `${this.baseUrl}/${id}`
    );
  }
}