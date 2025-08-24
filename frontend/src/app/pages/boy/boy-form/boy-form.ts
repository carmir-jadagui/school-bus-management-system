// Librerias de angular
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
// Librerias de material
import { MATERIAL_IMPORTS, MATERIAL_PROVIDERS } from '../../../meterial/material.module';
// Servicios
import { BoyService } from '../../../core/services/boyServices';
import { MessageService } from '../../../shared/services/message.service';
import { HelpersService } from '../../../shared/services/helpers.service';
// Modelos
import { BoyModel } from '../../../core/models/boyModel';
import { ResultModel } from '../../../core/models/resultModel';
import { ResponseBaseModel } from '../../../core/models/responseBaseModel';

@Component({
  selector: 'app-boy-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MATERIAL_IMPORTS],
  providers: [MATERIAL_PROVIDERS],
  templateUrl: './boy-form.html',
})

export class BoyForm implements OnInit {
  error: string | null = null;
  boyForm!: FormGroup;
  id?: number;
  isEditMode = false;
  genderOptions = [
    { value: 'M', viewValue: 'Masculino' },
    { value: 'F', viewValue: 'Femenino' },
    { value: 'O', viewValue: 'Otro' }
  ];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private boyService: BoyService,
    private messageService: MessageService,
    private helpersService: HelpersService
  ) { }

  ngOnInit(): void {
    this.boyForm = this.fb.group({
      id: [''],
      dni: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      age: ['', [Validators.required, Validators.min(5), Validators.max(18)]]
    });

    this.route.paramMap.subscribe(params => {
      const idParam = params.get('id');
      if (idParam) {
        this.id = +idParam;
        this.isEditMode = true;
        this.loadBoy(this.id);
      }
    });
  }

  loadBoy(id: number) {
    this.boyService.getBoyById(id).subscribe({
      next: (res: ResultModel<BoyModel>) => {
        if (res.ok) {
          this.boyForm.patchValue(res.data);
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
    if (this.boyForm.invalid) {
      this.boyForm.markAllAsTouched();
      return;
    }

    const boy: BoyModel = this.boyForm.value;

    if (this.isEditMode && this.id) { //Para modificar
      this.boyService.updateBoy(boy).subscribe({
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
      this.boyService.createBoy(boy).subscribe({
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
  onKeyPress(event: KeyboardEvent) {
    this.helpersService.allowOnlyNumbers(event);
  }
}