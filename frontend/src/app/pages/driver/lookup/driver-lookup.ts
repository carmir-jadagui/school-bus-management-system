import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { DriverModel } from '../../../core/models/driverModel';
import { ResultModel } from '../../../core/models/resultModel';
import { DriverService } from '../../../core/services/driverServices';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-driver-lookup',
  standalone: true,
  imports: [
    MatFormFieldModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
  ],
  templateUrl: './driver-lookup.html',
  styleUrl: './driver-lookup.css'
})
export class DriverLookup implements AfterViewInit {
  displayedColumns: string[] = ['id', 'dni', 'apellidos', 'nombres', 'telefono'];
  dataSource = new MatTableDataSource<DriverModel>([]);
  filterValue = '';
  error: string | null = null;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private driverService: DriverService) {}

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
}