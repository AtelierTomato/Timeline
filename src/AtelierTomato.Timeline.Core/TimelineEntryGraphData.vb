Public Class TimelineEntryGraphData
	Public Property Offset As Integer       ' Defines how many days the StartDate is offset from the timeline's StartDate
	Public Property Length As Integer       ' Defines how many days are between the StartDate and EndDate
	Public Property StackLevel As Integer   ' Defines what level the bar will be placed on the graph, with two bars not being able to overlap
	Public Sub New(offset As Integer, length As Integer, stackLevel As Integer)
		Me.Offset = offset
		Me.Length = length
		Me.StackLevel = stackLevel
	End Sub
End Class
