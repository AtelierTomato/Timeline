<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TimelineForm
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
		timelineControl = New TimelineControl()
		SuspendLayout()
		' 
		' timelineControl
		' 
		timelineControl.BackColor = Color.White
		timelineControl.BarColor = Color.LightBlue
		timelineControl.BarFont = New Font("Arial", 10F)
		timelineControl.BarHeight = 20
		timelineControl.BarLabelAlignment = ContentAlignment.MiddleLeft
		timelineControl.BarLabelPadding = New Padding(3, 0, 0, 0)
		timelineControl.BarLengthPerDay = 2
		timelineControl.BarLineColor = Color.Black
		timelineControl.BarTextcolor = Color.Black
		timelineControl.Dock = DockStyle.Fill
		timelineControl.DrawHorizontalLine = True
		timelineControl.DrawVerticalLine = VerticalLineMode.BelowMonthLabels
		timelineControl.Location = New Point(0, 0)
		timelineControl.MonthLabelAlignment = ContentAlignment.MiddleCenter
		timelineControl.MonthLabelColor = Color.Black
		timelineControl.MonthLabelFont = New Font("Arial", 8F)
		timelineControl.MonthLabelHeight = 20
		timelineControl.MonthLabelLineColor = Color.Black
		timelineControl.MonthLabelPadding = New Padding(0)
		timelineControl.Name = "timelineControl"
		timelineControl.PaddingBetweenBars = 0
		timelineControl.Size = New Size(800, 450)
		timelineControl.TabIndex = 0
		timelineControl.Timeline = Nothing
		' 
		' TimelineForm
		' 
		AutoScaleDimensions = New SizeF(7F, 15F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(800, 450)
		Controls.Add(timelineControl)
		Name = "TimelineForm"
		Text = "Timeline"
		ResumeLayout(False)
	End Sub

	Friend WithEvents timelineControl As TimelineControl
End Class
