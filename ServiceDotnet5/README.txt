*****
PUBBLICARE L'APP
dotnet publish Service --output ".\publish"
*****

*****
CREARE IL WINDOWS SERVIZIO
sc.exe create "Service " binpath=".\publish\Service .exe"

    Se � necessario modificare la radice del contenuto della configurazione host, 
    � possibile passarla come argomento della riga di comando quando si specifica binpath :
    sc.exe create "Svc Name" binpath="C:\Path\To\App.exe --contentRoot C:\Other\Path"

Verr� visualizzato un messaggio di output:
[SC] CreateService SUCCESS
*****

*****
AVVIARE IL WINDOWS SERVIZIO
sc.exe start "Service"
*****

*****
ARRESTARE IL WINDOWS SERVIZIO
sc.exe stop "Service"
*****

*****
ELIMINARE IL WINDOWS SERVIZIO
sc.exe delete "Service"

Verr� visualizzato un messaggio di output:
[SC] DeleteService SUCCESS
*****