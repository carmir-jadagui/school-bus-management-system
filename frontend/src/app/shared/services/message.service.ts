// Librerias de angular
import { Injectable } from "@angular/core";
// Librerias de material
import { MatDialog } from "@angular/material/dialog";
// Componentes
import { ConfirmationMessage } from "../message/confirmation-message/confirmation-message";
import { InformationMessage } from "../message/information-message/information-message";

@Injectable({
    providedIn: 'root'
})

export class MessageService {
    constructor(private dialog: MatDialog) { }

    async showInformation(mensaje: string, titulo: string = 'Aviso') {
        const dialogRef = this.dialog.open(InformationMessage, {
            data: {
                title: titulo,
                content: mensaje
            }
        });

        return await dialogRef.afterClosed().toPromise();
    }

    async showConfirmation(mensaje: string, aceptar: string = 'Aceptar', cancelar: string = 'Cancelar', titulo: string = 'Confirmación'): Promise<boolean> {
        const dialogRef = this.dialog.open(ConfirmationMessage, {
            data: {
                title: titulo,
                content: mensaje,
                cancel: cancelar,
                ok: aceptar
            }
        });

        return await dialogRef.afterClosed().toPromise();
    }
}