using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dsw2025Ej8.Domain;
using Dsw2025Ej8.Exceptions;

namespace Dsw2025Ej8;

public class TestCuentas
{
    public static void Ejecutar()
    {
        var cuentas = new List<CuentaBancaria>();

        var titulares1 = new[] { "Paula" };
        var titulares2 = new[] { "Lara" };

        var caja1 = new CajaDeAhorro("CA001", 10000, titulares1) { TasaDeInteres = 0.03m };
        var caja2 = new CajaDeAhorro("CA002", 500, titulares2) { TasaDeInteres = 0.05m };

        var cuenta1 = new CuentaCorriente("CC001", 2000, titulares1) { LimiteDeDescubierto = 1000, Comision = 0.05m };
        var cuenta2 = new CuentaCorriente("CC002", 300, titulares2) { LimiteDeDescubierto = 500, Comision = 0.02m };

        cuentas.Add(caja1);
        cuentas.Add(caja2);
        cuentas.Add(cuenta1);
        cuentas.Add(cuenta2);

        // Operaciones exitosas ;))))
        caja1.Depositar(1000);
        caja1.Retirar(200);
        caja1.AplicarInteres();

        cuenta1.Depositar(1000);
        cuenta1.Retirar(2500); // aun dentro del limite

        // Simular excepciones <3
        try
        {
            caja2.Depositar(0); // Monto no valido capo
        }
        catch (Exception ex)
        {
            Console.WriteLine("Excepcion encontrada!!! " + ex.Message);
        }

        try
        {
            cuenta2.Retirar(1000); // Excede el limite, suspende y lanza excepcion !!!!!!!!!!!!
        }
        catch (Exception ex)
        {
            Console.WriteLine("Excepcion encontrada!!! " + ex.Message);
        }

        try
        {
            cuenta2.Depositar(100); // No deberia poder operar porque ya esta suspendida obviamente
        }
        catch (Exception ex)
        {
            Console.WriteLine("Excepcion encontrada!!! " + ex.Message);
        }

        // Mostrar resumen de todas las cuentas <3
        foreach (var resumen in cuentas.Select(c => new { c.Numero, Tipo = c.GetType().Name, c.Saldo }))
        {
            Console.WriteLine($"Cuenta: {resumen.Numero}, Tipo: {resumen.Tipo}, Saldo: {resumen.Saldo:C}");
        }
    }
}
