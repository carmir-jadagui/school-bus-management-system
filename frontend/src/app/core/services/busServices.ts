// Librerias de angular
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
// Otros
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';
// Modelos
import { ResultModel } from '../models/resultModel';
import { ResponseBaseModel } from '../models/responseBaseModel';
import { BusModel } from '../models/busModel';

@Injectable({
  providedIn: 'root'
})

export class BusService {
  private baseUrl = environment.apiUrlBase + "bus";

  constructor(private http: HttpClient) { }

  getBusesAll(): Observable<ResultModel<BusModel[]>> {
    return this.http.get<ResultModel<BusModel[]>>(
      `${this.baseUrl}`
    );
  }

  getBusById(id: number): Observable<ResultModel<BusModel>> {
    return this.http.get<ResultModel<BusModel>>(
      `${this.baseUrl}/${id}`
    );
  }

  createBus(bus: BusModel): Observable<ResultModel<ResponseBaseModel>> {
    // Para eliminar el campo id, y el back permita la creación del registro
    const busC: Omit<BusModel, 'id'> = bus;
    delete (busC as any).id;

    return this.http.post<ResultModel<ResponseBaseModel>>(
      `${this.baseUrl}`, busC
    );
  }

  updateBus(bus: BusModel): Observable<ResultModel<ResponseBaseModel>> {
    return this.http.put<ResultModel<ResponseBaseModel>>(
      `${this.baseUrl}`, bus
    );
  }

  deleteBus(id: number): Observable<ResultModel<ResponseBaseModel>> {
    return this.http.delete<ResultModel<ResponseBaseModel>>(
      `${this.baseUrl}/${id}`
    );
  }
}