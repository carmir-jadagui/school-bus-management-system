// Librerias de angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
// Librerias de material
import { MATERIAL_IMPORTS, MATERIAL_PROVIDERS } from '../../../meterial/material.module';
// Servicios
import { BusService } from '../../../core/services/busServices';
import { MessageService } from '../../../shared/services/message.service';
// Modelos
import { BusModel } from '../../../core/models/busModel';
import { ResultModel } from '../../../core/models/resultModel';
import { ResponseBaseModel } from '../../../core/models/responseBaseModel';

@Component({
  selector: 'app-bus-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MATERIAL_IMPORTS],
  providers: [MATERIAL_PROVIDERS],
  templateUrl: './bus-form.html',
})

export class BusForm implements OnInit {
  error: string | null = null;
  busForm!: FormGroup;
  id?: number;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private busService: BusService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.busForm = this.fb.group({
      id: [''],
      plate: ['', [Validators.required, Validators.pattern(/^([A-Z]{3}-?\d{3}|[A-Z]{2}\s?\d{3}\s?[A-Z]{2})$/i)]],
      brand: ['']
    });

    this.route.paramMap.subscribe(params => {
      const idParam = params.get('id');
      if (idParam) {
        this.id = +idParam;
        this.isEditMode = true;
        this.loadBus(this.id);
      }
    });
  }

  loadBus(id: number) {
    this.busService.getBusById(id).subscribe({
      next: (res: ResultModel<BusModel>) => {
        if (res.ok) {
          this.busForm.patchValue(res.data);
        } else {
          this.error = res.message || 'Error al cargar datos';
        }
      },
      error: (err) => {
        this.error = 'No se pudo conectar al backend';
        console.error(err);
      }
    });
  }

  save() {
    if (this.busForm.invalid) {
      this.busForm.markAllAsTouched();
      return;
    }

    const bus: BusModel = this.busForm.value;

    if (this.isEditMode && this.id) { //Para modificar
      this.busService.updateBus(bus).subscribe({
        next: (res: ResultModel<ResponseBaseModel>) => {
          if (res.ok) {
            this.messageService.showInformation(res.message.toString());
          } else {
            this.error = res.errors[0].message || 'Error al cargar datos';
          }
        },
        error: (err) => {
          this.error = err.error.title;
          console.error(err);
        }
      });
    } else { //Para agregar
      this.busService.createBus(bus).subscribe({
        next: (res: ResultModel<ResponseBaseModel>) => {
          if (res.ok) {
            let meesage = res.message.toString() + ', con el ID: ' + res.data.id.toString();
            this.messageService.showInformation(meesage);
          } else {
            this.error = res.errors[0].message || 'Error al cargar datos';
          }
        },
        error: (err) => {
          this.error = err.error.title;
          console.error(err);
        }
      });
    }
  }
}