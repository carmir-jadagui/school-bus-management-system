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
import { DriverModel } from '../../../core/models/driverModel';
// Servicios
import { MATERIAL_IMPORTS, MATERIAL_PROVIDERS } from '../../../meterial/material.module';
import { MessageService } from '../../../shared/services/message.service';
import { DriverService } from '../../../core/services/driverServices';

@Component({
  selector: 'app-driver-lookup',
  standalone: true,
  imports: [FormsModule, RouterLink, MATERIAL_IMPORTS],
  providers: [MATERIAL_PROVIDERS],
  templateUrl: './driver-lookup.html',
  styleUrl: './driver-lookup.css'
})

export class DriverLookup implements AfterViewInit {
  displayedColumns: string[] = ['id', 'dni', 'apellidos', 'nombres', 'telefono', 'acciones'];
  dataSource = new MatTableDataSource<DriverModel>([]);
  filterValue = '';
  error: string | null = null;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private driverService: DriverService,
    private messageService: MessageService
  ) { }

  ngAfterViewInit() {
    this.driverService.getDriversAll().subscribe({
      next: (res: ResultModel<DriverModel[]>) => {
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
    return '/driver/form';
  }

  async getLinkDelete(id: number) {
    const respuesta = await this.messageService.showConfirmation(
      `¿Está seguro que desea eliminar a este Chofer?`);
    if (respuesta) {
      this.driverService.deleteDriver(id).subscribe({
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