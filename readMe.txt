Beskrivning för att köra applikation.
.NET ramverks version som krävs för att köra applikationen är 4.6.
TechTest applikationen körs bäst på IIS, har använt IIS express från VisualStudio 2017 vid utveckling.
Den kan annars sättas upp som en site på en windows maskin som har IIS installerad.

När TechTest applikationen körs måste ClientApplication konfigureras.
I app.config filen finns en binding mot server applikationen, den måste ändras till rätt adress.


Simple Tech test i did a while back, the client application can be run in multiple instances.
This is defently not something i would run in production enviroment, its just a consept.
