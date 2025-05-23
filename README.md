# EventManagement - Etkinlik Planlama ve Bilet Satış Sistemi

**EventManagement**'a hoş geldiniz! Bu proje, etkinliklerin oluşturulmasını, yönetilmesini ve takip edilmesini kolaylaştıran bir etkinlik planlama ve bilet satış sistemidir.

EventManagement, organizatörlerin etkinlik oluşturmasına, bilet satmasına, katılımcıları takip etmesine ve raporlar üretmesine olanak tanır. İlk aşamada basitlik adına kullanıcı kimlik doğrulaması kullanılmamaktadır. Bu proje, CRUD işlemleri, ilişkisel veritabanı tasarımı ve raporlama becerilerini sergileyen pratik bir case çalışması olarak tasarlanmıştır.

## Proje Genel Bakış

EventManagement şunları sağlar:
- Organizatörler etkinlikleri oluşturup yönetebilir.
- Etkinlikler için biletler eklenebilir ve satılabilir.
- Bilet alan katılımcılar takip edilebilir.
- Etkinlik bazında raporlar (örneğin, toplam gelir, satılan bilet sayısı) oluşturulabilir.

Bu proje, ölçeklenebilirlik ve genişletilebilirlik göz önünde bulundurularak geliştirilmektedir.

## Özellikler

### Etkinlik Yönetimi
- Etkinlik oluşturma, güncelleme ve silme.
- Etkinlikleri ID, tarih veya tarih aralığına göre getirme.
- Yaklaşan etkinlikleri listeleme ve etkinlik kapasite durumunu kontrol etme.

### Bilet Satışı
- Etkinliklere bilet türleri (örneğin, VIP, Genel) ekleme.
- Bilet stokunu ve satışlarını takip etme.
- Bilet satış özetlerini hesaplama (örneğin, bilet türü başına gelir).

### Katılımcı Takibi
- Bilet satın alan katılımcıların isim ve e-posta gibi bilgilerini kaydetme.
- Etkinlik sırasında katılımcıları giriş yapmış olarak işaretleme.
- Katılımcıları etkinlik veya bilet türüne göre listeleme.

### Raporlama
- Etkinlik bazında raporlar üretme (örneğin, toplam gelir, satılan bilet sayısı).
- Raporları asenkron olarak bir kuyruk sistemi (RabbitMQ) ile işleme.

## Teknoloji Yığını

- **Backend**: .NET (ASP.NET Web API)
- **Frontend**: Henüz belirlenmedi (seçenekler: Blazor, ASP.NET MVC veya Angular)
- **Veritabanı**: SQL Server
- **Mesaj Kuyruğu**: RabbitMQ (asenkron raporlama için)
- **Gerçek Zamanlı Güncellemeler**: SignalR (UI entegrasyonu için planlandı)
- **Doğrulama**: FluentValidation
- **ORM**: Entity Framework Core

## Proje Yapısı

Proje, temiz mimari yaklaşımıyla ve sorumlulukların ayrılmasıyla tasarlanmıştır. Planlanan yapı şu şekildedir:

```bash
EventManagement/
|── src/
|   |── EventManagement.Api/               # Web API katmanı
|   |── EventManagement.Domain/            # Entities, DTOs, enums
|   |── EventManagement.Application/       # İş mantığı (commands, queries, handlers, iş kuralları)
|   |── EventManagement.Infrastructure/    # Data access, repositories, RabbitMQ entegrasyonu
|   |── EventManagement.WorkerService/     # Rapor üretimi için arka plan servisi
|   
|── tests/
|   |── EventManagement.UnitTests/         # Birim testleri
|   |── EventManagement.IntegrationTests/  # Entegrasyon testleri
|
|── EventManagement.sln                    # Çözüm dosyası
```

## Veritabanı Şeması

Sistem, aşağıdaki temel tablolarla ilişkisel bir veritabanı kullanır:

1. **Events**: Etkinlik detaylarını saklar (örneğin, başlık, tarih, yer, organizatör).
2. **Tickets**: Her etkinlik için bilet türlerini ve satış verilerini yönetir.
3. **Attendees**: Bilet alan bireyleri takip eder.
4. **Reports**: Üretilen rapor verilerini depolar (örneğin, bilet satış özetleri).

Varlık ilişkileri:
- `Events` (1) → (*) `Tickets`
- `Tickets` (1) → (*) `Attendees`
- `Events` (1) → (*) `Reports`

## Geliştirme Yol Haritası

Projenin geliştirme süreci aşağıdaki adımlarla ilerleyecektir:

- [X] **Başlangıç Kurulumu**: İlk proje yapısını ve çözüm dosyasını oluşturma.
- [X] **Entities Eklenmesi**: Events, Tickets ve Attendees varlıklarının eklenmesi.
- [X] **Veritabanı İşlemleri**: Veritabanı bağlantısının gerçekleştirilmesi.
- [X] **CRUD İşlemleri**: Events ve Tickets için oluşturma, okuma, güncelleme ve silme (CRUD) işlevlerini uygulama.
- [X] **Katılımcı Yönetimi**: Katılımcı takibi ve etkinlik sırasında giriş yapma (check-in) işlevselliğini ekleme.
- [X] **Doğrulama**: FluentValidation ile varlık doğrulama kurallarını uygulama.
- [X] **Asenkron Raporlama**: RabbitMQ entegrasyonu ile asenkron rapor üretim sistemini kurma.
- [X] **Raporlama Özellikleri**: Worker Service kullanarak bilet satışları ve katılımcı raporlarını geliştirme.
- [ ] **Testler**: Birim testleri ve entegrasyon testlerini yazma ve test kapsamını artırma.
- [ ] **Frontend Geliştirme**: Frontend teknolojisini (Blazor, Angular veya React) seçme ve kullanıcı arayüzünü uygulama.

### Kurulum
1. **Repository klonlayın**:
   ```bash
   git clone https://github.com/mkaanerinc/EventManagement.git
   cd EventManagement
   ```

2. **Bağımlılıkları Yükleyin**:
   ```bash
   dotnet restore EventManagement.sln
   ```

3. **Veritabanını kurun**:
   - appsettings.json dosyasındaki bağlantı dizesini güncelleyin (EventManagement.Api veya EventManagement.Infrastructure altında).
   - Veritabanını oluşturmak için göçleri çalıştırın:
   <br><br>
   ```bash
   dotnet ef migrations add InitialCreate --project EventManagement.Infrastructure
   dotnet ef database update --project EventManagement.Infrastructure
   ```
   
4. **API’yi çalıştırın**:
   ```bash
   dotnet run --project EventManagement.Api
   ```
   
5. **Worker Servisini çalıştırın (raporlama için)**:
   ```bash
   dotnet run --project EventManagement.WorkerService
   ```
### Yapılandırma
- appsettings.json dosyasında RabbitMQ ayarlarını (host, kuyruk adı vb.) yapılandırın.
- Veritabanı sağlayıcısını (SQL Server) ApplicationDbContext içinde ayarlayın.

## Lisans

Bu proje [MIT Lisansı](https://opensource.org/licenses/MIT) kapsamında lisanslanmıştır.
