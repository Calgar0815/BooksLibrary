This programm is a library software for your books. You can save informations of all of your books and mark the ones which are lented to people. To make saving new books more comfortable it is connected with the German National Library (DNB) and an open library.

It is especially notable that this project was not intended to be published in the first place. And it is still work in progress (like every software before the end of life ;-)). Right now I'm working on a possibility to change records that need changes (e. g. typos etc.).

#Getting started
Before starting the program you need to create the PostgreSQL database with the CreatePostgreSQLDB.sql-script. To use the program you have to open it in Visual Studio and start it once. Then the initial settings.xml will be created in the superior folder to the exe-file. End the program and change the database credentials in the Settings.xml. When starting next time the database will get connected automatically.

#Working with the program
##Search
The first tab is to search for books that are already in the database. It is possible to search by ISBN (10 and 13), title, subtitle, author first name, author family name, series, format, and in time periods. It is also possible to include or exclude searching for books in series. If you want to search in a certain time period you have to unlock this via clicking on the "Use Dates"-checkbox. By default there are two checkboxes set. "Show including lent" means that all books in the database are shown even if they are lent right now. "Only show first author" is to optimize the output. If a book has more than one author only the first one stored in the database will be shown. If you will see all the authors for every author will be shown an own row in the output.

##New entry
The second tab is to write new books to the database. This is possible with using an ISBN (10 and 13) or without an ISBN (click on "Fill in without ISBN"). If you put in an ISBN the DNB and the open library will get checked for information and the information will be shown. You can change the information if necessary and save the new book. If you choose not to use the ISBN (or the book can't be found) you have to fill in everything on your own.
If the format of the book is not in the combobox you cann add it with writing it into the combobox. It will get saved in the database and be ready to get chosen from then on. If you have one ISBN you can calculate the other one with a click on "Calculate". It is also possible to mention that a book is part of a series, including the number in the series. You can either choose one of the existing series or add a new one. When saving the author(s) of a book you have to mention if that author already exists in the ddatabase. There will be a doublette check before saving so that there won't be issues with authors saved in the database several times. A dublette-check will also be carried out when saving the book to prevent from adding a book several times.

##Lent
The third tab is if you lent books to other persons. You can find the book by giving ISBN, Title, Subtitle, or author name. Then you have to select the books to lent in the datagridview and with clicking on "Pull". The chosen book(s) will be shown in the lower datagridview. Here you can add the correct lentdate and the name of the person who has lent the book. With a click on "Lent" the book will be marked as lent. A click on "Remove" will put the book back to the upper datagridview.

##Return
The fourth tab is to administrate lent books. With a click on "Show all" you can show all lent books. If you have marked "Show also former" every lent that has been noted is shown. You can also find lent books by searching with ISBN (13 and 10), book title, and name of the person who lent the book. When clicking on a row in the datagridview and a click on "Return" the lent can be ended. The book(s) are not marked as lented anymore.

#Change Language
The program is available in German and English. Feel free to add other languages. The language strings are located in the LabelTexts.xml file (right next to the settings.xml). This file will be created automatically too. To add new a language add new texts in the LanguageWorker, add the new language in the mLanguagesEnum in Form1.cs, delete the existing LabelTexts.xml, and start the program.