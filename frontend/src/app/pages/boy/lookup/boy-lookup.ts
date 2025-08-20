import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { BoyModel } from '../../../core/models/boyModel';
import { ResultModel } from '../../../core/models/resultModel';
import { BoyService } from '../../../core/services/boyServices';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-boy-lookup',
  standalone: true,
  imports: [
    MatFormFieldModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
  ],
  templateUrl: './boy-lookup.html',
  styleUrl: './boy-lookup.css'
})
export class BoyLookup implements AfterViewInit {
  displayedColumns: string[] = ['id', 'dni', 'apellidos', 'nombres', 'sexo', 'edad'];
  dataSource = new MatTableDataSource<BoyModel>([]);
  filterValue = '';
  error: string | null = null;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private boyService: BoyService) {}

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
}