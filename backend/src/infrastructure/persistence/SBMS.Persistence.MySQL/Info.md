Para actualizar la estructura de la BD se debe:

- Ir a Tools -> NuGet Package Manager -> Package Console
- Seleccionar el proyecto de la capa de persistencia: SBMS.Persistence.MySql
- Ejecutar la siguiente instrucci�n:
  Scaffold-DbContext "Server=[SERVER];Database=[DB];User=[USER];Password=[PASSWORD];TreatTinyAsBoolean=true;" "Pomelo.EntityFrameworkCore.MySql" -OutputDir ./Entities -ContextDir ./Entities -Context SBMSContext -Force

  Nota: reemplazar los valores [SERVER], [DB], [USER] y [PASSWORD] por los valores correspondientes, sin usar los corchetes [].

Importante: al ejecutar el comando (Scaffold-DbContext), se crear� el Context con el m�todo OnConfiguring, el cual hay que borrar, ya que detalla los las credeciales usadas.