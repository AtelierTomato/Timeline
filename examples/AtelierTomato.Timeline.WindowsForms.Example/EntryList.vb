Imports AtelierTomato.Timeline.Core

Public Class EntryList
	Public Entries As New List(Of TimelineEntry)
	Private Sub btnAddEntry_Click(sender As Object, e As EventArgs) Handles btnAddEntry.Click
		OpenAddEntryForm()
	End Sub

	Private Sub mnuEntriesAdd_Click(sender As Object, e As EventArgs) Handles mnuEntriesAdd.Click
		OpenAddEntryForm()
	End Sub

	Private Sub OpenAddEntryForm()
		' Create and show the AddEntry form
		Dim addEntryForm As New AddEntry()
		If addEntryForm.ShowDialog() = DialogResult.OK Then
			' Add the new entry to the list
			HandleAddEntryFormReturn(addEntryForm)
		End If
	End Sub

	Private Sub RefreshEntries()
		' Refresh the list box
		lstTimelineEntries.DataSource = Nothing
		lstTimelineEntries.DataSource = Entries
	End Sub

	Private Sub mnuEntriesRemove_Click(sender As Object, e As EventArgs) Handles mnuEntriesRemove.Click
		RemoveEntry()
	End Sub

	Private Sub btnRemoveEntry_Click(sender As Object, e As EventArgs) Handles btnRemoveEntry.Click
		RemoveEntry()
	End Sub

	Private Sub RemoveEntry()
		' Ensure an item is selected
		If lstTimelineEntries.SelectedItem Is Nothing Then
			MessageBox.Show("Please select an entry to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		' Get the selected entry
		Dim selectedEntry As TimelineEntry = CType(lstTimelineEntries.SelectedItem, TimelineEntry)

		' Confirm deletion
		Dim result = MessageBox.Show($"Are you sure you want to remove the entry '{selectedEntry.Name}'?",
									 "Confirm Deletion",
									 MessageBoxButtons.YesNo,
									 MessageBoxIcon.Warning)

		If result = DialogResult.Yes Then
			' Remove the entry
			Entries.RemoveAll(Function(e) e.ID = selectedEntry.ID)

			' Refresh the list
			RefreshEntries()
			MessageBox.Show("Entry removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Sub mnuEntriesClear_Click(sender As Object, e As EventArgs) Handles mnuEntriesClear.Click
		Dim result = MessageBox.Show($"Are you sure you want to remove all entries?",
									 "Confirm clear",
									 MessageBoxButtons.YesNo,
									 MessageBoxIcon.Warning)

		If result = DialogResult.Yes Then
			' Reset Entries
			Entries = New List(Of TimelineEntry)
			RefreshEntries()
			MessageBox.Show("Entries reset.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	Private Sub mnuEntriesGenerate_Click(sender As Object, e As EventArgs) Handles mnuEntriesGenerate.Click
		Dim generateForm As New GenerateEntries()
		' Pass the existing list of entries 
		generateForm.Entries = Me.Entries
		generateForm.ShowDialog()
		' Set Entries to entires + new generated entires and refresh the form
		Me.Entries = generateForm.Entries
		RefreshEntries()
	End Sub

	Private Sub lstTimelineEntries_DoubleClick(sender As Object, e As EventArgs) Handles lstTimelineEntries.DoubleClick
		' Get the selected entry from the ListBox
		Dim selectedEntry As TimelineEntry = CType(lstTimelineEntries.SelectedItem, TimelineEntry)

		' Check if there is a valid selection
		If selectedEntry IsNot Nothing Then
			' Open the AddEntry form and pass the selected entry's data
			Dim addEntryForm As New AddEntry(selectedEntry)
			If addEntryForm.ShowDialog() = DialogResult.OK Then
				' Add the new entry to the list
				HandleAddEntryFormReturn(addEntryForm)
			End If
		End If
	End Sub

	Private Sub btnShowTimeline_Click(sender As Object, e As EventArgs) Handles btnShowTimeline.Click
		If Not Entries.Count = 0 Then
			Dim timelineForm As New TimelineForm(Entries)
			timelineForm.ShowDialog()
			RefreshEntries()
		Else
			MessageBox.Show("Please add at least one entry before generating a timeline.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
		End If
	End Sub

	Private Sub mnuFileExit_Click(sender As Object, e As EventArgs) Handles mnuFileExit.Click
		Me.Close()
	End Sub

	Private Sub HandleAddEntryFormReturn(addEntryForm As AddEntry)
		If addEntryForm.Entry IsNot Nothing Then
			If Entries.FirstOrDefault(Function(e) e.ID = addEntryForm.Entry.ID) IsNot Nothing Then
				' If an entry with the same ID already exists, ask the user if they want to overwrite it
				Dim result = MessageBox.Show("Duplicate ID detected, do you want to overwrite the conflicting entry?",
											 "Duplicate ID Detected",
											 MessageBoxButtons.YesNo,
											 MessageBoxIcon.Warning
				)

				If result = DialogResult.Yes Then
					' Remove the old entry and add the new one
					Entries.RemoveAll(Function(e) e.ID = addEntryForm.Entry.ID)
					Entries.Add(addEntryForm.Entry)
					MessageBox.Show("Entry overwritten successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

					RefreshEntries()
				Else
					MessageBox.Show("Entry was not added.", "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
				End If
			Else
				Entries.Add(addEntryForm.Entry)
				MessageBox.Show("Entry added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

				RefreshEntries()
			End If
		End If
	End Sub
End Class
