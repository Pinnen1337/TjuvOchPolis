# Uppgiften

Ni ska skapa en stad med tre sorters personer:
- Medborgare
- Tjuv
- Polis

Alla personerna har ett antal properties:
- Var de befinner sig i X och Y-led
- Vilken riktning de färdas i, t ex Xdirection = -1 och Ydirection=0
- Inventory (Lista med saker)
 
Staden ska ritas ut i konsollen, med bokstäverna:
- P för polis
- T för tjuv
- M för medborgare

Alla de här personerna rör sig enligt 9 olika riktningar, som avgörs slumpmässigt då personen skapas.
Så fort en person försvinner utanför staden så kommer hen tillbaka i andra ändan, och fortsätter sin promenad i samma riktning som tidigare.

Då och då möts personerna, och beroende på vilka som möts händer olika saker:
- Polis möter tjuv
  - Polisen tar tjuven, och det han stulit hamnar i polisens inventory
- Medborgare möter tjuv
  - Tjuven rånar medborgaren på en av de saker medborgaren bär på sig. Stöldgodset hamnar i tjuvens inventory.
- Medborgare möter polis
  - Inget händer

Så fort något händer i staden ska detta skrivas ut, t ex “Tjuv rånar medborgare” eller “Polis tar tjuv”.

# Inventory - Inventarielista
Varje medborgare har följande saker med sig:
- Nycklar
- Mobiltelefon
- Pengar
- Klocka

- Varje gång en tjuv rånar en medborgare så tar han EN slumpmässig sak.
  - Den saken hamnar i tjuvens inventory.
- När polisen tar tjuven så tar polisen ALLA saker.
# Tips och råd
Skapa en basklass, Person
- Skapa subklasser för Tjuv, Polis och Medborgare - Arv
- Skapa en klass för “sak”, som sedan läggs i en lista, Inventory, men kalla den olika beroende på vem som innehar den.
- Tjuvens inventory är: Stöldgods
- Polisens inventory är: Beslagtaget
- Medborgarens inventory är: Tillhörigheter
- Ägna mycket tid åt att fundera på uppgiften, innan ni börjar koda.
- Skissa gärna på papper
◦ Ni får själva avgöra vilka properties som behövs för att lösa uppgiften.
# Andra riktlinjer
- Gör staden ganska stor, t ex 100 x 25 “rutor”.
- Skapa personerna antingen slumpmässigt, eller att du bestämmer att det ska vara ett visst antal av varje persontyp, t ex 10 poliser, 20 tjuvar och 30 medborgare. Laborera med antalet
för att se hur brottsligheten i staden förändras.
- Programmet ska loopa automatiskt, så länge ingen person möter någon annan.
- Då någon möter en annan person ska programmet pausa i ett par sekunder, och en text om berättar vad som hänt visas.

- Nederst i UI ska följande redovisas:
  - Antal rånade medborgare: 12
  - Antal gripna tjuvar: 7
