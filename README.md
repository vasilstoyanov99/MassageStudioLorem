# MassageStudioLorem
Web application for booking massage appointments. My project for the ASP.NET Core course at SoftUni.
https://massagestudiolorem.azurewebsites.net/

## :information_source: How It Works

**Guests**
- Have access to the welcome page for guests and can register or log in.

**Clients**
- Can browse the categories, massages, and masseurs and see all the reviews of the selected masseur.
- Can book a massage as well as check their upcoming and past appointments.
- Can cancel an upcoming appointment and leave a review of the masseur from a past appointment.
- Can join the studio's team by becoming a masseur.

**Masseurs**
- Can check their work schedule which consists of upcoming appointments.
- Can check all of the reviews left from their clients.

**Admin**
- Can add or delete categories.
- Can edit, add or delete massages. 
- Can edit or delete masseurs.
- Can delete reviews.

**Time zones**
- The project uses the difference, in minutes, between a date as evaluated in the UTC zone, and the same date as evaluated in the client's local time zone. The offset is then used to calculate the client's current local date and time which is used in the booking, post a review, and showing appointments functionalities. The project assumes that the clients and masseurs live in the same time zone.

**When you run the project for the first time sample data will be seeded as well as these test accounts:**

- Client: username: Client / password: 123456
- Male masseur: username: Brayden / password: 123456
- Female masseur: username: Natalie / password: 123456
- Admin: username: BestAdminEver / password: 123456

**The seeded client's Time Zone Offset is 180 minutes! (GMT+3)**

**[Here](https://imgur.com/a/fvwuCm6) is a screenshot of the project's database diagram**

## :hammer_and_pick: Built With
- ASP.NET Core 5
- Entity Framework Core 5.0.9
- Microsoft SQL Server Express
- ASP.NET Identity System
- MVC Areas
- Razor Pages + Partial Views
- Customized Blazor Log in and Resiter pages
- Dependency Injection
- Sorting and Paging with EF Core
- Data Validation, both Client-side, and Server-side
- Data Validation in the Input View Models
- Custom Validation Attributes
- Responsive Design
- Bootstrap
- jQuery
- HtmlSanitizer 6.0.441
 
