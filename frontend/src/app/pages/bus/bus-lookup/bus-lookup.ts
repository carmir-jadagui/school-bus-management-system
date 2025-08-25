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
import { BusModel } from '../../../core/models/busModel';
// Servicios
import { MATERIAL_IMPORTS, MATERIAL_PROVIDERS } from '../../../meterial/material.module';
import { MessageService } from '../../../shared/services/message.service';
import { BusService } from '../../../core/services/busServices';

@Component({
  selector: 'app-bus-lookup',
  standalone: true,
  imports: [FormsModule, RouterLink, MATERIAL_IMPORTS],
  providers: [MATERIAL_PROVIDERS],
  templateUrl: './bus-lookup.html',
  styleUrl: './bus-lookup.css'
})

export class BusLookup implements AfterViewInit {
  displayedColumns: string[] = ['id', 'plate', 'brand', 'driverData', 'boysCount', 'acciones'];
  dataSource = new MatTableDataSource<BusModel>([]);
  filterValue = '';
  error: string | null = null;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private busService: BusService,
    private messageService: MessageService
  ) { }

  ngAfterViewInit() {
    this.busService.getBusesAll().subscribe({
      next: (res: ResultModel<BusModel[]>) => {
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

  getLinkDetail(): string {
    return '/bus/form';
  }

  getLinkModify(): string {
    return '/bus/form';
  }

  async getLinkDelete(id: number) {
    const respuesta = await this.messageService.showConfirmation(
      `¿Está seguro que desea eliminar a este Micro?`);
    if (respuesta) {
      this.busService.deleteBus(id).subscribe({
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