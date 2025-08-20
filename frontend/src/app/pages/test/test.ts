import { Component, OnInit } from '@angular/core';
import { ResultModel } from '../../core/models/resultModel';
import { TestModel } from '../../core/models/testModel';
import { TestService } from '../../core/services/testServices';

@Component({
  selector: 'app-test',
  imports: [],
  templateUrl: './test.html',
  styleUrl: './test.css'
})
export class Test implements OnInit{
  testBackEnd: string | null = null;
  testBD: ResultModel<TestModel[]> | null = null;
  error: string | null = null;

  constructor(private testService: TestService) {}

  ngOnInit() {
    this.testService.getTestBackEnd().subscribe({
      next: (data) => {
        this.testBackEnd = data;
      },
      error: (err) => {
        this.error = 'No se pudo obtener la respuesta de TestBackEnd';
        console.error('Error en API:', err);
      }
    });

    this.testService.getTestBD().subscribe({
      next: (data) => {
        this.testBD = data;
      },
      error: (err) => {
        this.error = 'No se pudo obtener la respuesta de getTestBD';
        console.error('Error en API:', err);
      }
    });
  }
}