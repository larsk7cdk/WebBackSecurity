## Afleveringsopgave3
Lav en simpel bruger-login til din webapplikation. Login feature skal lede en bruger til en beskyttet side, hvor man kan ogs� kunne logge sig ud. 

Webapplikationen kan fx v�re til din tidligere Bilsalg opgave eller en helt nyt projekt.

Du kan bygge din login fx med "CookieAuthenticationDefaults" eller vha. NET Core Identity services underst�ttet af EF Core framework.

(Husk at kun inds�tte din link til din opgaverepository.)

Hvis bogens gennemgang af Sikkerhed virker for kringlet kan du se f�lgende links for inspiration:

ASP.NET Core: Security. (Kapitel 1). Lynda.com https://www.lynda.com/ASP-NET-Core-tutorials/ASP-NET-Core-Security/612194-2.html

og 

https://youtu.be/WoseLEA3scI


## ToDo inden start af l�sningen

Der skal oprettes 2 tabeller i databasen med f�lgende kommandoer i Package Manager Console

- Update-Database -c IdentityDbContext
- Update-Database -c TodoDbContext


## Test af API

Hvis man har oprettet en eller flere ToDo's, kan man se dem via postman p� f�lgende m�de

1. Kald auth controlleren for at hente en JWT Bearer token 
   https://localhost:5001/api/auth/authenticate
2. Kopier Bearer token og kald API listen med en POST p�
   https://localhost:5001/api/todoapi
   
   Tilf�j en header Authorization med value Bearer "token"
   
   Og med f�lgende body
   
   {
     "email": "Email",
     "password" : "kodeord"
   }

   Dette returnerer de ToDo's som er oprettet under brugeren