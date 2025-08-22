// Librerias de angular
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
// Liberias de material
import { MatIconRegistry, MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    MatIconModule,
    MatPaginatorModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule
  ],
  providers: [
    MatIconRegistry
  ]
})
export class MaterialModule {}