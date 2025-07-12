# 📘 Serilog – Notes de Cours Complètes

## 🧠 Objectif de Serilog

Serilog est un framework de **structured logging** pour .NET.  
Il permet de logguer des messages enrichis de **données structurées** (objets, contextes, métadonnées), tout en gardant une grande flexibilité sur les cibles (sinks) et les formats.

---

## ⚙️ Modes de Configuration

### 1. Configuration par code (Program.cs)

```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
```


2. Configuration via appsettings.json
 ```json
    "Serilog": {
      "MinimumLevel": {
        "Default": "Information",
        "Override": {
          "Microsoft": "Warning",
          "System": "Warning"
        }
      },
      "WriteTo": [
        { "Name": "Console" },
        {
          "Name": "File",
          "Args": {
            "path": "Logs/log-.txt",
            "rollingInterval": "Day"
          }
        }
      ],
      "Enrich": [
        "FromLogContext",
        "WithMachineName",
        "WithThreadId"
      ],
      "Properties": {
        "Application": "BookCar",
        "Environment": "Development"
      }
    }
```
## In Program.cs :

```csharp
      builder.Host.UseSerilog((ctx, lc) =>
          lc.ReadFrom.Configuration(ctx.Configuration));
```
## 🚀 Bootstrap Logger
Utilisé pour capturer les erreurs avant que l’application ne soit complètement démarrée.
```csharp
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateBootstrapLogger();
```
## 🧩 Enrichers – Ajout d'informations automatiques
 **Enricher**	              |**Rôle**
- WithMachineName()	        |Ajoute le nom de la machine
- WithThreadId()	          |Ajoute l’ID du thread
- WithEnvironmentUserName()	|Ajoute le nom de l’utilisateur système (Windows/Linux)
- FromLogContext()	        |Permet d’ajouter dynamiquement des propriétés (LogContext.PushProperty

#### Exemple :
```csharp
    using (LogContext.PushProperty("UserId", 42))
    {
        Log.Information("User performed action.");
    }
```
## 🎚️ MinimumLevel et Override
- Version statique :
```csharp
.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
```
- Version dynamique avec LoggingLevelSwitch :
```csharp
  var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Warning);
  .MinimumLevel.Override("Microsoft", levelSwitch);
```
 Modification à chaud possible :

``` csharp
levelSwitch.MinimumLevel = LogEventLevel.Debug;
```
## 🧼 Filters – Exclure certains logs
#### Exemple dans appsettings.json :
``` json
"Filter": [
  {
    "Name": "ByExcluding",
    "Args": {
      "expression": "RequestPath like '%/health%'"
    }
  }
]
```
**Nom** 	**Description**
- ByExcluding	           | Exclut les logs correspondant à la condition
- ByIncludingOnly	       | Garde uniquement les logs correspondant à la condition

## 🔬 Destructuring – Contrôle de la sérialisation
Permet de gérer comment les objets sont loggués (profondeur, taille, etc.).

#### Exemple appsettings.json :
``` json
  "Destructure": [
    {
      "Name": "ToMaximumDepth",
      "Args": { "maximumDestructuringDepth": 2 }
    }
  ]
```
**Option** 	              |**Effet**
ToMaximumDepth	          | Limite la profondeur d’objets loggués
ToMaximumStringLength	    | Tronque les longues chaînes
ToMaximumCollectionCount	| Limite le nombre d’éléments d’une liste ou collection

## 📥 WriteTo – Sinks supportés
- **Sink**	        |Description
<hr/>

- **Console**	      |affiche les logs dans la console
- **File**	        |Enregistre les logs dans un fichier
- **Seq**	          |Visualisation centralisée (locale ou cloud)
- **MSSqlServer**	  |Stockage en base SQL Server
- **Elasticsearch**	|Logging distribué pour observabilité

### ✅ Bonnes pratiques

- Toujours commencer avec un Bootstrap Logger au tout début (CreateBootstrapLogger)

- Utiliser FromLogContext() pour ajouter du contexte métier à chaque log

- Utiliser Override() pour réduire le bruit des logs internes de .NET (Microsoft, System)

- Utiliser des Filter pour exclure des endpoints inutiles (/health, /metrics, etc.)

- Utiliser LoggingLevelSwitch pour changer dynamiquement les niveaux de log

- Bien gérer la destructuration pour éviter des logs trop volumineux ou sensibles

### 📌 Ressources utiles
- Serilog Officiel : https://serilog.net/

- Sinks supportés : https://github.com/serilog/serilog/wiki/Third-Party-Sinks

- Visualiseur Seq : https://datalust.co/seq

- Guide Destructuring : https://github.com/serilog/serilog/wiki/Destructuring-Objects

## Schema



  [Log.Information(...)]
        ↓
  [Enrichers] → ajoutent des infos automatiques (UserId, MachineName, etc.)
        ↓
  [Filters]   → ignorent ou gardent certains logs
        ↓
  [Destructurers] → transforment les objets complexes en données loggables
        ↓
  [Sinks]     → destinations finales : Console, Fichier, DB, ou… 🔥 **Seq**


## Notion à Explorer

Microservices + Logging distribué

Monitoring + Alerting

Clean Architecture + Observabilité

Ou même un Logging as a Service fait maison 😎

🌐 Centralisation avec Elastic ou Grafana Loki

🔗 Corrélation entre logs et requêtes HTTP (trace ID)

📤 Export vers cloud logging (Azure Monitor, CloudWatch, etc.)

🔐 Masquage/filtrage de données sensibles (RGPD-style)
