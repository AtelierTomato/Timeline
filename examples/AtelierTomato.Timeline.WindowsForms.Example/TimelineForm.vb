Imports AtelierTomato.Timeline.Core

Public Class TimelineForm
	Private _entries As List(Of TimelineEntry)
	Public Sub New(entries As IEnumerable(Of TimelineEntry))
		InitializeComponent()
		_entries = entries
	End Sub
	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

	End Sub
	Private Sub TimelineForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim timeline As Core.Timeline = New Core.Timeline(_entries)
		timelineControl.Timeline = timeline
	End Sub
End Class