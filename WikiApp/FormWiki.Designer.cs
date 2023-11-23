namespace WikiApp
{
    partial class FormWiki
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWiki));
            textBoxName = new TextBox();
            comboBoxCategory = new ComboBox();
            radioButtonLinear = new RadioButton();
            radioButtonNonLinear = new RadioButton();
            textBoxDefinition = new TextBox();
            listViewInformation = new ListView();
            columnHeaderName = new ColumnHeader();
            columnHeaderCategory = new ColumnHeader();
            columnHeaderStructure = new ColumnHeader();
            columnHeaderDefinition = new ColumnHeader();
            textBoxSearch = new TextBox();
            buttonAdd = new Button();
            buttonEdit = new Button();
            buttonDelete = new Button();
            buttonSearch = new Button();
            buttonOpen = new Button();
            buttonSave = new Button();
            buttonReset = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolTip1 = new ToolTip(components);
            groupBox1 = new GroupBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(13, 41);
            textBoxName.Name = "textBoxName";
            textBoxName.PlaceholderText = "enter name";
            textBoxName.Size = new Size(160, 27);
            textBoxName.TabIndex = 0;
            textBoxName.TextChanged += textBoxName_TextChanged;
            textBoxName.MouseDoubleClick += textBoxName_MouseDoubleClick;
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new Point(13, 92);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new Size(160, 28);
            comboBoxCategory.TabIndex = 1;
            comboBoxCategory.TextChanged += comboBoxCategory_TextChanged;
            comboBoxCategory.MouseClick += comboBoxCategory_MouseClick;
            // 
            // radioButtonLinear
            // 
            radioButtonLinear.AutoSize = true;
            radioButtonLinear.BackColor = Color.LightSkyBlue;
            radioButtonLinear.Font = new Font("Bahnschrift Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonLinear.Location = new Point(13, 143);
            radioButtonLinear.Name = "radioButtonLinear";
            radioButtonLinear.Size = new Size(58, 22);
            radioButtonLinear.TabIndex = 2;
            radioButtonLinear.TabStop = true;
            radioButtonLinear.Text = "Linear";
            radioButtonLinear.UseVisualStyleBackColor = false;
            // 
            // radioButtonNonLinear
            // 
            radioButtonNonLinear.AutoSize = true;
            radioButtonNonLinear.BackColor = Color.SteelBlue;
            radioButtonNonLinear.Font = new Font("Bahnschrift Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonNonLinear.Location = new Point(91, 143);
            radioButtonNonLinear.Name = "radioButtonNonLinear";
            radioButtonNonLinear.Size = new Size(82, 22);
            radioButtonNonLinear.TabIndex = 3;
            radioButtonNonLinear.TabStop = true;
            radioButtonNonLinear.Text = "Non-Linear";
            radioButtonNonLinear.UseVisualStyleBackColor = false;
            // 
            // textBoxDefinition
            // 
            textBoxDefinition.Location = new Point(13, 189);
            textBoxDefinition.Multiline = true;
            textBoxDefinition.Name = "textBoxDefinition";
            textBoxDefinition.PlaceholderText = "enter definition";
            textBoxDefinition.Size = new Size(313, 160);
            textBoxDefinition.TabIndex = 4;
            textBoxDefinition.TextChanged += textBoxDefinition_TextChanged;
            // 
            // listViewInformation
            // 
            listViewInformation.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderCategory, columnHeaderStructure, columnHeaderDefinition });
            listViewInformation.FullRowSelect = true;
            listViewInformation.Location = new Point(389, 53);
            listViewInformation.Name = "listViewInformation";
            listViewInformation.Size = new Size(340, 308);
            listViewInformation.TabIndex = 5;
            listViewInformation.UseCompatibleStateImageBehavior = false;
            listViewInformation.View = View.Details;
            listViewInformation.SelectedIndexChanged += listViewInformation_SelectedIndexChanged;
            // 
            // columnHeaderName
            // 
            columnHeaderName.Text = "Name";
            columnHeaderName.Width = 160;
            // 
            // columnHeaderCategory
            // 
            columnHeaderCategory.Text = "Category";
            columnHeaderCategory.Width = 80;
            // 
            // columnHeaderStructure
            // 
            columnHeaderStructure.Text = "Structure";
            columnHeaderStructure.Width = 0;
            // 
            // columnHeaderDefinition
            // 
            columnHeaderDefinition.Text = "Definition";
            columnHeaderDefinition.Width = 0;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(389, 20);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.PlaceholderText = "Search";
            textBoxSearch.Size = new Size(340, 27);
            textBoxSearch.TabIndex = 6;
            // 
            // buttonAdd
            // 
            buttonAdd.BackColor = Color.DeepSkyBlue;
            buttonAdd.ForeColor = SystemColors.ButtonHighlight;
            buttonAdd.Location = new Point(229, 39);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(97, 37);
            buttonAdd.TabIndex = 7;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.BackColor = SystemColors.AppWorkspace;
            buttonEdit.ForeColor = SystemColors.ButtonHighlight;
            buttonEdit.Location = new Point(229, 123);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(97, 37);
            buttonEdit.TabIndex = 8;
            buttonEdit.Text = "Edit";
            buttonEdit.UseVisualStyleBackColor = false;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.Crimson;
            buttonDelete.ForeColor = SystemColors.ButtonHighlight;
            buttonDelete.Location = new Point(229, 80);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(97, 37);
            buttonDelete.TabIndex = 9;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonSearch
            // 
            buttonSearch.Image = (Image)resources.GetObject("buttonSearch.Image");
            buttonSearch.Location = new Point(355, 19);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(33, 29);
            buttonSearch.TabIndex = 10;
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // buttonOpen
            // 
            buttonOpen.BackColor = Color.FromArgb(255, 192, 192);
            buttonOpen.Location = new Point(389, 367);
            buttonOpen.Name = "buttonOpen";
            buttonOpen.Size = new Size(97, 37);
            buttonOpen.TabIndex = 11;
            buttonOpen.Text = "Open";
            buttonOpen.UseVisualStyleBackColor = false;
            buttonOpen.Click += buttonOpen_Click;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.FromArgb(192, 255, 192);
            buttonSave.Location = new Point(510, 367);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(97, 37);
            buttonSave.TabIndex = 12;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(631, 367);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(97, 37);
            buttonReset.TabIndex = 13;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 416);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(773, 25);
            statusStrip1.TabIndex = 14;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(151, 20);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonDelete);
            groupBox1.Controls.Add(buttonAdd);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(buttonEdit);
            groupBox1.Controls.Add(textBoxName);
            groupBox1.Controls.Add(comboBoxCategory);
            groupBox1.Controls.Add(radioButtonLinear);
            groupBox1.Controls.Add(radioButtonNonLinear);
            groupBox1.Controls.Add(textBoxDefinition);
            groupBox1.Location = new Point(12, 20);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(337, 392);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Wiki Information";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(13, 20);
            label1.Name = "label1";
            label1.Size = new Size(36, 18);
            label1.TabIndex = 16;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Bahnschrift Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(13, 71);
            label2.Name = "label2";
            label2.Size = new Size(53, 18);
            label2.TabIndex = 17;
            label2.Text = "Category";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Bahnschrift Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(13, 123);
            label3.Name = "label3";
            label3.Size = new Size(56, 18);
            label3.TabIndex = 18;
            label3.Text = "Structure";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Bahnschrift Condensed", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(13, 168);
            label4.Name = "label4";
            label4.Size = new Size(55, 18);
            label4.TabIndex = 19;
            label4.Text = "Definition";
            // 
            // FormWiki
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(773, 441);
            Controls.Add(statusStrip1);
            Controls.Add(buttonReset);
            Controls.Add(buttonSave);
            Controls.Add(buttonOpen);
            Controls.Add(buttonSearch);
            Controls.Add(textBoxSearch);
            Controls.Add(listViewInformation);
            Controls.Add(groupBox1);
            Name = "FormWiki";
            Text = "WikiApp Version 1.1.1-Alpha";
            FormClosing += FormWiki_FormClosing;
            Load += FormWiki_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxName;
        private ComboBox comboBoxCategory;
        private RadioButton radioButtonLinear;
        private RadioButton radioButtonNonLinear;
        private TextBox textBoxDefinition;
        private ListView listViewInformation;
        private TextBox textBoxSearch;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonDelete;
        private Button buttonSearch;
        private Button buttonOpen;
        private Button buttonSave;
        private Button buttonReset;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderCategory;
        private ColumnHeader columnHeaderStructure;
        private ColumnHeader columnHeaderDefinition;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}