// Librerias de angular
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
// Librerias de material
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule, CommonModule, MatToolbarModule, MatMenuModule, MatButtonModule],
  templateUrl: './header.html',
  styleUrl: './header.css'
})

export class Header {
}