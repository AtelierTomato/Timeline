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
		' Overall StartDate and EndDate are correctly calculated
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		' PixelData is made for each entry and is correct
		timeline.PixelData.Count.Should.Be(5)
		timeline.PixelData("1").Offset.Should.Be(12302)
		timeline.PixelData("1").Length.Should.Be(3512)
		timeline.PixelData("2").Offset.Should.Be(18016)
		timeline.PixelData("2").Length.Should.Be(374)
		timeline.PixelData("3").Offset.Should.Be(4071)
		timeline.PixelData("3").Length.Should.Be(4162)
		timeline.PixelData("4").Offset.Should.Be(16076)
		timeline.PixelData("4").Length.Should.Be(1556)
		timeline.PixelData("5").Offset.Should.Be(0)
		timeline.PixelData("5").Length.Should.Be(16627)
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
		' Overall StartDate and EndDate are correctly calculated
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		' PixelData is made for each entry and is correct
		timeline.PixelData.Count.Should.Be(5)
		timeline.PixelData("1").Offset.Should.Be(12302)
		timeline.PixelData("1").Length.Should.Be(3512)
		timeline.PixelData("2").Offset.Should.Be(18016)
		timeline.PixelData("2").Length.Should.Be(374)
		timeline.PixelData("3").Offset.Should.Be(4071)
		timeline.PixelData("3").Length.Should.Be(4162)
		timeline.PixelData("4").Offset.Should.Be(16076)
		timeline.PixelData("4").Length.Should.Be(1556)
		timeline.PixelData("5").Offset.Should.Be(0)
		timeline.PixelData("5").Length.Should.Be(16627)

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
		' Overall StartDate and EndDate have not changed
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		' PixelData has been updated to include extraEntry and the data for prior entries have not changed
		timeline.PixelData.Count.Should.Be(6)
		timeline.PixelData("1").Offset.Should.Be(12302)
		timeline.PixelData("1").Length.Should.Be(3512)
		timeline.PixelData("2").Offset.Should.Be(18016)
		timeline.PixelData("2").Length.Should.Be(374)
		timeline.PixelData("3").Offset.Should.Be(4071)
		timeline.PixelData("3").Length.Should.Be(4162)
		timeline.PixelData("4").Offset.Should.Be(16076)
		timeline.PixelData("4").Length.Should.Be(1556)
		timeline.PixelData("5").Offset.Should.Be(0)
		timeline.PixelData("5").Length.Should.Be(16627)
		timeline.PixelData("6").Offset.Should.Be(16661)
		timeline.PixelData("6").Length.Should.Be(983)
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
		' Overall StartDate and EndDate are correctly calculated
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		' PixelData is made for each entry and is correct
		timeline.PixelData.Count.Should.Be(5)
		timeline.PixelData("1").Offset.Should.Be(12302)
		timeline.PixelData("1").Length.Should.Be(3512)
		timeline.PixelData("2").Offset.Should.Be(18016)
		timeline.PixelData("2").Length.Should.Be(374)
		timeline.PixelData("3").Offset.Should.Be(4071)
		timeline.PixelData("3").Length.Should.Be(4162)
		timeline.PixelData("4").Offset.Should.Be(16076)
		timeline.PixelData("4").Length.Should.Be(1556)
		timeline.PixelData("5").Offset.Should.Be(0)
		timeline.PixelData("5").Length.Should.Be(16627)

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
		' Overall StartDate has changed, but EndDate has not
		timeline.StartDate.Should.Be(earlierEntry.StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		' PixelData has been updated to include earlierEntry, and the Offset (but not the length) of all entries should have changed
		timeline.PixelData.Count.Should.Be(6)
		timeline.PixelData("1").Offset.Should.Be(12302 + 7816)
		timeline.PixelData("1").Length.Should.Be(3512)
		timeline.PixelData("2").Offset.Should.Be(18016 + 7816)
		timeline.PixelData("2").Length.Should.Be(374)
		timeline.PixelData("3").Offset.Should.Be(4071 + 7816)
		timeline.PixelData("3").Length.Should.Be(4162)
		timeline.PixelData("4").Offset.Should.Be(16076 + 7816)
		timeline.PixelData("4").Length.Should.Be(1556)
		timeline.PixelData("5").Offset.Should.Be(7816)
		timeline.PixelData("5").Length.Should.Be(16627)
		timeline.PixelData("7").Offset.Should.Be(0)
		timeline.PixelData("7").Length.Should.Be(24516)
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
		' Overall StartDate and EndDate are correctly calculated
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(entries(1).EndDate)
		' PixelData is made for each entry and is correct
		timeline.PixelData.Count.Should.Be(5)
		timeline.PixelData("1").Offset.Should.Be(12302)
		timeline.PixelData("1").Length.Should.Be(3512)
		timeline.PixelData("2").Offset.Should.Be(18016)
		timeline.PixelData("2").Length.Should.Be(374)
		timeline.PixelData("3").Offset.Should.Be(4071)
		timeline.PixelData("3").Length.Should.Be(4162)
		timeline.PixelData("4").Offset.Should.Be(16076)
		timeline.PixelData("4").Length.Should.Be(1556)
		timeline.PixelData("5").Offset.Should.Be(0)
		timeline.PixelData("5").Length.Should.Be(16627)

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
		' Overall EndDate has changed, but StartDate has not
		timeline.StartDate.Should.Be(entries(4).StartDate)
		timeline.EndDate.Should.Be(laterEntry.EndDate)
		' PixelData has been updated to include laterEntry and the data for prior entries have not changed
		timeline.PixelData.Count.Should.Be(6)
		timeline.PixelData("1").Offset.Should.Be(12302)
		timeline.PixelData("1").Length.Should.Be(3512)
		timeline.PixelData("2").Offset.Should.Be(18016)
		timeline.PixelData("2").Length.Should.Be(374)
		timeline.PixelData("3").Offset.Should.Be(4071)
		timeline.PixelData("3").Length.Should.Be(4162)
		timeline.PixelData("4").Offset.Should.Be(16076)
		timeline.PixelData("4").Length.Should.Be(1556)
		timeline.PixelData("5").Offset.Should.Be(0)
		timeline.PixelData("5").Length.Should.Be(16627)
		timeline.PixelData("8").Offset.Should.Be(16482)
		timeline.PixelData("8").Length.Should.Be(4136)
	End Sub

	<Fact>
	Sub TimelineSameDayAppearsAsOnePixelTest()
		Dim timeline As New Timeline
		timeline.AddEntry(sameDayEntry)

		timeline.Entries.Should.BeEquivalentTo({sameDayEntry})
		timeline.Entries(0).Should.BeEquivalentTo(sameDayEntry)
		timeline.StartDate.Should.Be(sameDayEntry.StartDate)
		timeline.EndDate.Should.Be(sameDayEntry.EndDate)
		timeline.PixelData.Count.Should.Be(1)
		timeline.PixelData("9").Offset.Should.Be(0)
		timeline.PixelData("9").Length.Should.Be(1)
	End Sub
End Class
