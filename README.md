🧾 Elkartea — TPV aplikazio txikia

WPF · .NET 8 · SQLite

Elkartea elkarte txikientzako TPV (Terminal Punto de Venta) aplikazio sinple eta arina da, WPF eta .NET 8 erabiliz garatua.
Datuen kudeaketa SQLite bidez egiten da, konfigurazio erraz eta lokalarekin.

✨ Ezaugarri nagusiak

🖥️ WPF Desktop aplikazioa

🧱 EF Core + SQLite datu-basea

👥 Erabiltzaileen, produktuen eta eskaeren kudeaketa

🧾 Ordainketa eta tiketen sorrera

🇪🇺 Mezuak eta erroreak euskaraz

🛠️ Garapen eta hedapenerako prestatua

▶️ Nola exekutatu
Aukera 1: Komando-lerrotik
cd Elkartea
dotnet build
dotnet run

Aukera 2: Visual Studio

Ireki proiektua

Sakatu Start Debug (F5)

🗄️ Datu-basea (SQLite)

Konexioa:
Elkartea/Data/AppDbContext.cs

optionsBuilder.UseSqlite("Data Source=elkartea.db")


Runtime-an sortutako fitxategia:

bin/Debug/net8.0-windows/elkartea.db

🔄 Migrazioak eta Seed datuak
📁 Migrazioak

Kokapena: Elkartea/Migrations

Snapshot eguneratua badago

🌱 Hasierako datuak (Seeder)

Fitxategia: Elkartea/Data/DatabaseSeeder.cs

Uneko implementazioa:

db.Database.EnsureCreated();


⚠️ Kontuz: EnsureCreated()-ek EZ ditu migrazioak aplikatzen.

✔️ Gomendioa

Produzkiorako edo garapen serioan:

db.Database.Migrate();


edo:

dotnet ef database update

🧰 EF Core komando erabilgarriak
dotnet tool install --global dotnet-ef
dotnet ef migrations add Izena
dotnet ef database update

📂 Proiektuaren egitura
📦 Ereduak

Elkartea/Models

Product

User

Order

Reservation

🧠 Datu-kudeaketa

Elkartea/Data/AppDbContext.cs

Elkartea/Data/DatabaseSeeder.cs

🪟 Ikuspegi nagusiak (Views)

StockWindow

UsersWindow

OrdersWindow

UserPayWindow

…

🚀 Hasierako logika

MainWindow.xaml.cs

LoginWindow

💳 Ordainketak eta tiketak

Ordaindu botoia:
UserPayWindow.xaml.cs → Pay_Click

Tiketa sortu eta inprimatzeko aukera eskaintzen du

⚠️ Une honetan:

Ordainketak ez dira datu-basean gordetzen

Datuak memorian bakarrik kudeatzen dira

🛠️ Hobekuntza gomendatua

Erabili AppDbContext

Gorde ordainketa erregistroak

Eguneratu produktuen stock-a (Cantidad murriztuz)

🚨 Error handling

Erroreen kudeaketa zentralizatua:

Elkartea/Utils/ErrorHelper.cs


Mezuak euskaraz, erabiltzailearentzat lagungarriak

🔐 Produkziorako gomendioak

Logging sistema gehitu (adib. Serilog)

Global exception handlers:

DispatcherUnhandledException

AppDomain.CurrentDomain.UnhandledException

⚠️ Ohar garrantzitsuak

💰 Diruaren tipoa
Uneko migrazioek double erabiltzen dute (Precio, Total)
➜ decimal erabiltzea gomendatzen da
(migrazio berri bat beharko da)

🔄 EnsureCreated vs Migrate

EnsureCreated() → azkarra baina mugatua

Migrate() → gomendatua deploy eta eboluziorako

⚙️ Aldaketarako puntu garrantzitsuak

IVA tasa:

const decimal taxRate = 0.21m;


(UserPayWindow.xaml.cs)

Produktuak DB-tik kargatzea:

UserPayWindow eraikitzailean

DB hutsik badago, adibide-datuak erabiltzen dira

🆘 Laguntza eta arazoak

SQLite fitxategia blokeatuta badago:

Itxi aplikazioa

Itxi Elkartea.exe prozesua

Berreraiki proiektua

Errore bat baduzu:

Adierazi fitxategia eta errore-mezua
