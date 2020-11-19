using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuVie
{
    class Program
    {
        const int Mort = 0;
        const int Vivant1 = 1;      // pour la population de type "#"
        const int ANaitre1 = 2;     // pour la population de type "#"
        const int AMourir = 3;
        const int Vivant2 = 4;      // pour la population de type "@"
        const int ANaitre2 = 5;     // pour la population de type "@"
        const int ChoixMenuA = 1;
        const int ChoixMenuB = 2;
        const int ChoixMenuC = 3;
        const int ChoixMenuD = 4;
        const int Bonus2 = 2;


        static int [,] GenererGrilleDeJeu(int ChoixMenu)
        {
            int nombreLigne;
            int nombreColonne;
            int nombreCellulesVivantes;
            do
            {
                Console.WriteLine("Entrez le nombre de lignes ");
                nombreLigne = Convert.ToInt32(Console.ReadLine());
            } while (nombreLigne < 1);

            do
            {
                Console.WriteLine("Entrez le nombre de colonnes ");
                nombreColonne = Convert.ToInt32(Console.ReadLine());
            } while (nombreColonne < 1);

            nombreCellulesVivantes = TauxRemplissage(nombreLigne, nombreColonne);
         
            int [,] grille = new int [nombreLigne, nombreColonne];

            for (int ligne =0; ligne < grille.GetLength(0); ligne++)        // grille.GetLength(0) = nombreLigne
            {
                for (int colonne = 0; colonne < grille.GetLength(1); colonne++)     // grille.GetLength(1) = nombreColonne
                {
                    grille[ligne, colonne] = Mort;
                }
            }
            RemplissageAleatoireCellules(grille, nombreCellulesVivantes, ChoixMenu);
            return grille;
        }

        static int TauxRemplissage(int nombreLigne, int nombreColonne)
        {
            Random rand = new Random();
            double TauxRemplissage = (double)GenererNombreAleatoire(rand, 10, 90) / 100;
            Console.WriteLine("Le taux de remplissage est de " + TauxRemplissage*100 + " % ");
            double temp = nombreLigne * nombreColonne * TauxRemplissage;
            int nombreCellulesVivantes = Convert.ToInt32(temp);
            return nombreCellulesVivantes;
        }

        static void RemplissageAleatoireCellules(int [,] grille, int nombreCellulesVivantes, int ChoixMenu)
        {
            if (MatriceValide(grille) && nombreCellulesVivantes>0)
            {
                int ligne;
                int colonne;
                Random rand = new Random();

                for (int CompteurCelVivante = 1; CompteurCelVivante <= nombreCellulesVivantes / 2; CompteurCelVivante++)
                {
                    bool test = false;
                    do
                    {
                        ligne = GenererNombreAleatoire(rand, 0, grille.GetLength(0) - 1);
                        colonne = GenererNombreAleatoire(rand, 0, grille.GetLength(1) - 1);
                        if (grille[ligne, colonne] == Mort)
                        {
                            grille[ligne, colonne] = Vivant1;
                            test = true;
                        }
                    } while (!(test));

                }
                int DebutBoucle = nombreCellulesVivantes / 2 + 1;
                if ((nombreCellulesVivantes % 2 == 1) && (ChoixMenu == ChoixMenuC || ChoixMenu == ChoixMenuD))
                {
                    DebutBoucle = DebutBoucle + 1;
                }
                for (int CompteurCelVivante = DebutBoucle; CompteurCelVivante <= nombreCellulesVivantes; CompteurCelVivante++)
                {
                    bool test = false;
                    do
                    {
                        // Random rand = new Random();
                        ligne = GenererNombreAleatoire(rand, 0, grille.GetLength(0) - 1);
                        colonne = GenererNombreAleatoire(rand, 0, grille.GetLength(1) - 1);
                        if (grille[ligne, colonne] == Mort)
                        {
                            if (ChoixMenu == ChoixMenuA || ChoixMenu == ChoixMenuB)
                            {
                                grille[ligne, colonne] = Vivant1; ;
                                test = true;
                            }
                            else
                            {
                                grille[ligne, colonne] = Vivant2;
                                test = true;
                            }
                        }
                    } while (!(test));
                }
            }
        }


        static int TaillePopulation(int [,] grille, int TypePopulation)     // par type de population on entend celle représentée par le symbole "#" (soit 1 dans la matrice) ou #@# (soit 4 dans la matrice)
        {
            int NbCellulesVivantes = 0;
            if (MatriceValide(grille))
            {
                for (int ligne = 0; ligne < grille.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < grille.GetLength(1); colonne++)
                    {
                        if ((TypePopulation == 1) && (grille[ligne, colonne] == Vivant1))
                        {
                            NbCellulesVivantes++;
                        }
                        if ((TypePopulation == 2) && (grille[ligne, colonne] == Vivant2))
                        {
                            NbCellulesVivantes++;
                        }
                    }
                }
            }
            return NbCellulesVivantes;
        }

        static void AfficherGrille (int [,] grille)
        {
            if (MatriceValide(grille))
            {
                for (int ligne = 0; ligne < grille.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < grille.GetLength(1); colonne++)
                    {
                        if (grille[ligne,colonne] == Mort)
                        {
                            Console.Write(". ");
                        }
                        else if (grille[ligne, colonne] == Vivant1)
                        {
                            Console.Write("# ");
                        }
                        else if (grille[ligne, colonne] == ANaitre1)
                        {
                            Console.Write("- ");
                        }
                        else if (grille[ligne,colonne] == AMourir)
                        {
                            Console.Write("* ");
                        }
                        else if (grille[ligne,colonne] == Vivant2)
                        {
                            Console.Write("@ ");
                        }
                        else
                        {
                            Console.Write("~ ");               
                        } 
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        static int GenererNombreAleatoire(Random rand, int min, int max)
        {
            int nombre = 0;
            if ((min >= 0) && max >= min)
            {
                nombre = rand.Next(min, max + 1);
            }
            return nombre;
        }


        static void DeterminationEtatProvisoire(int[,] grille, int[,] matriceProvisoire,int ligne, int colonne)
        {   
            if (MatriceValide(grille) && MatriceValide(matriceProvisoire) && IndexValides(grille, ligne, colonne))
            {
                int compteur = 0;
                int[] Coordonnees = new int[2];
                int nouvelleLigne;
                int nouvelleColonne;
                for (int l = ligne - 1; l <= ligne + 1; l++)
                {
                    for (int c = colonne - 1; c <= colonne + 1; c++)
                    {
                        Coordonnees = AjustementIndexLigneColonne(grille, l, c);    // Ce tableau contient l'index de la ligne et de la colonne de la case à regarder
                        nouvelleLigne = Coordonnees[0];
                        nouvelleColonne = Coordonnees[1];
                        if (matriceProvisoire[nouvelleLigne, nouvelleColonne] == Vivant1)
                        {
                            compteur++;
                        }
                    }
                }

                if (matriceProvisoire[ligne, colonne] == Vivant1)      // la case matriceproviosire[ligne,colonne] est celle dont on étudie l'état futur, il ne faut donc pas que son état fausse le compteur ici
                {
                    compteur -= 1;
                    if ((compteur < 2) || (compteur > 3))                  // compteur < 2 => cas de sous population et compteur >3 => cas de surpopulation 
                    {
                        grille[ligne, colonne] = AMourir;
                    }

                }
                else
                {
                    if (compteur == 3)
                    {
                        grille[ligne, colonne] = ANaitre1;
                    }
                }
            }
        }


        static int [,] MatriceProvisoire (int [,] grille, int [,] matrice)
        {
            if (MatriceValide(grille) && MatriceValide(matrice))
            {
                for (int ligne =0; ligne < matrice.GetLength(0); ligne++)
                {
                    for (int colonne =0; colonne < matrice.GetLength(1); colonne++)
                    {
                        matrice[ligne, colonne] = grille[ligne, colonne];
                    }
                }
            }
            return matrice;
        }

        static int [] AjustementIndexLigneColonne (int [,] grille, int l, int c)       
        {
            int[] Coordonnées = new int[2] { l, c };
            if (MatriceValide(grille))
            {
                for (int index = 0; index <= 1; index++)                // au premier tour de boucle l'index représente la ligne 1 et au deuxième il représente la colonne c
                {
                    if (Coordonnées[index] < 0)
                    {
                        Coordonnées[index] += grille.GetLength(index);
                    }
                    else if (Coordonnées[index] >= grille.GetLength(index))
                    {
                        Coordonnées[index] -= grille.GetLength(index);
                    }
                }
            }
            return Coordonnées;
        }

        static bool MatriceValide (int [,] matrice)
        {
            bool reponse = false;
            if (matrice != null && matrice.Length > 0)  
            {
                reponse = true; 
            }
            else
            {
                Console.WriteLine("Plateau de Jeu non valide (null ou vide)");
            }
            return reponse;
        }

        static bool IndexValides (int [,] matrice, int ligne, int colonne)
        {
            bool reponse = false;
            if (MatriceValide(matrice))
            {
                if ((ligne >= 0) && (ligne < matrice.GetLength(0)) && (colonne >= 0) && (colonne < matrice.GetLength(1)))
                {
                    reponse = true;
                }
                else
                {
                    Console.WriteLine("L'index de la ligne ou de la colonne n'est pas compatible avec les dimensions de la matrice ");
                }
            }
            return reponse;
        }

        static void EtatGenerationSuivante(int [,] grille)
        {
            if (MatriceValide(grille))
            {
                for (int ligne = 0; ligne < grille.GetLength(0); ligne++)
                {
                    for (int colonne =0; colonne < grille.GetLength(1); colonne++)
                    {
                        if (grille[ligne, colonne] == ANaitre1)
                        {
                            grille[ligne, colonne] = Vivant1;
                        }
                        if (grille[ligne,colonne] == ANaitre2)
                        {
                            grille[ligne, colonne] = Vivant2;
                        }
                        if (grille[ligne, colonne] == AMourir)
                        {
                            grille[ligne, colonne] = Mort;
                        }
                    }
                }
            }
        }


        static void DeroulementJeuDeLaVie()
        {
            // début du jeu avec la création du plateau de jeu, choix des options dans le menu, et initialisations
            int NumeroGeneration = 0;
            int TypePopulation = 1;                                          // Par défaut le type de population est réduit au cas d'un seule sous population '#'
            int taille2 = 0;                                                // Dans le cas où l'utilisateur choisirait les options c ou d avec 2 sous populations
            int choixMenu = ChoixOptionMenu();
            int Bonus = -1;
            if (choixMenu == ChoixMenuC || choixMenu == ChoixMenuD)
            {
                TypePopulation = 2;
                Bonus = ChoixBonus();
            }
            int[,] plateauJeu = GenererGrilleDeJeu(choixMenu);
            int[,] matriceProvisoire = new int[plateauJeu.GetLength(0), plateauJeu.GetLength(1)];
            AfficherGrille(plateauJeu);
            Console.WriteLine(" Génération numéro " + NumeroGeneration);
            int taille = TaillePopulation(plateauJeu, 1);
            if (TypePopulation == 1)
            {
                Console.WriteLine(" La taille de la population est de " + taille + " cellules");
            }
            else
            {
                Console.WriteLine(" La taille de la population 1 (#) est de " + taille + " cellules");
                taille2 = TaillePopulation(plateauJeu, TypePopulation);
                Console.WriteLine(" La taille de la population 2 (@) est de " + taille2 + " cellules");
            }


            bool continuer;

            do
            {
                continuer = false;
                // On détermine ici l'état provisoire de chaque case du plateau de jeu
                matriceProvisoire = MatriceProvisoire(plateauJeu, matriceProvisoire);
                for (int ligne = 0; ligne < plateauJeu.GetLength(0); ligne++)
                {
                    for (int colonne = 0; colonne < plateauJeu.GetLength(1); colonne++)
                    {
                        if (TypePopulation == 1)
                        {
                            DeterminationEtatProvisoire(plateauJeu, matriceProvisoire, ligne, colonne);
                        }
                        else
                        {
                            DeterminationEtatsProvisoires2Populations(plateauJeu, matriceProvisoire, ligne, colonne, Bonus);
                        }
                    }
                }               

                // On affiche ici les états provisoires si demandé
                if (choixMenu == ChoixMenuB || choixMenu == ChoixMenuD)
                {
                    Console.WriteLine(" Visualisation des états futurs ");
                    AfficherGrille(plateauJeu);
                    
                }

                // On affiche maintenant les états de la génération suivante
                EtatGenerationSuivante(plateauJeu);
                AfficherGrille(plateauJeu);
                NumeroGeneration++;
                Console.WriteLine(" Génération numéro " + NumeroGeneration);
                taille = TaillePopulation(plateauJeu, 1);
                if (TypePopulation == 1)
                {
                    Console.WriteLine(" La taille de la population est de " + taille + " cellules");
                }
                else
                {
                    Console.WriteLine(" La taille de la population 1 (#) est de " + taille + " cellules");
                    taille2 = TaillePopulation(plateauJeu, TypePopulation);
                    Console.WriteLine(" La taille de la population 2 (@) est de " + taille2 + " cellules");
                }
                

                // pour passer à la génération suivante
                Console.WriteLine(" Appuyez sur la touche 'a' (minusculue) pour passer à la génération suivante ");
                string reponse = Console.ReadLine();
                if (reponse == "a")
                {
                    continuer = true;
                }
            } while (continuer);
        }

        static int ChoixOptionMenu()
        {
            int choix = 0;
            Console.WriteLine(" Choisissez une option : ");
            Console.WriteLine(" (a) : Jeu DLV classique sans visualisation intermédiaire des états futurs ");
            Console.WriteLine(" (b) : Jeu DLV classique avec visualisation des états futurs (à naître et à mourir ) ");
            Console.WriteLine(" (c) : Jeu DLV variante(2 populations) sans visualisation des états futurs ");
            Console.WriteLine(" (d) : Jeu DLV variante (2 populations) avec visualisation des états futurs ");
            do
            {
                Console.WriteLine(" Entrez la lettre correspondante (en minuscule) ");
                string chaine = Console.ReadLine();
                if (chaine == "a")
                {
                    choix = ChoixMenuA;
                }
                if (chaine == "b")
                {
                    choix = ChoixMenuB;
                }
                if (chaine  == "c")
                {
                    choix = ChoixMenuC;
                }
                if (chaine == "d")
                {
                    choix = ChoixMenuD;
                }
            } while (choix == 0);
            Console.WriteLine();
            return choix ;
        }

        static int ChoixBonus()
        {
            int reponse = -1;
            do
            {
                Console.WriteLine(" Choisissez un bonus :");
                Console.WriteLine(" 1 : Pas de bonus");
                Console.WriteLine(" 2 : Variante de la règle R4b");
                Console.WriteLine(" Entrez le chiffre correspondant à votre choix (1 ou 2) ");
                reponse = Convert.ToInt32(Console.ReadLine());
            } while (reponse == -1);
            Console.WriteLine();
            return reponse;
        }
        

        // Fonctions utilisées seulement pour la version avec deux populations de cellules

        static void EtatProvisoireCelulleVivante2Populations (int [,] grille, int[,] grilleProvisoire, int ligne, int colonne)
        {
            if (MatriceValide(grille) && MatriceValide(grilleProvisoire) && IndexValides(grille, ligne , colonne))
            {
                int compteur = -1;
                int[] Coordonnees = new int[2];
                int nouvelleLigne;
                int nouvelleColonne;
                for (int l = ligne - 1; l <= ligne + 1; l++)
                {
                    for (int c = colonne - 1; c <= colonne + 1; c++)
                    {
                        Coordonnees = AjustementIndexLigneColonne(grille, l, c);
                        nouvelleLigne = Coordonnees[0];
                        nouvelleColonne = Coordonnees[1];
                        if (grilleProvisoire[ligne, colonne] == Vivant1)
                        {
                            if (grilleProvisoire[nouvelleLigne, nouvelleColonne] == Vivant1)
                            {
                                compteur++;
                            }
                        }
                        else if (grilleProvisoire[nouvelleLigne, nouvelleColonne] == Vivant2)
                        {
                            compteur++;
                        }
                    }
                }

                if ((compteur < 2) || (compteur > 3))
                {
                    grille[ligne, colonne] = AMourir;
                }
            }
        }
        

        static void EtatProvisoireCelluleMorte2Populations(int [,] grille, int [,] grilleProvisoire, int ligne, int colonne, int Bonus) // Dans le cas d'un cellule morte
        {
            if (MatriceValide(grille) && MatriceValide(grilleProvisoire) && IndexValides(grille, ligne, colonne))
            {
                int compteur1 = 0;
                int compteur2 = 0;
                int[] Coordonnees = new int[2];
                int nouvelleLigne;
                int nouvelleColonne;
                for (int l = ligne - 1; l <= ligne + 1; l++)
                {
                    for (int c = colonne - 1; c <= colonne + 1; c++)
                    {
                        Coordonnees = AjustementIndexLigneColonne(grille, l, c);
                        nouvelleLigne = Coordonnees[0];
                        nouvelleColonne = Coordonnees[1];
                        if (grilleProvisoire[nouvelleLigne, nouvelleColonne] == Vivant1)
                        {
                            compteur1++;
                        }
                        else if (grilleProvisoire[nouvelleLigne, nouvelleColonne] == Vivant2)
                        {
                            compteur2++;
                        }
                    }
                }
                // Règle R4b
                if ((compteur1 == 3) && (compteur2 == 3))
                {
                    RegleR4b(grille, grilleProvisoire, ligne, colonne, Coordonnees, Bonus);
                }
                // Règle R3b
                else if (compteur1 == 3)
                {
                    grille[ligne, colonne] = ANaitre1;
                }
                else if (compteur2 == 3)
                {
                    grille[ligne, colonne] = ANaitre2;
                }
            }
        }


        static void RegleR4b(int [,] grille, int [,] grilleProvisoire, int ligne, int colonne, int [] Coordonnees, int Bonus)
        {
            if (MatriceValide(grille) && MatriceValide(grilleProvisoire) && IndexValides(grille, ligne, colonne) && Coordonnees != null && Coordonnees.Length >0)
            {
                int compteur1 = 0;
                int compteur2 = 0;
                int nouvelleLigne = 0;
                int nouvelleColonne = 0;
                for (int l = ligne - 2; l <= ligne + 2; l++)
                {
                    for (int c = colonne - 2; c <= colonne + 2; c++)
                    {
                        Coordonnees = AjustementIndexLigneColonne(grille, l, c);
                        nouvelleLigne = Coordonnees[0];
                        nouvelleColonne = Coordonnees[1];
                        if (grilleProvisoire[nouvelleLigne, nouvelleColonne] == Vivant1)
                        {
                            compteur1++;
                        }
                        else if (grilleProvisoire[nouvelleLigne, nouvelleColonne] == Vivant2)
                        {
                            compteur2++;
                        }
                    }
                }
                if (compteur1 > compteur2)
                {
                    grille[ligne, colonne] = ANaitre1;
                }
                else if (compteur2 > compteur1)
                {
                    grille[ligne, colonne] = ANaitre2;
                }
                else
                {
                    if (Bonus == Bonus2)
                    {
                        ExecutionBonus2(grille, ligne, colonne);
                    }
                    else
                    {
                        int taillePop1 = TaillePopulation(grille, 1);
                        int taillePop2 = TaillePopulation(grille, 2);
                        if (taillePop1 > taillePop2)
                        {
                            grille[ligne, colonne] = ANaitre1;
                        }
                        else if (taillePop2 > taillePop1)
                        {
                            grille[ligne, colonne] = ANaitre2;
                        }
                        else
                        {
                            grille[ligne, colonne] = Mort;
                        }
                    }
                }
            }
        }

        static void DeterminationEtatsProvisoires2Populations(int [,] grille, int [,] matriceProvisoire, int ligne, int colonne, int bonus)
        {
            if (MatriceValide(grille) && MatriceValide(matriceProvisoire) && IndexValides(grille, ligne, colonne))
            {
                if (matriceProvisoire[ligne, colonne] == Mort)
                {
                    EtatProvisoireCelluleMorte2Populations(grille, matriceProvisoire, ligne, colonne, bonus);
                }
                else
                {
                    EtatProvisoireCelulleVivante2Populations(grille, matriceProvisoire, ligne, colonne);
                }
            }
        }

        static void ExecutionBonus2(int [,] grille, int ligne, int colonne)
        {
            if (MatriceValide(grille) && IndexValides(grille, ligne, colonne))
            {
                Random rand = new Random();
                int nbrAleatoire = GenererNombreAleatoire(rand, 1, 2);
                if (nbrAleatoire == 1)
                {
                    grille[ligne, colonne] = ANaitre1;
                }
                else
                {
                    grille[ligne, colonne] = ANaitre2;
                }
            }
        }


        static void Main(string[] args)
        {
            DeroulementJeuDeLaVie();
            Console.ReadKey();
        }
    }
}
