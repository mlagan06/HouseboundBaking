using HouseboundBaking.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.Data
{
    class UserDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            SQLiteFunctionality SQLite = new SQLiteFunctionality();

            if (!SQLite.TableExists("UserModel"))
            {
                database.CreateTable<UserModel>();
            }
        }

        public bool CreateNewUser(string fullName, string email, string addressLine1, string addressLine2, string city, string county, string country, string postcode, string mobileNumber, string password)
        {
            bool isCreated = false;

            lock (locker)
            {
                var maxPK = database.Table<UserModel>().OrderByDescending(c => c.UserId).FirstOrDefault();

                UserModel NewUser = new UserModel()
                {
                    UserId = (maxPK == null ? 1 : maxPK.UserId + 1),
                    Email = email,
                    FullName = fullName,
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    City = city,
                    County = county,
                    Country = country,
                    Postcode = postcode,
                    MobileNumber = mobileNumber,
                    DateUserCreated = DateTime.UtcNow,
                    Password = password,
                };

                database.Insert(NewUser);

                isCreated = true;
            }

            return isCreated;
        }

        public List<UserModel> GetUserByEmail(string email)
        {
            lock (locker)
            {
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("UserModel"))
                {
                    return null;
                }

                var result = database.Table<UserModel>().Where(x => x.Email == email).ToList();

                if (result.Count < 1)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        public List<UserModel> GetUserByMobileNumber(string mobileNumber)
        {
            lock (locker)
            {
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("UserModel"))
                {
                    return null;
                }

                var result = database.Table<UserModel>().Where(x => x.MobileNumber == mobileNumber).ToList();

                if (result.Count < 1)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        public List<UserModel> GetAllUsers()
        {
            lock (locker)
            {
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("UserModel"))
                {
                    return null;
                }

                var result = database.Table<UserModel>().ToList();

                if (result.Count < 1)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        public List<UserModel> GetUserWithId(int UserId)
        {
            lock (locker)
            {
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("UserModel"))
                {
                    return null;
                }

                var result = database.Table<UserModel>().Where(x => x.UserId == UserId).ToList();

                if (result.Count < 1)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        public List<UserModel> DoesUserExist(string email, string password)
        {
            lock (locker)
            {
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("UserModel"))
                {
                    return null;
                }

                var result = database.Table<UserModel>().Where(x => x.Email == email && x.Password == password).ToList();

                if (result.Count < 1)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

    }
}