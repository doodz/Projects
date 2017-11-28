using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace IvidatalinkShortcut
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     Handler sur le changement d'une propriété
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Méthode appelé lors du changement d'une propriété
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Affecte une valeur pour la propriété property
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected void SetProperty<TProperty>(ref TProperty property, string name, TProperty value)
        {
            property = value;
            OnPropertyChanged(name);
        }

        /// <summary>
        ///     Retourne les valeurs des propriétés dont les noms sont listés par @properties
        /// </summary>
        /// <param name="properties">Noms des propriétés à retourner</param>
        /// <returns>Valeurs des propriétés, sous la forme d'une liste de tuples (nom, valeur)</returns>
        protected List<Tuple<string, object>> GetProperties(IEnumerable<string> properties)
        {
            var r = new List<Tuple<string, object>>();

            var t = GetType();
            var pa = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var pn in properties)
            {
                var pinf = pa.FirstOrDefault(p => 0 == string.Compare(pn, p.Name, true));

                if (pinf != null)
                    r.Add(new Tuple<string, object>(pn, pinf.GetValue(this, null)));
            }

            return r;
        }


        /// <summary>
        ///     Définit les valeurs des propriétés décrites par @properties
        /// </summary>
        /// <param name="properties">Noms et valeurs des propriétés à définir</param>
        protected void SetProperties(IEnumerable<Tuple<string, object>> properties)
        {
            var t = GetType();
            var pa = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var kv in properties)
            {
                var pinf = pa.FirstOrDefault(p => 0 == string.Compare(kv.Item1, p.Name, true));

                if (pinf != null)
                    pinf.SetValue(this, kv.Item2, null);
            }
        }
    }
}