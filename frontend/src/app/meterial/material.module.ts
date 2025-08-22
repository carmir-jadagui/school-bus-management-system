import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

export const MATERIAL_IMPORTS = [
  MatIconModule,
  MatPaginatorModule,
  MatTableModule,
  MatFormFieldModule,
  MatInputModule
];

export const MATERIAL_PROVIDERS = [
  MatIconRegistry
];