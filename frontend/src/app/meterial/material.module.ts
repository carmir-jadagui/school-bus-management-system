// Librerias de angular
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
// Liberias de material
import { MatIconRegistry, MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

// export const MATERIAL_IMPORTS = [
//   MatIconRegistry,
//   MatIconModule,
//   MatPaginatorModule,
//   MatTableModule,
//   MatFormFieldModule,
//   MatInputModule
// ];


// import { MatRadioModule } from '@angular/material/radio';
// import { NgModule } from '@angular/core';
// import { CoreModule } from '../_metronic/core';
// import { MatSlideToggleModule } from '@angular/material/slide-toggle';

// import {
//   MatRippleModule,
//   MatNativeDateModule,
//   MAT_DATE_LOCALE,
//   MatOptionModule
// } from '@angular/material/core';

// import { MatDialogModule } from '@angular/material/dialog';
// import { MatCheckboxModule } from '@angular/material/checkbox';
// import { MatDatepickerModule } from '@angular/material/datepicker';

// // Data table
// import { MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
// import { MatFormFieldModule, MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
// import { MatAutocompleteModule } from '@angular/material/autocomplete';
// import { MatButtonModule } from '@angular/material/button';
// import { MatTabsModule } from '@angular/material/tabs';
// import { MatTooltipModule } from '@angular/material/tooltip';
// import { MatSelectModule } from '@angular/material/select';
// import { MatCardModule } from '@angular/material/card';
// import { MatMenuModule } from '@angular/material/menu';
// import { MatProgressBarModule } from '@angular/material/progress-bar';
// import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
// import { MatSnackBarModule } from '@angular/material/snack-bar';
// import { MatListModule } from '@angular/material/list';
// import { MatSidenavModule } from '@angular/material/sidenav';
// import { MatSliderModule } from '@angular/material/slider';
// import { MatGridListModule } from '@angular/material/grid-list';
// import { MatButtonToggleModule } from '@angular/material/button-toggle';
// import { MatTreeModule } from '@angular/material/tree';
// import { MatChipsModule } from '@angular/material/chips';
// import { MatStepperModule } from '@angular/material/stepper';
// import { MatSortModule } from '@angular/material/sort';
// import { MatToolbarModule } from '@angular/material/toolbar';

// import {
//   MatBottomSheetModule,
//   MatBottomSheetRef,
//   MAT_BOTTOM_SHEET_DATA,
// } from '@angular/material/bottom-sheet';

// import { MatExpansionModule } from '@angular/material/expansion';
// import { MatDividerModule } from '@angular/material/divider';


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



    // CommonModule,
    // MatDatepickerModule,
    // MatAutocompleteModule,
    // MatListModule,
    // MatSliderModule,
    // MatCardModule,
    // MatSelectModule,
    // MatOptionModule,
    // MatButtonModule,
    // MatNativeDateModule,
    // MatSlideToggleModule,
    // MatCheckboxModule,
    // MatMenuModule,
    // MatTabsModule,
    // MatTooltipModule,
    // MatSidenavModule,
    // MatProgressBarModule,
    // MatProgressSpinnerModule,
    // MatSnackBarModule,
    // MatGridListModule,
    // MatToolbarModule,
    // MatBottomSheetModule,
    // MatExpansionModule,
    // MatDividerModule,
    // MatSortModule,
    // MatStepperModule,
    // MatChipsModule,
    // MatDialogModule,
    // MatRippleModule,
    // MatRadioModule,
    // MatTreeModule,
    // MatButtonToggleModule,
    // CoreModule
  ],
  providers: [
    MatIconRegistry
    // { provide: MatBottomSheetRef, useValue: {} },
    // { provide: MAT_BOTTOM_SHEET_DATA, useValue: {} },
    // { provide: MAT_DATE_LOCALE, useValue: 'es-ES' },
    // { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
    // { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'standard'}}
  ]
})
export class MaterialModule {}