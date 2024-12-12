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
	Public Property PaddingBetweenBars As Integer = 0
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarLengthMultiplier As Integer = 2
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelFont As Font = New Font("Arial", 8)
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelHeight As Integer = 20
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelColor As Color = Color.Black
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property PaddingAboveBars As Integer = 50

	Protected Overrides Sub OnResize(e As EventArgs)
		MyBase.OnResize(e)
		' Invalidate on resize so that text is redrawn in the center of what is visible of the bar
		Invalidate()
	End Sub

	Protected Overrides Sub OnScroll(se As ScrollEventArgs)
		MyBase.OnScroll(se)
		' Invalidate on scroll so that text is redrawn in the center of what is visible of the bar
		Invalidate()
	End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		If _timeline Is Nothing Then Return ' Prevent errors if _timeline is not set

		' Get the graphics object for drawing
		Dim g As Graphics = e.Graphics
		g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

		Me.DoubleBuffered = True

		' Set up drawing parameters
		Dim pen As New Pen(Color.Black)

		' Get the current horizontal scroll position
		Dim scrollOffsetX As Integer = -Me.AutoScrollPosition.X
		Dim scrollOffsetY As Integer = -Me.AutoScrollPosition.Y

		' Draw the month labels above the graph
		Dim currentMonth As New DateTimeOffset(_timeline.StartDate.Year, _timeline.StartDate.Month, 1, 0, 0, 0, _timeline.StartDate.Offset)
		Dim daysOffset As Integer = _timeline.StartDate.Day - 1
		Dim monthX As Integer = 0 - scrollOffsetX
		Dim monthY As Integer = PaddingAboveBars - scrollOffsetY

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
		Dim requiredHeight As Integer = (_timeline.MaxStackLevel * (BarHeight + PaddingBetweenBars)) + PaddingAboveBars + MonthLabelHeight

		' Update AutoScrollMinSize to ensure it fits everything
		Me.AutoScrollMinSize = New Size(monthX + scrollOffsetX, requiredHeight)

		' Draw a bar for each entry in the Timeline
		For Each entry As TimelineEntry In _timeline.Entries
			Dim x As Integer = (_timeline.GraphData(entry.ID).Offset + daysOffset) * BarLengthMultiplier - scrollOffsetX
			Dim y As Integer = MonthLabelHeight + PaddingAboveBars + (_timeline.GraphData(entry.ID).StackLevel - 1) * (BarHeight + PaddingBetweenBars) - scrollOffsetY
			Dim width As Integer = (_timeline.GraphData(entry.ID).Length + 1) * BarLengthMultiplier
			Dim height As Integer = BarHeight

			' Draw a rectangle for each timeline entry
			If x + width >= 0 AndAlso x <= Me.ClientSize.Width Then
				g.FillRectangle(New SolidBrush(BarColor), x, y, width, height)
				g.DrawRectangle(pen, x, y, width, height)

				' Draw the name of the entry on the bar
				Dim entryName As String = entry.Name
				Dim textSize As SizeF = g.MeasureString(entryName, BarFont)
				' We want to draw the label on the center of what is on the screen, so that we can see bar labels at all times
				Dim displayedXMin As Integer = x, displayedXMax As Integer = width
				If x < 0 Then
					displayedXMin -= x
					displayedXMax += x
				End If
				If x + width > Me.Width Then
					displayedXMax = Me.Width - displayedXMin
				End If
				Dim textX As Integer = displayedXMin + (displayedXMax - textSize.Width) / 2
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
