Imports AtelierTomato.Timeline.Core

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
		TimelineControl1.AutoSize = True
		TimelineControl1.BackColor = Color.White
		TimelineControl1.BarColor = Color.LightBlue
		TimelineControl1.BarFont = New Font("Arial", 10.0F)
		TimelineControl1.BarHeight = 20
		TimelineControl1.BarLengthPerDay = 2
		TimelineControl1.BarLineColor = Color.Black
		TimelineControl1.BarTextcolor = Color.Black
		TimelineControl1.BorderStyle = BorderStyle.FixedSingle
		TimelineControl1.Location = New Point(12, 12)
		TimelineControl1.MonthLabelColor = Color.Black
		TimelineControl1.MonthLabelFont = New Font("Arial", 8.0F)
		TimelineControl1.MonthLabelLineColor = Color.Black
		TimelineControl1.Name = "TimelineControl1"
		TimelineControl1.PaddingBetweenBars = 0
		TimelineControl1.Size = New Size(975, 595)
		TimelineControl1.TabIndex = 0
		TimelineControl1.Timeline = New Core.Timeline(New List(Of TimelineEntry) From {
			New TimelineEntry("1", "one", New DateTimeOffset(2014, 7, 11, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 13, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("2", "two", New DateTimeOffset(2014, 7, 16, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 21, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("3", "three", New DateTimeOffset(2014, 7, 11, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 8, 5, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("4", "four", New DateTimeOffset(2014, 7, 12, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 23, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("5", "five", New DateTimeOffset(2014, 8, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 8, 3, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("6", "six", New DateTimeOffset(2014, 7, 13, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 13, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("6.2", "six.2", New DateTimeOffset(2014, 7, 14, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 14, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("7", "seven", New DateTimeOffset(2014, 7, 24, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("8", "eight", New DateTimeOffset(2015, 1, 2, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2016, 3, 4, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("9", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("10", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("11", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("12", "4134nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("92", "nqerine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("93", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("94", "rnine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("95", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("96", "nine4", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("97", "nin6e", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("98", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("99", "niq431erne", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("90", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("911", "ni413ne", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("922", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("91", "nineqr", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("932", "nreqine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 31, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("934", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("954", "nir32ne", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("914", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero)),
			New TimelineEntry("83", "eight3", New DateTimeOffset(2014, 7, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 1, 20, 0, 0, 0, TimeSpan.Zero))
		})
		TimelineControl1.DrawVerticalLine = VerticalLineMode.Full
		TimelineControl1.BarLabelAlignment = ContentAlignment.MiddleLeft
		TimelineControl1.MonthLabelAlignment = ContentAlignment.MiddleCenter
		' 
		' Form1
		' 
		AutoScaleDimensions = New SizeF(7.0F, 15.0F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(999, 619)
		Controls.Add(TimelineControl1)
		Name = "Form1"
		Text = "Form1"
		ResumeLayout(False)
		PerformLayout()
	End Sub

	Friend WithEvents TimelineControl1 As TimelineControl

End Class
