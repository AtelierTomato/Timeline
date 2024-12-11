Imports AtelierTomato.Timeline.Core

Public Class Form1
	Private ReadOnly entries As New List(Of TimelineEntry) From {
		New TimelineEntry("1", "one", New DateTimeOffset(1998, 11, 5, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2008, 6, 17, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("2", "two", New DateTimeOffset(2014, 6, 28, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 7, 7, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("3", "three", New DateTimeOffset(1976, 4, 23, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(1987, 9, 15, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("4", "four", New DateTimeOffset(2009, 3, 6, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2013, 6, 9, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("5", "five", New DateTimeOffset(1965, 3, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2010, 9, 8, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("6", "six", New DateTimeOffset(1965, 3, 2, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2010, 9, 8, 0, 0, 0, TimeSpan.Zero))
	}

	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		TimelineControl1.Timeline = New Core.Timeline
		TimelineControl1.Timeline.AddEntries(entries)
	End Sub
End Class
