Imports AtelierTomato.Timeline.Core

Public Class GenerateEntries
	' List of entries to append to
	Public Entries As List(Of TimelineEntry)
	Private _rng As New Random()

	Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
		' Validate input
		Dim numEntries As Integer

		If Not Integer.TryParse(txtAmount.Text, numEntries) OrElse numEntries <= 0 Then
			MessageBox.Show("Please enter a valid number of entries.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		Dim absoluteStartDate As DateTimeOffset = dtpStartDate.Value
		Dim absoluteEndDate As DateTimeOffset = dtpEndDate.Value
		If absoluteStartDate > absoluteEndDate Then
			MessageBox.Show("The start date should be before the end date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return
		End If

		' Generate entries
		Dim newEntries As New List(Of TimelineEntry)()
		Dim currentID As Integer = 1
		Dim generatedCount As Integer = 0

		While generatedCount < numEntries
			' Skip existing IDs
			While Entries.Any(Function(en) en.ID = currentID.ToString())
				currentID += 1
			End While

			' Generate random start and end dates
			Dim startDate = absoluteStartDate.AddDays(_rng.NextDouble() * (absoluteEndDate - absoluteStartDate).TotalDays)
			Dim daysToAdd = _rng.Next(0, 500)
			Dim endDate = startDate.AddDays(daysToAdd)
			If endDate > absoluteEndDate Then
				endDate = absoluteEndDate.AddDays(-_rng.Next(0, 30))
				If endDate < startDate Then
					endDate = startDate
				End If
			End If

			' Generate name
			Dim name = GenerateName()

			' Create a new TimelineEntry
			Dim newEntry = New TimelineEntry(currentID.ToString(), name, startDate, endDate)
			newEntries.Add(newEntry)

			' Increment counters
			generatedCount += 1
			currentID += 1
		End While

		' Append to the main list
		Entries.AddRange(newEntries)

		' Close the form
		MessageBox.Show($"{newEntries.Count} entries generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Me.Close()
	End Sub

	Private Function GenerateName() As String
		' Word lists
		Dim adjectives = {"Quick", "Lazy", "Bright", "Calm", "Brave", "Bold", "Clever", "Friendly", "Gentle", "Happy",
						 "Quiet", "Strong", "Smart", "Silly", "Jolly", "Proud", "Tall", "Short", "Round", "Sharp",
						 "Lively", "Kind", "Fierce", "Honest", "Shy", "Loyal", "Funny", "Nice", "Cool", "Rich",
						 "Poor", "Loud", "Sweet", "Hard", "Soft", "Warm", "Cold", "Fast", "Slow", "Clear", "Dark",
						 "Plain", "Fancy", "Clean", "Neat", "Messy", "Big", "Small", "Fair", "Hungry", "Tasty"}

		Dim nouns = {"Tree", "River", "Mountain", "Ocean", "Sky", "Dog", "Cat", "Bird", "Cloud", "Grass", "Song",
					"Stone", "Flower", "Star", "Moon", "Sun", "House", "Road", "Bridge", "Car", "Train", "Number",
					"Boat", "Chair", "Table", "Window", "Door", "Book", "Pen", "Cup", "Bottle", "Basket", "Peregrine",
					"Clock", "Lamp", "Bed", "Mirror", "Bag", "Hat", "Shoe", "Shirt", "Box", "Wall", "Leaf", "Oarfish",
					"Key", "Ring", "Bell", "Coin", "Fish", "Horse", "Cow", "Sheep", "Tomato", "Fish", "Abacus"}

		Dim verbs = {"Running", "Jumping", "Laughing", "Singing", "Dancing", "Climbing", "Swimming", "Eating", "Sleeping", "Driving",
					"Writing", "Reading", "Cooking", "Painting", "Building", "Drawing", "Flying", "Walking", "Talking", "Listening",
					"Playing", "Watching", "Smiling", "Screaming", "Cleaning", "Sewing", "Knitting", "Hiding", "Exploring", "Planting",
					"Digging", "Hunting", "Shooting", "Catching", "Growing", "Waving", "Cutting", "Folding", "Breaking", "Fixing",
					"Polishing", "Lighting", "Fighting", "Washing", "Ironing", "Kneeling", "Crawling", "Sitting", "Spinning"}

		' Randomly pick one word from each list
		Dim adjective = adjectives(_rng.Next(adjectives.Length))
		Dim noun = nouns(_rng.Next(nouns.Length))
		Dim verb = verbs(_rng.Next(verbs.Length))

		' Return the combined name
		Return $"{adjective} {noun} {verb}"
	End Function
End Class