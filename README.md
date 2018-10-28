# RecordCollection
You are now ready to clone/download this project.  I have GitHub Desktop installed so I click on the download option to open it with Desktop. You are almost ready to debug the project locally.  A couple of things first... 

When you open the solution in Visual Studio, it will complain that the project references NuGet packages that are missing on your computer.  In order to rectify this, click 'Tools' > 'NuGet Package Manager' > 'Manage NuGet Packages For Solution'.  You should then see a message with a yellow background saying, "Click to restore from your online package sources".  Follow this instruction and click "Restore".

Unfortunately, you will also have to edit two hard coded file paths in the code.  In Quiz.cs line 20, change the file path so that it matches your own file path.  The file name remains the same.  In CdXml2.cs, do the same in lines 20 and 51.  I will fix this soon.

You should now be ready to have a look into my record collection, to see which artists and producers feature the most, and to see who I rate the most, with the help of table and chart visualisations.  There is also a quiz for you to test your musical knowledge.  I use an xml document rather than relational tables in a database; the latter would be my preferred option and I would use IDs but I just wanted to get something up and running quickly. 
