using System;
using System.Collections.Generic;

namespace LiquidacionHaberes
{
    public abstract class Empleado
    {
        // ENCAPSULAMIENTO
        private string _nombre;
        private string _dni;
        private decimal _sueldoBase;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        public decimal SueldoBase
        {
            get { return _sueldoBase; }
            set { _sueldoBase = value >= 0 ? value : 0; }
        }
        public Empleado(string nombre, string dni, decimal sueldoBase)
        {
            Nombre = nombre;
            Dni = dni;
            SueldoBase = sueldoBase;
        }

        //POLIMORFISMO
        public abstract decimal CalcularSalarioFinal();

        public void MostrarDatos()
        {
            Console.WriteLine($"Nombre: {Nombre} | DNI: {Dni} | Sueldo Base: ${SueldoBase:F2}");
        }
    }

    //HERENCIA
    public class Vendedor : Empleado
    {
        //ENCAPSULAMIENTO
        private decimal _comision;

        public decimal Comision
        {
            get { return _comision; }
            set { _comision = value >= 0 ? value : 0; }
        }

        //HERENCIA
        public Vendedor(string nombre, string dni, decimal sueldoBase, decimal comision)
            : base(nombre, dni, sueldoBase)
        {
            Comision = comision;
        }
        //POLIMORFISMO
        public override decimal CalcularSalarioFinal()
        {
            return SueldoBase + Comision;
        }
    }

    // HERENCIA
    public class Directivo : Empleado
    {
        //ENCAPSULAMIENTO
        private decimal _bono;

        public decimal Bono
        {
            get { return _bono; }
            set { _bono = value >= 0 ? value : 0; }
        }
        //HERENCIA
        public Directivo(string nombre, string dni, decimal sueldoBase, decimal bono)
            : base(nombre, dni, sueldoBase)
        {
            Bono = bono;
        }

        //POLIMORFISMO
        public override decimal CalcularSalarioFinal()
        {
            return SueldoBase + Bono;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LIQUIDACIÓN DE HABERES ===\n");

            // --- POLIMORFISMO
            List<Empleado> personal = new List<Empleado>
            {
                new Vendedor("María López", "28.567.890", 650000m, 120000m),
                new Vendedor("Carlos Ruiz", "30.123.456", 600000m, 95000m),
                new Directivo("Ana Martínez", "24.987.654", 950000m, 350000m),
                new Directivo("Roberto Gómez", "22.456.789", 980000m, 420000m)
            };

            decimal total = 0;

            foreach (Empleado emp in personal)
            {
                emp.MostrarDatos();

                if (emp is Vendedor v)
                    Console.WriteLine($"Comisión: ${v.Comision:F2}");
                else if (emp is Directivo d)
                    Console.WriteLine($"Bono: ${d.Bono:F2}");

                //POLIMORFISMO
                decimal pago = emp.CalcularSalarioFinal();

                Console.WriteLine($"A depositar: ${pago:F2}\n");
                total += pago;
            }

            Console.WriteLine($"TOTAL A PAGAR: ${total:F2}");
            Console.ReadKey();
        }
    }
}