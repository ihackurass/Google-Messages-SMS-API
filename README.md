# Google Messages SMS API

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![Status](https://img.shields.io/badge/Estado-Producción-brightgreen.svg)]()

## Visión General

**Google Messages SMS API** es una solución de código abierto para envío de SMS mediante Google Messages Web. Diseñada para sistemas electorales, notificaciones de confirmación de voto, y aplicaciones que requieren envío confiable de mensajes SMS sin costos de APIs comerciales como Twilio.

## Video Demostrativo

<div align="center">
   <img src="images/test.gif" alt="Demo GIF" width="100%"/>
</div>

## Características Principales

- **API RESTful**: Endpoints HTTP para envío y seguimiento de mensajes
- **Cola de Mensajes**: Sistema de cola con procesamiento en background
- **Tracking Completo**: Seguimiento de estado de cada mensaje (Pendiente, Enviando, Enviado, Error)
- **Persistencia**: Historial completo de mensajes enviados para auditoría
- **Automatización con Selenium**: Control de Google Messages Web mediante ChromeDriver
- **Swagger UI**: Documentación interactiva de la API
- **Auditoría Electoral**: Trazabilidad completa para cumplimiento normativo

## Tecnologías Utilizadas

<div align="center">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET"/>
  <img src="https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core"/>
  <img src="https://img.shields.io/badge/Selenium-43B02A?style=for-the-badge&logo=selenium&logoColor=white" alt="Selenium"/>
  <img src="https://img.shields.io/badge/Google%20Chrome-4285F4?style=for-the-badge&logo=googlechrome&logoColor=white" alt="Chrome"/>
  <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger"/>
</div>

## Arquitectura del Sistema

<div align="center">
  <img src="images/arqui.png" alt="Arquitectura del Sistema" width="80%"/>
</div>

## Guía de Instalación

### Requisitos Previos
- .NET 8.0 SDK o superior ([Descargar](https://dotnet.microsoft.com/download/dotnet/8.0))
- Google Chrome instalado
- Teléfono Android con Google Messages

### Pasos de Instalación

1. **Clonar el Repositorio**
   - Descargar o clonar este repositorio
```bash
   git clone https://github.com/ihackurass/Google-Messages-SMS-API.git
   cd Google-Messages-SMS-API

```

2. **Restaurar Dependencias**
   - Restaurar paquetes NuGet
```bash
   dotnet restore
```

3. **Compilar el Proyecto**
   - Compilar en modo Release
```bash
   dotnet build -c Release
```

4. **Ejecutar la Aplicación**
   - Iniciar la API
```bash
   dotnet run
```

5. **Configurar Google Messages (Primera Vez)**
   - La aplicación abrirá Chrome automáticamente
   - Escanear el código QR desde tu teléfono Android
   - La sesión quedará guardada permanentemente

6. **Acceder a la API (OPCIONAL TESTING)** 
   - Interfaz Swagger: http://localhost:5000/swagger
   - Endpoint base: http://localhost:5000/api/Sms

## Uso de la API

### Endpoints Disponibles

#### Enviar SMS
```http
POST /api/sms/enviar
Content-Type: application/json

{
  "telefono": "+51987654321",
  "mensaje": "Su voto ha sido registrado exitosamente"
}
```

#### Ver Historial de Envíos
```http
GET /api/sms/history?limite=50
```

## Estructura del Proyecto
```
SMSWebApi/
├── Controllers/
│   └── SMSController.cs           # Endpoints de la API
├── DTO/
│   └── SMSRequest.cs              # DTO para requests
├── Models/
│   └── SMS.cs                     # Modelo de mensaje
├── Services/
│   ├── ColaService.cs             # Gestión de la cola de mensajes
│   └── SMSBackgroundService.cs    # Worker que procesa la cola
├── Program.cs                     # Configuración principal de la aplicación
├── appsettings.json               # Configuración general
├── appsettings.Development.json   # Configuración para desarrollo
├── SMSWebApi.csproj               # Archivo de proyecto
└── readme.md                      # Documentación
```

## Capturas de Pantalla

<div align="center">
  <img src="images/swagger.jpg" alt="Interfaz Swagger" width="100%"/>
</div>

## Contacto

Si tienes preguntas o sugerencias, no dudes en contactarme :)

---

<div align="center">
  <sub>Desarrollado para sistemas electorales y notificaciones masivas confiables con un costo sumamente reducido 🗳️📱</sub>
</div>
