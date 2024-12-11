Imports FluentAssertions
Imports Xunit

Public Class TimelineTests
	Private ReadOnly entries As New List(Of TimelineEntry) From {
		New TimelineEntry("1", "one", New DateTimeOffset(1998, 11, 5, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2008, 6, 17, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("2", "two", New DateTimeOffset(2014, 6, 28, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2015, 7, 7, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("3", "three", New DateTimeOffset(1976, 4, 23, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(1987, 9, 15, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("4", "four", New DateTimeOffset(2009, 3, 6, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2013, 6, 9, 0, 0, 0, TimeSpan.Zero)),
		New TimelineEntry("5", "five", New DateTimeOffset(1965, 3, 1, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2010, 9, 8, 0, 0, 0, TimeSpan.Zero))
	}
	Private ReadOnly duplicateEntry As New TimelineEntry("1", "oneTwo", New DateTimeOffset(1950, 2, 4, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2010, 10, 23, 0, 0, 0, TimeSpan.Zero))
	Private ReadOnly extraEntry As New TimelineEntry("6", "six", New DateTimeOffset(2010, 10, 12, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2013, 6, 21, 0, 0, 0, TimeSpan.Zero))
	Private ReadOnly earlierEntry As New TimelineEntry("7", "seven", New DateTimeOffset(1943, 10, 7, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2010, 11, 20, 0, 0, 0, TimeSpan.Zero))
	Private ReadOnly laterEntry As New TimelineEntry("8", "eight", New DateTimeOffset(2010, 4, 16, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2021, 8, 12, 0, 0, 0, TimeSpan.Zero))
	Private ReadOnly sameDayEntry As New TimelineEntry("9", "nine", New DateTimeOffset(2024, 12, 25, 0, 0, 0, TimeSpan.Zero), New DateTimeOffset(2024, 12, 25, 0, 0, 0, TimeSpan.Zero))

	<Fact>
	Sub TimelineInitializationTest()
		Dim timeline As New Timeline()
		timeline.AddEntries(entries)

		' All entries in entries are included in timeline.Entries
		timeline.Entries.Should.BeEquivalentTo(entries)
		' Entries in the timeline are sorted by StartDate
		timeline.Entries(0).Should.BeEquivalentTo(entries(4))
		timeline.Entries(1).Should.BeEquivalentTo(entries(2))
		timeline.Entries(2).Should.BeEquivalentTo(entries(0))
		timeline.Entries(3).Should.BeEquivalentTo(entries(3))
		timeline.Entries(4).Should.BeEquivalentTo(entries(1))
		' Overall StartDate, EndDate, and MaxStackLevel are correctly calculated
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		timeline.MaxStackLevel.Should.Be(2)
		' GraphData is made for each entry and is correct
		timeline.GraphData.Count.Should.Be(5)
		timeline.GraphData("1").Should.BeEquivalentTo(New TimelineEntryGraphData(12302, 3512, 2))
		timeline.GraphData("2").Should.BeEquivalentTo(New TimelineEntryGraphData(18016, 374, 1))
		timeline.GraphData("3").Should.BeEquivalentTo(New TimelineEntryGraphData(4071, 4162, 2))
		timeline.GraphData("4").Should.BeEquivalentTo(New TimelineEntryGraphData(16076, 1556, 2))
		timeline.GraphData("5").Should.BeEquivalentTo(New TimelineEntryGraphData(0, 16627, 1))
	End Sub

	<Fact>
	Sub TimelineDuplicateEntryInInputEntriesFailTest()
		Dim timeline As New Timeline()
		Dim act As Action = Sub()
								timeline.AddEntries(entries.Concat({duplicateEntry}))
							End Sub

		act.Should.Throw(Of ArgumentException)().WithMessage("Duplicate IDs detected: 1. (Parameter 'entries')")
	End Sub

	<Fact>
	Sub TimelineDuplicateEntryBetweenInputAndCurrentEntriesFailTest()
		Dim timeline As New Timeline()
		timeline.AddEntries(entries)
		Dim act As Action = Sub()
								timeline.AddEntry(duplicateEntry)
							End Sub

		act.Should.Throw(Of ArgumentException)().WithMessage("Duplicate IDs detected: 1. (Parameter 'entries')")
	End Sub

	<Fact>
	Sub TimelineAddTest()
		Dim timeline As New Timeline
		timeline.AddEntries(entries)

		' All entries in entries are included in timeline.Entries
		timeline.Entries.Should.BeEquivalentTo(entries)
		' Entries in the timeline are sorted by StartDate
		timeline.Entries(0).Should.BeEquivalentTo(entries(4))
		timeline.Entries(1).Should.BeEquivalentTo(entries(2))
		timeline.Entries(2).Should.BeEquivalentTo(entries(0))
		timeline.Entries(3).Should.BeEquivalentTo(entries(3))
		timeline.Entries(4).Should.BeEquivalentTo(entries(1))
		' Overall StartDate, EndDate, and MaxStackLevel are correctly calculated
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		timeline.MaxStackLevel.Should.Be(2)
		' GraphData is made for each entry and is correct
		timeline.GraphData.Count.Should.Be(5)
		timeline.GraphData("1").Should.BeEquivalentTo(New TimelineEntryGraphData(12302, 3512, 2))
		timeline.GraphData("2").Should.BeEquivalentTo(New TimelineEntryGraphData(18016, 374, 1))
		timeline.GraphData("3").Should.BeEquivalentTo(New TimelineEntryGraphData(4071, 4162, 2))
		timeline.GraphData("4").Should.BeEquivalentTo(New TimelineEntryGraphData(16076, 1556, 2))
		timeline.GraphData("5").Should.BeEquivalentTo(New TimelineEntryGraphData(0, 16627, 1))

		' Add an extra entry
		timeline.AddEntry(extraEntry)

		' All entries in entries + extraEntry are included in timeline.Entries
		timeline.Entries.Should.BeEquivalentTo(entries.Concat({extraEntry}))
		' Entries in the timeline have been re-sorted by StartDate
		timeline.Entries(0).Should.BeEquivalentTo(entries(4))
		timeline.Entries(1).Should.BeEquivalentTo(entries(2))
		timeline.Entries(2).Should.BeEquivalentTo(entries(0))
		timeline.Entries(3).Should.BeEquivalentTo(entries(3))
		timeline.Entries(4).Should.BeEquivalentTo(extraEntry)
		timeline.Entries(5).Should.BeEquivalentTo(entries(1))
		' Overall StartDate, EndDate, and StackLevel have not changed
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		timeline.MaxStackLevel.Should.Be(2)
		' GraphData has been updated to include extraEntry and only the StackLevel for prior entries has changed
		timeline.GraphData.Count.Should.Be(6)
		timeline.GraphData("1").Should.BeEquivalentTo(New TimelineEntryGraphData(12302, 3512, 2))
		timeline.GraphData("2").Should.BeEquivalentTo(New TimelineEntryGraphData(18016, 374, 1))
		timeline.GraphData("3").Should.BeEquivalentTo(New TimelineEntryGraphData(4071, 4162, 2))
		timeline.GraphData("4").Should.BeEquivalentTo(New TimelineEntryGraphData(16076, 1556, 2))
		timeline.GraphData("5").Should.BeEquivalentTo(New TimelineEntryGraphData(0, 16627, 1))
		timeline.GraphData("6").Should.BeEquivalentTo(New TimelineEntryGraphData(16661, 983, 1))
	End Sub

	<Fact>
	Sub TimelineAddEarlierEntryTest()
		Dim timeline As New Timeline
		timeline.AddEntries(entries)

		' All entries in entries are included in timeline.Entries
		timeline.Entries.Should.BeEquivalentTo(entries)
		' Entries in the timeline are sorted by StartDate
		timeline.Entries(0).Should.BeEquivalentTo(entries(4))
		timeline.Entries(1).Should.BeEquivalentTo(entries(2))
		timeline.Entries(2).Should.BeEquivalentTo(entries(0))
		timeline.Entries(3).Should.BeEquivalentTo(entries(3))
		timeline.Entries(4).Should.BeEquivalentTo(entries(1))
		' Overall StartDate, EndDate, and MaxStackLevel are correctly calculated
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		timeline.MaxStackLevel.Should.Be(2)
		' GraphData is made for each entry and is correct
		timeline.GraphData.Count.Should.Be(5)
		timeline.GraphData("1").Should.BeEquivalentTo(New TimelineEntryGraphData(12302, 3512, 2))
		timeline.GraphData("2").Should.BeEquivalentTo(New TimelineEntryGraphData(18016, 374, 1))
		timeline.GraphData("3").Should.BeEquivalentTo(New TimelineEntryGraphData(4071, 4162, 2))
		timeline.GraphData("4").Should.BeEquivalentTo(New TimelineEntryGraphData(16076, 1556, 2))
		timeline.GraphData("5").Should.BeEquivalentTo(New TimelineEntryGraphData(0, 16627, 1))

		' Add an earlier entry
		timeline.AddEntry(earlierEntry)

		' All entries in entries + earlierEntry are included in timeline.Entries
		timeline.Entries.Should.BeEquivalentTo(entries.Concat({earlierEntry}))
		' Entries in the timeline have been re-sorted by StartDate
		timeline.Entries(0).Should.BeEquivalentTo(earlierEntry)
		timeline.Entries(1).Should.BeEquivalentTo(entries(4))
		timeline.Entries(2).Should.BeEquivalentTo(entries(2))
		timeline.Entries(3).Should.BeEquivalentTo(entries(0))
		timeline.Entries(4).Should.BeEquivalentTo(entries(3))
		timeline.Entries(5).Should.BeEquivalentTo(entries(1))
		' Overall StartDate and MaxStackLevel have changed, but EndDate has not
		timeline.StartDate.Should.Be(earlierEntry.StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		timeline.MaxStackLevel.Should.Be(3)
		' GraphData has been updated to include earlierEntry, and the Offset (but not the length) of all entries should have changed, along with some StackLevels.
		timeline.GraphData.Count.Should.Be(6)
		timeline.GraphData("1").Should.BeEquivalentTo(New TimelineEntryGraphData(12302 + 7816, 3512, 3))
		timeline.GraphData("2").Should.BeEquivalentTo(New TimelineEntryGraphData(18016 + 7816, 374, 1))
		timeline.GraphData("3").Should.BeEquivalentTo(New TimelineEntryGraphData(4071 + 7816, 4162, 3))
		timeline.GraphData("4").Should.BeEquivalentTo(New TimelineEntryGraphData(16076 + 7816, 1556, 3))
		timeline.GraphData("5").Should.BeEquivalentTo(New TimelineEntryGraphData(7816, 16627, 2))
		timeline.GraphData("7").Should.BeEquivalentTo(New TimelineEntryGraphData(0, 24516, 1))
	End Sub

	<Fact>
	Sub TimeLineAddLaterEntryTest()
		Dim timeline As New Timeline
		timeline.AddEntries(entries)

		' All entries in entries are included in timeline.Entries
		timeline.Entries.Should.BeEquivalentTo(entries)
		' Entries in the timeline are sorted by StartDate
		timeline.Entries(0).Should.BeEquivalentTo(entries(4))
		timeline.Entries(1).Should.BeEquivalentTo(entries(2))
		timeline.Entries(2).Should.BeEquivalentTo(entries(0))
		timeline.Entries(3).Should.BeEquivalentTo(entries(3))
		timeline.Entries(4).Should.BeEquivalentTo(entries(1))
		' Overall StartDate, EndDate, and MaxStackLevel are correctly calculated
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		timeline.MaxStackLevel.Should.Be(2)
		' GraphData is made for each entry and is correct
		timeline.GraphData.Count.Should.Be(5)
		timeline.GraphData("1").Should.BeEquivalentTo(New TimelineEntryGraphData(12302, 3512, 2))
		timeline.GraphData("2").Should.BeEquivalentTo(New TimelineEntryGraphData(18016, 374, 1))
		timeline.GraphData("3").Should.BeEquivalentTo(New TimelineEntryGraphData(4071, 4162, 2))
		timeline.GraphData("4").Should.BeEquivalentTo(New TimelineEntryGraphData(16076, 1556, 2))
		timeline.GraphData("5").Should.BeEquivalentTo(New TimelineEntryGraphData(0, 16627, 1))

		' Add an earlier entry
		timeline.AddEntry(laterEntry)

		' All entries in entries + laterEntry are included in timeline.Entries
		timeline.Entries.Should.BeEquivalentTo(entries.Concat({laterEntry}))
		' Entries in the timeline have been re-sorted by StartDate
		timeline.Entries(0).Should.BeEquivalentTo(entries(4))
		timeline.Entries(1).Should.BeEquivalentTo(entries(2))
		timeline.Entries(2).Should.BeEquivalentTo(entries(0))
		timeline.Entries(3).Should.BeEquivalentTo(entries(3))
		timeline.Entries(4).Should.BeEquivalentTo(laterEntry)
		timeline.Entries(5).Should.BeEquivalentTo(entries(1))
		' Overall EndDate and MaxStackLevel have changed, but StartDate has not
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(laterEntry.EndDate)
		timeline.MaxStackLevel.Should.Be(3)
		' GraphData has been updated to include laterEntry and the data for prior entries has not changed
		timeline.GraphData.Count.Should.Be(6)
		timeline.GraphData("1").Should.BeEquivalentTo(New TimelineEntryGraphData(12302, 3512, 2))
		timeline.GraphData("2").Should.BeEquivalentTo(New TimelineEntryGraphData(18016, 374, 1))
		timeline.GraphData("3").Should.BeEquivalentTo(New TimelineEntryGraphData(4071, 4162, 2))
		timeline.GraphData("4").Should.BeEquivalentTo(New TimelineEntryGraphData(16076, 1556, 2))
		timeline.GraphData("5").Should.BeEquivalentTo(New TimelineEntryGraphData(0, 16627, 1))
		timeline.GraphData("8").Should.BeEquivalentTo(New TimelineEntryGraphData(16482, 4136, 3))
	End Sub

	<Fact>
	Sub TimelineSameDayAppearsAsOnePixelTest()
		Dim timeline As New Timeline
		timeline.AddEntry(sameDayEntry)

		timeline.Entries.Should.BeEquivalentTo({sameDayEntry})
		timeline.Entries(0).Should.BeEquivalentTo(sameDayEntry)
		timeline.StartDate.Should.Be(sameDayEntry.StartDate)
		timeline.EndDate.Should.Be(sameDayEntry.EndDate)
		timeline.MaxStackLevel.Should.Be(1)
		timeline.GraphData.Count.Should.Be(1)
		timeline.GraphData("9").Should.BeEquivalentTo(New TimelineEntryGraphData(0, 1, 1))
	End Sub
End Class
