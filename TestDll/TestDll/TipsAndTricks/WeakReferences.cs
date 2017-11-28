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


        public static ArrayList Employ�s;
        private static string line = new string('-', 60);

        private static void Return()
        {
            Console.WriteLine("<return> pour continuer...");
            Console.ReadLine();
        }

        public static void ListeEmploy�s()
        {
            Console.WriteLine(line);
            foreach (Personne p in Employ�s)
                Console.WriteLine(p.Nom);
            Console.WriteLine(line);
            Return();
        }

        public void Run()
        {
            Employ�s = new ArrayList();
            Employ�s.Add(new Personne("Olivier"));
            Employ�s.Add(new Personne("Barbara"));
            Employ�s.Add(new Personne("Jacky"));
            Employ�s.Add(new Personne("Val�rie"));

            Console.WriteLine("Liste originale");
            ListeEmploy�s();

            Personne p = (Personne)Employ�s[0]; // pointe "olivier"
            Employ�s.RemoveAt(0); // suppresson de "olivier" dans la liste
            Console.WriteLine("p pointe : " + p.Nom);
            Console.WriteLine("L'�l�ment 0 de la liste a �t� supprim�");
            Console.WriteLine();
            ListeEmploy�s();
            Console.WriteLine("Mais l'objet point� par p existe toujours : " + p.Nom);
            Return();

            WeakReference wr = new WeakReference(Employ�s[0]); // pointe "barbara"
            Console.WriteLine("wr est une r�f�rence faible sur: " + ((Personne)wr.Target).Nom);
            Return();

            Console.WriteLine("La cible de wr est vivante ? : " + wr.IsAlive.ToString());
            Return();
            Employ�s.RemoveAt(0); // suppression de "barbara" 
            Console.WriteLine("L'�l�ment 0 ('barbara') a �t� supprim�. La liste devient :");
            ListeEmploy�s();

            Console.WriteLine("La cible de wr est vivante ? : " + wr.IsAlive.ToString());
            Console.WriteLine("On peut r�acqu�rir la cible : " + ((Personne)wr.Target).Nom);
            Return();

            Console.WriteLine("Mais si le GC passe par l�...");
            GC.Collect(GC.MaxGeneration);
            Console.WriteLine("La cible de wr est vivante ? : " + wr.IsAlive.ToString()); // false !
            Console.WriteLine("La r�f�rence faible n'a pas interdit sa destruction totale.");
            Return();
        }
    }
}