// Librerias de angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
// Librerias de material
import { MATERIAL_IMPORTS, MATERIAL_PROVIDERS } from '../../../meterial/material.module';
// Servicios
import { DriverService } from '../../../core/services/driverServices';
import { MessageService } from '../../../shared/services/message.service';
import { HelpersService } from '../../../shared/services/helpers.service';
// Modelos
import { DriverModel } from '../../../core/models/driverModel';
import { ResultModel } from '../../../core/models/resultModel';
import { ResponseBaseModel } from '../../../core/models/responseBaseModel';

@Component({
  selector: 'app-driver-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MATERIAL_IMPORTS],
  providers: [MATERIAL_PROVIDERS],
  templateUrl: './driver-form.html',
})

export class DriverForm implements OnInit {
  error: string | null = null;
  driverForm!: FormGroup;
  id?: number;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private driverService: DriverService,
    private messageService: MessageService,
    private helpersService: HelpersService
  ) { }

  ngOnInit(): void {
    this.driverForm = this.fb.group({
      id: [''],
      dni: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      telephone: ['', [Validators.pattern(/^\+?[0-9 ]*$/)]]
    });

    this.route.paramMap.subscribe(params => {
      const idParam = params.get('id');
      if (idParam) {
        this.id = +idParam;
        this.isEditMode = true;
        this.loadDriver(this.id);
      }
    });
  }

  loadDriver(id: number) {
    this.driverService.getDriverById(id).subscribe({
      next: (res: ResultModel<DriverModel>) => {
        if (res.ok) {
          this.driverForm.patchValue(res.data);
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
    if (this.driverForm.invalid) {
      this.driverForm.markAllAsTouched();
      return;
    }

    const driver: DriverModel = this.driverForm.value;

    if (this.isEditMode && this.id) { //Para modificar
      this.driverService.updateDriver(driver).subscribe({
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
      this.driverService.createDriver(driver).subscribe({
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

  // Permite solo números
  onKeyPressDNI(event: KeyboardEvent) {
    this.helpersService.allowOnlyNumbers(event);
  }

  // Permite el signo de + sólo en el primer caracter, numeros y espacios
  onKeyPressTlf(event: KeyboardEvent) {
    const input = event.target as HTMLInputElement;
    const value = input.value;
    const char = event.key;

    // Permitir teclas de control (borrar, flechas, tab, etc.)
    if (event.ctrlKey || event.altKey || char.length > 1) {
      return;
    }

    // Primera posición sólo se permite '+' o número
    if (value.length === 0) {
      if (!/[0-9+]/.test(char)) {
        event.preventDefault();
      }
      return;
    }

    // A partir de la segunda posición sólo números o espacio
    if (!/[0-9 ]/.test(char)) {
      event.preventDefault();
    }
  }
}