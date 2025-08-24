// Librerias de angular
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";
// Librerias de materia
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
// Modelos
import { ResultModel } from '../../../core/models/resultModel';
import { ResponseBaseModel } from '../../../core/models/responseBaseModel';
import { BoyModel } from '../../../core/models/boyModel';
// Servicios
import { MATERIAL_IMPORTS, MATERIAL_PROVIDERS } from '../../../meterial/material.module';
import { MessageService } from '../../../shared/services/message.service';
import { BoyService } from '../../../core/services/boyServices';

@Component({
  selector: 'app-boy-lookup',
  standalone: true,
  imports: [FormsModule, RouterLink, MATERIAL_IMPORTS],
  providers: [MATERIAL_PROVIDERS],
  templateUrl: './boy-lookup.html',
  styleUrl: './boy-lookup.css'
})

export class BoyLookup implements AfterViewInit {
  displayedColumns: string[] = ['id', 'dni', 'apellidos', 'nombres', 'sexo', 'edad', 'acciones'];
  dataSource = new MatTableDataSource<BoyModel>([]);
  filterValue = '';
  error: string | null = null;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private boyService: BoyService,
    private messageService: MessageService
  ) { }

  ngAfterViewInit() {
    this.boyService.getBoysAll().subscribe({
      next: (res: ResultModel<BoyModel[]>) => {
        if (res.ok) {
          this.dataSource = new MatTableDataSource(res.data);
          this.dataSource.paginator = this.paginator;
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

  applyFilter(): void {
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }

  getLinkModify(): string {
    return '/boy/form';
  }

  async getLinkDelete(id: number) {
    const respuesta = await this.messageService.showConfirmation(
      `¿Está seguro que desea eliminar a este(a) Chico(a)?`);
    if (respuesta) {
      this.boyService.deleteBoy(id).subscribe({
        next: (res: ResultModel<ResponseBaseModel>) => {
          if (res.ok) {
            this.messageService.showInformation(res.message);
            this.ngAfterViewInit();
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
  }
}