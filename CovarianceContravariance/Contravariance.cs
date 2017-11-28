using System;
using System.Collections.Generic;

namespace IvidataLink_Covariance_Contravariance
{
    /// <summary>
    /// Vous permet d'utiliser un type plus générique (moins dérivé) que celui spécifié à l'origine.
    /// Vous pouvez affecter une instance de IEnumerable<Base> à une variable de type IEnumerable<Derived>.
    /// </summary>
    public class Contravariance
    {

        public Contravariance()
        {
            // L'exemple suivant crée un délégué de type Action <Base> , puis affecte ce délégué à une variable de type Action <Derived>.
            Action<Base> b = (target) => { Console.WriteLine(target.GetType().Name); };
            Action<Derived> d = b;
            d(new Derived());




            // L'expression lambda correspond au délégué auquel elle est assignée, 
            // donc elle définit une méthode qui prend un paramètre de type Base 
            // et qui n'a pas de valeur de retour. 
            // Le délégué résultant peut être affecté à une variable de type Action <Derived> 
            // car le paramètre de type T du délégué Action <T> est contravariant. 
            // Le code est sécurisé car T indique un type de paramètre. 
            // Lorsque le délégué de type Action <Base> est appelé comme s'il s'agissait d'un délégué de type Action <Derived>, 
            // son argument doit être de type Derived. 
            // Cet argument peut toujours être transmis en toute sécurité à la méthode sous-jacente,
            // car le paramètre de la méthode est de type Base.
        }



        abstract class Shape
        {
            public virtual double Area { get { return 0; } }
        }

        class Circle : Shape
        {
            private double r;
            public Circle(double radius) { r = radius; }
            public double Radius { get { return r; } }
            public override double Area { get { return Math.PI * r * r; } }
        }

        class ShapeAreaComparer : System.Collections.Generic.IComparer<Shape>
        {
            int IComparer<Shape>.Compare(Shape a, Shape b)
            {
                if (a == null) return b == null ? 0 : -1;
                return b == null ? 1 : a.Area.CompareTo(b.Area);
            }
        }


        private void test()
        {


            // Vous pouvez passer ShapeAreaComparer, qui implémente IComparer <Shape>,
            // même si le constructeur de SortedSet <Circle> attend
            // IComparer <Circle>, car le paramètre de type T de IComparer <T> est
            // contravariant.


            SortedSet<Circle> circlesByArea =
                new SortedSet<Circle>(new ShapeAreaComparer())
                    { new Circle(7.2), new Circle(100), null, new Circle(.01) };

            foreach (Circle c in circlesByArea)
            {
                Console.WriteLine(c == null ? "null" : "Circle with area " + c.Area);
            }
        }

    }
}