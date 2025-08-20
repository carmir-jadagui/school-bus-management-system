import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { BusModel } from '../../../core/models/busModel';
import { ResultModel } from '../../../core/models/resultModel';
import { BusService } from '../../../core/services/busServices';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-bus-lookup',
  standalone: true,
  imports: [
    MatFormFieldModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
  ],
  templateUrl: './bus-lookup.html',
  styleUrl: './bus-lookup.css'
})
export class BusLookup implements AfterViewInit {
  displayedColumns: string[] = ['id', 'plate', 'brand'];
  dataSource = new MatTableDataSource<BusModel>([]);
  filterValue = '';
  error: string | null = null;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private busService: BusService) {}

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
}