using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Users.Models;

namespace Users.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly XDocument doc = XDocument.Load("./Repository/UserData.xml"); 

        /// <summary>
        /// Calculate Age of User
        /// </summary>
        public int CalculateAge(DateTime dob)
        {
            DateTime dt = DateTime.Today;
            if (dt.Day < dob.Day)
            {
                dt = dt.AddMonths(-1);
            }

            int months = dt.Month - dob.Month;
            if (months < 0)
            {
                dt = dt.AddYears(-1);
                
            }

            int years = dt.Year - dob.Year;
            return years;
        }

        /// <summary>
        /// Getting All the Users
        /// </summary>
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = from e in doc.Descendants("user")
                                          select new User()
                                          {
                                              Id = (int)e.Attribute("id"),
                                              FirstName = (string)e.Attribute("firstname"),
                                              LastName = (string)e.Attribute("lastname"),
                                              StreetName = (string)e.Attribute("streetname"),
                                              HouseNumber = (string)e.Attribute("housenumber"),
                                              ApartmentNumber = (string)e.Attribute("apartmentnumber"),
                                              PostalCode = (string)e.Attribute("postalcode"),
                                              Town = (string)e.Attribute("town"),
                                              PhoneNumber = (string)e.Attribute("phonenumber"),
                                              DateOfBirth = DateTime.ParseExact((string)e.Attribute("dateofbirth"), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                              Age = (int)e.Attribute("age")
                                          };
            return users;

        }


        /// <summary>
        /// Creating a User
        /// </summary>
        public void AddUser(User user)
        {
            string dateString = String.Format("{0:dd/MM/yyyy}", user.DateOfBirth);
            int age = CalculateAge(user.DateOfBirth);

            List<int> ids = doc.Descendants("user")
                                        .Select(e => e.Attribute("id").Value)
                                        .Select(s => int.Parse(s)).ToList();
            
            int id = 1;
            if (ids.Any())
            {
                ids.Sort();
                id = ids.Last() + 1;
            }

            XElement root = new XElement("user");
            root.Add(new XAttribute("id", id));
            root.Add(new XAttribute("firstname", user.FirstName));
            root.Add(new XAttribute("lastname", user.LastName));
            root.Add(new XAttribute("streetname", user.StreetName));
            root.Add(new XAttribute("housenumber", user.HouseNumber));
            root.Add(new XAttribute("postalcode", user.PostalCode));
            root.Add(new XAttribute("town", user.Town));
            root.Add(new XAttribute("phonenumber", user.PhoneNumber));
            root.Add(new XAttribute("dateofbirth", dateString));
            root.Add(new XAttribute("age", age));
            doc.Element("users").Add(root);

            if (user.ApartmentNumber == null)
            {
                root.Add(new XAttribute("apartmentnumber", "-"));
                doc.Save("./Repository/UserData.xml");
            }
            else
            {
                root.Add(new XAttribute("apartmentnumber", user.ApartmentNumber));
                doc.Save("./Repository/UserData.xml");
            }
            
        }


        /// <summary>
        /// Deleting a User
        /// </summary>
        public void DeleteUser(int id)
        {
            var target = doc.Descendants("user")
                            .Where(e => e.Attribute("id").Value == id.ToString())
                            .FirstOrDefault();
            
            target.Remove();
            doc.Save("./Repository/UserData.xml");

        }


        /// <summary>
        /// Updating User Data
        /// </summary>
        public void UpdateUser(User user)
        {
            string dateString = String.Format("{0:dd/MM/yyyy}", user.DateOfBirth);
            int age = CalculateAge(user.DateOfBirth);

            var target = doc.Descendants("user")
                    .Where(e => e.Attribute("id").Value == user.Id.ToString())
                    .FirstOrDefault();

            target.Attribute("firstname").Value = user.FirstName;
            target.Attribute("lastname").Value = user.LastName;
            target.Attribute("streetname").Value = user.StreetName;
            target.Attribute("housenumber").Value = user.HouseNumber;
            target.Attribute("postalcode").Value = user.PostalCode;
            target.Attribute("town").Value = user.Town;
            target.Attribute("phonenumber").Value = user.PhoneNumber;
            target.Attribute("dateofbirth").Value = dateString;
            target.Attribute("age").Value = age.ToString();

            if(user.ApartmentNumber == null)
            {
                user.ApartmentNumber = "-";
            }
            else target.Attribute("apartmentnumber").Value = user.ApartmentNumber;

            doc.Save("./Repository/UserData.xml");

        }


        /// <summary>
        /// Returning a Single User from Database
        /// </summary>
        public User GetSingleUser(int id)
        {
            var rec = doc.Descendants("user")
                    .Where(e => e.Attribute("id").Value == id.ToString())
                    .FirstOrDefault();

            var user = new User()
            {
                Id = (int)rec.Attribute("id"),
                FirstName = (string)rec.Attribute("firstname"),
                LastName = (string)rec.Attribute("lastname"),
                StreetName = (string)rec.Attribute("streetname"),
                HouseNumber = (string)rec.Attribute("housenumber"),
                ApartmentNumber = (string)rec.Attribute("apartmentnumber"),
                PostalCode = (string)rec.Attribute("postalcode"),
                Town = (string)rec.Attribute("town"),
                PhoneNumber = (string)rec.Attribute("phonenumber"),
                DateOfBirth = DateTime.ParseExact((string)rec.Attribute("dateofbirth"), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                
            };
            
            user.Age = CalculateAge(user.DateOfBirth);
            return user;

        }


        /// <summary>
        /// Check if a User already exists in Database
        /// </summary>
        public bool UserExists(User user)
        {
            return GetAllUsers().Where(u => u.FirstName == user.FirstName)
                                 .Where(u => u.LastName == user.LastName)
                                 .Where(u => u.PostalCode == user.PostalCode)
                                 .Where(u => u.StreetName == user.StreetName)
                                 .Where(u => u.HouseNumber == user.HouseNumber)
                                 .Where(u => u.Town == user.Town)
                                 .Any();
        }


        /// <summary>
        /// Check if an Id exists in the Database
        /// </summary>
        public bool IdExists(int id)
        {
            return  doc.Descendants("user")
                                        .Select(e => e.Attribute("id").Value)
                                        .Select(s => int.Parse(s)).ToList()
                                        .Where(i => i == id)
                                        .Any();
        }

    }
}
