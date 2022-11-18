using Microsoft.VisualBasic.ApplicationServices;
using MongoDBPractice.DA;
using MongoDBPractice.Model;
using System.Windows.Forms;

namespace MongoDBPractice
{
    public partial class MainEntryForm : Form
    {
        UserDA userDA = null;
        int selectedIDx = -1;
        bool isEdit=true;
        public MainEntryForm()
        {
            InitializeComponent();
            Connections.LoadConnection();
            userDA = new UserDA();
            this.btnEdit.Text = "Edit";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginID.Text) || string.IsNullOrWhiteSpace(LoginID.Text)) return;
                var user=userDA.GetByLoginID(LoginID.Text);
                if (user == null)
                {
                    userDA.Insert
                    (
                        new UserModel() 
                        {
                            LoginId= LoginID.Text,
                            Password=Password.Text,
                            FirstName= FirstName.Text, 
                            LastName= LastName.Text,
                            Email= Email.Text,
                        }
                    );
                }
                refresh();
                clearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEdit && selectedIDx > -1)
                {
                    loadSelected();
                    isEdit = false;
                    this.btnEdit.Text = "Update";
                }
                else
                {
                    string id = this.datagridview.Rows[selectedIDx].Cells[0].Value.ToString();
                    userDA.Update
                    (
                        new UserModel()
                        {
                            Id= id,
                            LoginId = LoginID.Text,
                            Password = Password.Text,
                            FirstName = FirstName.Text,
                            LastName = LastName.Text,
                            Email = Email.Text,
                        }
                    );
                    refresh();
                    clearAll();
                    isEdit = true;
                    this.btnEdit.Text = "Edit";
                }
            }
            catch (Exception ex)
            {
                isEdit = true;
                this.btnEdit.Text = "Edit";
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = this.datagridview.Rows[selectedIDx].Cells[0].Value.ToString();
                userDA.Delete(id);
                refresh();
                clearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void clearAll()
        {
            LoginID.Text = string.Empty;
            Password.Text = string.Empty;
            FirstName.Text = string.Empty;
            LastName.Text = string.Empty;
            Email.Text = string.Empty;
        }
        private void refresh()
        {
            userDA.GetAll();
            datagridview.DataSource = userDA.GetAll();
            this.datagridview.Columns["Id"].Visible = false;
            this.datagridview.Columns["Password"].Visible = false;
        }
        private void loadSelected()
        {
            if (selectedIDx > -1)
            {
                this.LoginID.Text = this.datagridview.Rows[selectedIDx].Cells[1].Value.ToString();
                this.Password.Text = this.datagridview.Rows[selectedIDx].Cells[2].Value.ToString();
                this.FirstName.Text = this.datagridview.Rows[selectedIDx].Cells[3].Value.ToString();
                this.LastName.Text = this.datagridview.Rows[selectedIDx].Cells[4].Value.ToString();
                this.Email.Text = this.datagridview.Rows[selectedIDx].Cells[5].Value.ToString();
            }
        }
        private void datagridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedIDx = e.RowIndex;
        }
    }
}