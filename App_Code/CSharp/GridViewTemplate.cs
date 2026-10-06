using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for GridViewTemplate
/// </summary>
public class GridViewTemplate : System.Web.UI.ITemplate

{
    //A variable to hold the type of ListItemType.
    ListItemType _templateType;

    //A variable to hold the column name.
    string _columnName;

    //Constructor where we define the template type and column name.
    public GridViewTemplate(ListItemType type, string colname)
    {
        //Stores the template type.
        _templateType = type;

        //Stores the column name.
        _columnName = colname;
    }

    void System.Web.UI.ITemplate.InstantiateIn(System.Web.UI.Control container)
    {
        switch (_templateType)
        {
            case System.Web.UI.WebControls.ListItemType.Header:
                //Creates a new label control and add it to the container.
                System.Web.UI.WebControls.Label lbl = new Label();            //Allocates the new label object.
                lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                lbl.CssClass = "dynamicrowHeading";
                container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                break;

            case System.Web.UI.WebControls.ListItemType.Item:
                //Creates a new text box control and add it to the container.
                TextBox tb1 = new TextBox();                            //Allocates the new text box object.
                tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                tb1.Columns = 4;                                        //Creates a column with size 4.
                container.Controls.Add(tb1);                            //Adds the newly created textbox to the container.
                break;

            case System.Web.UI.WebControls.ListItemType.EditItem:
                //As, I am not using any EditItem, I didnot added any code here.
                break;

            case System.Web.UI.WebControls.ListItemType.Footer:
                CheckBox chkColumn = new CheckBox();
                chkColumn.ID = "Chk" + _columnName;
                container.Controls.Add(chkColumn);
                break;
        }
    }

    /// <summary>
    /// This is the event, which will be raised when the binding happens.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void tb1_DataBinding(object sender, EventArgs e)
    {
        TextBox txtdata = (TextBox)sender;
        GridViewRow container = (GridViewRow)txtdata.NamingContainer;
        //object dataValue = System.Web.UI.DataBinder.Eval(container.DataItem, _columnName);
        //if (dataValue != DBNull.Value)
        //{
            //txtdata.Text = dataValue.ToString();
        txtdata.Text = "";
        //}
    }


}
