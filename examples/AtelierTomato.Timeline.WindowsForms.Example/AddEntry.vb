Imports System.ComponentModel
Imports AtelierTomato.Timeline.Core

Public Class AddEntry
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
	Public Property Entry As TimelineEntry

	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

	End Sub
	Public Sub New(entry As TimelineEntry)
		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		Me.Entry = entry

		' Pre-fill the form controls with the entry's data
		If entry IsNot Nothing Then
			txtID.Text = Me.Entry.ID
			txtName.Text = Me.Entry.Name
			dtpStartDate.Value = Me.Entry.StartDate.DateTime
			dtpEndDate.Value = Me.Entry.EndDate.DateTime
		End If
	End Sub

	Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
		Try
			Dim id As String = txtID.Text
			Dim name As String = txtName.Text
			Dim startDate As DateTimeOffset = dtpStartDate.Value
			Dim endDate As DateTimeOffset = dtpEndDate.Value

			' Ensure all fields are filled
			If String.IsNullOrWhiteSpace(id) OrElse String.IsNullOrWhiteSpace(name) Then
				MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
				Return
			End If

			If startDate > endDate Then
				MessageBox.Show("The start date should be before the end date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
				Return
			End If

			' Validate and create the entry
			Entry = New TimelineEntry(id, name, startDate, endDate)

			' Close the form with success
			Me.DialogResult = DialogResult.OK
			Me.Close()
		Catch ex As Exception
			MessageBox.Show($"Error creating entry: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
		txtID.Text = String.Empty
		txtName.Text = String.Empty
		dtpStartDate.Value = New Date(1999, 10, 7, 0, 0, 0, 0)
		dtpEndDate.Value = New Date(2024, 12, 13, 0, 0, 0, 0)
	End Sub

	Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
		Me.DialogResult = DialogResult.Cancel
		Me.Close()
	End Sub
End Class