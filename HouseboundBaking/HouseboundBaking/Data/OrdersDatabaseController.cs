using HouseboundBaking.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.Data
{
    class OrdersDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;// = DependencyService.Get<ISQLite>().GetConnection();

        public OrdersDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            //todo - above and below lines are both calling new db connection, could refactor code so only one call
            SQLiteFunctionality SQLite = new SQLiteFunctionality();

            if (!SQLite.TableExists("OrderModel"))
            {
                database.CreateTable<OrderModel>();
            }

            if (!SQLite.TableExists("OrderDetailsModel"))
            {
                database.CreateTable<OrderDetailsModel>();
            }
        }

        //todo should these be public?
        public List<OrderModel> GetAllPreviousOrders()
        {
            lock (locker)
            {
                //fail safe to stop crash if no orders yet made
                //todo look at above function about calling sqlite twice - maybe make global?
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("OrderModel"))
                {
                    return null;
                }

                var result = database.Table<OrderModel>().ToList();

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

        public List<OrderDetailsModel> GetAllOrderDetailsByOrderId(int orderModelId)
        {
            lock (locker)
            {
                //fail safe to stop crash if no orders yet made
                //todo look at above function about calling sqlite twice - maybe make global?
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("OrderDetailsModel"))
                {
                    return null;
                }

                var result = database.Table<OrderDetailsModel>().Where(x => x.OrderModelId == orderModelId).ToList();

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

        public List<OrderModel> GetPreviousOrdersByEmail(string userEmail)
        {
            lock (locker)
            {

                //_listView.ItemsSource = db.Table<Orders>().OrderBy(x => x.UserAccountNo).ToList();

                //REMOVE
                // UserId = 12345;

                var result = database.Table<OrderModel>().Where(x => x.UserName == userEmail).ToList();

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


        public List<OrderModel> GetPreviousOrder(int UserId)
        {
            lock (locker)
            {

                //_listView.ItemsSource = db.Table<Orders>().OrderBy(x => x.UserAccountNo).ToList();

                //REMOVE
                // UserId = 12345;

                var result = database.Table<OrderModel>().OrderBy(x => x.UserName).ToList();

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

        //todo should these be public
        public int RecordNewOrder(decimal totalPrice, string userName, string stripeRefCode, int isUser, string usersFullName)
        {
            lock (locker)
            {
                //NOTE: TODO: to stop possible hack, can double check totalprice, against values of all products in order

                // database.CreateTable<Order>();

                var maxPK = database.Table<OrderModel>().OrderByDescending(c => c.OrderModelId).FirstOrDefault();

                OrderModel NewOrder = new OrderModel()
                {
                    OrderModelId = (maxPK == null ? 1 : maxPK.OrderModelId + 1),
                    UserName = userName,
                    TotalPrice = totalPrice,
                    DateOrdered = DateTime.UtcNow,
                    // ConfirmedOrderId = 12345,
                    StripeRefCode = stripeRefCode,
                    IsUser = isUser,
                    FullName = usersFullName
                };

                database.Insert(NewOrder);

                return NewOrder.OrderModelId;
                //  await DisplayAlert(null, "Order Placed for: " + NewOrder.UserName, "OK");
                //  await Navigation.PopAsync();
                // }
            }
        }

        public void RecordNewOrderDetais(ObservableCollection<ProductModel> productsOrdered, int orderModelId, decimal totalPrice, string userName, string userMobileNumber)
        {
            lock (locker)
            {
                //NOTE: TODO: to stop possible hack, can double check totalprice, against values of all products in order

                foreach (ProductModel p in productsOrdered)
                {
                    var maxPK = database.Table<OrderDetailsModel>().OrderByDescending(c => c.OrderModelId).FirstOrDefault();

                    OrderDetailsModel NewOrder = new OrderDetailsModel()
                    {
                        OrderDetailsId = (maxPK == null ? 1 : maxPK.OrderModelId + 1),
                        ProductId = p.ProductId,
                        UserName = userName,
                        OrderModelId = orderModelId,
                        Quantity = p.Quantity,
                        PriceOfItem = p.Price,
                        SubtotalForThisItem = p.Price * Convert.ToDecimal(p.Quantity),
                        TotalPriceForAllProductsInOrder = totalPrice,
                        DateOrdered = DateTime.UtcNow,
                        UserMobileNumber = userMobileNumber
                    };

                    database.Insert(NewOrder);
                }
            }
        }
    }
}
