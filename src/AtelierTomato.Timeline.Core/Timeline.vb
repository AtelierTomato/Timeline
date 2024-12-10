Public Class Timeline
	' Private list of entries
	Private ReadOnly _entries As List(Of TimelineEntry)
	' Dictionary to map IDs to pixel offsets and lengths
	Private ReadOnly _pixelData As Dictionary(Of String, (DateTimeOffset As Integer, Length As Integer))
	' Backing fields for global start and end dates
	Private _startDate As DateTimeOffset
	Private _endDate As DateTimeOffset

	' Public read-only access to private members
	Public ReadOnly Property Entries As IReadOnlyList(Of TimelineEntry)
		Get
			Return _entries
		End Get
	End Property
	Public ReadOnly Property PixelData As IReadOnlyDictionary(Of String, (DateTimeOffset As Integer, Length As Integer))
		Get
			Return _pixelData
		End Get
	End Property
	Public ReadOnly Property StartDate As DateTimeOffset
		Get
			Return _startDate
		End Get
	End Property
	Public ReadOnly Property EndDate As DateTimeOffset
		Get
			Return _endDate
		End Get
	End Property

	Public Sub New()
		_entries = New List(Of TimelineEntry)()
		_pixelData = New Dictionary(Of String, (Offset As Integer, Length As Integer))()
		_startDate = DateTimeOffset.MaxValue    ' Initialize with a high value
		_endDate = DateTimeOffset.MinValue      ' Initialize with a low value
	End Sub

	' Add a single entry to the timeline
	Public Sub AddEntry(entry As TimelineEntry)
		' Add the entry via bulk method for consistency
		AddEntries({entry})
	End Sub

	' Add multiple entries
	Public Sub AddEntries(entries As IEnumerable(Of TimelineEntry))
		If Not entries.Any() Then Return

		' Check for duplicate IDs in both existing entries and new entries
		Dim allEntries = _entries.Concat(entries.ToList())
		Dim duplicateIDs = allEntries.GroupBy(Function(e) e.ID) _
									 .Where(Function(g) g.Count() > 1) _
									 .Select(Function(g) g.Key) _
									 .ToList()
		If duplicateIDs.Count <> 0 Then
			Throw New ArgumentException($"Duplicate IDs detected: {String.Join(", ", duplicateIDs)}", NameOf(entries))
		End If

		' Clean up to free resources
		allEntries = Nothing
		duplicateIDs = Nothing

		' Sort entries by start date
		Dim sortedEntries = entries.OrderBy(Function(e) e.StartDate).ToList()

		' Check for changes to start and end dates
		Dim earliestDate = sortedEntries.First().StartDate
		Dim latestDate = sortedEntries.Last().EndDate
		Dim startDateChanged = UpdateGlobalDateIfNeeded(earliestDate, True)
		UpdateGlobalDateIfNeeded(latestDate, False)

		If startDateChanged Then
			RecalculatePixelData()
		End If

		' Calculate pixel data for new entries
		For Each entry In sortedEntries
			_pixelData(entry.ID) = CalculatePixelData(entry)
		Next

		' Add new entries to the list
		_entries.AddRange(sortedEntries)
	End Sub

	' Helper function to update global start or end date if needed
	Private Function UpdateGlobalDateIfNeeded(newDate As DateTimeOffset, isStartDate As Boolean) As Boolean
		If isStartDate Then
			If newDate < _startDate Then
				_startDate = newDate
				Return True
			End If
		Else
			If newDate > _endDate Then
				_endDate = newDate
				Return True
			End If
		End If
		Return False
	End Function

	' Recalculate pixel data for all existing entries
	Private Sub RecalculatePixelData()
		For Each entry In _entries
			_pixelData(entry.ID) = CalculatePixelData(entry)
		Next
	End Sub

	' Calculate pixel data for a single entry
	Private Function CalculatePixelData(entry As TimelineEntry) As (Offset As Integer, Length As Integer)
		Dim offset As Integer = (entry.StartDate - _startDate).Days
		Dim length As Integer = Math.Max(1, (entry.EndDate - entry.StartDate).Days)
		Return (offset, length)
	End Function
End Class
