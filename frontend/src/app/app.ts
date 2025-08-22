// Librerias de angular
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
// Shared
import { Header } from './shared/header/header';
import { Footer } from './shared/footer/footer';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Header, Footer],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('Sistema de Gestión de Micros Escolares, Alumnos y Choferes');
}