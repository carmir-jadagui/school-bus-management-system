// Librerias de angular
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
// Shared
import { Header } from './shared/header/header';
import { Footer } from './shared/footer/footer';
import { MaterialModule } from './meterial/material.module';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Footer, MaterialModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('Sistema de Gestión de Micros Escolares, Alumnos y Choferes');
}