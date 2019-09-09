Hemuppgift - backend - mashup

Denna uppgift går ut på att skapa ett REST API som helt enkelt erbjuder en mashup av några bakomliggande API:er.

De API:er som ska kombineras är MusicBrainz, Wikidata/Wikipedia och Cover Art Archive.

    MusicBrainz erbjuder ett API med bland annat detaljerad information om musikartister (information såsom artistens namn, födelseår, födelseland osv).
    Wikipedia är en community-wiki som innehåller beskrivande information om bland annat just musikartister. Ibland länkar MusicBrainz till Wikipedia, men oftast så länkar de till Wikidata som fungerar som en språkproxy mot Wikipedia. Wikidata innehåller alltså information om alla olika Wikipedia-länkar (svenska, engelska, franska osv).
    Cover Art Archive är ett systerprojekt till MusicBrainz som innehåller omslagsbilder till olika album, singlar eps osv som släppts av en artist.

Ditt API - själva hemuppgiften - ska ta emot ett MBID (MusicBrainz Identifier) och leverera tillbaka ett resultat bestående av

    En beskrivning av artisten som hämtas från Wikipedia. Wikipedia innehåller inte några MBID utan mappningen mellan MBID och Wikipedia-identifierare finns via MusicBrainz API (antingen en direktreferens eller via språkproxyn Wikidata).
    En lista över alla album som artisten släppt och länkar till bilder för varje album. Listan över album finns i MusicBrainz men bilderna finns på Cover Art Archive.

Externa API:er
MusicBrainz

    Dokumentation: http://musicbrainz.org/doc/Development/XML_Web_Service/Version_2
    URL till API: http://musicbrainz.org/ws/2
    Exempelfråga: http://musicbrainz.org/ws/2/artist/5b11f4ce-a62d-471e-81fc-a69a8278c7da?&fmt=json&inc=url-rels+release-groups

Wikidata

    Dokumentation: https://www.wikidata.org/w/api.php
    URL till API: https://www.wikidata.org/w/api.php
    Exempelfråga: https://www.wikidata.org/w/api.php?action=wbgetentities&ids=Q11649&format=json&props=sitelinks

Hint: I JSON-svaret på exempelfrågan mot MusicBrainz ovan hittar du en lista vid namn relations. Kolla in relationen vars type är wikidata. Där hittar du identifieraren som kan användas för uppslag mot Wikidatas API nämligen Q11649
Wikipedia

    Dokumentation: https://www.mediawiki.org/wiki/API:Main_page
    URL till API: https://en.wikipedia.org/w/api.php
    Exempelfråga: https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles=Nirvana_(band)

I svaret från Wikidata hittar du (förhoppningsvis) en hel del sitelinks, bland annat enwiki som länkar till den engelska versionen av Wikipedia. I svaret hittar du title, i Nirvana-fallet Nirvana (band). Denna titel används för att hämta data från wikipedia. Obs att URL-encode:a titeln, annars får du inga träffar (Nirvana%20(band)). Exempelfråga: https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles=Nirvana%20(band).

Specialfall: Ibland länkar MusicBrainz direkt till Wikipedia. Då finns en relation (relations) där type-attributet har värdet wikipedia. I de fallen hittar du namnet som kan användas för uppslag mot Wikipedias API direkt i MusicBrainz-svaret (i Nirvana-fallet är namnet Nirvana_(band)).
Cover Art Archive

    Dokumentation: https://wiki.musicbrainz.org/Cover_Art_Archive/API
    URL till API: http://coverartarchive.org/
    Exempelfråga: http://coverartarchive.org/release-group/1b022e01-4da6-387b-8658-8678046e4cef

Hint: I JSON-svaret på exempelfrågan mot MusicBrainz ovan hittar ni en lista vid namn release-groups. Där finns bland annat ett albums titel (title) och dess MBID (id). Detta MBID används sedan för uppslaget mot Cover Art Archive.
Krav

Ditt API ska vara förberett för att kunna köras i produktion (om du inte hinner fixa allt - exempelvis felhantering osv så är det bra om du kan beskriva detta i lösningen). API:t måste kunna hantera hög last vilket kan vara utmanande eftersom de bakomliggande API:erna kan vara ganska långsamma och har konsumtionsbegränsningar. Vi vill att du levererar din lösning med tydliga instruktioner som beskriver hur vi ska installera och köra lösningen.

Tänk på ditt arbete som ett proof of concept där Cygni är kund/beställare och du inhyrd konsult. Med proof of concept avses en lösning som är startpunkt för ett större projekt som andra ska kunna bygga vidare på. Val av teknik (språk, ramverk samt bibliotek) måste kunna motiveras utifrån uppgiftens krav och kommer diskuteras på en eventuell senare teknikintervju.

Det är okej att utelämna eller ta genvägar ifall uppgiftens omfång blir för stor men dokumentera gärna dessa antingen i koden eller i dokumentationen. Ifall något är oklart kan du kontakta Cygni för att tydliggöra kraven.

Vid granskning av uppgiften bedömer vi kodvalité, struktur och förväntar oss att du kan motivera de val du har gjort.

    API:et ska vara REST-baserat
    Som transportformat ska JSON användas.
    Ditt resultat - som innehåller beskrivning och album - ska vara ett JSON-svar.

Ett svar från er tjänst kan se ut något i stil med följande:

{
   "mbid" : "5b11f4ce-a62d-471e-81fc-a69a8278c7da",
   "description" : "<p><b>Nirvana</b> was an American rock band that was formed ... osv osv ... ",
   "albums" : [
       {
           "title" : "Nevermind",
           "id": "1b022e01-4da6-387b-8658-8678046e4cef",
           "image": "http://coverartarchive.org/release/a146429a-cedc-3ab0-9e41-1aaf5f6cdc2d/3012495605.jpg"
       },
       {
       ... more albums...
       }
   ]
}

Java

    Om du bygger en Java-lösning ska Apache Maven eller Gradle användas för att bygga och paketera API:et.
    API:et ska kunna startas direkt från Maven/Gradle (exempelvis genom att Jetty och mvn jetty:run) eller som en executable jar via exempelvis Spring Boot.
    Exempel på möjliga ramverk: SpringMVC (Spring Boot), Dropwizard, Jersey

.NET

    Om du bygger en .NET-lösning ska antingen .NET Core CLI 2.x eller en Visual Studio 2017-solution användas.
    Använder du en Visual Studio 2017-solution ska lösningen kunna startas direkt från Visual Studio.
    Använd gärna ASP.NET Core eller ASP.NET
    Paketera lösningen utan bin/ och obj/-kataloger i en zip-fil.

JavaScript - Node.js

    Om du bygger en Node.js-lösning skall den gå att starta via npm eller yarn genom exempelvis yarn run start eller npm run start.
    Kika gärna på ramverk som Hapi, Koa eller Express
    Paketera lösningen utan node_modules i en zip-fil.