Imports FluentAssertions
Imports Xunit

Public Class TimelineEntryTests
	Private ReadOnly laterDate As New DateTimeOffset(2024, 10, 7, 0, 0, 0, TimeSpan.Zero)
	Private ReadOnly earlierDate As New DateTimeOffset(2023, 10, 7, 0, 0, 0, TimeSpan.Zero)

	<Fact>
	Sub StartDateIsAfterEndDateFailTest()
		Dim act As Action = Sub()
								Dim entry = New TimelineEntry("1", "name", laterDate, earlierDate)
							End Sub

		act.Should().Throw(Of ArgumentException)().WithMessage("StartDate must be on or before EndDate. (Parameter 'startDate')")
	End Sub

	<Fact>
	Sub StartDateIsBeforeEndDatePassTest()
		Dim entry = New TimelineEntry("1", "name", earlierDate, laterDate)

		entry.ID.Should.Be("1")
		entry.Name.Should.Be("name")
		entry.StartDate.Should.Be(earlierDate)
		entry.EndDate.Should.Be(laterDate)
	End Sub

	<Fact>
	Sub SameDatePassTest()
		Dim day As DateTimeOffset = New DateTimeOffset(2024, 12, 25, 0, 0, 0, TimeSpan.Zero)
		Dim entry = New TimelineEntry("1", "name", day, day)

		entry.ID.Should.Be("1")
		entry.Name.Should.Be("name")
		entry.StartDate.Should.Be(day)
		entry.EndDate.Should.Be(day)
	End Sub
End Class
