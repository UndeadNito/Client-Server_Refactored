# Perfect recipe of this server includes:

+ .NET compiler
+ MySQL server
+ [Client app](https://github.com/UndeadNito/Client_Refactored)

!Method tested on Windows 10!

## First things first download and install MySQL server. Set it up after:

  + use [DBdump](./DBdump.sql) located the root directory to set up database (you can use HeidiSQL if command line hates you as you hate it)
  + create new user for the app (default: name = requester, password = pass). You will need to specify it in [DBProvider](./Client-Server_Refactored/Server/DBProvider.cs)
  
      ```
       private const string connectionString = "Database=cssoftr;Uid=requester;Pwd=pass;host=localhost;";
      ```
       
       if you created user other then default then insert new nickname after `Uid=` and new password after `Pwd=`
  + now you can change user permissions (it will work without changes but do it just in case) so it could change only "cssoftr" database
  
## Secondly compile and run server

  + use any .Net compiler to produce `exe` file
  + run the file 

## Finally set up [Client app](https://github.com/UndeadNito/Client_Refactored)

  + don't forget to read its own [README](https://github.com/UndeadNito/Client_Refactored/blob/master/README.md) file
