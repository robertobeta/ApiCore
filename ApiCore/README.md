# ApiCore
 BackEnd Services

#Con la siguiente l�nea mapeamos la base de datos
(Windows)  Scaffold-DbContext "server=localhost; port=3306;database=VueDB;user=root;password=antonio20019;" Pomelo.EntityFrameworkCore.MySql -OutputDir Models 
(Linux or Mac) dotnet ef dbcontext scaffold "server=pjdbv2.ct8o2hbw0cgi.us-east-1.rds.amazonaws.com;port=3306;database=BDPoliza;user=pjinstance;password=trhc_0253;TreatTinyAsBoolean=true;" Pomelo.EntityFrameworkCore.MySql -c ApplicationDbContext -o Models -f 
