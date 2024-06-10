using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //must include in every controller file

namespace controller
{
    public class viewSparePartController : abstractController
    {
        string sqlCmd;
        private readonly Database _database;

        public viewSparePartController(Database database = null)
        {
            _database = database ?? new Database();
            sqlCmd = "";
        }

        public DataTable GetInfo(string num) //part num
        {
            sqlCmd =
                $"SELECT * FROM spare_part x, product y, supplier z WHERE x.partNumber = y.partNumber AND x.partNumber =\'{num}\' AND x.supplierID = z.supplierID";
            DataTable dt = _database.ExecuteDataTable(sqlCmd);
            return dt;
        }

        public Boolean IsFavourite(string num, string id) //part num, customer id
        {
            favouriteController c = new favouriteController();
            return c.IsFavourite(num, id);
        }

        public Boolean AddToFavourite(string num, string id)
        {
            favouriteController c = new favouriteController();
            return c.AddToFavourite(num, id);
        }

        public Boolean RemoveFavourite(string num, string id)
        {
            favouriteController c = new favouriteController();
            return c.RemoveFromFavourite(num, id);
        }

        public Boolean AddToCart(string id, string num, int qty, Boolean isLM) //customer id,part num, quantity
        {
            //get cart id of the customer first
            sqlCmd =
                $"SELECT cartID FROM cart x, customer_account y where x.customerAccountID = y.customerAccountID AND y.customerID = \'{id}\'";
            DataTable dt = _database.ExecuteDataTable(sqlCmd);
            string cartID = dt.Rows[0][0].ToString();

            //get item id
            sqlCmd =
                $"SELECT itemID from product x, spare_part y where x.partNumber = y.partNumber AND y.partNumber = \'{num}\'";
            dt = _database.ExecuteDataTable(sqlCmd);
            string itemID = dt.Rows[0][0].ToString();

            //check in cart or not first
            sqlCmd = $"SELECT * FROM product_in_cart WHERE itemID = '{itemID}' AND cartID = '{cartID}'";
            dt = _database.ExecuteDataTable(sqlCmd);
            Boolean isInCart = (dt.Rows.Count > 0);

            if (isInCart)
            {
                //get qty already in cart first
                sqlCmd =
                    $"SELECT quantity FROM product_in_cart WHERE itemID = '{itemID}' AND cartID = '{cartID}'";
                dt = _database.ExecuteDataTable(sqlCmd);
                int qtyAlreadyInCart = int.Parse(dt.Rows[0][0].ToString());

                //new qty in cart
                int newQty = qty + qtyAlreadyInCart;

                //update cart value
                sqlCmd =
                    $"UPDATE product_in_cart SET quantity = @qty WHERE itemID = \'{itemID}\' AND cartID = \'{cartID}\'";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@qty", newQty }
                };

                try
                {
                    _database.ExecuteNonQueryCommand(sqlCmd, parameters);
                }
                catch (Exception ex)
                {
                    Log.LogException(ex, "viewSparePartController");
                    return false;
                }

                return deductQtyInDB(itemID, num, qty, isLM);
            }
            else
            {
                //add to cart
                sqlCmd = "INSERT INTO product_in_cart VALUES (@cID, @iID, @qty,@date)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@cID", cartID },
                    { "@iID", itemID },
                    { "@qty", qty },
                    { "@date", DateTime.Now.ToString("dd/MM/yyyy") }
                };

                try
                {
                    _database.ExecuteNonQueryCommand(sqlCmd, parameters);
                }
                catch (Exception ex)
                {
                    Log.LogException(ex, "viewSparePartController");
                    return false;
                }

                if (deductQtyInDB(itemID, num, qty,
                        isLM)) //deduct qty in db as stated in function requirement that when add item to cart, deduct qty in db
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Boolean deductQtyInDB(string itemID, string partNum, int qty, Boolean isLM)
        {
            //get qty in spare_part table first
            sqlCmd = $"SELECT quantity FROM spare_part WHERE partNumber = \'{partNum}\'";
            DataTable dt = _database.ExecuteDataTable(sqlCmd);
            int qtyInDB = int.Parse(dt.Rows[0][0].ToString());

            //new qty in spare_part
            qtyInDB -= qty;

            //update spare_part table
            sqlCmd = $"UPDATE spare_part SET quantity = @qty WHERE partNumber = @partNum";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@qty", qtyInDB },
                { "@partNum", partNum }
            };

            try
            {
                _database.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception ex)
            {
                Log.LogException(ex, "viewSparePartController");
                return false;
            }

            //get qty in product table 
            sqlCmd = !isLM
                ? $"SELECT OnSaleQty FROM product WHERE itemID = \'{itemID}\'"
                : $"SELECT LM_OnSaleQty FROM product WHERE itemID = \'{itemID}\'";

            dt = _database.ExecuteDataTable(sqlCmd);
            qtyInDB = int.Parse(dt.Rows[0][0].ToString());

            //new qty in product
            qtyInDB -= qty;

            //update product table
            sqlCmd = !isLM
                ? $"UPDATE product SET OnSaleQty = @qty WHERE itemID = @id"
                : $"UPDATE product SET LM_OnSaleQty = @qty WHERE itemID = @id";

            parameters = new Dictionary<string, object>
            {
                { "@qty", qtyInDB },
                { "@id", itemID }
            };

            try
            {
                _database.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception ex)
            {
                Log.LogException(ex, "viewSparePartController");
                return false;
            }

            return true;
        }
    }
}