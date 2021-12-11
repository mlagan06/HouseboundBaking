using HouseboundBaking.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.Data
{
    public class ProductsDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;// = DependencyService.Get<ISQLite>().GetConnection();

        //  ObservableCollection<Quantity> QuantityList = new ObservableCollection<Quantity>();
        ObservableCollection<ProductModel> AllProducts = new ObservableCollection<ProductModel>();
        ObservableCollection<ProductModel> WineList = new ObservableCollection<ProductModel>();
        ObservableCollection<ProductModel> BeerList = new ObservableCollection<ProductModel>();
        ObservableCollection<ProductModel> VodkaList = new ObservableCollection<ProductModel>();
        //   public ObservableCollection<Quantity> List_Quantites = new ObservableCollection<Quantity>();

        public ProductsDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            //todo - above and below lines are both calling new db connection, could refactor code so only one call
            SQLiteFunctionality SQLite = new SQLiteFunctionality();

            //if (!SQLite.TableExists("QuantityModel"))
            //{
            //    database.CreateTable<Quantity>();
            //   // CreateQuantityList();
            //   // PopulateQuantitytModelDBTable();
            //}

            if (!SQLite.TableExists("ProductModel"))
            {
                database.CreateTable<ProductModel>();
                CreateProductsList();
                PopulateProductModelDBTable();
            }
        }

        //should these be public
        //TODO CHANGE TO INT, TO CHECK FAIL SAFE IF RETURNS VALUE, SUCCESSFULLY CREATED
        public void PopulateProductModelDBTable()
        {
            lock (locker)
            {
                //var maxPK = database.Table<ProductModel>().OrderByDescending(c => c.ProductId).FirstOrDefault();

                foreach (var p in AllProducts)
                {
                    ProductModel NewProduct = new ProductModel()
                    {
                        //TODO WHEN ADDED ALL PROUDCTS, POPULATE ID FROM BELOW LINE, BUT AS WILL NEED TO KNOW IDS FOR TESTING KEEP IT OUT FOR NOW,
                        //AND USE ID ADDED FROM LISTS
                        // ProductId = (maxPK == null ? 1 : maxPK.ProductId + 1),
                        ProductId = p.ProductId,
                        BrandName = p.BrandName,
                        Description = p.Description,
                        Genre = p.Genre,
                        Grape = p.Grape,
                        Image = p.Image,
                        // ListQuantites = p.ListQuantites,
                        Price = p.Price,
                        Quantity = p.Quantity,
                        // SelectedQuantity = p.SelectedQuantity,
                        Size = p.Size,
                        SubTotalForItem = p.SubTotalForItem
                    };

                    database.Insert(NewProduct);

                    //return NewProduct.ProductId;
                }
            }
        }

        public void CreateProductsList()
        {
            AllProducts = new ObservableCollection<ProductModel>();

            //  List_Quantites = PickerService.GetQuantitiesForProductPage();

            PopulateAllLists();

            ////AllProducts.Add(WineList);
            ////AllProducts.Add(BeerList);
            ////AllProducts.Add(VodkaList);

            foreach (var w in WineList)
            {
                AllProducts.Add(w);
            }
            foreach (var b in BeerList)
            {
                AllProducts.Add(b);
            }
            foreach (var v in VodkaList)
            {
                AllProducts.Add(v);
            }
        }

        public void PopulateAllLists()
        {
            PopulateWineList();
            //   PopulateBeerList();
            //  PopulateVodkaList();
        }

        public void PopulateWineList()
        {
            WineList = new ObservableCollection<ProductModel>();
            WineList.Add(new ProductModel { ProductId = 1, BrandName = "Full Loaf", Grape = "White Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Large", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 2, BrandName = "1/2 Loaf", Grape = "White Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Medium", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 3, BrandName = "Full Wheaten Side", Grape = "Brown Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Large", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 4, BrandName = "1/2 Wheaten Side", Grape = "Brown Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Medium", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 5, BrandName = "Dozen Scones", Grape = "Plain Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Large", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 6, BrandName = "1/2 Dozen Scones", Grape = "Plain Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Medium", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 7, BrandName = "Fruit Loaf", Grape = "Fruit Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Large", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 8, BrandName = "1/2 Fruit Loaf", Grape = "Fruit Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Medium", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 9, BrandName = "2 Soda Farls", Grape = "Plain Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Large", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 10, BrandName = "1 Soda Farl", Grape = "Plain Bread", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Medium", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 11, BrandName = "4 Cream Buns", Grape = "Plain", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Large", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Bread" });
            WineList.Add(new ProductModel { ProductId = 12, BrandName = "2 Cream Buns", Grape = "Plain", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "Medium", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Bread" });


            //WineList = new ObservableCollection<ProductModel>();
            //WineList.Add(new ProductModel { ProductId = 1, BrandName = "Mc guigans", Grape = "Jammy Red", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 2, BrandName = "Mc Guigans", Grape = "Chardonnay",Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 3, BrandName = "Mc Guigans", Grape = "Merlot",Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 4, BrandName = "Mc Guigans", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 5, BrandName = "Mc Guigans", Grape = "Pinot Grigio", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 6, BrandName = "Mc Guigans", Grape = "Shiraz", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 7, BrandName = "Barefoot", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 8, BrandName = "Barefoot", Grape = "Malbec", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 9, BrandName = "Barefoot", Grape = "Pinot Grigio", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 10, BrandName = "Barefoot", Grape = "Jammy Red", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 11, BrandName = "Barefoot", Grape = "Cabernet Sav", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 12, BrandName = "Barefoot", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 13, BrandName = "Barefoot", Grape = "Pink Moscato", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 14, BrandName = "120", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 15, BrandName = "120", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 16, BrandName = "120", Grape = "Cabernet Sav", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 17, BrandName = "120", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 18, BrandName = "19 Crimes", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 20.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 19, BrandName = "19 Crimes", Grape = "Sauv Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 20, BrandName = "19 Crimes", Grape = "Red", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 20.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 21, BrandName = "19 Crimes", Grape = "Cabernet Sav", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 22, BrandName = "Yellow Tail", Grape = "Jammy Red", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 23, BrandName = "Yellow Tail", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 24, BrandName = "Yellow Tail", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 25, BrandName = "Yellow Tail", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 26, BrandName = "Yellow Tail", Grape = "Pinot Grigio", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 27, BrandName = "Yellow Tail", Grape = "Shiraz", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 28, BrandName = "Yellow Tail", Grape = "Pink Moscato", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });


            //WineList.Add(new ProductModel { ProductId = 29, BrandName = "Barefoot", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 30, BrandName = "Barefoot", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 31, BrandName = "Barefoot", Grape = "Pinot Grigio", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 32, BrandName = "Barefoot", Grape = "Cabernet Sav", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 33, BrandName = "Barefoot", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 34, BrandName = "Brancott Estate", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 35, BrandName = "Brancott Estate", Grape = "Pinot Grigio", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 36, BrandName = "Blossom Hill", Grape = "Soft & Fruity", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 37, BrandName = "Blossom Hill", Grape = "Crisp & Dry", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 38, BrandName = "Blossom Hill", Grape = "Red", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 39, BrandName = "Villa Marie", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 40, BrandName = "Villa Marie", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 41, BrandName = "Mud House", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 42, BrandName = "Oyster Bay", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 43, BrandName = "Oyster Bay", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 44, BrandName = "Oyster Bay", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 45, BrandName = "Castillo Del Diablo", Grape = "Shiraz", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 46, BrandName = "Castillo Del Diablo", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 47, BrandName = "Castillo Del Diablo", Grape = "Cabernet Sav", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 48, BrandName = "Castillo Del Diablo", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 49, BrandName = "Castillo Del Diablo", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 50, BrandName = "Castillo Del Diablo", Grape = "Shiraz Rose", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });


            //WineList.Add(new ProductModel { ProductId = 51, BrandName = "Stamp", Grape = "Shiraz Rose", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 52, BrandName = "Stamp", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 53, BrandName = "Stamp", Grape = "Pinot Grigio", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 54, BrandName = "Stamp", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 55, BrandName = "Stamp", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 56, BrandName = "Hardys", Grape = "Shiraz", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 57, BrandName = "Hardys", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 58, BrandName = "Gato", Grape = "Carmenere", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 59, BrandName = "Gato", Grape = "Cabernet Rose", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 60, BrandName = "Gato", Grape = "Cabernet Sav", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 61, BrandName = "Gato", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 62, BrandName = "Gato", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 63, BrandName = "Gato", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 64, BrandName = "Gato", Grape = "Pinot Noir", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

            //WineList.Add(new ProductModel { ProductId = 65, BrandName = "Martini", Grape = "Asti", Image = "HomeBakerHat.png", Quantity = 0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });

        }

        public void PopulateBeerList()
        {
            BeerList = new ObservableCollection<ProductModel>();
            BeerList.Add(new ProductModel { ProductId = 16, BrandName = "Bud Btls 330ml", Grape = "", Image = "HomeBakerHat.png", Quantity = 0, Description = "Tasty", Size = "15 Pack", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Beer" });
            BeerList.Add(new ProductModel { ProductId = 17, BrandName = "Coors Btls 330ml", Grape = "", Image = "HomeBakerHat.png", Quantity = 0, Description = "Lovely", Size = "12 Pack", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Beer" });
            BeerList.Add(new ProductModel { ProductId = 18, BrandName = "Harp Cans 440ml", Grape = "", Image = "HomeBakerHat.png", Quantity = 0, Description = "To Die For", Size = "12 Pack", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Beer" });
        }

        public void PopulateVodkaList()
        {
            VodkaList = new ObservableCollection<ProductModel>();
            VodkaList.Add(new ProductModel { ProductId = 19, BrandName = "Smirnoff", Grape = "", Image = "HomeBakerHat.png", Quantity = 0, Description = "Do Well", Size = "1 Lt", Price = 20.00M, SubTotalForItem = 0.00M, Genre = "Vodka" });
        }

        public List<ProductModel> GetWineList()
        {
            lock (locker)
            {
                //fail safe to stop crash if no orders yet made
                //todo look at above function about calling sqlite twice - maybe make global?
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("ProductModel"))
                {
                    return null;
                }

                // var result = database.Table<ProductModel>().Where(x=> x.Genre == "Wine").ToList();
                var result = database.Table<ProductModel>().Where(x => x.ProductId > 0).ToList();

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



        public string GetProductNameForAdminSection(string product_Id)
        {
            lock (locker)
            {

                //fail safe to stop crash if no orders yet made
                //todo look at above function about calling sqlite twice - maybe make global?
                SQLiteFunctionality SQLite = new SQLiteFunctionality();
                if (!SQLite.TableExists("ProductModel"))
                {
                    return null;
                }

                int productId = int.Parse(product_Id);
                var result = database.Table<ProductModel>().Where(x => x.ProductId == productId).FirstOrDefault();
                string productName = string.Empty;

                if (result != null)
                {
                    productName = result.BrandName + ", " + result.Grape;
                }

                return productName;

            }
        }

        ////public void CreateQuantityList()
        ////{
        ////    QuantityList = new ObservableCollection<Quantity>();
        ////    QuantityList.Add(new Quantity() { QuantityId = 1, Key = 1, Value = "0" });
        ////    QuantityList.Add(new Quantity() { QuantityId = 2, Key = 2, Value = "1" });
        ////    QuantityList.Add(new Quantity() { QuantityId = 3, Key = 3, Value = "2" });

        ////    //var quantities = new ObservableCollection<QuantityModel>()
        ////    //{
        ////    //    new QuantityModel() {Key=1, Value="0"},
        ////    //    new QuantityModel() {Key=2, Value="1"},
        ////    //    new QuantityModel() {Key=3, Value="2"},
        ////    //    new QuantityModel() {Key=4, Value="3"},
        ////    //    new QuantityModel() {Key=5, Value="4"},
        ////    //    new QuantityModel() {Key=6, Value="5"},
        ////    //    new QuantityModel() {Key=7, Value="6"},
        ////    //    new QuantityModel() {Key=8, Value="7"},
        ////    //    new QuantityModel() {Key=9, Value="8"},
        ////    //    new QuantityModel() {Key=10, Value="9"},
        ////    //    new QuantityModel() {Key=11, Value="10"}
        ////    //};
        ////}

        //TODO CHANGE TO INT TO CHECK FAIL SAFE, RETURN INT IF SUCCESSFULLY CREATED
        //public void PopulateQuantitytModelDBTable()
        //{
        //    lock (locker)
        //    {
        //        //var maxPK = database.Table<ProductModel>().OrderByDescending(c => c.ProductId).FirstOrDefault();

        //        foreach (var q in QuantityList)
        //        {
        //            Quantity NewQuantity = new Quantity()
        //            {
        //                //TODO WHEN ADDED ALL PROUDCTS, POPULATE ID FROM BELOW LINE, BUT AS WILL NEED TO KNOW IDS FOR TESTING KEEP IT OUT FOR NOW,
        //                //AND USE ID ADDED FROM LISTS
        //                // ProductId = (maxPK == null ? 1 : maxPK.ProductId + 1),
        //                QuantityId = q.QuantityId,
        //                Key = q.Key,
        //                Value = q.Value

        //            };

        //            database.Insert(NewQuantity);

        //            //return NewProduct.ProductId;
        //        }
        //    }
        //}


        public List<QuantityOLD> GetQuantityTable()
        {
            lock (locker)
            {

                //_listView.ItemsSource = db.Table<Orders>().OrderBy(x => x.UserAccountNo).ToList();

                //REMOVE
                // UserId = 12345;

                ////var result = database.Table<Quantity>().OrderBy(x => x.QuantityId).ToList();

                ////if (result.Count < 1)
                ////{
                ////    return null;
                ////}
                ////else
                ////{
                ////    return result;
                ////}
                return null;
            }
        }

        //should these be public
        //todo add failsafe if doesnt update or work throw error, need to re look error check try catch
        //public void UpdateQuantityOfProduct(int ProductId, int NewQuantity)
        //{
        //    lock (locker)
        //    {
        //        //NOTE: TODO: to stop possible hack, can double check totalprice, against values of all products in order

        //        // database.CreateTable<Order>();

        //        var productToUpdate = database.Table<ProductModel>().Where(c => c.ProductId == ProductId).FirstOrDefault();

        //        productToUpdate.Quantity = NewQuantity;

        //        //OrderModel NewOrder = new OrderModel()
        //        //{
        //        //    OrderModelId = (maxPK == null ? 1 : maxPK.OrderModelId + 1),
        //        //    UserName = userName,
        //        //    TotalPrice = totalPrice,
        //        //    DateOrdered = DateTime.UtcNow,
        //        //    // ConfirmedOrderId = 12345,
        //        //    StripeRefCode = stripeRefCode
        //        //};

        //        database.Update(productToUpdate);

        //        //return NewOrder.OrderModelId;
        //        //  await DisplayAlert(null, "Order Placed for: " + NewOrder.UserName, "OK");
        //        //  await Navigation.PopAsync();
        //        // }
        //    }
        //}
    }
}

