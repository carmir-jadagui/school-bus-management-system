// Librerias de angular
import { Component, Inject, OnInit } from '@angular/core';
// Librerias de material
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MATERIAL_IMPORTS, MATERIAL_PROVIDERS } from '../../../meterial/material.module';

@Component({
  selector: 'app-confirmation-message',
  standalone: true,
  imports: [MATERIAL_IMPORTS],
  providers: [MATERIAL_PROVIDERS],
  templateUrl: './confirmation-message.html'
})

export class ConfirmationMessage implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, public dialogRef: MatDialogRef<ConfirmationMessage>) { }

  ngOnInit(): void {
  }

  close(): void{
    this.dialogRef.close();
  }
}