namespace IvidataLink_Covariance_Contravariance
{
    /// <summary>
    /// Signifie que vous ne pouvez utiliser que le type spécifié à l'origine. un paramètre de type générique invariant n'est donc ni covariant ni contravariant.
    /// Vous ne pouvez pas affecter une instance de IEnumerable<Base> à une variable de type IEnumerable<Derived> ou vice versa.
    /// </summary>
    public class Invariance
    { }
}