using Dsw2025Ej8.Exceptions;

namespace Dsw2025Ej8.Domain;

public abstract class CuentaBancaria
{
    public string Numero { get; }
    public decimal Saldo { get; protected set; }
    public Estado Estado { get; protected set; } = Estado.Activa;
    public string[] Titulares { get; }

    protected CuentaBancaria(string numero, decimal saldo, string[] titulares)
    {
        Numero = numero;
        Saldo = saldo;
        Titulares = titulares;
    }

    public abstract void Depositar(decimal monto);
    public abstract void Retirar(decimal monto);
}

public class CajaDeAhorro : CuentaBancaria
{
    public decimal TasaDeInteres { get; set; }

    public CajaDeAhorro(string numero, decimal saldo, string[] titulares)
        : base(numero, saldo, titulares) { }

    public override void Depositar(decimal monto)
    {
        ValidarOperacion(monto);
        Saldo += monto;
    }

    public override void Retirar(decimal monto)
    {
        ValidarOperacion(monto);
        if (Saldo < monto)
        {
            Estado = Estado.Suspendida;
            throw new SaldoInsuficiente("La cuenta no cuenta con saldo para la operacion solicitada. Fue suspendida.");
        }
        Saldo -= monto;
    }

    public void AplicarInteres()
    {
        if (Estado != Estado.Activa) throw new CuentaNoActiva($"No se puede operar con la cuenta {Estado}");
        Saldo += Saldo * TasaDeInteres;
    }

    private void ValidarOperacion(decimal monto)
    {
        if (Estado != Estado.Activa) throw new CuentaNoActiva($"No se puede operar con la cuenta {Estado}");
        if (monto <= 0) throw new MontoNoValido("El monto ingresado no es valido para la operacion solicitada");
    }
}

public class CuentaCorriente : CuentaBancaria
{
    public decimal LimiteDeDescubierto { get; set; }
    public decimal Comision { get; set; }

    public CuentaCorriente(string numero, decimal saldo, string[] titulares)
        : base(numero, saldo, titulares) { }

    public override void Depositar(decimal monto)
    {
        ValidarOperacion(monto);
        monto -= monto * Comision;
        Saldo += monto;
    }

    public override void Retirar(decimal monto)
    {
        ValidarOperacion(monto);
        if (Saldo - monto < -LimiteDeDescubierto)
        {
            Estado = Estado.Suspendida;
            throw new SaldoInsuficiente("La cuenta no cuenta con saldo para la operacion solicitada. Fue suspendida.");
        }
        Saldo -= monto;
    }

    private void ValidarOperacion(decimal monto)
    {
        if (Estado != Estado.Activa) throw new CuentaNoActiva($"No se puede operar con la cuenta {Estado}");
        if (monto <= 0) throw new MontoNoValido("El monto ingresado no es valido para la operacion solicitada");
    }
}
