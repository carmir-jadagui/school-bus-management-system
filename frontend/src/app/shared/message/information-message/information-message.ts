// Librerias de angular
import { Component, Inject, OnInit } from '@angular/core';
// Librerias de material
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MATERIAL_IMPORTS, MATERIAL_PROVIDERS } from '../../../meterial/material.module';

@Component({
  selector: 'app-information-message',
  imports: [MATERIAL_IMPORTS],
  providers: [MATERIAL_PROVIDERS],
  templateUrl: './information-message.html',
})
export class InformationMessage implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, public dialogRef: MatDialogRef<InformationMessage>) { }

  ngOnInit(): void {

  }

  close(): void {
    this.dialogRef.close();
  }
}