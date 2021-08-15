# MassageStudioLorem
Web application for booking massage appointments. My project for the ASP.NET Core course at SoftUni.
http://massagestudiolorem.azurewebsites.net/

## :information_source: How It Works

**Guests:**
- Have access to the welcome page for guests and can register or log in.

**Clients**
- Can browse the categories, massages and masseurs and see all the reviews of the selected masseur.
- Can book a massage as well as check their upcoming and past appointments.
- Can cancel an upcoming appointment and leave a review of the masseur from a past appointment.
- Can join the studio's team by becomming a masseur.

Test accounts:


- Client: username: Client / password: 123456
- Male masseur: username: Brayden / password: 123456
- Female masseur: username: Natalie / password: 123456
-
- Admin: username: BestAdminEver / password: 123456 

**The seeded client's Time Zone Offset is 180 minutes! (GMT+3)**

 The project uses the difference, in minutes, between a date as evaluated in the UTC time zone, and the same date as evaluated in the client's local time zone. The offset it then used to calculate the client's current date and time which is used in the booking, post a review and showing appointments functionalities. The project assumes that the clients and masseurs live in the same time zone.
