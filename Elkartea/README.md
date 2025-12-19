Elkartea — TPV aplikazio txiki (WPF, .NET 8)

Laburpena
- WPF aplikazioa (Elkartea proiektua) dotnet 8 erabilita.
- Datuak SQLite fitxategian gordetzen dira (elkartea.db).

Nola exekutatu
1. Proiektu karpetan:
   cd Elkartea
   dotnet build
   dotnet run
2. Edo Visual Studio-n: proiektua ireki eta Start Debug.

Datu-basea
- Konexioa: Elkartea/Data/AppDbContext.cs
  optionsBuilder.UseSqlite("Data Source=elkartea.db")
- Runtime fitxategia: bin/Debug/net8.0-windows/elkartea.db

Migratziotik eta seed-etik
- Migrazioak karpeta: Elkartea/Migrations (eguneratutako snapshot dago).
- Seeder: Elkartea/Data/DatabaseSeeder.cs erabiltzen da datu hasierakoak txertatzeko.
  - Uneko implementazioak db.Database.EnsureCreated() erabiltzen du (ez ditu migrations aplikatzen).
  - Migrations aplikatu nahi badituzu, erabili db.Database.Migrate() edo exekutatu `dotnet ef database update`.

EF Core komando erabilgarriak
- dotnet tool install --global dotnet-ef
- dotnet ef migrations add Izena
- dotnet ef database update

Non dauden ereduak eta ikuspegi nagusiak
- Ereduak: Elkartea/Models (Product, User, Order, Reservation)
- DbContext: Elkartea/Data/AppDbContext.cs
- Seeder: Elkartea/Data/DatabaseSeeder.cs
- Ikuspegiak: Elkartea/Views (StockWindow, UsersWindow, OrdersWindow, UserPayWindow...)
- Hasierako logika: Elkartea/MainWindow.xaml.cs eta LoginWindow

Ordainketa eta tiketa
- Ordaindu botoia UserPayWindow.xaml.cs (Pay_Click) tiketa sortzen du eta inprimatu aukerak ditu.
- Uneko implementazioan ordainketak ez dira datu-basera gordetzen (ordainketak memorian soilik kudeatzen dira).
  Ordainketak gordetzeko eta stock eguneratzeko, aldatu Pay_Click eta erabili AppDbContext ordainketa erregistroak eta produktuen Cantidad murrizteko.

Error handling
- Erroreak zentralizatuta: Elkartea/Utils/ErrorHelper.cs (mezulari lagungarria, euskarazko mezuak).
- Gomendioa produkzioan: gehitu logging (eg. Serilog) eta global exception handlers (DispatcherUnhandledException, AppDomain.CurrentDomain.UnhandledException).

Oharrak garrantzitsuak
- Prezioak tipoan: migration-ek Precio eta Total double erabiltzen dute. Diruarekin lan egokia egiteko decimal gomendagarria da — horrek migrazio berria eskatzen du.
- EnsureCreated vs Migrate: EnsureCreated-ek datu-basea sortzen du baina ez du migrazio-historia eguneratzen; deploy edo garapenean hobe Database.Migrate() erabiltzea.

Aldaketak eta seinalerako tokia
- IVA-ren tasa: UserPayWindow.xaml.cs -> const decimal taxRate = 0.21m;
- Produktuak kargatu DB-tik: UserPayWindow eraikitzean dagoen kodea aldatu da (DBtik kargatzeko saiakera egiten du, bestela adibidea erabiltzen da).

Laguntza / Erroreak
- Kodean akatsik baduzu edo exekuzioan fitxategia blokeatuta badago, itxi aplikazioa edo itxi prozesua (Elkartea.exe) eta berreraiki.

Kontaktua
- Proiektu hau jatorrizko repo batean oinarritzen da. Arazoak izanez gero adierazi fitxategi eta errore mezua.

Eskerrik asko.
