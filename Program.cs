using System;

// 7 вариант (3 задание)
namespace BPO_3_Polymorphism
{
    interface IFigure
    {
        double Area();
        double Perimeter();
        void ShowInfo();
    }

    abstract class Figure : IFigure
    {
        protected string name;

        public Figure(string name)
        {
            this.name = name;
            Console.WriteLine($"Создана фигура: {name}");
        }

        ~Figure()
        {
            Console.WriteLine($"Деструктор: {name} удалена.");
        }

        public abstract double Area();
        public abstract double Perimeter();
        public abstract void ShowInfo();
    }

    abstract class Quadrilateral : Figure
    {
        protected double sideA, sideB;
        protected Quadrilateral(string name, double a, double b) : base(name)
        {
            sideA = a;
            sideB = b;
        }

        public override double Perimeter() => 2 * (sideA + sideB);
    }

    abstract class Triangle : Figure
    {
        protected double sideA, sideB, sideC;
        protected Triangle(string name, double a, double b, double c) : base(name)
        {
            sideA = a;
            sideB = b;
            sideC = c;
        }

        public override double Perimeter() => sideA + sideB + sideC;
        public abstract override double Area();
    }

    class Square : Quadrilateral
    {
        public Square(double side) : base("Квадрат", side, side)
        {
            Console.WriteLine("Квадрат успешно создан.");
        }

        ~Square() => Console.WriteLine("Деструктор Квадрата вызван.");

        public override double Area() => sideA * sideA;

        public override void ShowInfo()
        {
            Console.WriteLine($"\n{name}");
            Console.WriteLine($"Сторона: {sideA:F2}");
            Console.WriteLine($"Периметр: {Perimeter():F2}");
            Console.WriteLine($"Площадь: {Area():F2}");
        }
    }

    class IsoscelesTriangle : Triangle
    {
        public IsoscelesTriangle(double leg, double baseS)
            : base("Равнобедренный треугольник", leg, leg, baseS)
        {
            if (baseS >= 2 * leg)
                throw new ArgumentException("Такой равнобедренный треугольник не существует.");
        
            Console.WriteLine("Равнобедренный треугольник успешно создан.");
        }

        ~IsoscelesTriangle() => Console.WriteLine("Деструктор Равнобедренного треугольника вызван.");

        public override double Area()
        {
            double height = Math.Sqrt(sideA * sideA - (sideC / 2) * (sideC / 2));
            return sideC * height / 2;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"\n=== {name} ===");
            Console.WriteLine($"Боковая сторона: {sideA:F2}");
            Console.WriteLine($"Основание: {sideC:F2}");
            Console.WriteLine($"Периметр: {Perimeter():F2}");
            Console.WriteLine($"Площадь: {Area():F2}");
        }
    }

    class RightTriangle : Triangle
    {
        public RightTriangle(double legA, double legB)
            : base("Прямоугольный треугольник", legA, legB, Math.Sqrt(legA * legA + legB * legB))
        {
            Console.WriteLine("Прямоугольный треугольник успешно создан.");
        }

        ~RightTriangle() => Console.WriteLine("Деструктор Прямоугольного треугольника вызван.");

        public override double Area() => sideA * sideB / 2;

        public override void ShowInfo()
        {
            Console.WriteLine($"\n{name}");
            Console.WriteLine($"Катет A: {sideA:F2}");
            Console.WriteLine($"Катет B: {sideB:F2}");
            Console.WriteLine($"Гипотенуза: {sideC:F2}");
            Console.WriteLine($"Периметр: {Perimeter():F2}");
            Console.WriteLine($"Площадь: {Area():F2}");
        }
    }

    class EquilateralTriangle : Triangle
    {
        public EquilateralTriangle(double side)
            : base("Равносторонний треугольник", side, side, side)
        {
            Console.WriteLine("Равносторонний треугольник успешно создан.");
        }

        ~EquilateralTriangle() => Console.WriteLine("Деструктор Равностороннего треугольника вызван.");

        public override double Area() => (Math.Sqrt(3) / 4) * sideA * sideA;

        public override void ShowInfo()
        {
            Console.WriteLine($"\n=== {name} ===");
            Console.WriteLine($"Сторона: {sideA:F2}");
            Console.WriteLine($"Периметр: {Perimeter():F2}");
            Console.WriteLine($"Площадь: {Area():F2}");
        }
    }

    class Program
    {
        static double ReadPositiveDouble(string prompt)
        {
            double value;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.Write("Введите корректное положительное число: ");
            }
            return value;
        }

        static void Main()
        {
            Console.WriteLine("Полиморфизм: Геометрические фигуры (Вариант 7)\n");

            double sq = ReadPositiveDouble("Квадрат - Сторона: ");
            double leg = ReadPositiveDouble("\nРавнобедренный треугольник - Боковая сторона: ");
            double bs = ReadPositiveDouble("Основание: ");
            
            while (bs >= 2 * leg)
                {
                    Console.WriteLine("Основание должно быть меньше суммы двух боковых сторон.");
                    bs = ReadPositiveDouble("Введите основание ещё раз: ");
                }
            
            double ca = ReadPositiveDouble("\nПрямоугольный треугольник - Катет A: ");
            double cb = ReadPositiveDouble("Катет B: ");
            double eq = ReadPositiveDouble("\nРавносторонний треугольник - Сторона: ");

            Console.WriteLine("\nСоздание объектов\n");

            Figure[] figures =
            {
                new Square(sq),
                new IsoscelesTriangle(leg, bs),
                new RightTriangle(ca, cb),
                new EquilateralTriangle(eq)
            };

            Console.WriteLine("\nРезультаты");
            foreach (var fig in figures)
                fig.ShowInfo();

            Console.WriteLine("\nПрограмма завершена.");
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
