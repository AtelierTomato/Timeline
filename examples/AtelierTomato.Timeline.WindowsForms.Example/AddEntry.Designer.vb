<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddEntry
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Label1 = New Label()
		txtID = New TextBox()
		Label2 = New Label()
		txtName = New TextBox()
		Label3 = New Label()
		dtpStartDate = New DateTimePicker()
		Label4 = New Label()
		dtpEndDate = New DateTimePicker()
		btnAdd = New Button()
		btnClear = New Button()
		btnCancel = New Button()
		SuspendLayout()
		' 
		' Label1
		' 
		Label1.AutoSize = True
		Label1.Location = New Point(12, 9)
		Label1.Name = "Label1"
		Label1.Size = New Size(21, 15)
		Label1.TabIndex = 0
		Label1.Text = "ID:"
		' 
		' txtID
		' 
		txtID.Location = New Point(89, 6)
		txtID.Name = "txtID"
		txtID.Size = New Size(119, 23)
		txtID.TabIndex = 1
		' 
		' Label2
		' 
		Label2.AutoSize = True
		Label2.Location = New Point(12, 38)
		Label2.Name = "Label2"
		Label2.Size = New Size(42, 15)
		Label2.TabIndex = 2
		Label2.Text = "Name:"
		' 
		' txtName
		' 
		txtName.Location = New Point(89, 35)
		txtName.Name = "txtName"
		txtName.Size = New Size(119, 23)
		txtName.TabIndex = 3
		' 
		' Label3
		' 
		Label3.AutoSize = True
		Label3.Location = New Point(12, 70)
		Label3.Name = "Label3"
		Label3.Size = New Size(61, 15)
		Label3.TabIndex = 4
		Label3.Text = "Start Date:"
		' 
		' dtpStartDate
		' 
		dtpStartDate.Format = DateTimePickerFormat.Short
		dtpStartDate.Location = New Point(89, 64)
		dtpStartDate.Name = "dtpStartDate"
		dtpStartDate.Size = New Size(119, 23)
		dtpStartDate.TabIndex = 5
		dtpStartDate.Value = New Date(1999, 10, 7, 0, 0, 0, 0)
		' 
		' Label4
		' 
		Label4.AutoSize = True
		Label4.Location = New Point(12, 99)
		Label4.Name = "Label4"
		Label4.Size = New Size(57, 15)
		Label4.TabIndex = 6
		Label4.Text = "End Date:"
		' 
		' dtpEndDate
		' 
		dtpEndDate.Format = DateTimePickerFormat.Short
		dtpEndDate.Location = New Point(89, 93)
		dtpEndDate.Name = "dtpEndDate"
		dtpEndDate.Size = New Size(119, 23)
		dtpEndDate.TabIndex = 7
		dtpEndDate.Value = New Date(2024, 12, 13, 0, 0, 0, 0)
		' 
		' btnAdd
		' 
		btnAdd.Location = New Point(13, 128)
		btnAdd.Name = "btnAdd"
		btnAdd.Size = New Size(61, 23)
		btnAdd.TabIndex = 8
		btnAdd.Text = "&Add"
		btnAdd.UseVisualStyleBackColor = True
		' 
		' btnClear
		' 
		btnClear.Location = New Point(80, 128)
		btnClear.Name = "btnClear"
		btnClear.Size = New Size(61, 23)
		btnClear.TabIndex = 9
		btnClear.Text = "C&lear"
		btnClear.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		btnCancel.Location = New Point(147, 128)
		btnCancel.Name = "btnCancel"
		btnCancel.Size = New Size(61, 23)
		btnCancel.TabIndex = 10
		btnCancel.Text = "&Cancel"
		btnCancel.UseVisualStyleBackColor = True
		' 
		' AddEntry
		' 
		AcceptButton = btnAdd
		AutoScaleDimensions = New SizeF(7F, 15F)
		AutoScaleMode = AutoScaleMode.Font
		CancelButton = btnCancel
		ClientSize = New Size(221, 162)
		Controls.Add(btnCancel)
		Controls.Add(btnClear)
		Controls.Add(btnAdd)
		Controls.Add(dtpEndDate)
		Controls.Add(Label4)
		Controls.Add(dtpStartDate)
		Controls.Add(Label3)
		Controls.Add(txtName)
		Controls.Add(Label2)
		Controls.Add(txtID)
		Controls.Add(Label1)
		FormBorderStyle = FormBorderStyle.FixedToolWindow
		Name = "AddEntry"
		Text = "Add Or Edit Entry"
		ResumeLayout(False)
		PerformLayout()
	End Sub

	Friend WithEvents Label1 As Label
	Friend WithEvents txtID As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents txtName As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents dtpStartDate As DateTimePicker
	Friend WithEvents Label4 As Label
	Friend WithEvents dtpEndDate As DateTimePicker
	Friend WithEvents btnAdd As Button
	Friend WithEvents btnClear As Button
	Friend WithEvents btnCancel As Button
End Class
