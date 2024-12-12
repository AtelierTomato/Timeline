Imports System.ComponentModel
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
			_barHeight = value
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
			_paddingBetweenBars = value
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
			_barLengthPerDay = value
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
			_monthLabelHeight = value
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
	Private _paddingAboveBars As Integer = 10
	<Browsable(True)>
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
	Public Property PaddingAboveBars As Integer
		Get
			Return _paddingAboveBars
		End Get
		Set(value As Integer)
			_paddingAboveBars = value
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
		Dim monthY As Integer = _paddingAboveBars + Me.AutoScrollPosition.Y

		' Loop through each month until the end date
		While currentMonth <= _timeline.EndDate
			' Draw the month label at the X position
			Dim monthLabel As String = currentMonth.ToString("MMM yyyy")
			Dim textSize As SizeF = g.MeasureString(monthLabel, _monthLabelFont)
			g.DrawString(monthLabel, _monthLabelFont, New SolidBrush(_monthLabelColor), monthX, monthY)

			' Draw a vertical line under the label for the month's start position
			g.DrawLine(New Pen(_monthLabelLineColor), monthX, monthY + textSize.Height, monthX, Me.ClientSize.Height)

			' Add pixels for the number of days in the current month
			monthX += DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month) * _barLengthPerDay

			' Increment the currentMonth to the next month
			currentMonth = currentMonth.AddMonths(1)
		End While

		' Calculate the total required height based on the max stack level
		Dim requiredHeight As Integer = (_timeline.MaxStackLevel * (_barHeight + _paddingBetweenBars)) + _paddingAboveBars + _monthLabelHeight

		' Update AutoScrollMinSize to ensure it fits everything
		Me.AutoScrollMinSize = New Size(monthX - Me.AutoScrollPosition.X, requiredHeight)

		' Draw a bar for each entry in the Timeline
		For Each entry As TimelineEntry In _timeline.Entries
			Dim barRect As Rectangle = GetBarRectangle(entry)

			' Draw a rectangle for each timeline entry
			If barRect.X + barRect.Width >= 0 AndAlso barRect.X <= Me.ClientSize.Width Then
				g.FillRectangle(New SolidBrush(_barColor), barRect.X, barRect.Y, barRect.Width, barRect.Height)
				g.DrawRectangle(New Pen(_barLineColor), barRect.X, barRect.Y, barRect.Width, barRect.Height)

				' Draw the name of the entry on the bar
				Dim entryName As String = entry.Name
				Dim textSize As SizeF = g.MeasureString(entryName, _barFont)
				' We want to draw the label on the center of what is on the screen, so that we can see bar labels at all times
				Dim displayedXMin As Integer = barRect.X, displayedXMax As Integer = barRect.Width
				If barRect.X < 0 Then
					displayedXMin -= barRect.X
					displayedXMax += barRect.X
				End If
				If barRect.X + barRect.Width > Me.Width Then
					displayedXMax = Me.Width - displayedXMin
				End If
				Dim textX As Integer = displayedXMin + (displayedXMax - textSize.Width) / 2
				Dim textY As Integer = barRect.Y + (barRect.Height - textSize.Height) / 2
				g.DrawString(entryName, _barFont, New SolidBrush(_barTextColor), textX, textY)
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
				Return $"{entry.Name} ({entry.StartDate:MM/dd/yyyy} - {entry.EndDate:MM/dd/yyyy})"
			End If
		Next

		Return Nothing
	End Function


	Protected Overrides Sub OnMouseClick(ByVal e As MouseEventArgs)
		MyBase.OnMouseClick(e)

		Dim clickedEntry As TimelineEntry = GetClickedEntry(e.Location)
		If clickedEntry IsNot Nothing Then
			RaiseEvent BarClicked(Me, New BarClickedEventArgs(clickedEntry))
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
		Dim y As Integer = _monthLabelHeight + _paddingAboveBars + (_timeline.GraphData(entry.ID).StackLevel - 1) * (_barHeight + _paddingBetweenBars) + Me.AutoScrollPosition.Y
		Dim width As Integer = (_timeline.GraphData(entry.ID).Length + 1) * _barLengthPerDay
		Dim height As Integer = _barHeight
		Dim barRect As New Rectangle(x, y, width, height)
		Return barRect
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
