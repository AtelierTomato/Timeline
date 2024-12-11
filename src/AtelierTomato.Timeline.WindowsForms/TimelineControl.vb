﻿Imports System.ComponentModel
Imports AtelierTomato.Timeline.Core

Partial Public Class TimelineControl
	Inherits UserControl

	Private _timeline As Core.Timeline
	Private _scrollOffset As Point

	Public Sub New()
		InitializeComponent()
		_scrollOffset = Point.Empty
	End Sub


	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property Timeline As Core.Timeline
		Get
			Return _timeline
		End Get
		Set(value As Core.Timeline)
			_timeline = value
			Invalidate()
		End Set
	End Property

	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarColor As Color = Color.LightBlue
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarTextcolor As Color = Color.Black
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarFont As Font = New Font("Arial", 10)
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarHeight As Integer = 20
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property PaddingBetweenBars As Integer = 5
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarLengthMultiplier As Integer = 2
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelFont As Font = New Font("Arial", 8)
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelColor As Color = Color.Black
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property PaddingAboveBars As Integer = 50

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		If _timeline Is Nothing Then Return ' Prevent errors if _timeline is not set

		' Get the graphics object for drawing
		Dim g As Graphics = e.Graphics
		g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

		' Set up drawing parameters
		Dim pen As New Pen(Color.Black)

		' Get the current horizontal scroll position
		Dim scrollOffset As Integer = -Me.AutoScrollPosition.X

		' Draw the month labels above the graph
		Dim currentMonth As DateTimeOffset = New DateTimeOffset(_timeline.StartDate.Year, _timeline.StartDate.Month, 1, 0, 0, 0, _timeline.StartDate.Offset)
		Dim daysOffset As Integer = _timeline.StartDate.Day - 1
		Dim monthX As Integer = 0 - scrollOffset
		Dim monthLabelHeight As Integer = 20
		Dim monthY As Integer = PaddingAboveBars

		' Loop through each month until the end date
		While currentMonth <= _timeline.EndDate
			' Draw the month label at the X position
			Dim monthLabel As String = currentMonth.ToString("MMM yyyy")
			Dim textSize As SizeF = g.MeasureString(monthLabel, MonthLabelFont)
			g.DrawString(monthLabel, MonthLabelFont, New SolidBrush(MonthLabelColor), monthX, monthY)

			' Draw a vertical line under the label for the month's start position
			g.DrawLine(pen, monthX, monthY + textSize.Height, monthX, Me.ClientSize.Height)

			' Add pixels for the number of days in the current month
			monthX += (DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month) * BarLengthMultiplier)

			' Increment the currentMonth to the next month
			currentMonth = currentMonth.AddMonths(1)
		End While

		' Calculate the total required height based on the max stack level
		Dim requiredHeight As Integer = (_timeline.MaxStackLevel * (BarHeight + PaddingBetweenBars)) + PaddingAboveBars + monthLabelHeight

		' Update AutoScrollMinSize to ensure it fits everything
		Me.AutoScrollMinSize = New Size(monthX + scrollOffset, requiredHeight)

		' Draw a bar for each entry in the Timeline
		For Each entry As TimelineEntry In _timeline.Entries
			Dim x As Integer = (_timeline.GraphData(entry.ID).Offset + daysOffset) * BarLengthMultiplier - scrollOffset
			Dim y As Integer = monthLabelHeight + PaddingAboveBars + (_timeline.GraphData(entry.ID).StackLevel - 1) * (BarHeight + PaddingBetweenBars)
			Dim width As Integer = _timeline.GraphData(entry.ID).Length * BarLengthMultiplier
			Dim height As Integer = BarHeight

			' Draw a rectangle for each timeline entry
			If x + width >= 0 AndAlso x <= Me.ClientSize.Width Then
				g.FillRectangle(New SolidBrush(BarColor), x, y, width, height)
				g.DrawRectangle(pen, x, y, width, height)

				' Draw the name of the entry on the bar
				Dim entryName As String = entry.Name
				Dim textSize As SizeF = g.MeasureString(entryName, BarFont)
				Dim textX As Integer = x + (width - textSize.Width) / 2
				Dim textY As Integer = y + (height - textSize.Height) / 2
				g.DrawString(entryName, BarFont, New SolidBrush(BarTextcolor), textX, textY)
			End If
		Next
	End Sub

	<System.Diagnostics.DebuggerNonUserCode()>
	Protected Overrides Sub Dispose(disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub
End Class
