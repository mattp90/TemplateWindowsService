*****
PUBBLICARE L'APP
dotnet publish Service --output ".\publish"
*****

*****
CREARE IL WINDOWS SERVIZIO
sc.exe create "Service " binpath=".\publish\Service .exe"

    Se è necessario modificare la radice del contenuto della configurazione host, 
    è possibile passarla come argomento della riga di comando quando si specifica binpath :
    sc.exe create "Svc Name" binpath="C:\Path\To\App.exe --contentRoot C:\Other\Path"

Verrà visualizzato un messaggio di output:
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

Verrà visualizzato un messaggio di output:
[SC] DeleteService SUCCESS
*****