using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace Biblioteka
{

	class Program
	{
		//TODO izmena (takodje uz izmenu, placanje clanarine) i uklanjanje korisnika za domaci
		//isto za knjige
		//Kod ispisa, treba napraviti da kada se prikazuju knjige prikazu kod kojih su sve clanova
		//kopije. Kod clanova, treba da ispisemo knjige koje su kod njih
		//Treba implemetirati zapisivanje u fajlove. Posto korisnici imaju list knjiga, kao racun u pos-u
		//otrpilike, treba da zapisemo sifru svake knjige koja je kod korisnika

		static List<Clan> Clanovi = new List<Clan>();
		static List<Knjiga> Knjige = new List<Knjiga>();

		static void Main(string[] args)
		{
			CitanjeFajlova();
			string unos;
			do
			{
				Meni();
				unos = Console.ReadKey().KeyChar.ToString();
				Console.WriteLine();
				switch (unos)
				{
					case "0":
						Izdaj();
						break;
					case "1":
						DodajKnjigu();
						break;
					case "2":
						IzmenaKnjiga();
						break;
					case "3":
						IzbrisiKnjigu();
						break;
					case "4":
						IspisKnjiga();
						break;
					case "5":
						UnosClana();
						break;
					case "6":
						IzmenaClana();
						break;
					case "7":
						UkloniClana();
						break;
					case "8":
						IspisClanova();
						break;
					case "9":
						Console.WriteLine("Bye :)");
						SnimanjeFajlova();
						break;
					default:
						Console.WriteLine("Unos nije razumljiv :(");
						break;
				}
			} while (unos != "9");
			Console.ReadKey();
		}

		static void IzbrisiKnjigu()
		{
			while (true)
			{
				Console.Write("Unesite sifru knjige koju zelite da uklonite ");
				string br = Console.ReadLine();

				foreach (Knjiga k in Knjige)
				{
					if (br == k.Sifra)
					{
						Knjige.Remove(k);
						Console.WriteLine("Brisanje uspesno izvrseno");
						return;
					}
				}

				Console.WriteLine("Sifra nije pronadjena pokusajte ponovo");
			}
		}

		static void IzmenaKnjiga()
		{
			while (true)
			{
				Console.Write("Unesite sifru knjige koju zelite da promenite ");
				string sifra = Console.ReadLine();

				foreach (Knjiga k in Knjige)
				{
					if (sifra == k.Sifra)
					{
						IzmeniKnjigu(k);
						return;
					}
				}

				Console.WriteLine("Sifra nije pronadjena pokusajte ponovo");
			}
		}

		static void IzmeniKnjigu(Knjiga k)
		{
			Console.Clear();
			char unos;

			do
			{
				IzmenaKnjigaMeni();
				unos = Console.ReadKey().KeyChar;
				Console.Write(Environment.NewLine);

				switch (unos)
				{
					case '1':
						PromeniNazivKnjige(k);
						break;
					case '2':
						PromeniSifruKnjige(k);
						break;
					case '3':
						PromeniZanrKnjige(k);
						break;
					case '4':
						Console.Clear();
						break;
				}

			} while (unos != '4');
		}


		static void PromeniZanrKnjige(Knjiga k)
		{
			string unos;
			do
			{
				Console.WriteLine("Unesite zanr :");
				unos = Console.ReadLine();
			} while (string.IsNullOrWhiteSpace(unos));

			k.Zanr = unos;
		}
		static void PromeniSifruKnjige(Knjiga k)
		{
			string sifra;
			while (true)
			{
				Console.Write("Unesite sifru: ");
				sifra = Console.ReadLine();
				bool Problem = false;

				if (string.IsNullOrEmpty(sifra))
				{
					Problem = true;
					Console.WriteLine("Los unos :(");
				}

				foreach (Knjiga kg in Knjige)
				{
					if (kg.Sifra == sifra)
					{
						Problem = true;
						Console.WriteLine("Sifra je duplikat!");
					}
				}
				if (!Problem)
				{
					break;
				}
			}

			k.Sifra = sifra;
		}

		static void PromeniNazivKnjige(Knjiga k)
		{
			string unos;
			do
			{
				Console.WriteLine("Unesite naziv :");
				unos = Console.ReadLine();
			} while (string.IsNullOrWhiteSpace(unos));

			k.Naziv = unos;

		}

		static void IzmenaKnjigaMeni()
		{
			Console.WriteLine("1 --- Promeni naziv");
			Console.WriteLine("2 --- Promeni sifru");
			Console.WriteLine("3 --- Promeni zanr");
			Console.WriteLine("4 --- Povratak na meni");
			Console.WriteLine("---------------------");
			Console.Write("Unesite izbor: ");
		}

		static void IzmenaClana()
		{
			while (true)
			{
				Console.Write("Unesite clanski broj korisnika kojeg zelite da promenite ");
				string br = Console.ReadLine();

				foreach (Clan c in Clanovi)
				{
					if (br == c.ClanskiBroj)
					{
						IzmeniClana(c);
						return;
					}
				}

				Console.WriteLine("Clanski broj nije pronadjen pokusajte ponovo");
			}
		}

		static void IzmeniClana(Clan c)
		{
			Console.Clear();
			char unos;

			do
			{
				IzmenaClanovaMeni();
				unos = Console.ReadKey().KeyChar;
				Console.Write(Environment.NewLine);

				switch (unos)
				{
					case '1':
						PromeniImeClana(c);
						break;
					case '2':
						PromeniSifruClana(c);
						break;
					case '3':
						ObnoviClanarinu(c);
						break;
					case '4':
						Console.Clear();
						break;
				}

			} while (unos != '4');

		}

		static void ObnoviClanarinu(Clan c)
		{
			Console.Write("Pritisnite d za obnovu clanarine :");
			char unos = Console.ReadKey().KeyChar;

			if (unos == 'd')
				c.ZadnjaClanarina = DateTime.Now;
		}

		static void PromeniSifruClana(Clan c)
		{
			string clanska;
			while (true)
			{
				Console.Write("Unesite broj clanske: ");
				clanska = Console.ReadLine();
				bool Problem = false;

				if (string.IsNullOrEmpty(clanska))
				{
					Problem = true;
					Console.WriteLine("Los unos :(");
				}

				foreach (Clan cl in Clanovi)
				{
					if (cl.ClanskiBroj == clanska)
					{
						Problem = true;
						Console.WriteLine("Broj je duplikat!");
					}
				}
				if (!Problem)
				{
					break;
				}
			}

			c.ClanskiBroj = clanska;
		}

		static void PromeniImeClana(Clan c)
		{
			string ImeIPrezime;
			while (true)
			{
				Console.Write("Unesite ime i prezime: ");
				ImeIPrezime = Console.ReadLine();
				if (ImeIPrezime.Split(' ').Length == 2)
				{
					break;
				}
				Console.WriteLine("Los unos :(");
			}
			string[] iIp = ImeIPrezime.Split(' ');
			c.Ime = iIp[0];
			c.Prezime = iIp[1];
		}

		static void IzmenaClanovaMeni()
		{
			Console.WriteLine("1 --- Promeni naziv");
			Console.WriteLine("2 --- Promeni sifru");
			Console.WriteLine("3 --- Obnovi clanarinu");
			Console.WriteLine("4 --- Povratak na meni");
			Console.WriteLine("---------------------");
			Console.Write("Unesite izbor: ");
		}

		static void UkloniClana()
		{
			while (true)
			{
				Console.Write("Unesite clanski broj korisnika kojeg zelite da uklonite ");
				string br = Console.ReadLine();

				foreach (Clan c in Clanovi)
				{
					if (br == c.ClanskiBroj)
					{
						Clanovi.Remove(c);
						Console.WriteLine("Brisanje uspesno izvrseno");
						return;
					}
				}

				Console.WriteLine("Clanski broj nije pronadjen pokusajte ponovo");
			}

		}

		static void Izdaj()
		{
			Console.Write("Unesite sifru clana: ");
			string sifra = Console.ReadLine();

			Clan c = null;
			foreach (Clan cl in Clanovi)
			{
				if (cl.ClanskiBroj == sifra)
				{
					c = cl;
					break;
				}
			}

			if (c == null)
			{
				Console.WriteLine("Pogresan broj");
				return;
			}

			TimeSpan kas = c.ProveriClanarinu();
			if (kas.Days != 0)
			{
				Console.WriteLine($"Clanarina kasni {kas.Days}");
				return;
			}

			Console.Write("Unesite sifru knjige: ");
			sifra = Console.ReadLine();

			Knjiga k = null;
			foreach (Knjiga kg in Knjige)
			{
				if (kg.Sifra == sifra)
				{
					k = kg;
					break;
				}
			}

			if (k == null)
			{
				Console.WriteLine("Pogresan broj knjige");
				return;
			}

			if (k.BrojPrimeraka == 0)
			{
				Console.WriteLine("Nema slobodan primerak :(");
			}

			c.Knjige.Add(k);
			k.BrojPrimeraka--;

		}

		static void IspisKnjiga()
		{
			foreach (Knjiga k in Knjige)
			{
				Console.WriteLine("==========================================================");
				Console.WriteLine($"{k.Sifra} - {k.Naziv} {k.Zanr} {k.BrojPrimeraka}");

				foreach (Clan c in Clanovi)
				{
					if (c.Knjige.Contains(k))
						Console.Write($"|{c.Ime} {c.Prezime} --- {c.ClanskiBroj}| ");
				}

				Console.Write(Environment.NewLine);
				Console.WriteLine("==========================================================");

			}
		}

		static void DodajKnjigu()
		{
			Knjiga k = new Knjiga();
			Console.Write("Unesite sifra: ");
			k.Sifra = Console.ReadLine();
			Console.Write("Unesite naziv: ");
			k.Naziv = Console.ReadLine();
			Console.Write("Unesite zanr: ");
			k.Zanr = Console.ReadLine();
			Console.Write("Unesite broj primeraka: ");
			k.BrojPrimeraka = int.Parse(Console.ReadLine());
			Knjige.Add(k);
		}

		static void IspisClanova()
		{
			foreach (Clan c in Clanovi)
			{
				Console.WriteLine("==========================================================");
				Console.Write($"{c.ClanskiBroj} -- {c.Ime} {c.Prezime} ");
				DateTime sada = DateTime.Now;
				TimeSpan kasnjenje = c.ProveriClanarinu();
				if (kasnjenje.Days == 0)
				{
					Console.WriteLine("-- Clanarina OK");
				}
				else
				{
					Console.WriteLine($"Kasni {kasnjenje.Days} dana");
				}
				foreach (Knjiga k in c.Knjige)
				{
					Console.Write($"|{k.Naziv} -- {k.Sifra}| ");
				}
				Console.Write(Environment.NewLine);
				Console.WriteLine("==========================================================");
			}
		}

		static void UnosClana()
		{
			string ImeIPrezime;
			while (true)
			{
				Console.Write("Unesite ime i prezime: ");
				ImeIPrezime = Console.ReadLine();
				if (ImeIPrezime.Split(' ').Length == 2)
				{
					break;
				}
				Console.WriteLine("Los unos :(");
			}

			string clanska;
			while (true)
			{
				Console.Write("Unesite broj clanske: ");
				clanska = Console.ReadLine();
				bool Problem = false;

				if (string.IsNullOrEmpty(clanska))
				{
					Problem = true;
					Console.WriteLine("Los unos :(");
				}

				foreach (Clan c in Clanovi)
				{
					if (c.ClanskiBroj == clanska)
					{
						Problem = true;
						Console.WriteLine("Broj je duplikat!");
					}
				}
				if (!Problem)
				{
					break;
				}
			}

			string[] iIp = ImeIPrezime.Split(' ');

			Clanovi.Add(new Clan(iIp[0], iIp[1], clanska));
		}

		static void Meni()
		{
			Console.WriteLine("0 - Izdaj knjigu");
			Console.WriteLine("1 - Dodaj knjigu");
			Console.WriteLine("2 - Izmeni knjigu");
			Console.WriteLine("3 - Ukloni knjigu");
			Console.WriteLine("4 - Ispis knjiga");
			Console.WriteLine("5 - Unos clana");
			Console.WriteLine("6 - Izmena clana");
			Console.WriteLine("7 - Uklanjanje clana");
			Console.WriteLine("8 - Ispis clanova");
			Console.WriteLine("9 - Izlaz");
			Console.WriteLine("---------------------");
			Console.Write("Unesite izbor: ");
		}
		static void SnimanjeFajlova()
		{
			if (File.Exists("clanovi.txt"))
				File.Delete("clanovi.txt");

			if (File.Exists("knjige.txt"))
				File.Delete("knjige.txt");

			foreach (Knjiga k in Knjige)
			{
				File.AppendAllText("knjige.txt", $"{k.Sifra};{k.Naziv};{k.Zanr};{k.BrojPrimeraka};" + Environment.NewLine);
			}

			foreach (Clan c in Clanovi)
			{
				File.AppendAllText("clanovi.txt", $"{c.Ime};{c.Prezime};{c.ClanskiBroj};{c.ZadnjaClanarina};");
				foreach (Knjiga k in c.Knjige)
				{
					File.AppendAllText("clanovi.txt", $"{k.Sifra};");
				}
				File.AppendAllText("clanovi.txt", Environment.NewLine);
			}

		}

		static void CitanjeFajlova()
		{
			if (File.Exists("knjige.txt"))
			{
				foreach (string linija in File.ReadAllLines("knjige.txt"))
				{
					string[] niz = linija.Split(';');
					Knjige.Add(new Knjiga(niz[0], niz[1], niz[2], int.Parse(niz[3])));
				}
			}

			if (File.Exists("clanovi.txt"))
			{
				foreach (string linija in File.ReadAllLines("clanovi.txt"))
				{
					string[] niz = linija.Split(';');
					Clan c = new Clan(niz[0], niz[1], niz[2], niz[3]);
					for (int indeks = 4; indeks < niz.Length; indeks++)
					{
						foreach (Knjiga k in Knjige)
						{
							if (k.Sifra == niz[indeks])
							{
								c.Knjige.Add(k);
								break;
							}
						}
					}
					Clanovi.Add(c);
				}
			}
		}


	}
}
