**MyContact**
MyContact, kişilerin iletişim bilgilerini yönetmek için geliştirilmiş bir mikroservis tabanlı bir uygulamadır. Proje, .NET Core teknolojisi ile geliştirilmiş ve çeşitli servisler arasında iletişim RabbitMQ gibi mesajlaşma sistemleri ile sağlanmaktadır. Proje, katmanlı mimari ve veritabanı yapısına dayalı olarak yapılandırılmıştır.

**Proje Yapısı**
API: RESTful API üzerinden mikroservislerle iletişim kurulur.
Application: İş mantığı ve servisler burada yer alır.
Core: Ortak veri modelleri ve arayüzler içerir.
Infra: Veritabanı ve dışa bağımlılıkları yönetir.
Tests: Birim testleri ve entegrasyon testleri içerir.

**Gereksinimler**
.NET 6.0 veya daha yüksek bir sürüm
PostgreSQL (Veritabanı için)
RabbitMQ (Mesaj kuyruğu için)

**Proje Özellikleri**
Kişi Yönetimi: Kişi ekleme, silme ve güncelleme işlemleri.
Telefon Numarası Yönetimi: Her bir kişinin birden fazla telefon numarasını yönetme.
İstatistikler: Kişi ve telefon sayısı gibi istatistikleri döndüren mikroservisler.
Raporlama: Kişi ve telefon sayısı gibi verileri raporlama servisi üzerinden almak.

**API Kullanımı**
Aşağıda, MyContact projesindeki bazı önemli API endpoint'lerinin nasıl kullanılacağını açıklıyorum. Bu API'ler RESTful bir yapıya sahiptir ve HTTP metodları ile çalışır.
**1. Kişi (Person) Yönetimi
1.1 Rehberde kişi oluşturma**
Endpoint: POST /person
Açıklama: Yeni bir kişi ekler.
Örnek İstek:
{
  "firstName": "Berk",
  "lastName": "Coven",
  "company": "Freelance"
}
**1.2 Tüm Kişileri Getir (Get All People)**
Endpoint: GET /person
Açıklama: Sistemdeki tüm kişileri getirir.
**1.3 Kişi Silme (Delete Person)**
Endpoint: DELETE /person/{personId}
Açıklama: Belirtilen personId ile kişiyi siler.
Örnek İstek:
DELETE http://localhost:5000/api/person/{personId}
**1.4 Kişi Detaylarını Getir **
Endpoint: GET /person/{personId}
Açıklama: Belirtilen kişinin detaylarını getirir.
Örnek İstek:
GET http://localhost:5000/api/person/{personId}
**2. İletişim Bilgisi Yönetimi
2.1 İletişim Bilgisi Ekleme (Add Contact Information)**
Endpoint: POST /ContactInformation
Açıklama: Bir kişiye iletişim bilgisi ekler.
Örnek İstek:
{
  "personId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "infoType": 1, (Phone,Email,Location)
  "infoContent": "string"
}
**2.2 İletişim Bilgisi Silme**
Endpoint: DELETE /ContactInformation/{id}
Açıklama: Belirtilen id ile iletişim bilgisi siler.
Örnek İstek:
DELETE http://localhost:5000/api//ContactInformation/{id}
**3. Raporlama (Reports)
3.1 Tüm Raporları Getir (Get All Reports)**
Endpoint: GET /report
Açıklama: Sistemdeki tüm raporları getirir.
Örnek İstek:
GET http://localhost:5000/api/report
**3.2 Rapor Detaylarını Getir (Get Report Details)**
Endpoint: GET /report/{id}
Açıklama: Belirtilen raporun detaylarını getirir.
Örnek İstek:
GET http://localhost:5000/api/report/{id}
**3.3 Konuma göre Rapor Oluşturma**
Endpoint: POST /report/request-report
Açıklama: Verilen lokasyona göre rapor oluşturur.
Örnek İstek:
POST 
{
  "location": "string",
  "date": "2025-04-01T20:21:46.889Z"
}
