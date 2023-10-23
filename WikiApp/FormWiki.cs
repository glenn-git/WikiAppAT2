using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
//Glenn M122777
//4/10/2023
//Assessment Task Two
namespace WikiApp
{
    public partial class FormWiki : Form
    {
        public FormWiki()
        {
            InitializeComponent();
        }
        #region List<T> Global
        /// <summary>
        /// 6.2 Create a global List<T> of type Information called Wiki. 
        /// </summary>
        #endregion

        #region ADD button
        /// <summary>
        /// 6.3 Create a button method to ADD a new item to the list.
        /// Use TextBox for Name input, ComboBox for Category, Radio group for Structure and Multiline TextBox for Definition.
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region LOAD method
        /// <summary>
        /// 6.4 Create a custom method to populate the ComboBox when the Form Load method is called.
        /// The six categories must be read from a simple text file.
        /// </summary>
        #endregion

        #region VALIDNAME method
        /// <summary>
        /// 6.5 Create a custom ValidName method which will take a parameter string value from the Textbox Name
        /// Returns a Boolean after checking for duplicates.
        /// Use the built in List<T> method “Exists” to answer this requirement.
        /// </summary>
        #endregion

        #region STRUCTURE method
        /// <summary>
        /// 6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        /// The first method must return a string value from the selected radio button(Linear or Non-Linear).
        /// The second method must send an integer index which will highlight an appropriate radio button.
        /// </summary>
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

        }
        #endregion

        #region SAVE method
        /// <summary>
        /// 6.8 Create a button method that will save the edited record of the currently selected item in the ListView.
        /// All the changes in the input controls will be written back to the list.
        /// Display an updated version of the sorted list at the end of this process.
        /// </summary>
        #endregion

        #region DISPLAY method
        /// <summary>
        /// 6.9 Create a single custom method that will sort and then display the Name and Category from the wiki information in the list.
        /// </summary>
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
        #endregion

        #region CLEAR method
        /// <summary>
        /// 6.12 Create a custom method that will clear and reset the TextBoxes, ComboBox and Radio button 
        /// </summary>
        #endregion

        #region MOUSE event
        /// <summary>
        /// 6.13 Create a double click event on the Name TextBox to clear the TextBboxes, ComboBox and Radio button. 
        /// </summary>
        #endregion

        #region SAVE LOAD button
        /// <summary>
        /// 6.14 Create two buttons for the manual open and save option; this must use a dialog box to select a file or rename a saved file.
        /// All Wiki data is stored/retrieved using a binary reader/writer file format.
        /// </summary>
        //Open button
        private void buttonOpen_Click(object sender, EventArgs e)
        {

        }
        //Save button
        private void buttonSave_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region FORMCLOSE event
        /// <summary>
        /// 6.15 The Wiki application will save data when the form closes.
        /// </summary>
        #endregion

        #region
        /// <summary>
        /// 6.16 All code is required to be adequately commented.
        /// Map the programming criteria and features to your code/methods by adding comments above the method signatures.
        /// Ensure your code is compliant with the CITEMS coding standards (refer http://www.citems.com.au/). 
        /// </summary>

        //Edit button
        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }

        //Reset button
        private void buttonReset_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
/* NOTES
 * ListView properties "FullRowSelect" true to make entire row highlighted
 * ListVIew right click Edit Columns Width 160, Width 0 to hide. set Text name.. e.g. listViewArray.Columns[0].Width = 0; https://stackoverflow.com/questions/7811669/how-to-hide-a-column-in-a-listview-control
 * 
 * 
 * if (listViewArray.CheckedIndices.Count == 0) //cannot edit/delete if click elsewhere
 * citems standards https://www.citems.com.au/services/application-development/
 * naming namespace https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces
 * 
 */
