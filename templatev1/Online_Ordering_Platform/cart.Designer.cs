
namespace templatev1.Onsale
{
    partial class cart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.palLoc = new System.Windows.Forms.Panel();
            this.lblLoc = new System.Windows.Forms.Label();
            this.palDate = new System.Windows.Forms.Panel();
            this.lblUid = new System.Windows.Forms.Label();
            this.lblTimeDate = new System.Windows.Forms.Label();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.btnEditQty = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.lblTitTotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDBTotal = new System.Windows.Forms.Label();
            this.palNav = new System.Windows.Forms.Panel();
            this.palSelect4 = new System.Windows.Forms.Panel();
            this.palSelect2 = new System.Windows.Forms.Panel();
            this.palSelect1 = new System.Windows.Forms.Panel();
            this.picBWMode = new System.Windows.Forms.PictureBox();
            this.btnProFile = new System.Windows.Forms.Button();
            this.palSelect3 = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.lblCorpName = new System.Windows.Forms.Label();
            this.btnFunction4 = new System.Windows.Forms.Button();
            this.btnFunction3 = new System.Windows.Forms.Button();
            this.btnFunction2 = new System.Windows.Forms.Button();
            this.btnFunction1 = new System.Windows.Forms.Button();
            this.btnBackToSearch = new System.Windows.Forms.Button();
            this.lblDBUnitPrice = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblDBQty = new System.Windows.Forms.Label();
            this.lblDBSpartPartName = new System.Windows.Forms.Label();
            this.lblDBCat = new System.Windows.Forms.Label();
            this.palLoc.SuspendLayout();
            this.palDate.SuspendLayout();
            this.palNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBWMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.SuspendLayout();
            // 
            // palLoc
            // 
            this.palLoc.BackColor = System.Drawing.SystemColors.ControlLight;
            this.palLoc.Controls.Add(this.lblTimeDate);
            this.palLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.palLoc.Location = new System.Drawing.Point(198, 40);
            this.palLoc.Name = "palLoc";
            this.palLoc.Size = new System.Drawing.Size(972, 38);
            this.palLoc.TabIndex = 72;
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoc.Location = new System.Drawing.Point(6, 9);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(44, 22);
            this.lblLoc.TabIndex = 0;
            this.lblLoc.Text = "Cart";
            // 
            // palDate
            // 
            this.palDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.palDate.Controls.Add(this.lblLoc);
            this.palDate.Controls.Add(this.lblUid);
            this.palDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.palDate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.palDate.Location = new System.Drawing.Point(198, 0);
            this.palDate.Margin = new System.Windows.Forms.Padding(2);
            this.palDate.Name = "palDate";
            this.palDate.Size = new System.Drawing.Size(972, 40);
            this.palDate.TabIndex = 71;
            // 
            // lblUid
            // 
            this.lblUid.AutoSize = true;
            this.lblUid.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUid.Location = new System.Drawing.Point(814, 9);
            this.lblUid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUid.Name = "lblUid";
            this.lblUid.Size = new System.Drawing.Size(134, 22);
            this.lblUid.TabIndex = 1;
            this.lblUid.Text = "Uid: LMXXXX";
            // 
            // lblTimeDate
            // 
            this.lblTimeDate.AutoSize = true;
            this.lblTimeDate.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeDate.Location = new System.Drawing.Point(6, 8);
            this.lblTimeDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeDate.Name = "lblTimeDate";
            this.lblTimeDate.Size = new System.Drawing.Size(57, 22);
            this.lblTimeDate.TabIndex = 0;
            this.lblTimeDate.Text = "TIME";
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCreateOrder.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateOrder.Location = new System.Drawing.Point(995, 522);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(142, 65);
            this.btnCreateOrder.TabIndex = 68;
            this.btnCreateOrder.Text = "Create Order";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            // 
            // btnEditQty
            // 
            this.btnEditQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnEditQty.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditQty.Location = new System.Drawing.Point(995, 436);
            this.btnEditQty.Name = "btnEditQty";
            this.btnEditQty.Size = new System.Drawing.Size(142, 65);
            this.btnEditQty.TabIndex = 67;
            this.btnEditQty.Text = "Edit Quantity";
            this.btnEditQty.UseVisualStyleBackColor = false;
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRemoveAll.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAll.Location = new System.Drawing.Point(995, 350);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(142, 65);
            this.btnRemoveAll.TabIndex = 66;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.UseVisualStyleBackColor = false;
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRemoveItem.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveItem.Location = new System.Drawing.Point(995, 262);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(142, 67);
            this.btnRemoveItem.TabIndex = 65;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            // 
            // lblTitTotal
            // 
            this.lblTitTotal.AutoSize = true;
            this.lblTitTotal.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitTotal.Location = new System.Drawing.Point(212, 698);
            this.lblTitTotal.Name = "lblTitTotal";
            this.lblTitTotal.Size = new System.Drawing.Size(84, 31);
            this.lblTitTotal.TabIndex = 64;
            this.lblTitTotal.Text = "Total:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(839, 698);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(119, 31);
            this.lblTotal.TabIndex = 63;
            this.lblTotal.Text = "¥ 130000";
            // 
            // lblDBTotal
            // 
            this.lblDBTotal.AutoSize = true;
            this.lblDBTotal.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.lblDBTotal.Location = new System.Drawing.Point(836, 182);
            this.lblDBTotal.Name = "lblDBTotal";
            this.lblDBTotal.Size = new System.Drawing.Size(53, 23);
            this.lblDBTotal.TabIndex = 60;
            this.lblDBTotal.Text = "Total";
            // 
            // palNav
            // 
            this.palNav.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.palNav.Controls.Add(this.palSelect4);
            this.palNav.Controls.Add(this.palSelect2);
            this.palNav.Controls.Add(this.palSelect1);
            this.palNav.Controls.Add(this.picBWMode);
            this.palNav.Controls.Add(this.btnProFile);
            this.palNav.Controls.Add(this.palSelect3);
            this.palNav.Controls.Add(this.btnLogOut);
            this.palNav.Controls.Add(this.picHome);
            this.palNav.Controls.Add(this.lblCorpName);
            this.palNav.Controls.Add(this.btnFunction4);
            this.palNav.Controls.Add(this.btnFunction3);
            this.palNav.Controls.Add(this.btnFunction2);
            this.palNav.Controls.Add(this.btnFunction1);
            this.palNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.palNav.Location = new System.Drawing.Point(0, 0);
            this.palNav.Margin = new System.Windows.Forms.Padding(2);
            this.palNav.Name = "palNav";
            this.palNav.Size = new System.Drawing.Size(198, 941);
            this.palNav.TabIndex = 70;
            // 
            // palSelect4
            // 
            this.palSelect4.BackColor = System.Drawing.Color.Red;
            this.palSelect4.Location = new System.Drawing.Point(0, 296);
            this.palSelect4.Name = "palSelect4";
            this.palSelect4.Size = new System.Drawing.Size(10, 55);
            this.palSelect4.TabIndex = 4;
            // 
            // palSelect2
            // 
            this.palSelect2.BackColor = System.Drawing.Color.Red;
            this.palSelect2.Location = new System.Drawing.Point(0, 150);
            this.palSelect2.Name = "palSelect2";
            this.palSelect2.Size = new System.Drawing.Size(10, 55);
            this.palSelect2.TabIndex = 4;
            // 
            // palSelect1
            // 
            this.palSelect1.BackColor = System.Drawing.Color.Red;
            this.palSelect1.Location = new System.Drawing.Point(0, 77);
            this.palSelect1.Name = "palSelect1";
            this.palSelect1.Size = new System.Drawing.Size(10, 55);
            this.palSelect1.TabIndex = 4;
            // 
            // picBWMode
            // 
            this.picBWMode.Image = global::templatev1.Properties.Resources.LB;
            this.picBWMode.Location = new System.Drawing.Point(143, 24);
            this.picBWMode.Name = "picBWMode";
            this.picBWMode.Size = new System.Drawing.Size(49, 46);
            this.picBWMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBWMode.TabIndex = 22;
            this.picBWMode.TabStop = false;
            // 
            // btnProFile
            // 
            this.btnProFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnProFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnProFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProFile.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProFile.Location = new System.Drawing.Point(0, 794);
            this.btnProFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnProFile.Name = "btnProFile";
            this.btnProFile.Size = new System.Drawing.Size(198, 34);
            this.btnProFile.TabIndex = 20;
            this.btnProFile.Text = "ProFile";
            this.btnProFile.UseVisualStyleBackColor = false;
            // 
            // palSelect3
            // 
            this.palSelect3.BackColor = System.Drawing.Color.Red;
            this.palSelect3.Location = new System.Drawing.Point(0, 223);
            this.palSelect3.Name = "palSelect3";
            this.palSelect3.Size = new System.Drawing.Size(10, 55);
            this.palSelect3.TabIndex = 3;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLogOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Location = new System.Drawing.Point(0, 836);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(198, 34);
            this.btnLogOut.TabIndex = 19;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            // 
            // picHome
            // 
            this.picHome.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.picHome.Image = global::templatev1.Properties.Resources.home;
            this.picHome.Location = new System.Drawing.Point(13, 13);
            this.picHome.Margin = new System.Windows.Forms.Padding(4);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(57, 56);
            this.picHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHome.TabIndex = 18;
            this.picHome.TabStop = false;
            // 
            // lblCorpName
            // 
            this.lblCorpName.Font = new System.Drawing.Font("Times New Roman", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorpName.ForeColor = System.Drawing.Color.Red;
            this.lblCorpName.Location = new System.Drawing.Point(1, 892);
            this.lblCorpName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCorpName.Name = "lblCorpName";
            this.lblCorpName.Size = new System.Drawing.Size(163, 49);
            this.lblCorpName.TabIndex = 10;
            this.lblCorpName.Text = "Legend Motor Company";
            // 
            // btnFunction4
            // 
            this.btnFunction4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction4.Location = new System.Drawing.Point(0, 296);
            this.btnFunction4.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction4.Name = "btnFunction4";
            this.btnFunction4.Size = new System.Drawing.Size(198, 55);
            this.btnFunction4.TabIndex = 14;
            this.btnFunction4.Text = "Favourite";
            this.btnFunction4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction4.UseVisualStyleBackColor = false;
            // 
            // btnFunction3
            // 
            this.btnFunction3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction3.Location = new System.Drawing.Point(0, 223);
            this.btnFunction3.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction3.Name = "btnFunction3";
            this.btnFunction3.Size = new System.Drawing.Size(198, 55);
            this.btnFunction3.TabIndex = 13;
            this.btnFunction3.Text = "Cart";
            this.btnFunction3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction3.UseVisualStyleBackColor = false;
            // 
            // btnFunction2
            // 
            this.btnFunction2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction2.Location = new System.Drawing.Point(0, 150);
            this.btnFunction2.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction2.Name = "btnFunction2";
            this.btnFunction2.Size = new System.Drawing.Size(198, 55);
            this.btnFunction2.TabIndex = 12;
            this.btnFunction2.Text = "Spare Part";
            this.btnFunction2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction2.UseVisualStyleBackColor = false;
            // 
            // btnFunction1
            // 
            this.btnFunction1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(213)))), ((int)(((byte)(184)))));
            this.btnFunction1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFunction1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction1.Location = new System.Drawing.Point(0, 77);
            this.btnFunction1.Margin = new System.Windows.Forms.Padding(4);
            this.btnFunction1.Name = "btnFunction1";
            this.btnFunction1.Size = new System.Drawing.Size(198, 55);
            this.btnFunction1.TabIndex = 11;
            this.btnFunction1.Text = "Order Management";
            this.btnFunction1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFunction1.UseVisualStyleBackColor = false;
            // 
            // btnBackToSearch
            // 
            this.btnBackToSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnBackToSearch.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.btnBackToSearch.Location = new System.Drawing.Point(909, 778);
            this.btnBackToSearch.Name = "btnBackToSearch";
            this.btnBackToSearch.Size = new System.Drawing.Size(237, 39);
            this.btnBackToSearch.TabIndex = 69;
            this.btnBackToSearch.Text = "Back to Search Sapre Part";
            this.btnBackToSearch.UseVisualStyleBackColor = false;
            // 
            // lblDBUnitPrice
            // 
            this.lblDBUnitPrice.AutoSize = true;
            this.lblDBUnitPrice.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.lblDBUnitPrice.Location = new System.Drawing.Point(735, 182);
            this.lblDBUnitPrice.Name = "lblDBUnitPrice";
            this.lblDBUnitPrice.Size = new System.Drawing.Size(95, 23);
            this.lblDBUnitPrice.TabIndex = 59;
            this.lblDBUnitPrice.Text = "Unit Price";
            // 
            // lblDBQty
            // 
            this.lblDBQty.AutoSize = true;
            this.lblDBQty.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.lblDBQty.Location = new System.Drawing.Point(636, 183);
            this.lblDBQty.Name = "lblDBQty";
            this.lblDBQty.Size = new System.Drawing.Size(85, 23);
            this.lblDBQty.TabIndex = 57;
            this.lblDBQty.Text = "Quantity";
            // 
            // lblDBSpartPartName
            // 
            this.lblDBSpartPartName.AutoSize = true;
            this.lblDBSpartPartName.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.lblDBSpartPartName.Location = new System.Drawing.Point(464, 184);
            this.lblDBSpartPartName.Name = "lblDBSpartPartName";
            this.lblDBSpartPartName.Size = new System.Drawing.Size(152, 23);
            this.lblDBSpartPartName.TabIndex = 56;
            this.lblDBSpartPartName.Text = "Spart Part Name";
            // 
            // lblDBCat
            // 
            this.lblDBCat.AutoSize = true;
            this.lblDBCat.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold);
            this.lblDBCat.Location = new System.Drawing.Point(297, 183);
            this.lblDBCat.Name = "lblDBCat";
            this.lblDBCat.Size = new System.Drawing.Size(89, 23);
            this.lblDBCat.TabIndex = 58;
            this.lblDBCat.Text = "Category";
            // 
            // cart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 941);
            this.Controls.Add(this.palLoc);
            this.Controls.Add(this.palDate);
            this.Controls.Add(this.btnCreateOrder);
            this.Controls.Add(this.btnEditQty);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.lblTitTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblDBTotal);
            this.Controls.Add(this.palNav);
            this.Controls.Add(this.btnBackToSearch);
            this.Controls.Add(this.lblDBUnitPrice);
            this.Controls.Add(this.lblDBQty);
            this.Controls.Add(this.lblDBSpartPartName);
            this.Controls.Add(this.lblDBCat);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "cart";
            this.Text = "Legend Motor Company Online Ordering Platform";
            this.palLoc.ResumeLayout(false);
            this.palLoc.PerformLayout();
            this.palDate.ResumeLayout(false);
            this.palDate.PerformLayout();
            this.palNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBWMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel palLoc;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.Panel palDate;
        private System.Windows.Forms.Label lblUid;
        private System.Windows.Forms.Label lblTimeDate;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button btnEditQty;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Label lblTitTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblDBTotal;
        private System.Windows.Forms.Panel palNav;
        private System.Windows.Forms.Panel palSelect4;
        private System.Windows.Forms.Panel palSelect2;
        private System.Windows.Forms.Panel palSelect1;
        private System.Windows.Forms.PictureBox picBWMode;
        private System.Windows.Forms.Button btnProFile;
        private System.Windows.Forms.Panel palSelect3;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.Label lblCorpName;
        private System.Windows.Forms.Button btnFunction4;
        private System.Windows.Forms.Button btnFunction3;
        private System.Windows.Forms.Button btnFunction2;
        private System.Windows.Forms.Button btnBackToSearch;
        private System.Windows.Forms.Label lblDBUnitPrice;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblDBQty;
        private System.Windows.Forms.Label lblDBSpartPartName;
        private System.Windows.Forms.Label lblDBCat;
        private System.Windows.Forms.Button btnFunction1;
    }
}