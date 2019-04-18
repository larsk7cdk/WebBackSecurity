## Afleveringsopgave3
Lav en simpel bruger-login til din webapplikation. Login feature skal lede en bruger til en beskyttet side, hvor man kan også kunne logge sig ud. 

Webapplikationen kan fx være til din tidligere Bilsalg opgave eller en helt nyt projekt.

Du kan bygge din login fx med "CookieAuthenticationDefaults" eller vha. NET Core Identity services understøttet af EF Core framework.

(Husk at kun indsætte din link til din opgaverepository.)

Hvis bogens gennemgang af Sikkerhed virker for kringlet kan du se følgende links for inspiration:

ASP.NET Core: Security. (Kapitel 1). Lynda.com https://www.lynda.com/ASP-NET-Core-tutorials/ASP-NET-Core-Security/612194-2.html

og 

https://youtu.be/WoseLEA3scI


## ToDo inden start af løsningen

Der skal oprettes 2 tabeller i databasen med følgende kommandoer i Package Manager Console

- Update-Database -c IdentityDbContext
- Update-Database -c TodoDbContext


## Test af API

Hvis man har oprettet en eller flere ToDo's, kan man se dem via postman på følgende måde

1. Kald auth controlleren for at hente en JWT Bearer token 
   https://localhost:5001/api/auth/authenticate
2. Kopier Bearer token og kald API listen med en POST på
   https://localhost:5001/api/todoapi
   
   Tilføj en header Authorization med value Bearer "token"
   
   Og med følgende body
   
   {
     "email": "Email",
     "password" : "kodeord"
   }

   Dette returnerer de ToDo's som er oprettet under brugeren