// Librerias de angular
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class HelpersService {

  constructor() { }

  // Permite solo números
  allowOnlyNumbers(event: KeyboardEvent) {
    const charCode = event.key;
    if (!/[0-9]/.test(charCode)) {
      event.preventDefault();
    }
  }
}