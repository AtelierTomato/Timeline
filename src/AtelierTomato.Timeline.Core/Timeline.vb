Public Class Timeline
	' Private list of entries
	Private _entries As List(Of TimelineEntry)
	' Dictionary to map IDs to pixel offsets and lengths
	Private ReadOnly _graphData As Dictionary(Of String, TimelineEntryGraphData)
	' Backing fields for global start and end dates and max stack level
	Private _startDate As DateTimeOffset
	Private _endDate As DateTimeOffset
	Private _maxStackLevel As Integer

	' Public read-only access to private members
	Public ReadOnly Property Entries As IReadOnlyList(Of TimelineEntry)
		Get
			Return _entries
		End Get
	End Property
	Public ReadOnly Property GraphData As IReadOnlyDictionary(Of String, TimelineEntryGraphData)
		Get
			Return _graphData
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
	Public ReadOnly Property MaxStackLevel As Integer
		Get
			Return _maxStackLevel
		End Get
	End Property

	Public Sub New()
		_entries = New List(Of TimelineEntry)()
		_graphData = New Dictionary(Of String, TimelineEntryGraphData)()
		_startDate = DateTimeOffset.MaxValue    ' Initialize with a high value
		_endDate = DateTimeOffset.MinValue      ' Initialize with a low value
	End Sub
	Public Sub New(entries As IEnumerable(Of TimelineEntry))
		_entries = New List(Of TimelineEntry)()
		_graphData = New Dictionary(Of String, TimelineEntryGraphData)()
		_startDate = DateTimeOffset.MaxValue    ' Initialize with a high value
		_endDate = DateTimeOffset.MinValue      ' Initialize with a low value
		AddEntries(entries)
	End Sub
	Public Sub New(entry As TimelineEntry)
		_entries = New List(Of TimelineEntry)()
		_graphData = New Dictionary(Of String, TimelineEntryGraphData)()
		_startDate = DateTimeOffset.MaxValue    ' Initialize with a high value
		_endDate = DateTimeOffset.MinValue      ' Initialize with a low value
		AddEntry(entry)
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
			Throw New ArgumentException($"Duplicate IDs detected: {String.Join(", ", duplicateIDs)}.", NameOf(entries))
		End If

		' Clean up to free resources
		allEntries = Nothing
		duplicateIDs = Nothing

		' Sort entries by start date
		Dim sortedByEarliestDateEntries = entries.OrderBy(Function(e) e.StartDate).ToList()

		' Check for changes to start and end dates
		Dim earliestDate = sortedByEarliestDateEntries.First().StartDate
		Dim latestDate = entries.OrderBy(Function(e) e.EndDate).Last().EndDate
		Dim startDateChanged = UpdateGlobalDateIfNeeded(earliestDate, True)
		UpdateGlobalDateIfNeeded(latestDate, False)

		' Add new entries to the list
		_entries.AddRange(sortedByEarliestDateEntries)

		' Sort the entries in _entries
		_entries = _entries.OrderBy(Function(e) e.StartDate).ToList()

		If startDateChanged Then
			CalculateGraphData(_entries)
		Else
			CalculateGraphData(sortedByEarliestDateEntries)
		End If
	End Sub

	Public Sub RemoveEntry(id As String)
		' Remove the entry via bulk method for consistency
		RemoveEntries({id})
	End Sub

	Public Sub RemoveEntries(ids As IEnumerable(Of String))
		' Check if there are any IDs to remove.
		If Not ids.Any() Then Return

		' Remove the entries from the _entries list
		_entries.RemoveAll(Function(entry) ids.Contains(entry.ID))

		' Remove the entries from the _graphData dictionary
		For Each id In ids
			If _graphData.ContainsKey(id) Then
				_graphData.Remove(id)
			End If
		Next

		' Recalculate the global start and end dates after removing entries
		If _entries.Count <> 0 Then
			_startDate = _entries.Min(Function(e) e.StartDate)
			_endDate = _entries.Max(Function(e) e.EndDate)
		Else
			_startDate = DateTimeOffset.MaxValue
			_endDate = DateTimeOffset.MinValue
		End If

		' Recalculate the graph data after removal
		CalculateGraphData(_entries)
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
	Private Sub CalculateGraphData(entries As IEnumerable(Of TimelineEntry))
		' List to track the stack levels currently in use
		Dim occupiedStackLevels As New Dictionary(Of Integer, DateTimeOffset)()
		Dim maxStackLevel As Integer = 0

		' Iterate through each entry and assign stack level
		For Each entry In _entries
			' Free stack levels that are no longer in use (end date is before the current entry's start date)
			For Each level In occupiedStackLevels.Keys.ToList()
				If occupiedStackLevels(level) < entry.StartDate Then
					occupiedStackLevels.Remove(level)   ' Free up the stack level
				End If
			Next

			' Find the first available StackLevel
			Dim stackLevel As Integer = 1
			While occupiedStackLevels.ContainsKey(stackLevel)
				stackLevel += 1
			End While

			' Assign the StackLevel to the current entry and update the graph data with the assigned stack level, leaving other values intact
			If Not _graphData.ContainsKey(entry.ID) Then
				' If the entry does not exist in the graph data, create a new entry with negative placeholder Offset and Length
				_graphData(entry.ID) = New TimelineEntryGraphData(-1, -1, stackLevel)
			Else
				' If the entry already exists, just update the StackLevel
				_graphData(entry.ID) = New TimelineEntryGraphData(_graphData(entry.ID).Offset, _graphData(entry.ID).Length, stackLevel)
			End If

			' Store the end date for this stack level so we can track if it's freed
			occupiedStackLevels(stackLevel) = entry.EndDate

			' Check if current stackLevel is higher than maxStackLevel, if so, make maxStackLevel current stackLevel
			If stackLevel > maxStackLevel Then
				maxStackLevel = stackLevel
			End If
		Next
		' Set _maxStackLevel to new max stack level
		_maxStackLevel = maxStackLevel
		For Each entry In entries
			Dim result = CalculateGraphOffsetAndLength(entry)
			If Not _graphData.ContainsKey(entry.ID) Then
				Throw New InvalidOperationException($"Offset data was not found for entry with ID ""{entry.ID}"".")
			Else
				_graphData(entry.ID).Offset = result.Offset
				_graphData(entry.ID).Length = result.Length
			End If
		Next

		' Validation: Ensure that no negative values exist in any of the integer fields (or lower than 1 for StackLevel) of TimelineEntryGraphData
		For Each entry In _graphData.Values
			If entry.Offset < 0 OrElse entry.Length < 0 Then
				Throw New InvalidOperationException($"Negative values detected in {NameOf(TimelineEntryGraphData)} fields. {NameOf(TimelineEntryGraphData.Offset)} and {NameOf(TimelineEntryGraphData.Length)} must not be negative.")
			End If
		Next
	End Sub

	' Calculate pixel data for a single entry
	Private Function CalculateGraphOffsetAndLength(entry As TimelineEntry) As (Offset As Integer, Length As Integer)
		Dim offset As Integer = (entry.StartDate - _startDate).Days
		Dim length As Integer = Math.Max(1, (entry.EndDate - entry.StartDate).Days)
		Return (offset, length)
	End Function
End Class
