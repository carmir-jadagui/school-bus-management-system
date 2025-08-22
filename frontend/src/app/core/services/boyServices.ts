// Librerias de angular
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
// Otros
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';
// Modelos
import { ResultModel } from '../models/resultModel';
import { ResponseBaseModel } from '../models/ResponseBaseModel';
import { BoyModel } from '../models/boyModel';

@Injectable({
  providedIn: 'root'
})

export class BoyService {
  private baseUrl = environment.apiUrlBase + "boy";

  constructor(private http: HttpClient) { }

  getBoysAll(): Observable<ResultModel<BoyModel[]>> {
    return this.http.get<ResultModel<BoyModel[]>>(
      `${this.baseUrl}`
    );
  }

  deleteBoy(id: number): Observable<ResultModel<ResponseBaseModel>> {
    return this.http.delete<ResultModel<ResponseBaseModel>>(
      `${this.baseUrl}/${id}`
    );
  }
}