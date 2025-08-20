import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultModel } from '../models/resultModel';
import { environment } from '../../../environments/environments';
import { TestModel } from '../models/testModel';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  private baseUrl = environment.apiUrlBase + "test";

  constructor(private http: HttpClient) {}

  getTestBackEnd(): Observable<string> {
    return this.http.get<string>(`${this.baseUrl}/TestBackEnd`, 
      { responseType: 'text' as 'json' });
  }

  getTestBD(): Observable<ResultModel<TestModel[]>> {
    return this.http.get<ResultModel<TestModel[]>>(
      `${this.baseUrl}/TestBD`
    );
  }
}