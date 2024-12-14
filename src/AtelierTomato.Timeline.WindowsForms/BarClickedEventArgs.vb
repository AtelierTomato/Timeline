Imports AtelierTomato.Timeline.Core

Public Class BarClickedEventArgs
	Inherits MouseEventArgs

	Public Property ClickedEntry As TimelineEntry

	Public Sub New(entry As TimelineEntry, e As MouseEventArgs)
		MyBase.New(e.Button, e.Clicks, e.X, e.Y, e.Delta)
		Me.ClickedEntry = entry
	End Sub
End Class
