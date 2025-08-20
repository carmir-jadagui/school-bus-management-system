import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultModel } from '../models/resultModel';
import { environment } from '../../../environments/environments';
import { BoyModel } from '../models/boyModel';

@Injectable({
  providedIn: 'root'
})
export class BoyService {
  private baseUrl = environment.apiUrlBase + "boy";

  constructor(private http: HttpClient) {}

  getBoysAll(): Observable<ResultModel<BoyModel[]>> {
    return this.http.get<ResultModel<BoyModel[]>>(
      `${this.baseUrl}`
    );
  }
}