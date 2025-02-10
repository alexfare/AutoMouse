<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.btnRecord = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.btnStopPlayback = New System.Windows.Forms.Button()
        Me.lstRecordedActions = New System.Windows.Forms.ListBox()
        Me.lblCountdown = New System.Windows.Forms.Label()
        Me.chkBlockInput = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnRecord
        '
        Me.btnRecord.Location = New System.Drawing.Point(284, 25)
        Me.btnRecord.Name = "btnRecord"
        Me.btnRecord.Size = New System.Drawing.Size(95, 23)
        Me.btnRecord.TabIndex = 0
        Me.btnRecord.Text = "Start Recording"
        Me.btnRecord.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(284, 54)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(95, 23)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "Stop Recording"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnPlay
        '
        Me.btnPlay.Location = New System.Drawing.Point(284, 83)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(95, 23)
        Me.btnPlay.TabIndex = 2
        Me.btnPlay.Text = "Start Playback"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'btnStopPlayback
        '
        Me.btnStopPlayback.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnStopPlayback.Location = New System.Drawing.Point(284, 112)
        Me.btnStopPlayback.Name = "btnStopPlayback"
        Me.btnStopPlayback.Size = New System.Drawing.Size(95, 23)
        Me.btnStopPlayback.TabIndex = 3
        Me.btnStopPlayback.Text = "Stop Playback"
        Me.btnStopPlayback.UseVisualStyleBackColor = True
        '
        'lstRecordedActions
        '
        Me.lstRecordedActions.FormattingEnabled = True
        Me.lstRecordedActions.Location = New System.Drawing.Point(12, 25)
        Me.lstRecordedActions.Name = "lstRecordedActions"
        Me.lstRecordedActions.Size = New System.Drawing.Size(266, 303)
        Me.lstRecordedActions.TabIndex = 4
        '
        'lblCountdown
        '
        Me.lblCountdown.AutoSize = True
        Me.lblCountdown.Location = New System.Drawing.Point(12, 9)
        Me.lblCountdown.Name = "lblCountdown"
        Me.lblCountdown.Size = New System.Drawing.Size(39, 13)
        Me.lblCountdown.TabIndex = 5
        Me.lblCountdown.Text = "Label1"
        '
        'chkBlockInput
        '
        Me.chkBlockInput.AutoSize = True
        Me.chkBlockInput.Location = New System.Drawing.Point(284, 157)
        Me.chkBlockInput.Name = "chkBlockInput"
        Me.chkBlockInput.Size = New System.Drawing.Size(102, 17)
        Me.chkBlockInput.TabIndex = 6
        Me.chkBlockInput.Text = "Block user input"
        Me.chkBlockInput.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 331)
        Me.Controls.Add(Me.lblCountdown)
        Me.Controls.Add(Me.chkBlockInput)
        Me.Controls.Add(Me.lstRecordedActions)
        Me.Controls.Add(Me.btnRecord)
        Me.Controls.Add(Me.btnStopPlayback)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnPlay)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AutoMouse"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnRecord As Button
    Friend WithEvents btnStop As Button
    Friend WithEvents btnPlay As Button
    Friend WithEvents btnStopPlayback As Button
    Friend WithEvents lstRecordedActions As ListBox
    Friend WithEvents lblCountdown As Label
    Friend WithEvents chkBlockInput As CheckBox
End Class
