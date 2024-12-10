Public Class TimelineEntry
	Public ReadOnly Property ID As String       ' ID that will be used to ensure timeline entries are handled separately even if named the same
	Public ReadOnly Property Name As String     ' The name that will be displayed on the timeline entry
	Public ReadOnly Property StartDate As DateTimeOffset
	Public ReadOnly Property EndDate As DateTimeOffset

	Public Sub New(id As String, name As String, startDate As DateTimeOffset, endDate As DateTimeOffset)
		If startDate > endDate Then
			Throw New ArgumentException("StartDate must be on or before EndDate.", NameOf(startDate))
		End If

		Me.ID = id
		Me.Name = name
		Me.StartDate = startDate
		Me.EndDate = endDate
	End Sub
End Class
