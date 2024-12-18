﻿Imports System.ComponentModel
Imports AtelierTomato.Timeline.Core

Partial Public Class TimelineControl
	Inherits UserControl

	Private _scrollOffset As Point
	Private myToolTip As New ToolTip()
	Private lastToolTipText As String = String.Empty
	Private lastToolTipLocation As Point

	Public Sub New()
		InitializeComponent()
		Me.DoubleBuffered = True
		_scrollOffset = Point.Empty
	End Sub

	Private _timeline As Core.Timeline
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
	Private _barColor As Color = Color.LightBlue
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarColor As Color
		Get
			Return _barColor
		End Get
		Set(value As Color)
			_barColor = value
			Invalidate()
		End Set
	End Property
	Private _barLineColor As Color = Color.Black
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarLineColor As Color
		Get
			Return _barLineColor
		End Get
		Set(value As Color)
			_barLineColor = value
			Invalidate()
		End Set
	End Property
	Private _barTextColor As Color = Color.Black
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarTextcolor As Color
		Get
			Return _barTextColor
		End Get
		Set(value As Color)
			_barTextColor = value
			Invalidate()
		End Set
	End Property
	Private _barFont As New Font("Arial", 10)
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarFont As Font
		Get
			Return _barFont
		End Get
		Set(value As Font)
			_barFont = value
			Invalidate()
		End Set
	End Property
	Private _barHeight As Integer = 20
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarHeight As Integer
		Get
			Return _barHeight
		End Get
		Set(value As Integer)
			_barHeight = Math.Max(value, 1)
			Invalidate()
		End Set
	End Property
	Private _paddingBetweenBars As Integer = 0
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property PaddingBetweenBars As Integer
		Get
			Return _paddingBetweenBars
		End Get
		Set(value As Integer)
			_paddingBetweenBars = Math.Max(value, 0)
			Invalidate()
		End Set
	End Property
	Private _barLengthPerDay As Integer = 2
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarLengthPerDay As Integer
		Get
			Return _barLengthPerDay
		End Get
		Set(value As Integer)
			_barLengthPerDay = Math.Max(value, 1)
			Invalidate()
		End Set
	End Property
	Private _monthLabelFont As New Font("Arial", 8)
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelFont As Font
		Get
			Return _monthLabelFont
		End Get
		Set(value As Font)
			_monthLabelFont = value
			Invalidate()
		End Set
	End Property
	Private _monthLabelHeight As Integer = 20
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelHeight As Integer
		Get
			Return _monthLabelHeight
		End Get
		Set(value As Integer)
			_monthLabelHeight = Math.Max(value, 0)
			Invalidate()
		End Set
	End Property
	Private _monthLabelColor As Color = Color.Black
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelColor As Color
		Get
			Return _monthLabelColor
		End Get
		Set(value As Color)
			_monthLabelColor = value
			Invalidate()
		End Set
	End Property
	Private _monthLabelLineColor As Color = Color.Black
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelLineColor As Color
		Get
			Return _monthLabelLineColor
		End Get
		Set(value As Color)
			_monthLabelLineColor = value
			Invalidate()
		End Set
	End Property
	Private _drawVerticalLine As VerticalLineMode = VerticalLineMode.BelowMonthLabels
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property DrawVerticalLine As VerticalLineMode
		Get
			Return _drawVerticalLine
		End Get
		Set(value As VerticalLineMode)
			_drawVerticalLine = value
			Invalidate()
		End Set
	End Property
	Private _drawHorizontalLine As Boolean = True
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property DrawHorizontalLine As Boolean
		Get
			Return _drawHorizontalLine
		End Get
		Set(value As Boolean)
			_drawHorizontalLine = value
			Invalidate()
		End Set
	End Property
	Private _monthLabelAlignment As ContentAlignment = ContentAlignment.MiddleCenter
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelAlignment As ContentAlignment
		Get
			Return _monthLabelAlignment
		End Get
		Set(value As ContentAlignment)
			_monthLabelAlignment = value
			Invalidate()
		End Set
	End Property
	Private _barLabelAlignment As ContentAlignment = ContentAlignment.MiddleLeft
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarLabelAlignment As ContentAlignment
		Get
			Return _barLabelAlignment
		End Get
		Set(value As ContentAlignment)
			_barLabelAlignment = value
			Invalidate()
		End Set
	End Property
	Private _monthLabelPadding As New Padding(0)
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property MonthLabelPadding As Padding
		Get
			Return _monthLabelPadding
		End Get
		Set(value As Padding)
			_monthLabelPadding = value
			Invalidate()
		End Set
	End Property
	Private _barLabelPadding As New Padding(3, 0, 0, 0)
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property BarLabelPadding As Padding
		Get
			Return _barLabelPadding
		End Get
		Set(value As Padding)
			_barLabelPadding = value
			Invalidate()
		End Set
	End Property

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

	Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
		MyBase.OnMouseWheel(e)
		' Invalidate on mouse wheel scroll, for some reason it's not handled in OnScroll
		Invalidate()
	End Sub

	Protected Overrides Sub OnPaint(e As PaintEventArgs)
		MyBase.OnPaint(e)

		If _timeline Is Nothing Then Return ' Prevent errors if _timeline is not set

		' Get the graphics object for drawing
		Dim g As Graphics = e.Graphics
		g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

		' Draw the month labels above the graph
		Dim currentMonth As New DateTimeOffset(_timeline.StartDate.Year, _timeline.StartDate.Month, 1, 0, 0, 0, _timeline.StartDate.Offset)
		Dim daysOffset As Integer = _timeline.StartDate.Day - 1
		Dim monthX As Integer = Me.AutoScrollPosition.X
		Dim monthY As Integer = Me.AutoScrollPosition.Y

		' Loop through each month until the end date
		While currentMonth <= _timeline.EndDate
			' Draw the month label at the X position
			Dim monthLabel As String = currentMonth.ToString("MMM yyyy")
			Dim textSize As SizeF = g.MeasureString(monthLabel, _monthLabelFont)

			' Define the width of the current month field
			Dim monthWidth As Integer = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month) * _barLengthPerDay

			Dim paddedMonthRect As New Rectangle(
				monthX + _monthLabelPadding.Left,
				monthY + _monthLabelPadding.Top,
				Math.Max(0, monthWidth - _monthLabelPadding.Left - _monthLabelPadding.Right),
				Math.Max(0, _monthLabelHeight - _monthLabelPadding.Top - _monthLabelPadding.Bottom)
			)

			Dim monthStringFormat As New StringFormat
			Dim monthTextX As Decimal
			Select Case _monthLabelAlignment
				Case ContentAlignment.TopLeft, ContentAlignment.MiddleLeft, ContentAlignment.BottomLeft
					monthStringFormat.Alignment = StringAlignment.Near
					monthTextX = paddedMonthRect.X
				Case ContentAlignment.TopCenter, ContentAlignment.MiddleCenter, ContentAlignment.BottomCenter
					monthStringFormat.Alignment = StringAlignment.Center
					monthTextX = paddedMonthRect.X + paddedMonthRect.Width / 2
				Case ContentAlignment.TopRight, ContentAlignment.MiddleRight, ContentAlignment.BottomRight
					monthStringFormat.Alignment = StringAlignment.Far
					monthTextX = paddedMonthRect.X + paddedMonthRect.Width
				Case Else
					Throw New InvalidOperationException($"{NameOf(BarLabelAlignment)} was not set to a recognize {NameOf(ContentAlignment)} value.")
			End Select
			Dim monthTextY As Decimal
			Select Case _monthLabelAlignment
				Case ContentAlignment.TopLeft, ContentAlignment.TopCenter, ContentAlignment.TopRight
					monthStringFormat.LineAlignment = StringAlignment.Near
					monthTextY = paddedMonthRect.Y
				Case ContentAlignment.MiddleLeft, ContentAlignment.MiddleCenter, ContentAlignment.MiddleRight
					monthStringFormat.LineAlignment = StringAlignment.Center
					monthTextY = paddedMonthRect.Y + paddedMonthRect.Height / 2
				Case ContentAlignment.BottomLeft, ContentAlignment.BottomCenter, ContentAlignment.BottomRight
					monthStringFormat.LineAlignment = StringAlignment.Far
					monthTextY = paddedMonthRect.Y + paddedMonthRect.Height
				Case Else
					Throw New InvalidOperationException($"{NameOf(BarLabelAlignment)} was not set to a recognize {NameOf(ContentAlignment)} value.")
			End Select

			g.DrawString(monthLabel, _monthLabelFont, New SolidBrush(_monthLabelColor), monthTextX, monthTextY, monthStringFormat)

			Dim monthLabelLinePen As Pen = New Pen(_monthLabelLineColor)

			' Draw a vertical line splitting the labels
			If _drawVerticalLine = VerticalLineMode.BelowMonthLabels Then
				g.DrawLine(monthLabelLinePen, monthX, _monthLabelHeight + Me.AutoScrollPosition.Y, monthX, ClientSize.Height)
			ElseIf _drawVerticalLine = VerticalLineMode.Full Then
				g.DrawLine(monthLabelLinePen, monthX, 0, monthX, ClientSize.Height)
			End If

			' Draw a horizontal line under the month labels
			If _drawHorizontalLine Then
				Dim yPosition As Integer = _monthLabelHeight + Me.AutoScrollPosition.Y
				g.DrawLine(monthLabelLinePen, 0, yPosition, Me.Width, yPosition)
			End If

			' Add pixels for the number of days in the current month
			monthX += monthWidth

			' Increment the currentMonth to the next month
			currentMonth = currentMonth.AddMonths(1)
		End While

		' Calculate the total required height based on the max stack level
		Dim requiredHeight As Integer = (_timeline.MaxStackLevel * (_barHeight + _paddingBetweenBars)) + _monthLabelHeight

		' Update AutoScrollMinSize to ensure it fits everything
		Me.AutoScrollMinSize = New Size(monthX - Me.AutoScrollPosition.X, requiredHeight)

		' Draw a bar for each entry in the Timeline
		For Each entry As TimelineEntry In _timeline.Entries
			Dim barRect As Rectangle = GetBarRectangle(entry)

			' Draw a rectangle for each timeline entry
			If barRect.X + barRect.Width >= 0 AndAlso barRect.X <= Me.ClientSize.Width Then
				g.FillRectangle(New SolidBrush(_barColor), barRect)
				g.DrawRectangle(New Pen(_barLineColor), barRect)

				' We want to draw the label on the center of what is on the screen, so that we can see bar labels at all times
				Dim visibleX As Integer = barRect.X
				Dim visibleWidth As Integer = barRect.Width
				If barRect.X < 0 Then
					visibleX -= barRect.X
					visibleWidth += barRect.X
				End If
				If barRect.X + barRect.Width > Me.Width Then
					If Me.VerticalScroll.Visible Then
						visibleWidth = Me.Width - visibleX - SystemInformation.VerticalScrollBarWidth
					Else
						visibleWidth = Me.Width - visibleX
					End If
				End If

				Dim paddedRect As New Rectangle(
					visibleX + _barLabelPadding.Left,
					barRect.Y + _barLabelPadding.Top,
					Math.Max(0, visibleWidth - _barLabelPadding.Left - _barLabelPadding.Right),
					Math.Max(0, barRect.Height - _barLabelPadding.Top - _barLabelPadding.Bottom)
				)

				' Truncate the name of the entry to fit on the bar
				Dim truncatedName = TruncateText(entry.Name, visibleWidth, _barFont)

				' Draw the name of the entry on the bar
				Dim textSize As SizeF = g.MeasureString(truncatedName, _barFont)

				Dim stringFormat As New StringFormat
				Dim textX As Decimal
				Select Case _barLabelAlignment
					Case ContentAlignment.TopLeft, ContentAlignment.MiddleLeft, ContentAlignment.BottomLeft
						stringFormat.Alignment = StringAlignment.Near
						textX = paddedRect.X
					Case ContentAlignment.TopCenter, ContentAlignment.MiddleCenter, ContentAlignment.BottomCenter
						stringFormat.Alignment = StringAlignment.Center
						textX = paddedRect.X + paddedRect.Width / 2
					Case ContentAlignment.TopRight, ContentAlignment.MiddleRight, ContentAlignment.BottomRight
						stringFormat.Alignment = StringAlignment.Far
						textX = paddedRect.X + paddedRect.Width
					Case Else
						Throw New InvalidOperationException($"{NameOf(BarLabelAlignment)} was not set to a recognize {NameOf(ContentAlignment)} value.")
				End Select
				Dim textY As Decimal
				Select Case _barLabelAlignment
					Case ContentAlignment.TopLeft, ContentAlignment.TopCenter, ContentAlignment.TopRight
						stringFormat.LineAlignment = StringAlignment.Near
						textY = paddedRect.Y
					Case ContentAlignment.MiddleLeft, ContentAlignment.MiddleCenter, ContentAlignment.MiddleRight
						stringFormat.LineAlignment = StringAlignment.Center
						textY = paddedRect.Y + paddedRect.Height / 2
					Case ContentAlignment.BottomLeft, ContentAlignment.BottomCenter, ContentAlignment.BottomRight
						stringFormat.LineAlignment = StringAlignment.Far
						textY = paddedRect.Y + _barHeight + _paddingBetweenBars
					Case Else
						Throw New InvalidOperationException($"{NameOf(BarLabelAlignment)} was not set to a recognize {NameOf(ContentAlignment)} value.")
				End Select
				g.DrawString(truncatedName, _barFont, New SolidBrush(_barTextColor), textX, textY, stringFormat)
			End If
		Next
	End Sub

	Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
		MyBase.OnMouseMove(e)

		' Update the tooltip state on mouse move
		Dim hoverText As String = GetBarHoverText(e.Location)
		If hoverText <> lastToolTipText OrElse e.Location <> lastToolTipLocation Then
			If Not String.IsNullOrEmpty(hoverText) Then
				Dim toolTipX As Integer = e.X + 15
				Dim toolTipY As Integer = e.Y + 15
				myToolTip.Show(hoverText, Me, toolTipX, toolTipY)
				If BarClickedEvent IsNot Nothing Then
					Me.Cursor = Cursors.Hand
				End If
			Else
				myToolTip.Hide(Me)
				Me.Cursor = Cursors.Default
			End If
			lastToolTipText = hoverText
			lastToolTipLocation = e.Location
		End If
	End Sub

	Private Function GetBarHoverText(mouseLocation As Point) As String
		For Each entry As TimelineEntry In _timeline.Entries
			Dim barRect As Rectangle = GetBarRectangle(entry)

			If barRect.Contains(mouseLocation) Then
				Return entry.ToString()
			End If
		Next

		Return Nothing
	End Function


	Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
		MyBase.OnMouseClick(e)

		Dim clickedEntry As TimelineEntry = GetClickedEntry(e.Location)
		If clickedEntry IsNot Nothing Then
			RaiseEvent BarClicked(Me, New BarClickedEventArgs(clickedEntry, e))
		End If
	End Sub

	Private Function GetClickedEntry(mouseLocation As Point) As TimelineEntry
		For Each entry As TimelineEntry In _timeline.Entries
			Dim barRect As Rectangle = GetBarRectangle(entry)

			If barRect.Contains(mouseLocation) Then
				Return entry
			End If
		Next
		Return Nothing
	End Function

	Private Function GetBarRectangle(entry As TimelineEntry) As Rectangle
		Dim x As Integer = (_timeline.GraphData(entry.ID).Offset + _timeline.StartDate.Day - 1) * _barLengthPerDay + Me.AutoScrollPosition.X
		Dim y As Integer = _monthLabelHeight + (_timeline.GraphData(entry.ID).StackLevel - 1) * (_barHeight + _paddingBetweenBars) + Me.AutoScrollPosition.Y
		Dim width As Integer = (_timeline.GraphData(entry.ID).Length + 1) * _barLengthPerDay
		Dim height As Integer = _barHeight
		Dim barRect As New Rectangle(x, y, width, height)
		Return barRect
	End Function

	Private Function TruncateText(text As String, availableWidth As Integer, font As Font) As String
		Dim originalText = text
		Dim size As SizeF = TextRenderer.MeasureText(text, font)
		While size.Width > availableWidth AndAlso Not text.Length = 0
			text = text.Substring(0, text.Length - 1)
			size = TextRenderer.MeasureText(text, font)
		End While
		If text Is originalText OrElse text.Length = 0 Then
			Return text
		Else
			While TextRenderer.MeasureText(text + "…", font).Width > availableWidth
				text = text.Substring(0, text.Length - 1)
				If text.Length = 0 Then
					Return text
				End If
			End While
			Return text + "…"
		End If
		Return text
	End Function

	Public Event BarClicked As EventHandler(Of BarClickedEventArgs)

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
