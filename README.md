AzureDemo-WebSites-ModernApps
==============

Walks through the deployment of a fully functional MVC4 + SignalR + Knockout.Js solution called ChirpyR.

Original ChirpyR Author: Sumit Maitra [@sumitkm](http://www.twitter.com/sumitkm)


# Steps #
1.   Create new WebSite with Database.
2.   Provision new Azure SQL Database server and database.
3.   Update the 2 connection strings in the MVC4 projects web.config to consume the Azure SQL Database created in the above step.
4.   Set up Git publishing.
5.   Navigate to solution location on local disk.
6.   `git init`
7.   `git add .`
8.   `git commit -m "initial commit"`
9.   `git remote add azure <GIT_URI_HERE>`
10.   `git push azure master`
