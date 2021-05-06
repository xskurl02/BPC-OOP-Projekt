<%@ Page Language="C#" CodeBehind="Index.aspx.cs" Inherits="SuperPrsi.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Škurla - Prší server</title>
</head>
<body>
<form id="HtmlForm" runat="server">
    <div>
        <h1>Prší</h1>
        Text je prebraný z <a href="https://karetnihry.blogspot.com/2010/05/prsi-pravidla.html">karetnihry.blogspot.com</a>
        <h2>Pravidla Hry prší</h2>
        Každý hráč dostane 4 karty a jedna karta je vyložena na stůl lícem nahoru.
        <br/>
        Hráč který řadě musí vždy buď vyhodit kartu se stejnou barvou jako je vyložená karta (Srdce, Listy, Kule, Žaludy) nebo kartu stejné číselné hodnoty (7, 8, 9, 10, spodek, král). 
        <br/>
        Svrška lze vyhodit na na kartu jakékoli barvy či hodnoty kromě esa a 7 (jen hned po jejich vyhození, poté už ano) a hráč který svrška vyložil rozhodne jakou barvu musí mít další vyložená karta. 
        <br/>
        Hráč který je na řadě potom co je vyložena Sedma si musí líznout dvě karty nebo vyložit jinou sedmu poté nutnost lízání přechází na dalšího hráče v pořadí a vzrůstá i počet karet (4, 6, 8). 
        <br/>
        Hráč který je na řadě po vyložení Esa stojí tzn. že v tomto kole nemůže vykládat ani lízat karty (v některých pravidlech je možnost přebití esa dalším esem). 
        <br/>
        Pokud hráč nemá žádnou kartu co by mohl vyložit nebo pokud nechce žádnou vyložit musí si líznout novou kartu z balíčku ležícího na stole.
        <h2>Vítěz hry</h2>
        Vítězem je hráč, který se jako první zbaví všech karet v ruce. Poslední hráč prohrává a v dalším kole míchá a rozdává
        <h2>Speciální karty prší</h2>
        <b>Svršek</b> - Pomocí něho je možné změnit průběh hry a barvu karet, které je potřebné vyložit. Svrška lze použít na jakoukoliv kartu, s vyjímkou Esa a Sedmy.
        <br/>
        <b>Eso</b> - Eso lze vyložit vždy na kartu stejné barvy s vyjímkou sedmy, nebo ho použít k přebití esa protihráče (není povinnost). Eso je stop karta, pokud jí následující hráč nemá možnost přebít vlastník Esem tak automaticky ztrácí svůj tah.
        <br/>
        <b>Sedma</b> - Sedma je nejoblíbenější a zároveň nejobávanější Prší karta. Lze jí vyložit na kartu stejné barvy nebo na přebití soupeřovy sedmy. Hráč, který je na řadě po vyložení sedmy a nemá v ruce sedmu vlastní ztrácí kolo a musí si z balíčku líznout 2 karty. Pokud je sedma přebitá, počet karet se násobí až do maxima 8 karet.
        <br/>
        <b>Červená sedma</b>  - Speciální využití červené sedmy je v tom, že takzvaně vrací do hry. Pokud některý  z  hráčů již dohrál hru, ale hráč v po jeho pravici vynese červenou sedmu, musí si líznout 2 karty a vrací se do hry.
        <h1>Prší Server</h1>
        <h2>API pointy - GameController:</h2>
        <ul>
            <li>ConnectToGame</li>
            api/Game/ConnectToGame?userName={userName}"
            <li>IsReadyToPlay</li>
            api/Game/IsReadyToPlay
            <li>BeginGame</li>
            api/Game/BeginGame/{clientId}
            <li>GetCardsOnHand</li>
            api/Game/GetCardsOnHand/{clientId}
            <li>GetPlayerStatus</li>
            api/Game/GetPlayerStatus/{clientId}
            <li>TakeCardFromStack</li>
            api/Game/TakeCardFromStack/{clientId}-{count}
            <li>DiscardCard</li>
            api/Game/DiscardCard/{clientId}-{cardId}
            api/Game/DiscardCard/{clientId}-{cardId}-{suite}
            <li>PlayerStopped</li>
            api/Game/PlayerStopped/{clientId}
            <li>LoseGame</li>
            api/Game/LoseGame/{clientId}
        </ul>
        <h2>Príklad prevolávania API pointov:</h2>
        SuperPrsi.DummyClient
    </div>
</form>
</body>
</html>