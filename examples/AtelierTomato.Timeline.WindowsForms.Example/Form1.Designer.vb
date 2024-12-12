<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(disposing As Boolean)
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		TimelineControl1 = New TimelineControl()
		SuspendLayout()
		' 
		' TimelineControl1
		' 
		TimelineControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
		TimelineControl1.BackColor = Color.White
		TimelineControl1.BarColor = Color.LightBlue
		TimelineControl1.BarFont = New Font("Arial", 10F)
		TimelineControl1.BarHeight = 20
		TimelineControl1.BarLengthMultiplier = 2
		TimelineControl1.BarTextcolor = Color.Black
		TimelineControl1.Location = New Point(12, 12)
		TimelineControl1.MonthLabelColor = Color.Black
		TimelineControl1.MonthLabelFont = New Font("Arial", 8F)
		TimelineControl1.MonthLabelHeight = 20
		TimelineControl1.Name = "TimelineControl1"
		TimelineControl1.PaddingAboveBars = 10
		TimelineControl1.PaddingBetweenBars = 0
		TimelineControl1.Size = New Size(975, 595)
		TimelineControl1.TabIndex = 0
		TimelineControl1.Timeline = Nothing
		' 
		' Form1
		' 
		AutoScaleDimensions = New SizeF(7F, 15F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(999, 619)
		Controls.Add(TimelineControl1)
		Name = "Form1"
		Text = "Form1"
		ResumeLayout(False)
	End Sub

	Friend WithEvents TimelineControl1 As TimelineControl

End Class
