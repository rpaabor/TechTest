Beskrivning f�r att k�ra applikation.
.NET ramverks version som kr�vs f�r att k�ra applikationen �r 4.6.
TechTest applikationen k�rs b�st p� IIS, har anv�nt IIS express fr�n VisualStudio 2017 vid utveckling.
Den kan annars s�ttas upp som en site p� en windows maskin som har IIS installerad.

N�r TechTest applikationen k�rs m�ste ClientApplication konfigureras.
I app.config filen finns en binding mot server applikationen, den m�ste �ndras till r�tt adress.