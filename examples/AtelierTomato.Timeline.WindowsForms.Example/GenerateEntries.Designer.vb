<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GenerateEntries
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
		txtAmount = New TextBox()
		Label2 = New Label()
		dtpStartDate = New DateTimePicker()
		Label3 = New Label()
		dtpEndDate = New DateTimePicker()
		btnCancel = New Button()
		btnGenerate = New Button()
		SuspendLayout()
		' 
		' Label1
		' 
		Label1.AutoSize = True
		Label1.Location = New Point(12, 9)
		Label1.Name = "Label1"
		Label1.Size = New Size(54, 15)
		Label1.TabIndex = 0
		Label1.Text = "Amount:"
		' 
		' txtAmount
		' 
		txtAmount.Location = New Point(88, 6)
		txtAmount.Name = "txtAmount"
		txtAmount.Size = New Size(119, 23)
		txtAmount.TabIndex = 2
		' 
		' Label2
		' 
		Label2.AutoSize = True
		Label2.Location = New Point(12, 39)
		Label2.Name = "Label2"
		Label2.Size = New Size(61, 15)
		Label2.TabIndex = 3
		Label2.Text = "Start Date:"
		' 
		' dtpStartDate
		' 
		dtpStartDate.Format = DateTimePickerFormat.Short
		dtpStartDate.Location = New Point(88, 35)
		dtpStartDate.Name = "dtpStartDate"
		dtpStartDate.Size = New Size(119, 23)
		dtpStartDate.TabIndex = 6
		dtpStartDate.Value = New Date(1999, 10, 7, 0, 0, 0, 0)
		' 
		' Label3
		' 
		Label3.AutoSize = True
		Label3.Location = New Point(12, 68)
		Label3.Name = "Label3"
		Label3.Size = New Size(57, 15)
		Label3.TabIndex = 7
		Label3.Text = "End Date:"
		' 
		' dtpEndDate
		' 
		dtpEndDate.Format = DateTimePickerFormat.Short
		dtpEndDate.Location = New Point(88, 64)
		dtpEndDate.Name = "dtpEndDate"
		dtpEndDate.Size = New Size(119, 23)
		dtpEndDate.TabIndex = 8
		dtpEndDate.Value = New Date(2024, 12, 13, 0, 0, 0, 0)
		' 
		' btnCancel
		' 
		btnCancel.Location = New Point(118, 91)
		btnCancel.Name = "btnCancel"
		btnCancel.Size = New Size(89, 23)
		btnCancel.TabIndex = 13
		btnCancel.Text = "&Cancel"
		btnCancel.UseVisualStyleBackColor = True
		' 
		' btnGenerate
		' 
		btnGenerate.Location = New Point(12, 91)
		btnGenerate.Name = "btnGenerate"
		btnGenerate.Size = New Size(89, 23)
		btnGenerate.TabIndex = 11
		btnGenerate.Text = "&Generate"
		btnGenerate.UseVisualStyleBackColor = True
		' 
		' GenerateEntries
		' 
		AcceptButton = btnGenerate
		AutoScaleDimensions = New SizeF(7.0F, 15.0F)
		AutoScaleMode = AutoScaleMode.Font
		CancelButton = btnCancel
		ClientSize = New Size(218, 120)
		Controls.Add(btnCancel)
		Controls.Add(btnGenerate)
		Controls.Add(dtpEndDate)
		Controls.Add(Label3)
		Controls.Add(dtpStartDate)
		Controls.Add(Label2)
		Controls.Add(txtAmount)
		Controls.Add(Label1)
		Name = "GenerateEntries"
		Text = "Generate Entries"
		ResumeLayout(False)
		PerformLayout()
	End Sub

	Friend WithEvents Label1 As Label
	Friend WithEvents txtAmount As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents dtpStartDate As DateTimePicker
	Friend WithEvents Label3 As Label
	Friend WithEvents dtpEndDate As DateTimePicker
	Friend WithEvents btnCancel As Button
	Friend WithEvents btnGenerate As Button
End Class
