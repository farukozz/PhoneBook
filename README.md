DataAccess katmanı altında
Concrete/EntityFramework/Context/PhoneBookContext.cs
classı içindeki Connection stringi değiştirdikten sonra

Package Manager Console da DataAccess katmanı seçiliyken aşağıdaki komutları çalıştırınız.
Add-Migration First
Update-Database -context:PhoneBookContext



1) Kullanıcı kayıt

(POST)
https://localhost:44346/api/Auth/register
{
	"Email":"o.faruk.oz@hotmail.com",
	"Password":"123456",
	"FirstName":"Faruk",
	"LastName":"ÖZ"
}

------------------------------------------------------

2)Kullanıcı giriş

(POST)
https://localhost:44346/api/Auth/login

{
	"Email":"o.faruk.oz@hotmail.com",
	"Password":"123456"
}

------------------------------------------------------

3) Rehbere kişi ekleme

(POST)
Headers
Authorization Bearer ......token........
https://localhost:44346/api/Person/add
{
	"FirstName":"Ali",
	"LastName":"Demir",
	"Company":"Deneme",
	"UserId":1,
	"PhoneNumbers":[
		{"PhoneNumber":"05073198480"},
		{"PhoneNumber":"05551231010"}
		]
}

------------------------------------------------------

4) Rehberden kişi silme

(GET)
Headers
Authorization Bearer ......token........
https://localhost:44346/api/Person/delete?personId=1

------------------------------------------------------

5) Rehberdeki kişiyi güncelleme

(POST)
Headers
Authorization Bearer ......token........
https://localhost:44346/api/Person/update
   {
        "id": 1,
        "firstName": "Ayşe",
        "lastName": "DEMIR",
        "company": "DENEME",
        "userId": 1,
        "phoneNumbers": [
            {
                "phoneNumber": "05414101010"
            },
            {
                "phoneNumber": "05305001010"
            }
        ]
    }

------------------------------------------------------

6) Kişiye ait rehber listesi

(GET)
Headers
Authorization Bearer ......token........
https://localhost:44346/api/Person/getlist

------------------------------------------------------

7) Kişinin rehberinde arama

(GET)
Headers
Authorization Bearer ......token........
https://localhost:44346/api/Person/search?userId=1&searchText=far