# 📚 Kitap & Değerlendirme Kataloğu (RESTful Web API)

ASP.NET Core 8 Web API ve Entity Framework Core kullanılarak geliştirilmiş; ilişkisel veri modeli, dinamik arama, sayfalama ve editoryal web arayüzüne sahip uçtan uca kitap kütüphanesi uygulaması.

---

## ✨ Öne Çıkan Özellikler

- **İlişkisel Veri Yönetimi (1:N):** `Books` ve `Reviews` tabloları arasında Foreign Key ilişkisi ve anlık ortalama puan/yorum sayısı hesaplama.
- **Performans Odaklı Sayfalama (Pagination):** LINQ tabanlı `Skip` ve `Take` mekanizmasıyla optimize veri akışı.
- **Dinamik Filtreleme & Arama:** Kitap başlığı ve yazar bilgisi üzerinden sunucu taraflı arama.
- **RESTful Mimarisi:** Swagger/OpenAPI destekli tam CRUD uç noktaları (`GET`, `POST`, `DELETE`).
- **Editoryal Web Arayüzü:** Vanilla JavaScript ve modern CSS ile tasarlanmış, sıcak keten/kağıt dokusuna sahip interaktif kullanıcı arayüzü (`wwwroot/index.html`).

---

## 🛠️ Kullanılan Teknolojiler

- **Backend:** C#, .NET 8, ASP.NET Core Web API
- **ORM & Veritabanı:** Entity Framework Core, Microsoft SQL Server
- **Dokümantasyon:** Swagger / OpenAPI
- **Frontend:** HTML5, CSS3, Fetch API

---

## 📌 API Uç Noktaları

| Metot | Uç Nokta | Açıklama |
| :--- | :--- | :--- |
| `GET` | `/api/Books` | Sayfalama ve arama destekli kitap listesi |
| `GET` | `/api/Books/{id}` | Belirtilen kitap ve tüm okur değerlendirmeleri |
| `POST` | `/api/Books` | Yeni kitap ekleme |
| `POST` | `/api/Books/add-review` | Kitaba puan ve yorum kaydetme |
| `DELETE`| `/api/Books/{id}` | Kitabı sistemden silme |

---

## 💻 Kurulum ve Çalıştırma

1. Projeyi klonlayın:
   ```bash
   git clone [https://github.com/Meltemplt/KitapKatalog-WebAPI.git](https://github.com/Meltemplt/KitapKatalog-WebAPI.git)
