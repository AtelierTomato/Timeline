Imports AtelierTomato.Timeline.Core

Public Class TimelineForm
	Private _entries As List(Of TimelineEntry)
	Public Sub New(ByRef entries As IEnumerable(Of TimelineEntry))
		InitializeComponent()
		_entries = entries
	End Sub
	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

	End Sub
	Private Sub TimelineForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		RefreshTimeline()
	End Sub

	Private Sub RefreshTimeline()
		Dim timeline As Core.Timeline = New Core.Timeline(_entries)
		timelineControl.Timeline = timeline
	End Sub

	Private Sub timelineControl_BarClicked(sender As Object, e As BarClickedEventArgs) Handles timelineControl.BarClicked
		If e.Button = MouseButtons.Left Then
			HandleLeftClick(e)
		ElseIf e.Button = MouseButtons.Right Then
			HandleRightClick(e)
		End If
	End Sub

	Private Sub HandleLeftClick(e As BarClickedEventArgs)
		' Open the AddEntry form and pass the selected entry's data
		Dim addEntryForm As New AddEntry(e.ClickedEntry)
		If addEntryForm.ShowDialog() = DialogResult.OK Then
			' Add the new entry to the list
			If addEntryForm.Entry IsNot Nothing Then
				If _entries.FirstOrDefault(Function(en) en.ID = addEntryForm.Entry.ID) IsNot Nothing Then
					' If an entry with the same ID already exists, ask the user if they want to overwrite it
					Dim result = MessageBox.Show("Duplicate ID detected, do you want to overwrite the conflicting entry?",
										 "Duplicate ID Detected",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning
			)

					If result = DialogResult.Yes Then
						' Remove the old entry and add the new one
						_entries.RemoveAll(Function(en) en.ID = addEntryForm.Entry.ID)
						_entries.Add(addEntryForm.Entry)
						MessageBox.Show("Entry overwritten successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

						RefreshTimeline()
					Else
						MessageBox.Show("Entry was not added.", "Operation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
					End If
				Else
					_entries.Add(addEntryForm.Entry)
					MessageBox.Show("Entry added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

					RefreshTimeline()
				End If
			End If
		End If
	End Sub

	Private Sub HandleRightClick(e As BarClickedEventArgs)
		Dim result = MessageBox.Show($"Would you like to delete the entry '{e.ClickedEntry.Name}'?",
									 "Confirm Deletion",
									 MessageBoxButtons.YesNo,
									 MessageBoxIcon.Question)

		If result = DialogResult.Yes Then
			_entries.Remove(e.ClickedEntry)
			MessageBox.Show($"Entry '{e.ClickedEntry.Name}' has been deleted.", "Entry Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
			RefreshTimeline()
		End If
	End Sub

	Private Sub TimelineForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
		If e.KeyCode = Keys.Escape Then
			Me.Close()
		End If
	End Sub
End Class