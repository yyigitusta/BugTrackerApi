BugTrackerApi (ASP.NET Core 8 + EF Core 8 + SQLite)

Basit bir Hata Takip (Issue Tracker) API’si.
Varlıklar: Project ve Issue (1→N).
Öğretici amaç: REST, EF Core, Migration, CRUD, filtreleme.

🚀 Teknolojiler

.NET 8.0 (ASP.NET Core Web API, Controllers)

EF Core 8 (SQLite, Migrations)

Swagger / OpenAPI

C#

📦 Gerekli Kurulum

.NET 8 SDK

(İlk kezse) EF CLI:

dotnet tool install --global dotnet-ef


(HTTPS için) Gerekirse:

dotnet dev-certs https --trust

📁 Klasör Yapısı
BugTrackerApi/
 ├─ Controllers/
 │   ├─ ProjectsController.cs
 │   └─ IssuesController.cs
 ├─ Data/
 │   └─ AppDbContext.cs
 ├─ Models/
 │   ├─ Project.cs
 │   └─ Issue.cs
 ├─ appsettings.json
 ├─ Program.cs
 └─ README.md

⚙️ Yapılandırma

appsettings.json:

{
  "ConnectionStrings": { "Default": "Data Source=bugtracker.db" },
  "Logging": { "LogLevel": { "Default": "Information", "Microsoft.AspNetCore": "Warning" } },
  "AllowedHosts": "*"
}


Program.cs (özet):

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

🗃️ Veritabanı (Migration)

Proje kökünde ( .csproj ile aynı dizin ):

dotnet clean
dotnet restore
dotnet build

dotnet ef migrations add InitialCreate
dotnet ef database update

▶️ Çalıştırma

Visual Studio: BugTrackerApi profiliyle ▶️
CLI:

dotnet run


Swagger UI: https://localhost:5001/swagger

Alternatif (HTTP): http://localhost:5000/swagger (profiline göre değişebilir)

🔗 API Uç Noktaları
Projects

GET /api/projects — tüm projeler (en yeni önce)

GET /api/projects/{id} — tek proje

POST /api/projects — proje oluştur

PUT /api/projects/{id} — proje güncelle

DELETE /api/projects/{id} — proje sil

Örnek POST /api/projects

{
  "name": "IhaKontrol",
  "description": "Yer kontrol yazılımı"
}

Issues

GET /api/issues?projectId={id}&status={Open|InProgress|Resolved|Closed} — liste + filtre

GET /api/issues/{id} — tek kayıt

POST /api/issues — kayıt oluştur

PUT /api/issues/{id} — güncelle

DELETE /api/issues/{id} — sil

Örnek POST /api/issues

{
  "title": "Harita katmanları yüklenmiyor",
  "body": "MBTiles ilk açılışta gelmiyor",
  "status": 0,
  "projectId": 1
}


status enum: Open=0, InProgress=1, Resolved=2, Closed=3
projectId mevcut bir projeye ait olmalı.

✅ Durum Kodları

200 OK – Başarılı liste/okuma

201 Created – Oluşturuldu (Location header ile)

204 No Content – Güncelle/Sil başarılı

400 Bad Request – Validasyon/iş kuralı hatası

404 Not Found – Kayıt bulunamadı

🧪 Hızlı Test (VS .http)

Proje köküne BugTrackerApi.http ekle:

### Ping
GET https://localhost:5001/api/projects

### Create Project
POST https://localhost:5001/api/projects
Content-Type: application/json

{
  "name": "IhaKontrol",
  "description": "Yer kontrol yazılımı"
}


Satır üstündeki Send Request ile çalıştırabilirsin.

🗺️ Yol Haritası (Sonraki Adımlar)

DTO katmanı (entity yerine DTO expose)

FluentValidation ile giriş kuralları

Global hata yönetimi (ProblemDetails)

Paging & Sorting & Search

JWT Authentication (+ Roles)

Dockerfile & CI
