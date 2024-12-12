Imports AtelierTomato.Timeline.Core

Public Class BarClickedEventArgs
	Inherits EventArgs

	Public Property ClickedEntry As TimelineEntry

	Public Sub New(entry As TimelineEntry)
		Me.ClickedEntry = entry
	End Sub
End Class
