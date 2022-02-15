# UsersTable
## Developer : Nader Tavana
Implementation of a Web application (MVC) which displays a table of Users containing the following information:
- First Name
- Last Name
- Street Name
- House Number
- Apartment Number (optional)
- Postal Code
- Town
- Phone Number
- Date of Birth
- Age (read-only)

The application allow a user to edit the data , add new users and delete existing ones. The data is stored in the XML file. The file is located in the Repository directory.
There are three buttons for create, update and delete. When we press the mentioned buttons we can manipulate data. In the new page there will be two buttons "Save" and "Cancel".
When the "Save" button is pressed changes made by a user will be stored in the XML file.
Pressing the "Cancel" button discards user changes and causes the table to be refreshed based on the data stored in the XML file.
After the first startup of the application the table is empty.

The application is implemented using **.NET Core 3.1 MVC**
