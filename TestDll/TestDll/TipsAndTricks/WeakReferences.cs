using System;
using System.Collections;

namespace Ividatalink.TipsAndTricks
{
    public class WeakReferences
    {

        public class Personne
        {
            private string nom;
            public string Nom
            {
                get { return nom; }
                set { nom = (value != null) ? value.Trim() : string.Empty; }
            }

            public Personne(string nom)
            { this.nom = nom.Trim(); }
        }


        public static ArrayList Employés;
        private static string line = new string('-', 60);

        private static void Return()
        {
            Console.WriteLine("<return> pour continuer...");
            Console.ReadLine();
        }

        public static void ListeEmployés()
        {
            Console.WriteLine(line);
            foreach (Personne p in Employés)
                Console.WriteLine(p.Nom);
            Console.WriteLine(line);
            Return();
        }

        public void Run()
        {
            Employés = new ArrayList();
            Employés.Add(new Personne("Olivier"));
            Employés.Add(new Personne("Barbara"));
            Employés.Add(new Personne("Jacky"));
            Employés.Add(new Personne("Valérie"));

            Console.WriteLine("Liste originale");
            ListeEmployés();

            Personne p = (Personne)Employés[0]; // pointe "olivier"
            Employés.RemoveAt(0); // suppresson de "olivier" dans la liste
            Console.WriteLine("p pointe : " + p.Nom);
            Console.WriteLine("L'élément 0 de la liste a été supprimé");
            Console.WriteLine();
            ListeEmployés();
            Console.WriteLine("Mais l'objet pointé par p existe toujours : " + p.Nom);
            Return();

            WeakReference wr = new WeakReference(Employés[0]); // pointe "barbara"
            Console.WriteLine("wr est une référence faible sur: " + ((Personne)wr.Target).Nom);
            Return();

            Console.WriteLine("La cible de wr est vivante ? : " + wr.IsAlive.ToString());
            Return();
            Employés.RemoveAt(0); // suppression de "barbara" 
            Console.WriteLine("L'élément 0 ('barbara') a été supprimé. La liste devient :");
            ListeEmployés();

            Console.WriteLine("La cible de wr est vivante ? : " + wr.IsAlive.ToString());
            Console.WriteLine("On peut réacquérir la cible : " + ((Personne)wr.Target).Nom);
            Return();

            Console.WriteLine("Mais si le GC passe par là...");
            GC.Collect(GC.MaxGeneration);
            Console.WriteLine("La cible de wr est vivante ? : " + wr.IsAlive.ToString()); // false !
            Console.WriteLine("La référence faible n'a pas interdit sa destruction totale.");
            Return();
        }
    }
}