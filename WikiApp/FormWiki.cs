//Glenn M122777
//4/10/2023
//Assessment Task Two

using System.Diagnostics;
using System.Windows.Forms;
using System.IO; // For TextWriter
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.Drawing;

namespace WikiApp
{
    public partial class FormWiki : Form
    {
        public FormWiki()
        {
            InitializeComponent();
            //Status
            toolTip1.SetToolTip(textBoxName, "Double click to clear textboxes");
            toolTip1.SetToolTip(textBoxSearch, "Double click to clear textboxes");
            toolStripStatusLabel1.Text = "Thank you for using " + this.Text + ". Welcome!";
            //Controls
            textBoxDefinition.ScrollBars = ScrollBars.Vertical;
        }
        #region FIELDS List<T> Global
        /// <summary>
        /// 6.2 Create a global List<T> of type Information called Wiki. 
        /// </summary>
        /// <remarks>
        /// Naming convention
        /// PascalCase for all identifiers (class names/constant names+local/properties/methods) except compound words and parameters.
        /// </remarks>
        private List<Information> Wiki = new List<Information>();
        private const string FileNameCategory = "Category.txt"; //file systems on Windows are case-insensitive. Not Linux / macOS. C# convention use PascalCase Categories.txt, but use conventions followed by team or community.
        private const string FileNameInformation = "definitions.dat";
        Stopwatch stopwatch = new Stopwatch(); // stopwatch to measure time
        #endregion

        #region ADD button
        /// <summary>
        /// 6.3 Create a button method to ADD a new item to the list.
        /// Use TextBox for Name input, ComboBox for Category, Radio group for Structure and Multiline TextBox for Definition.
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddRecord();
        }
        private void AddRecord()
        {
            //Get values from controls            
            string name = textBoxName.Text;
            string category = comboBoxCategory.Text;
            string structure; //
            //string category = comboBoxCategory.SelectedItem?.ToString();
            if (radioButtonLinear.Checked)
            {
                structure = radioButtonLinear.Text;
            }
            else if (radioButtonNonLinear.Checked)
            {
                structure = radioButtonNonLinear.Text;
            }
            else
            {
                structure = "~";
            }
            //string structure = radioButtonLinear.Checked ? "Linear" : (radioButtonNonLinear.Checked ? "Non-Linear" : ""); //ternary conditional operator still not sure how to use
            string definition = textBoxDefinition.Text;

            //Check duplicates. is name valid?
            if (ValidName(name))
            {
                //Start adding Information to listView
                Information info = new Information(name, category, structure, definition);
                Wiki.Add(info);
                //Display from Information to listView
                DisplayInformation(Wiki, listViewInformation);
                toolStripStatusLabel1.Text = $"{textBoxName.Text} is added successfully";
            }
            else
            {
                toolStripStatusLabel1.Text = $"{textBoxName.Text} exists. not a valid name";
            }
        }
        //Method to change text box colours. Receives paramater from method call (integer)
        private void ChangeControlColour(int colour, Control control)
        {
            if (control != null)
            {
                control.ForeColor = Color.Black;
                switch (colour)
                {
                    //case 0: control.BackColor = default(Color); break; //if case 0 is red, default stay red?
                    case 1: control.BackColor = Color.Red; break;
                    case 2: control.BackColor = Color.Orange; break;
                    case 3: control.BackColor = Color.Yellow; break;
                    case 4: control.BackColor = Color.Green; break;
                    case 5:
                        control.BackColor = Color.Blue;
                        control.ForeColor = Color.White; break;
                    case 6:
                        control.BackColor = Color.Indigo;
                        control.ForeColor = Color.White; break;
                    case 7: control.BackColor = Color.Violet; break;
                    default: control.BackColor = default(Color); break;
                }
            }
        }
        #endregion

        #region CATEGORY method
        /// <summary>
        /// 6.4 Create a custom method to populate the ComboBox when the Form Load method is called.
        /// The six categories must be read from a simple text file.
        /// </summary>
        /// <remarks>
        /// The TextReader class is an abstract class. Therefore, you do not instantiate it in your code. <see href= "https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader?view=net-8.0"></see>
        /// </remarks>
        private void FormWiki_Load(object sender, EventArgs e)
        {
            initializeCategory();
        }
        #endregion
        private void initializeCategory()
        {
            //Clear Categories
            comboBoxCategory.Items.Clear();
            //Open text file then write to comboBox
            try
            {
                //Clear Information before loading data
                Wiki.Clear();
                // Create a FileStream and BinaryWriter to read from the selected file
                // Cant use TextReader because it is abstract..StreamReader works
                // false to disable Byte Order Marks(BOM) construct with a new UTF8Encoding(false), instead of just Encoding.UTF8Encoding. This is the same as calling StreamWriter without the encoding argument, internally it's just doing the same thing. https://stackoverflow.com/questions/5266069/streamwriter-and-utf-8-byte-order-marks
                using (var fileStream = File.Open(FileNameCategory, FileMode.Open))
                using (var textReader = new StreamReader(fileStream, Encoding.UTF8, false))
                {
                    while (!textReader.EndOfStream)
                    {
                        string category = textReader.ReadLine();
                        // Set fields before adding item
                        comboBoxCategory.Items.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error while loading file: " + ex.Message;
                //Create a new file if cannot find
                if (!File.Exists(FileNameCategory))
                {
                    //string categories
                    string categories = "Abstract\nArray\nGraph\nHash\nList\nTree";
                    File.WriteAllText(FileNameCategory, categories);
                    MessageBox.Show("Cannot find Category.txt. empty file is created instead. Please try again");
                    //toolStripStatusLabel1.Text = "File created successfully!";
                }
            }
        }
        #region VALIDNAME method
        /// <summary>
        /// 6.5 Create a custom ValidName method which will take a parameter string value from the Textbox Name
        /// Returns a Boolean after checking for duplicates.
        /// Use the built in List<T> method “Exists” to answer this requirement.
        /// </summary>
        /// <remarks>
        ///             parts.Exists(x => x.PartId == 1444)); <see href= "https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.exists?view=net-8.0"></see>
        /// Determines whether the List<T> contains elements that match the conditions defined by the specified predicate.
        /// predicate function (a function that takes element type T as argument and returns a logical/boolean value for a condition)
        /// </remarks>
        private bool ValidName(string name)
        {
            //If match exists
            bool validName = Wiki.Exists(info => info.Name == name);
            //return false; //NO. if match should not return false as well. Can't add new
            return !validName;
        }
        #endregion

        #region STRUCTURE method
        /// <summary>
        /// 6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        /// The first method must return a string value from the selected radio button(Linear or Non-Linear).
        /// The second method must send an integer index which will highlight an appropriate radio button.
        /// </summary>
        //1st Method: Return string value from checked radio button
        private string CheckStructureValue()
        {
            if (radioButtonLinear.Checked)
            {
                return radioButtonLinear.Text;
            }
            else if (radioButtonNonLinear.Checked)
            {
                return radioButtonNonLinear.Text;
            }
            else
            {
                ChangeControlColour(1, radioButtonLinear);
                ChangeControlColour(1, radioButtonNonLinear);
                return "~";
            }
        }
        //2nd Method: Highlight 
        private void HighlightStructure(string structure)
        {
            if (structure == radioButtonLinear.Text)
            {
                radioButtonLinear.Checked = true;
                radioButtonNonLinear.Checked = false;
            }
            else if (structure == radioButtonNonLinear.Text)
            {
                radioButtonLinear.Checked = false;
                radioButtonNonLinear.Checked = true;
            }
            else
            {
                radioButtonLinear.Checked = false;
                radioButtonNonLinear.Checked = false;
            }
        }
        #endregion

        #region DELETE method
        /// <summary>
        /// 6.7 Create a button method that will delete the currently selected record in the ListView.
        /// Ensure the user has the option to backout of this action by using a dialog box.
        /// Display an updated version of the sorted list at the end of this process.
        /// </summary>
        //Delete button
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewInformation.SelectedItems.Count == 0)
            {
                toolStripStatusLabel1.Text = "Please select from the list first to delete";
            }
            else
            {
                if (textBoxName.Text.Trim() != "~")
                {
                    //DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result == DialogResult.Yes)
                    //{
                    int index = listViewInformation.SelectedIndices[0];
                    Wiki.RemoveAt(index);
                    DisplayInformation(Wiki, listViewInformation);
                    toolStripStatusLabel1.Text = $"{textBoxName.Text} is deleted successfully";
                    ClearInformation(); //Clear previous selection
                    //}
                }
                else
                    toolStripStatusLabel1.Text = "Please enter proper value";
            }
        }
        #endregion

        #region EDIT button
        /// <summary>
        /// 6.8 Create a button method that will save the edited record of the currently selected item in the ListView.
        /// All the changes in the input controls will be written back to the list.
        /// Display an updated version of the sorted list at the end of this process.
        /// </summary>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditInformation();
        }
        //Edit from listView to Information
        private void EditInformation()
        {
            if (listViewInformation.SelectedItems.Count == 0)
            {
                toolStripStatusLabel1.Text = "Please select from the list first to edit";
            }
            else
            {
                if (textBoxName.Text.Trim() != "~")
                {
                    int index = listViewInformation.SelectedIndices[0];
                    Information info = Wiki[index];
                    // show values from Information Class
                    info.Name = textBoxName.Text;
                    info.Category = comboBoxCategory.Text;
                    HighlightStructure(info.Structure); //highlight checked box
                    info.Definition = textBoxDefinition.Text;
                    // display new values to listView
                    DisplayInformation(Wiki, listViewInformation);
                    toolStripStatusLabel1.Text = $"{textBoxName.Text} is edited successfully";
                }
                else
                    toolStripStatusLabel1.Text = "Please enter proper value";
            }
        }
        #endregion

        #region DISPLAY method
        /// <summary>
        /// 6.9 Create a single custom method that will sort and then display the Name and Category from the wiki information in the list.
        /// </summary>
        private void DisplayInformation(List<Information> infoList, Control displayControl)
        {
            ChangeControlColour(default, textBoxName);
            ChangeControlColour(default, comboBoxCategory);
            ChangeControlColour(default, radioButtonLinear);
            ChangeControlColour(default, radioButtonNonLinear);
            ChangeControlColour(default, textBoxDefinition);
            if (displayControl is System.Windows.Forms.ListView listview)
            {
                listview.Items.Clear();
                Wiki.Sort();
                foreach (Information info in infoList)
                {
                    //Add to columns
                    ListViewItem item = new ListViewItem(info.Name);
                    item.SubItems.Add(info.Category);
                    item.SubItems.Add(info.Structure);
                    item.SubItems.Add(info.Definition);
                    listViewInformation.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Class Information is not placed into listview");
            }
        }
        #endregion

        #region SEARCH button
        /// <summary>
        /// 6.10 Create a button method that will use the builtin binary search to find a Data Structure name.
        /// If the record is found the associated details will populate the appropriate input controls and highlight the name
        /// in the ListView. At the end of the search process the search input TextBox must be cleared. 
        /// </summary>
        //Search button
        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region DISPLAY event
        /// <summary>
        /// 6.11 Create a ListView event so a user can select a Data Structure Name from the list of Names
        /// and the associated information will be displayed in the related text boxes combo box and radio button.
        /// </summary>
        //Selected item index changed in ListView, so update display in 4 fields
        private void listViewInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewInformation.SelectedItems.Count > 0)  //fix Value of 0 is not valid for 'index'
            {
                int index = listViewInformation.SelectedIndices[0];
                Information info = Wiki[index];
                // show values from Information
                textBoxName.Text = info.Name;
                comboBoxCategory.Text = info.Category;
                HighlightStructure(info.Structure); //highlight to checked box
                textBoxDefinition.Text = info.Definition;
            }
        }
        //private void DisplayInListView()
        //{
        //    listViewInformation.Items.Clear(); // Clear existing items

        //    foreach (var information in Wiki)
        //    {
        //        ListViewItem item = new ListViewItem(information.Name);
        //        item.SubItems.Add(information.Category);
        //        item.SubItems.Add(information.Structure);
        //        item.SubItems.Add(information.Definition);

        //        listViewInformation.Items.Add(item);
        //    }
        //}
        #endregion

        #region CLEAR method
        /// <summary>
        /// 6.12 Create a custom method that will clear and reset the TextBoxes, ComboBox and Radio button 
        /// </summary>
        private void ClearInformation()
        {
            textBoxName.Clear();
            comboBoxCategory.ResetText();
            //comboBoxCategory.SelectedIndex = -1; //-1 is no item selected
            radioButtonLinear.Checked = false;
            radioButtonNonLinear.Checked = false;
            textBoxDefinition.Clear();
            textBoxSearch.Clear();
        }
        #endregion

        #region MOUSE event
        /// <summary>
        /// 6.13 Create a double click event on the Name TextBox to clear the TextBboxes, ComboBox and Radio button. 
        /// </summary>
        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearInformation();
        }
        #endregion

        #region OPEN/SAVE button
        /// <summary>
        /// 6.14 Create two buttons for the manual open and save option; this must use a dialog box to select a file or rename a saved file.
        /// All Wiki data is stored/retrieved using a binary reader/writer file format.
        /// </summary>
        //Open button
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();
            //Open file dialog to return file path as string
            string filePath = OpenFileDialog();
            //Open from binary to Information Wiki
            OpenFromBinary(filePath);
            DisplayInformation(Wiki, listViewInformation);
            stopwatch.Stop();
            if (OpenFromBinary(filePath)) //bool, if true, display status as successful
            {
                toolStripStatusLabel1.Text = $"Load successful! ({stopwatch.Elapsed.TotalSeconds:F3} seconds)";
            }
        }
        /// <remarks>
        /// open file dialog <see href="https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-7.0"/>
        /// </remarks>
        private string OpenFileDialog()
        {
            try
            {
                //var fileContent = string.Empty;
                //var filePath = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = Application.StartupPath;
                    openFileDialog.Filter = "Data Files (*.dat)|*.dat|Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1; //Set file extensions in drop-down. if 1 dat 2 txt.
                    //openFileDialog.DefaultExt = ".dat";
                    openFileDialog.RestoreDirectory = true; //box restores current directory before closing

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        return openFileDialog.FileName;

                        //string selectedFileName = openFileDialog.FileName;
                        //OpenFromBinary(selectedFileName);
                    }
                    return string.Empty; //no file selected
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error while opening file dialog" + ex.Message;
                return string.Empty;
            }
        }
        /// <remarks>
        /// file stream <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-7.0"/><br></br>
        /// </remarks>
        private bool OpenFromBinary(string fileName)
        {
            try
            {
                //Initialize / Clear Information before loading data
                Wiki.Clear();
                // Create a FileStream and BinaryWriter to read from the selected file
                using (var fileStream = File.Open(fileName, FileMode.Open))
                using (var binaryReader = new BinaryReader(fileStream, Encoding.UTF8, false))
                //using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                //using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                    {
                        string name = binaryReader.ReadString();
                        string category = binaryReader.ReadString();
                        string structure = binaryReader.ReadString();
                        string definition = binaryReader.ReadString();
                        //Set fields before adding item
                        Information info = new Information(name, category, structure, definition);
                        Wiki.Add(info);
                    }
                }
                //LOAD METHOD FOR 2DARRAY (still has error)
                //// Initialize row and column counters
                //int rowCount = 0;
                //int columnCount = 0;
                //// Read the data from the file and populate the 2D array
                //for (int i = 0; i < max; i++)
                //{
                //    for (int j = 0; j < attributes; j++)
                //    {
                //        arrayRecord[i, j] = binaryReader.ReadString();
                //        columnCount++;
                //    }
                //    rowCount++;
                //    ptr++;
                //}
                //// Check if the number of rows and columns in the file matches the expected dimensions STILL ERROR
                //if (rowCount != max || columnCount != max * attributes)
                //{
                //    MessageBox.Show("Invalid file format. Number of Rows and columns do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}
                return true;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error while loading file: " + ex.Message;
                return false;
            }
        }
        //Save button //need to add for txt
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog();
            SaveToBinary(FileNameInformation);
        }
        private string SaveFileDialog()
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = Application.StartupPath;
                    saveFileDialog.Filter = "Data Files (*.dat)|*.dat|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    saveFileDialog.DefaultExt = ".dat";
                    saveFileDialog.AddExtension = true; //Add extension if user forget
                    saveFileDialog.FileName = FileNameInformation; // Set default file name

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        return saveFileDialog.FileName; //Get user input fileName

                        //string selectedFileName = saveFileDialog.FileName; // Use user input filename
                        //SaveToBinary(selectedFileName);
                    }
                    return string.Empty; //no file selected
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error while opening file dialog" + ex.Message;
                return string.Empty;
            }
        }
        private void SaveToBinary(string fileName)
        {
            try
            {
                // Create a FileStream and BinaryWriter to write to the selected file
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    // Loop through the items in the ListView and write them to the file
                    foreach (ListViewItem listViewItem in listViewInformation.Items)
                    {
                        binaryWriter.Write(listViewItem.Text);
                        for (int i = 1; i < listViewItem.SubItems.Count; i++)
                        {
                            binaryWriter.Write(listViewItem.SubItems[i].Text);
                        }
                    }
                }
                toolStripStatusLabel1.Text = "Save successful";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error writing data to file: " + ex.Message;
            }
        }
        #endregion

        #region FORMCLOSE event
        /// <summary>
        /// 6.15 The Wiki application will save data when the form closes.
        /// </summary>
        #endregion
        private void FormWiki_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save changes before closing?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SaveFileDialog();
                    SaveToBinary(FileNameInformation);
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.Text = "Error writing data to file: " + ex.Message;
                }
            }
            else if (result == DialogResult.Cancel)
            {
                // Cancel the form closing event
                e.Cancel = true;
            }
        }
        #region
        /// <summary>
        /// 6.16 All code is required to be adequately commented.
        /// Map the programming criteria and features to your code/methods by adding comments above the method signatures.
        /// Ensure your code is compliant with the CITEMS coding standards (refer http://www.citems.com.au/). 
        /// </summary>
                //ValidateText method
        private void ValidateText(Control control, int charLimit)
        {
            if (control is System.Windows.Forms.TextBox textBox)
            {
                int cursorPosition = textBox.SelectionStart;
                // Remove extra spaces by replacing them with a single space
                textBox.Text = Regex.Replace(textBox.Text, @"\s+", " ");

                // Restore cursor position
                textBox.SelectionStart = cursorPosition;

                // Handle KeyPress event to disallow symbols
                textBox.KeyPress += (sender, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '-')
                    {
                        e.Handled = true;
                    }
                };
                if (textBox.Text.Length > charLimit)
                {
                    //remove last character typed into control
                    textBox.Text = textBox.Text.Substring(0, charLimit);
                    //show error message
                    MessageBox.Show($"Character limit exceeded. Maximum {charLimit} characters allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    toolStripStatusLabel1.Text = $"Please use less than {charLimit} characters";
                }
            }
            else if (control is System.Windows.Forms.ComboBox comboBox)
            {
                int cursorPosition = comboBox.SelectionStart;
                // Remove extra spaces by replacing them with a single space
                comboBox.Text = Regex.Replace(comboBox.Text, @"\s+", " ");

                // Restore the cursor position
                comboBox.SelectionStart = cursorPosition;

                // Handle the KeyPress event to disallow symbols
                comboBox.KeyPress += (sender, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                };

                if (comboBox.Text.Length > charLimit)
                {
                    //remove last character typed into control
                    comboBox.Text = comboBox.Text.Substring(0, charLimit);
                    //show error message
                    MessageBox.Show($"Character limit exceeded. Maximum {charLimit} characters allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    toolStripStatusLabel1.Text = $"Please use less than {charLimit} characters";
                }
            }
        }
        private void textBoxDefinition_TextChanged_1(object sender, EventArgs e)
        {
            ValidateText(textBoxDefinition, 250);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            ValidateText(textBoxName, 32);
        }

        /// <summary>
        /// ented.
        /// </summary>
        /// <remarks>
        ///             <see href= "https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file">
        ///             using (var sr = new StreamReader("TestFile.txt"))<see href= "https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-read-text-from-a-file">
        /// </remarks>
        private void comboBoxCategory_MouseClick(object sender, MouseEventArgs e)
        {
            //comboBoxCategory.Items.Clear(); //for testing
            comboBoxCategory.BeginUpdate();
            //string filePath = "../" + FileNameCategories;
            if (File.Exists(FileNameCategory))
            {
                try
                {
                    // Read all lines from the file and add them to the ComboBox
                    string[] lines = File.ReadAllLines(FileNameCategory);
                    comboBoxCategory.Items.AddRange(lines);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            comboBoxCategory.Sorted = true;
            comboBoxCategory.EndUpdate();
        }

        //    try
        //{
        //    string filePath = "Category.txt";
        //    if (File.Exists(filePath))
        //    {
        //        using (var sr = new StreamReader("TestFile.txt"))
        //        {
        //            comboBoxCategory.Items.Add(sr.ReadToEnd);
        //        }




        //        //    string[] lines = File.ReadAllLines(filePath);
        //        //foreach (string line in lines)
        //        //{
        //        //    string category = line.Trim();
        //        //    if (!comboBoxCategory.Items.Contains(category))
        //        //    {
        //        //        comboBoxCategory.Items.Add(category);
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        MessageBox.Show("File not found");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
        //finally
        //{
        //    // Using StreamWriter
        //    StreamWriter sw = new StreamWriter(FileNameCategories, false, Encoding.UTF8);
        //    try
        //    {
        //        // create writer object - true means append
        //        TextWriter tw = new StreamWriter(FileNameCategories, true);
        //        // write each item as a separate line
        //        tw.WriteLine(comboBoxCategory.Text);
        //        // close the StreamWriter
        //        tw.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("what");
        //    }
        //    comboBoxCategory.Sorted = true;
        //    comboBoxCategory.EndUpdate();
        //}
        //for (int i = 0; i < ptr; i++)
        //{
        //    if (!comboBoxCategory.Items.Contains($"{arrayRecord[i, 1]}"))
        //    {
        //        comboBoxCategory.Items.Add($"{arrayRecord[i, 1]}");
        //    }
        //}
        //comboBoxCategory.Sorted = true;
        //comboBoxCategory.EndUpdate();

        //Reset button
        private void buttonReset_Click(object sender, EventArgs e)
        {
            listViewInformation.Items.Clear();
            ClearInformation();
            toolStripStatusLabel1.Text = string.Empty;
        }
        #endregion
    }
}
/* NOTES
 * Edit advanced Hide/Collapse all region blocks Ctrl+M+O. Ctrl+M+L to expand
 * Edit advanced Format document Ctrl+K+D
 * Initialise
 *             InitializeComponent();
            //Status
            toolTip1.SetToolTip(textBoxName, "Double click to clear textboxes");
            toolTip1.SetToolTip(textBoxSearch, "Double click to clear textboxes");
            toolStripStatusLabel1.Text = "Thank you for using " + this.Text + ". Welcome!";

            //Controls
            textBoxDefinition.ScrollBars = ScrollBars.Vertical; //TextBox properties appearance 'Scrollbars' vertical
 * Class Diagram: install component Class designer. Solution right click 'Add' new item ClassDiagram.cd, drag information class
 * Font: Bahnschrift, German, modern sanserif by Aaron Bell
 * ListView properties 'FullRowSelect' true to make entire row highlighted
 * ListView properties 'View' details to change from large icons
 * ListView right click Edit Columns Width 160 (80 for category, 0 for structure/definition to hide). set columnHeader name.. listViewInformation.Columns[0].Width = 0; https://stackoverflow.com/questions/7811669/how-to-hide-a-column-in-a-listview-control
 * 
 * METHODS
 * if (listViewInformation.CheckedIndices.Count == 0) //cannot edit/delete if click elsewhere
 * citems standards https://www.citems.com.au/services/application-development/
 * naming namespace https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces
 * 
 * ARRAYS
 * arrays https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/
 * delete array https://stackoverflow.com/questions/26303030/how-can-i-delete-rows-and-columns-from-2d-array-in-c
 * Foreach with arrays https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/using-foreach-with-arrays
 * 
 * HTML
 * Insert Line break <br></br>
        /// <summary>
        /// </summary>
        /// <remarks>
        /// file stream <see href="https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-7.0"/><br></br>
        /// open file dialog <see href="https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-7.0"/>
        /// </remarks>
 
ternary conditional operator https://www.reddit.com/r/csharp/comments/13i5rvr/operator/?rdt=51863
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
var x = foo ? "blue" : "red";
// same as
var x;
if(foo) {
    x = "blue";
} else {
    x = "red";
}

var x = foo ?? bar;
// same as
var x;
if(foo != null) {
    x = foo;
} else {
    x = bar;
}

var x ??= foo;
// same as
var x;
if(x == null) {
    x = foo;
}
 */
