Imports AtelierTomato.Timeline.Core

Public Class Form1
	Private ReadOnly entries As New List(Of TimelineEntry) From {
		New TimelineEntry("1", "one", New DateTimeOffset(2014, 7, 11, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 13, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("2", "two", New DateTimeOffset(2014, 7, 16, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 21, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("3", "three", New DateTimeOffset(2014, 7, 11, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 8, 5, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("4", "four", New DateTimeOffset(2014, 7, 12, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 23, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("5", "five", New DateTimeOffset(2014, 8, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 8, 3, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("6", "six", New DateTimeOffset(2014, 7, 13, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2014, 7, 13, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("7", "seven", New DateTimeOffset(2014, 7, 24, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("8", "eight", New DateTimeOffset(2015, 1, 2, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2016, 3, 4, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("9", "nine", New DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 5, 20, 0, 0, 0, TimeSpan.Zero))
	}

	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		TimelineControl1.Timeline = New Core.Timeline
		TimelineControl1.Timeline.AddEntries(entries)
	End Sub
End Class
