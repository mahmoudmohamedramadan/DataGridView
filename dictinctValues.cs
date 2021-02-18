	DataTable tblForm = new DataTable();

	/*
	   using AddToDGV method i have dictinct values in dataTable using method ToTable this method
	   take two argument first is boolean to determine if you want to make value dictinct or not
	   and second is the DataPropertyName Of dataGridView to initalize it to DataSource of
	   dataGridView.
	*/
        private void AddToDGV() {
            tblForm = tblForm.DefaultView.ToTable(true, "form");
            DGVData.DataSource = tblForm;
        }

	/*
	   via AddRows method i can add row but i should add column(s) in tblForm until i can add
	   row(s) in tblForm so i check if i had no column add one and it's name is form
	>> notice that i can't remove this check becuase if i click button to add row it will add
	   column then add row and if you click this button again it will raise error becuase
	   you want to add form column and it's already exit.
	   then i call AddToDGV method to make tblForm dictinct
	*/
        private void AddRows(string NODE) {
            if (tblForm.Columns.Count < 1) tblForm.Columns.Add("form");
            tblForm.Rows.Add(NODE);
            AddToDGV();
        }

	/*
	   here is the event of my button to add values from TreeView
	*/
	private void btnImport_Click(object sender, EventArgs e) {
            AddRows(treeView.SelectedNode.Text);
        }

	/*
	   btnMoveUp_Click first i had get index of selected row then i had check if this index equals
	   zero then return because this mean that this row  is the first row, then get index of 
	   column whose cell had selected, then i had store this row of tblForm in delete_row
	   variable to delete that row via Remove method then get value of cell in this row and create
	   insert_row variable and this variable has value of row which i had delete 
	   then i had insert new row upper the deleted row.
	
	>> briefly i take value of row which i had delete it then i had create a new row and insert
	   it upper the row which i had delete
	*/
	private void btnMoveUp_Click(object sender, EventArgs e) {
            try {
                int rowIndex = DGVData.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                int colIndex = DGVData.SelectedCells[0].OwningColumn.Index;
                DataRow delete_row = tblForm.Rows[rowIndex];
                string deleted_row_value = delete_row[0].ToString();
                tblForm.Rows.Remove(delete_row);
                DataRow insert_row = tblForm.NewRow();
                insert_row[0] = deleted_row_value;
                tblForm.Rows.InsertAt(insert_row, rowIndex - 1);
                DGVData.ClearSelection();
                DGVData.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            } catch { }
        }