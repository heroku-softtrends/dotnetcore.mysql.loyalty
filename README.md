# dotnetcore.mysql.sample
.Net Core Sample app that can be deployed on Heroku using dotnetcore.mysql.sample

@@ -8,8 +8,8 @@ You've created a new ASP.NET Core MVC project. [Learn what's new](https://go.mic
 
 You need to make the following changes in your Program.cs and project.json to deploy on Heroku
 <br/>
-**Program.cs**
-<br/>
+In **Program.cs**
+
 *   Add UseUrls method and pass args[0] as parameter to start your app. Because Heroku web dyno will start with dynamic port after sucessful deployment. We need to use the same port in code behind also then only your app will start and listen on that port else dotnet runtime will set default port 5000. Thereby we pass port number as parameter with url in Procfile
 <br/>
 public static void Main(string[] args
@@ -24,8 +24,8 @@ public static void Main(string[] args
 
             host.Run();
 }<br/>
-**project.json**
-<br/>
+In **project.json**
+
 *   Add a new property called "outputName": "Your_ProjectName" in buildOptions  
 *   Remove scripts section. It has prepublish and postpublish actions which are not needed
 <br/>
