//Glenn M122777
//4/10/2023
//Assessment Task Two

using System.Diagnostics;
using System.Windows.Forms;
using System.IO; // For StreamWriter, BinaryWriter
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Xml.Linq;
using System;

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
        private const string FileNameCategory = "category.txt"; //file systems on Windows are case-insensitive. Not Linux / macOS. C# convention use PascalCase Categories.txt, but use conventions followed by team or community.
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
            //Display from Information to listView
            //DisplayInformation(Wiki, listViewInformation); //ERROR. display change back ChangeControlColour to default
        }

        private void AddRecord()
        {
            //Get values from control inputs        
            string name = textBoxName.Text;
            string category = comboBoxCategory.Text;
            string structure; //get values later if radiobutton checked
            string definition = textBoxDefinition.Text;
            //Check name
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                toolStripStatusLabel1.Text = "Please enter name";
                ChangeControlColour(1, textBoxName);
                return;
            }
            if (textBoxName.Text == "~")
            {
                toolStripStatusLabel1.Text = "Please enter proper value";
                return;
            }
            //Check category
            if (string.IsNullOrWhiteSpace(comboBoxCategory.Text))
            {
                toolStripStatusLabel1.Text = "Please enter category";
                ChangeControlColour(1, comboBoxCategory);
                return;
            }
            //Check structure
            //condition ? consequent : alternative https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
            //string structure = radioButtonLinear.Checked ? "Linear" : (radioButtonNonLinear.Checked ? "Non-Linear" : ""); //ternary conditional operator still not sure how to use
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
                toolStripStatusLabel1.Text = "Please select structure";
                return;
            }
            //Check definition
            if (string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                toolStripStatusLabel1.Text = "Please enter definition";
                ChangeControlColour(1, textBoxDefinition);
                return;
            }
            //If trimmed after if (ValidName(name)) this will bypass duplicate check as values are always different
            string str = textBoxName.Text.Trim();
            name = char.ToUpper(str[0]) + str.Substring(1);
            //Finally check duplicate name. If valid then add
            if (ValidName(name))
            {
                //Replace other values that needs trim and capitalised first letter
                str = comboBoxCategory.Text.Trim();
                category = char.ToUpper(str[0]) + str.Substring(1);
                str = textBoxDefinition.Text.Trim();
                definition = char.ToUpper(str[0]) + str.Substring(1);

                //Start adding Information to listView
                Information info = new Information(name, category, structure, definition);
                Wiki.Add(info);
                //Also update category drop down if distinct
                if (!comboBoxCategory.Items.Contains(category))
                    comboBoxCategory.Items.Add(category);
                //Update display from Information to listView
                DisplayInformation(Wiki, listViewInformation);
                //Update status
                toolStripStatusLabel1.Text = $"{textBoxName.Text} is added successfully";
            }
            else
            {
                toolStripStatusLabel1.Text = $"{textBoxName.Text} exists. not a valid name";
            }
        }
        //Method to change control colour. Receives paramater from method call (integer)
        private void ChangeControlColour(int colour, Control control)
        {
            if (control != null)
            {
                control.ForeColor = Color.White;
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
                    default:
                        control.ForeColor = default(Color);
                        control.BackColor = default(Color); break;
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
                    MessageBox.Show("Cannot find Category.txt. default file is created instead. Please try again");
                    //toolStripStatusLabel1.Text = "File created successfully!";
                }
            }
        }
        #endregion
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
            bool validName = Wiki.Exists(info => info.GetName() == name);
            //return false; //ERROR. if match should not return false as well. Can't add new
            Trace.TraceInformation("name: {0} \n\tvalidName: {1} \n\t!validName: {2}", name, validName, !validName);

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
                    DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        int index = listViewInformation.SelectedIndices[0];
                        Wiki.RemoveAt(index);
                        DisplayInformation(Wiki, listViewInformation);
                        toolStripStatusLabel1.Text = $"{textBoxName.Text} is deleted successfully";
                        ClearInformation(); //Clear previous selection
                                            //Trace value on Output window Ctrl+Alt+O. {0} is format specifier, index is value that will replace {0} in log message
                        Trace.TraceInformation("WikiIndex: {0} \t name: {1} \n\t DialogResult: {2}", index, Wiki[index].GetName(), result);
                    }
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
                    info.SetName(textBoxName.Text);
                    info.SetCategory(comboBoxCategory.Text);
                    HighlightStructure(info.GetStructure()); //highlight checked box
                    info.SetDefinition(textBoxDefinition.Text);
                    //Trace value on Output window Ctrl+Alt+O. {0} is format specifier, index is value that will replace {0} in log message
                    Trace.TraceInformation("Before Sort Wiki.IndexOf(info): {0} \t Wiki[index].Name: {1} \n\t listViewInformation.SelectedItems[0].SubItems[0].Text current text: {2}", Wiki.IndexOf(info), Wiki[index].GetName(), listViewInformation.SelectedItems[0].SubItems[0].Text);
                    // display new values to listView
                    DisplayInformation(Wiki, listViewInformation);
                    toolStripStatusLabel1.Text = $"{textBoxName.Text} is edited successfully";
                    //For Tracing
                    listViewInformation.SelectedIndices.Clear(); //fix selecting previous index / making multiple .SelectedIndices.Add
                    listViewInformation.SelectedIndices.Add(Wiki.IndexOf(info));
                    Trace.TraceInformation("After Sort Wiki.IndexOf(info): {0} \t Wiki[index].Name: {1} \n\t listViewInformation.SelectedItems[0].SubItems[0].Text current text: {2}", Wiki.IndexOf(info), Wiki[index].GetName(), listViewInformation.SelectedItems[0].SubItems[0].Text);
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
                    ListViewItem item = new ListViewItem(info.GetName());
                    item.SubItems.Add(info.GetCategory());
                    item.SubItems.Add(info.GetStructure());
                    item.SubItems.Add(info.GetDefinition());
                    listViewInformation.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Class Information is not placed into listView");
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
            SearchName();
            textBoxSearch.Clear();
            textBoxSearch.Focus();//set input focus without selecting text.
            //textBoxSearch.Select(); //set input focus and select all text within control
        }
        //used internal static so Information class can use it. static for one shared instance
        internal static string ReplaceString(string input)
        {
            //match any characters that are not letters/digits, replace with empty string ""
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "").ToLowerInvariant();
        }
        private void SearchName()
        {
            stopwatch.Reset();
            stopwatch.Start();
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                toolStripStatusLabel1.Text = "Enter Name to search";
            }
            else
            {
                bool found = false;
                //replace method to ignore dash e.g. Self-Balance Tree
                string searchName = ReplaceString(textBoxSearch.Text);
                //Create local instance to compare in binary search
                Information searchInfo = new Information();
                searchInfo.SetName(searchName);
                //#2 
                //Information searchInfo = new Information
                //{
                //    SetName(searchName); //ERROR. Name = searchName does not work with separate setter
                //};
                // Use built-in binarysearch to find index
                //not sure how to use ReplaceString on Wiki before comparing.
                int searchIndex = Wiki.BinarySearch(searchInfo);
                //Wiki.BinarySearch(searchName); //ERROR CS1503 Cannot convert from string to WikiApp.Information
                if (searchIndex >= 0)
                {
                    //Select search index in listview
                    listViewInformation.SelectedIndices.Clear(); //fix selecting previous index / making multiple .SelectedIndices.Add
                    listViewInformation.SelectedIndices.Add(searchIndex);
                    toolStripStatusLabel1.Text = $"{textBoxName.Text:F2} is found! ({stopwatch.Elapsed.TotalSeconds:F3} seconds)";
                    found = true;
                    Trace.TraceInformation("Search Index: {0} Found: {1} \n\tName: {2} \n\tCategory: {3} \n\tStructure: {4} \n\tDefinition: {5}", searchIndex, found, Wiki[searchIndex].GetName(), Wiki[searchIndex].GetCategory(), Wiki[searchIndex].GetStructure(), Wiki[searchIndex].GetDefinition());
                }
                else
                {
                    toolStripStatusLabel1.Text = textBoxSearch.Text + $" not found ({stopwatch.Elapsed.TotalSeconds:F3} seconds)";
                    found = false;
                }
            }
            stopwatch.Stop();
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
                textBoxName.Text = info.GetName();
                comboBoxCategory.Text = info.GetCategory();
                HighlightStructure(info.GetStructure()); //highlight to checked box
                textBoxDefinition.Text = info.GetDefinition();
            }
        }
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
        private void FormWiki_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save changes before closing?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveFileDialog();
                SaveToBinary(FileNameInformation);
            }
            else if (result == DialogResult.Cancel)
            {
                // Cancel the form closing event
                e.Cancel = true;
            }
        }
        #endregion

        #region
        /// <summary>
        /// 6.16 All code is required to be adequately commented.
        /// Map the programming criteria and features to your code/methods by adding comments above the method signatures.
        /// Ensure your code is compliant with the CITEMS coding standards (refer http://www.citems.com.au/). 
        /// </summary>
        private void comboBoxCategory_MouseClick(object sender, MouseEventArgs e)
        {
            //Sort when drop down opened.
            comboBoxCategory.Sorted = true;
        }
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            ValidateText(textBoxName, 32);
        }
        private void comboBoxCategory_TextChanged(object sender, EventArgs e)
        {
            ValidateText(comboBoxCategory, 32);
        }
        private void textBoxDefinition_TextChanged(object sender, EventArgs e)
        {
            ValidateText(textBoxDefinition, 250);
        }
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
        //Reset button
        private void buttonReset_Click(object sender, EventArgs e)
        {
            //Clear input controls, list, listView, and status
            ClearInformation();
            ChangeControlColour(default, textBoxName);
            ChangeControlColour(default, comboBoxCategory);
            ChangeControlColour(default, radioButtonLinear);
            ChangeControlColour(default, radioButtonNonLinear);
            ChangeControlColour(default, textBoxDefinition);
            Wiki.Clear();
            listViewInformation.Items.Clear();
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
*/