
Windows Azure Badge Generator
-----------------------------
### What is it? ###

The Windows Azure Badge generator is an Azure Worker Role that allows you to generate all different kinds of graphics based on SVG. It has the following features:

 - Replace text within SVG files (It can do all kinds of modifications. Only the text replace feature is implemented)
 - Render SVG to PNG or other formats (Currently only PNG)
 - Manage and store SVG raw data (XML) in SQL Server
 - PCL (Portable Class Library) client for .NET 4.5 and higher, Windows Phone 8, Windows Store Apps

The project name is a bit misleading. You can generate all types of images based on SVGs and not only badges. Maybe in the future the project will be renamed. We will see. 

### Technology Stack ###

For this project the following SDKs, tools and components have been used:

 - Windows Azure SDK 2.2
 - Visual Studio 2013 RTM
 - Windows Phone 8 SDK
 - PCL 4.6
 - Azure Worker Role
 - Unity for dependency injection
 - OWIN hosted Web Api
 - TAP - Async/Await
 - ASP .NET MVC 5 using async controllers
 - Using the great **SVG.NET** implementation to replace SVG content: [SVG.NET][1]

### Setup ###

Basically you download the entire source and compile it in Visual Studio 2013 RTM. To be able to compile the source, you need the latest Windows Azure SDK. You can download the latest Windows Azure SDK here: [Windows Azure SDK for .NET Download Page ][2]

Before you compile the solution, you have to set some important configuration values to make the solution work. Mainly those settings are SQL Server connections. You have to change:

 - The SQL Server connection string for the SVG-Admin interface
 - The SQL Server Settings for the Worker Role


#### Change the SQL Server connection settings for the SVG-Admin Interface ###

The changes need to be done in Web.config. Please search for "BadgesEntities" and replace the following parts:

    <add name="BadgesEntities" connectionString="metadata=res://*/Models.Badges.csdl|res://*/Models.Badges.ssdl|res://*/Models.Badges.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=[DATABASESERVER];user id=[USERNAME];password=[PASSWORD];MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
    

 - Replace **[SERVERNAME]** with your SQL Server instance (without the square brackets)
 - Replace **[USERNAME]** with your SQL Server user (again without the square brackets)
 - Replace **[PASSWORD]** with your SQL Server user, password (again without the square brackets)

#### Change the SQL Server connection settings for the Azure Worker Role ###

Expand the Worker Role project (That one with the cloud symbol) and look for the "BadgeService" entry. Right click on "BadgeService" and select "Properties". Select the "Settings" tab and change the value "SqlConnection" according to your SQL Server/Azure SQL settings.
 
#### Create the SQL Server/Azure SQL database and the Badges table ###

First of all, you need to create a local SQL Server database or an Azure SQL database. If you want to test locally, you can install the latest version of SQL Server Express. You can download SQL Server 2012 Express here: [SQL Server Express 2012 Download][3]

There is a folder called "SQL" located inside the "BadgeService" project. You can find 2 files there:

 - SQLAzure.sql (Use that to execute it against an Azure SQL database)
 - SQLServerLocal.sql (Use that to execute it against a local SQL Server installation)

The nice thing is, that you can execute the scripts from within Visual Studio. You can also fire-up SSMS (SQL Sever Management Studio) and connect to your Azure SQL database or to your local database. In both cases execute the appropriate script against your database.

#### Access the service locally from within Windows Phone emulator 8 ###

Developing Azure servcies and testing API's locally is a PITA. To be able to access the badge service from within Windows Phone emulator you need to open a command session as administrator and execute the following command:

        C:\Windows\system32>netsh interface portproxy add v4tov4 listenport=800 connecta
    ddress=127.255.0.0 connectport=81 protocol=tcp

This will allow you to access the Badges service like this: http://[YOURPCNAME]:800/badgeservice/[CONTROLLERACTION] . It will work also in your local network. All credits for this tip go to **Sandro Di Mattia**: [Remotely accessing the Windows Azure Compute Emulator][4]

### How to use it ###

Using the Badge service to geneate badges on the fly is pretty easy. Just add a reference to this two projects to your .NET, Windows Phone 8 or WindowsRT app:

 - Model (Contains all the base classes to handle the badge data)
 - BadgeServicePCLClient (send requests against the badge service)

Here is a short sample (taken from the Windows Phone sample):

    BadgeServiceClient cl = new BadgeServiceClient("http://[YOURSERVICEHOST]:800/badges");

            BadgeGenData data = new BadgeGenData();

            data.BadgeName = "test";

            data.BadgeData.Add(new BadgeTextContent(){ElementName = "text3003", Value=this.txtBadgeparam.Text});

            var imageData = await cl.CreateBadgeImage(data);

            if(imageData != null)
            {
                if(imageData.Length > 0)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                                        
                    try
                    {

                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                           
                            bitmapImage.SetSource(ms);

                            this.imgBadge.Source = bitmapImage;
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        
                    }
                }
            }

 
First you create the BadgeServiceClient and add a valid url. In this case you can see the local service URL, when testing the service locally, after applying the netsh settings. Next thing that needs to be done is to create a new instance of the BadgeGenData class. This class is responsible to replace text in a specific SVG saved in the badges table. You set the BadgeName and create a bunch of properties key/value pairs to replace SVG text-properties with your custom text. Then you call the CreateBadgeImage method, that will return the bytes of the generated PNG file. You can convert these bytes to an imageSource and assign it to an image control or save it for later use.

You can contact me on Twitter: [@awsomedevsigner][5]

Read more on my [blog][6]


  [1]: https://github.com/vvvv/SVG
  [2]: http://www.windowsazure.com/en-us/downloads/?sdk=net "Windows Azure SDK for .NET Download Page"
  [3]: http://www.microsoft.com/en-us/download/details.aspx?id=29062
  [4]: http://fabriccontroller.net/blog/posts/remotely-accessing-the-windows-azure-compute-emulator/
  [5]: https://twitter.com/AWSOMEDEVSIGNER
  [6]:http://awsomedevsiger.cloudapp.net/2013/10/30/automate-svg-templating-on-windows-azure-the-azure-badge-generator/
