Imports AtelierTomato.Timeline.Core

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EntryList
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
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

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		lstTimelineEntries = New ListBox()
		btnShowTimeline = New Button()
		btnAddEntry = New Button()
		btnRemoveEntry = New Button()
		MenuStrip1 = New MenuStrip()
		mnuFile = New ToolStripMenuItem()
		mnuFileExit = New ToolStripMenuItem()
		mnuEntries = New ToolStripMenuItem()
		mnuEntriesAdd = New ToolStripMenuItem()
		mnuEntriesRemove = New ToolStripMenuItem()
		mnuEntriesClear = New ToolStripMenuItem()
		mnuEntriesGenerate = New ToolStripMenuItem()
		MenuStrip1.SuspendLayout()
		SuspendLayout()
		' 
		' lstTimelineEntries
		' 
		lstTimelineEntries.FormattingEnabled = True
		lstTimelineEntries.Location = New Point(12, 27)
		lstTimelineEntries.Name = "lstTimelineEntries"
		lstTimelineEntries.Size = New Size(328, 409)
		lstTimelineEntries.TabIndex = 0
		' 
		' btnShowTimeline
		' 
		btnShowTimeline.Location = New Point(12, 442)
		btnShowTimeline.Name = "btnShowTimeline"
		btnShowTimeline.Size = New Size(104, 56)
		btnShowTimeline.TabIndex = 1
		btnShowTimeline.Text = "Show &Timeline"
		btnShowTimeline.UseVisualStyleBackColor = True
		' 
		' btnAddEntry
		' 
		btnAddEntry.Location = New Point(124, 442)
		btnAddEntry.Name = "btnAddEntry"
		btnAddEntry.Size = New Size(104, 56)
		btnAddEntry.TabIndex = 2
		btnAddEntry.Text = "&Add Entry"
		btnAddEntry.UseVisualStyleBackColor = True
		' 
		' btnRemoveEntry
		' 
		btnRemoveEntry.Location = New Point(236, 442)
		btnRemoveEntry.Name = "btnRemoveEntry"
		btnRemoveEntry.Size = New Size(104, 56)
		btnRemoveEntry.TabIndex = 3
		btnRemoveEntry.Text = "&Remove Entry"
		btnRemoveEntry.UseVisualStyleBackColor = True
		' 
		' MenuStrip1
		' 
		MenuStrip1.Items.AddRange(New ToolStripItem() {mnuFile, mnuEntries})
		MenuStrip1.Location = New Point(0, 0)
		MenuStrip1.Name = "MenuStrip1"
		MenuStrip1.Size = New Size(352, 24)
		MenuStrip1.TabIndex = 4
		MenuStrip1.Text = "MenuStrip1"
		' 
		' mnuFile
		' 
		mnuFile.DropDownItems.AddRange(New ToolStripItem() {mnuFileExit})
		mnuFile.Name = "mnuFile"
		mnuFile.Size = New Size(37, 20)
		mnuFile.Text = "&File"
		' 
		' mnuFileExit
		' 
		mnuFileExit.Name = "mnuFileExit"
		mnuFileExit.ShortcutKeys = Keys.Control Or Keys.X
		mnuFileExit.Size = New Size(180, 22)
		mnuFileExit.Text = "E&xit"
		' 
		' mnuEntries
		' 
		mnuEntries.DropDownItems.AddRange(New ToolStripItem() {mnuEntriesAdd, mnuEntriesRemove, mnuEntriesClear, mnuEntriesGenerate})
		mnuEntries.Name = "mnuEntries"
		mnuEntries.Size = New Size(54, 20)
		mnuEntries.Text = "&Entries"
		' 
		' mnuEntriesAdd
		' 
		mnuEntriesAdd.Name = "mnuEntriesAdd"
		mnuEntriesAdd.ShortcutKeys = Keys.Control Or Keys.A
		mnuEntriesAdd.Size = New Size(180, 22)
		mnuEntriesAdd.Text = "&Add"
		' 
		' mnuEntriesRemove
		' 
		mnuEntriesRemove.Name = "mnuEntriesRemove"
		mnuEntriesRemove.ShortcutKeys = Keys.Control Or Keys.R
		mnuEntriesRemove.Size = New Size(180, 22)
		mnuEntriesRemove.Text = "&Remove"
		' 
		' mnuEntriesClear
		' 
		mnuEntriesClear.Name = "mnuEntriesClear"
		mnuEntriesClear.ShortcutKeys = Keys.Control Or Keys.L
		mnuEntriesClear.Size = New Size(180, 22)
		mnuEntriesClear.Text = "C&lear"
		' 
		' mnuEntriesGenerate
		' 
		mnuEntriesGenerate.Name = "mnuEntriesGenerate"
		mnuEntriesGenerate.ShortcutKeys = Keys.Control Or Keys.G
		mnuEntriesGenerate.Size = New Size(180, 22)
		mnuEntriesGenerate.Text = "&Generate"
		' 
		' EntryList
		' 
		AutoScaleDimensions = New SizeF(7F, 15F)
		AutoScaleMode = AutoScaleMode.Font
		ClientSize = New Size(352, 506)
		Controls.Add(btnRemoveEntry)
		Controls.Add(btnAddEntry)
		Controls.Add(btnShowTimeline)
		Controls.Add(lstTimelineEntries)
		Controls.Add(MenuStrip1)
		MainMenuStrip = MenuStrip1
		Name = "EntryList"
		Text = "Timeline Entry List"
		MenuStrip1.ResumeLayout(False)
		MenuStrip1.PerformLayout()
		ResumeLayout(False)
		PerformLayout()
	End Sub

	Friend WithEvents lstTimelineEntries As ListBox
	Friend WithEvents btnShowTimeline As Button
	Friend WithEvents btnAddEntry As Button
	Friend WithEvents btnRemoveEntry As Button
	Friend WithEvents MenuStrip1 As MenuStrip
	Friend WithEvents mnuFile As ToolStripMenuItem
	Friend WithEvents mnuFileExit As ToolStripMenuItem
	Friend WithEvents mnuEntries As ToolStripMenuItem
	Friend WithEvents mnuEntriesAdd As ToolStripMenuItem
	Friend WithEvents mnuEntriesRemove As ToolStripMenuItem
	Friend WithEvents mnuEntriesClear As ToolStripMenuItem
	Friend WithEvents mnuEntriesGenerate As ToolStripMenuItem

End Class
