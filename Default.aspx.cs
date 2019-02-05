using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prueba
{
    public partial class _Default : Page
    {
        SqlConnection connection;
        bool isReady;
        ArrayList data_customer_primary;
        ArrayList data_customer_secondary;
        bool afterAdd;
        ArrayList data_to_checkboxlist;

        public bool IsReady
        {
            get
            {
                return isReady;
            }

            set
            {
                isReady = value;
            }
        }

        public bool AfterAdd
        {
            get
            {
                return afterAdd;
            }

            set
            {
                afterAdd = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(System.Configuration.ConfigurationManager.
            ConnectionStrings["ConnectionIndividuals"].ConnectionString);
            connection.Open();


            if (!IsPostBack)
            {
                //set hidden customer details
                MainDetails.Visible = false;
                ConfirmData.Visible = false;
                //set false afteradd
                afterAdd = false;
                RefillCheckBox();  
                //fill listbox
                ArrayList Genders = new ArrayList();

                Genders.Add("Female");
                Genders.Add("Male");
                Genders.Add("Other");

                Gender.DataSource = Genders;
                Gender.DataBind();
            }
        }

        protected void RefillCheckBox()
        {
            data_to_checkboxlist = new ArrayList();
            CheckBoxList1.Items.Clear();
            SqlCommand cmd = new SqlCommand("Select * FROM Individual", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                System.Diagnostics.Debug.WriteLine("{0} {1} {2} {3}",
                        reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                CheckBoxList1.Items.Add(new ListItem(reader.GetString(1) + " " + reader.GetString(2), reader.GetInt32(0).ToString()));
                data_to_checkboxlist.Add(new ListItem(reader.GetString(1) + " " + reader.GetString(2), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            isReady = false;
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> selectionList = new List<string>();

            // Retrieve the selection
            selectionList = hdnSelectedValue.Value.Split(new string[] { "," },
                                StringSplitOptions.RemoveEmptyEntries).ToList();            
            // If new, add to the list
            if (selectionList.Count <= 0)
            {
                selectionList.Add(CheckBoxList1.SelectedItem.Value);
                id_primary.Value = CheckBoxList1.SelectedItem.Value;
                MainDetails.Visible = false;
            }                
            else
            {
                // Find the unchecked items and remove them
                CheckBoxList1.Items.OfType<ListItem>().ToList().Where(a => a.Selected != true).
                        ToList().ForEach(a => selectionList.Remove(a.Value));
                // Find the new selected items and add to the list
                var selectedItems = CheckBoxList1.Items.OfType<ListItem>().ToList().
                                           Where(a => a.Selected == true &&
                                                            !selectionList.Contains(a.Value)).
                                           ToList();
                if (selectedItems.Count > 0)
                {
                    if (selectionList.Count > 1)
                    {
                        try
                        {
                            CheckBoxList1.Items.FindByValue(selectionList[0]).Selected = false;
                            CheckBoxList1.Items.FindByValue(selectedItems.First().Value).Selected = true;
                            CheckBoxList1.Items.FindByValue(selectionList[1]).Selected = true;
                        }
                        catch
                        {
                            Console.WriteLine("Item does not exist");
                        }
                        CheckBoxList1.Items.FindByValue(selectedItems.First().Value).Selected = true;
                        selectionList[0] = selectionList[1];
                        selectionList[1] = (selectedItems.First().Value);
                    }
                    else
                    {
                        selectionList.Add(selectedItems.First().Value);
                    }
                    //set visible customer details
                    MainDetails.Visible = true;
                    //retrieve data
                    try
                    {
                        data_customer_primary = getCustomerData(int.Parse(selectionList[0]), ref Customer_data_primary);
                        data_customer_secondary = getCustomerData(int.Parse(selectionList[1]), ref Customer_data_secondary);
                    }
                    catch
                    {
                        Console.WriteLine("Item does not exist");
                    }
                    try
                    {
                        Customer ctm_primary = (Customer)data_customer_primary[0];
                        Customer ctm_secondary = (Customer)data_customer_secondary[0];
                        id_primary.Value = ctm_primary.RecordNumber;
                        id_secondary.Value = ctm_secondary.RecordNumber;
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }                    
                    Customer_data_primary.DataBind();
                    Customer_data_secondary.DataBind();
                    isReady = true;
                    ConfirmData.Visible = true;
                    afterAdd = false;
                    foreach (Customer ctm in data_customer_primary)
                    {
                        FirstName.Text = ctm.FirstName;
                        LastName.Text = ctm.LastName;
                        switch (ctm.Gender)
                        {
                            case "f": Gender.SelectedIndex = 0; break;
                            case "m": Gender.SelectedIndex = 1; break;
                            case "x": Gender.SelectedIndex = 3; break;
                        }
                    }

                }
                else
                {
                    //set hidden customer details
                    MainDetails.Visible = false;
                    ConfirmData.Visible = false;
                    isReady = false;
                    afterAdd = false;
                }

            }
            // update the label in comma separated value
            hdnSelectedValue.Value = string.Join(",", selectionList.ToArray());
            System.Diagnostics.Debug.WriteLine(hdnSelectedValue.Value);
        }
        protected ArrayList getCustomerData(int id, ref Repeater data)
        {
            ArrayList values = new ArrayList();
            SqlCommand cmd = new SqlCommand("Select Individual.*," +
                "Address.*," +
                "IndividualAddress.* " +
                "from Individual " +
                "LEFT JOIN IndividualAddress on Individual.RecordNumber = IndividualAddress.RecordNumber " +
                "LEFT JOIN Address on IndividualAddress.AddressId = Address.AddressId WHERE Individual.RecordNumber = @id", connection);
            cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                values.Add(new Customer(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(5), reader.GetString(6), reader.GetString(7)));
            }
            reader.Close();
            data.DataSource = values;
            data.DataBind();
            return values;
        }

        protected void Merge_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                List<string> selectionList = new List<string>();
                selectionList = hdnSelectedValue.Value.Split(new string[] { "," },
                                StringSplitOptions.RemoveEmptyEntries).ToList();
                data_customer_primary = getCustomerData(int.Parse(selectionList[0]), ref Customer_data_primary);
                data_customer_secondary = getCustomerData(int.Parse(selectionList[1]), ref Customer_data_secondary);
                Customer ctm_primary = (Customer)data_customer_primary[0];
                Customer ctm_secondary = (Customer)data_customer_secondary[0];                
                SqlCommand cmd = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");
                
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                try
                {         
                    cmd.CommandText =
                        "UPDATE Individual SET FirstName = @firstname, LastName = @lastname, Gender = @gender WHERE Individual.RecordNumber = @id";

                    switch (Gender.SelectedIndex)
                    {
                        case 0: cmd.Parameters.Add("gender", SqlDbType.Char).Value = 'f'; break;
                        case 1: cmd.Parameters.Add("gender", SqlDbType.Char).Value = 'm'; break;
                        case 3: cmd.Parameters.Add("gender", SqlDbType.Char).Value = 'x'; break;
                        default: return;
                    }
                    cmd.Parameters.Add("firstname", SqlDbType.NVarChar).Value = FirstName.Text;
                    cmd.Parameters.Add("lastname", SqlDbType.NVarChar).Value = LastName.Text;                    
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = ctm_primary.RecordNumber;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        "UPDATE IndividualAddress SET RecordNumber = @id_primary WHERE IndividualAddress.RecordNumber = @id_secondary";
                    cmd.Parameters.Add("id_primary", SqlDbType.Int).Value = ctm_primary.RecordNumber;
                    cmd.Parameters.Add("id_secondary", SqlDbType.Int).Value = ctm_secondary.RecordNumber;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        "Delete FROM Individual WHERE Individual.RecordNumber = @id_delete";
                    cmd.Parameters.Add("id_delete", SqlDbType.Int).Value = ctm_secondary.RecordNumber;
                    cmd.ExecuteNonQuery();

                    //set hidden customer details
                    MainDetails.Visible = false;                    
                    // Attempt to commit the transaction.
                    transaction.Commit();
                    RefillCheckBox();
                    //Clear states
                    Customer_data_primary.DataSource = null;
                    Customer_data_primary.DataBind();
                    Customer_data_secondary.DataSource = null;
                    Customer_data_secondary.DataBind();
                    hdnSelectedValue.Value = "";
                    //get customer result
                    getCustomerData(int.Parse(ctm_primary.RecordNumber), ref Customer_data_result);
                    Customer_data_result.DataBind();
                    //mostrar resultado
                    afterAdd = true;                    
                    Console.WriteLine("Both records are written to database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }


            }
        }        

        protected void Go_Click(object sender, EventArgs e)
        {
            if (Filter.Text.Length > 2)
            {
                data_to_checkboxlist = new ArrayList();
                foreach (ListItem item in CheckBoxList1.Items)
                {
                    data_to_checkboxlist.Add(new ListItem(item.Text, item.Value));
                }
                CheckBoxList1.Items.Clear();
                foreach (ListItem item in data_to_checkboxlist)
                {
                    if (item.Text.ToLower().Contains(Filter.Text.ToLower()))
                    {
                        CheckBoxList1.Items.Add(item);
                    }
                }


            }else
            {
                int idx1 = -1;
                int idx2 = -1;
                if (id_primary.Value.Length > 0)
                {
                    idx1 = int.Parse(id_primary.Value);                    
                }
                if (id_secondary.Value.Length > 0)
                {
                    idx2 = int.Parse(id_secondary.Value);
                }
                RefillCheckBox();
                if (idx1 >= 0)
                {
                    CheckBoxList1.Items[idx1-1].Selected = true;
                }
                if (idx2 >= 0)
                {
                    CheckBoxList1.Items[idx2-1].Selected = true;
                }
            }
        }
    }
}