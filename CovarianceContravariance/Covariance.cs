using System.Collections.Generic;
using System.Linq;

namespace IvidataLink_Covariance_Contravariance
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Vous permet d'utiliser un type plus dérivé que celui spécifié à l'origine.
    /// Vous pouvez affecter une instance de IEnumerable<Derived> à une variable de type IEnumerable<Base>.
    /// </remarks>
    public class Covariance
    {
        public Covariance()
        {
            // Les paramètres de type Covariant vous permettent d'effectuer des affectations qui ressemblent 
            // beaucoup au Polymorphisme ordinaire, comme indiqué dans le code suivant.

            List<Derived> d = new List<Derived>();


            IEnumerable<Base> b = d.Cast<Base>().ToList();



            var bb = b.Any();
            b.Count();
            for (int i = 0; i < 10; i++)
            {
                var res = b.ElementAt(i);
            }

        }
    }
}
