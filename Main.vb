Imports System.Runtime.InteropServices
Imports System.Threading

Public Class Main
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim getVersion As String = My.Settings.Version
        Me.Text = "AutoMouse - v" + getVersion
        lblCountdown.Text = ""

        RegisterHotKey(Me.Handle, HOTKEY_ID, MOD_ALT, VK_ESCAPE) ' ALT+ESC
    End Sub

    ' API to get the cursor position
    <DllImport("user32.dll")>
    Public Shared Function GetCursorPos(ByRef lpPoint As Point) As Boolean
    End Function

    ' API for mouse events (for playback)
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Sub mouse_event(dwFlags As UInteger, dx As UInteger, dy As UInteger, dwData As UInteger, dwExtraInfo As IntPtr)
    End Sub

    ' Block user input
    <DllImport("user32.dll")>
    Private Shared Function BlockInput(ByVal fBlock As Boolean) As Boolean
    End Function

    ' Hot Keys
    <DllImport("user32.dll")>
    Private Shared Function RegisterHotKey(hWnd As IntPtr, id As Integer, fsModifiers As Integer, vk As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function UnregisterHotKey(hWnd As IntPtr, id As Integer) As Boolean
    End Function

    Private Const MOD_ALT As Integer = &H1
    Private Const MOD_CONTROL As Integer = &H2
    Private Const VK_S As Integer = &H53
    Private Const VK_ESCAPE As Integer = &H1B

    Private Const HOTKEY_ID As Integer = 1


    ' Mouse event constants
    Private Const MOUSEEVENTF_LEFTDOWN As UInteger = &H2
    Private Const MOUSEEVENTF_LEFTUP As UInteger = &H4
    Private Const MOUSEEVENTF_RIGHTDOWN As UInteger = &H8
    Private Const MOUSEEVENTF_RIGHTUP As UInteger = &H10

    ' API to detect key states
    <DllImport("user32.dll")>
    Private Shared Function GetAsyncKeyState(ByVal vKey As Integer) As Short
    End Function

    Private Const VK_LBUTTON As Integer = &H1
    Private Const VK_RBUTTON As Integer = &H2

    Public Class MouseAction
        Public Property X As Integer
        Public Property Y As Integer
        Public Property ClickType As String
        Public Property Delay As Integer

        Public Overrides Function ToString() As String
            Return $"{ClickType} at ({X}, {Y}) after {Delay}ms"
        End Function
    End Class

    Private recording As Boolean = False
    Private playing As Boolean = False
    Private recordedActions As New List(Of MouseAction)()
    Private lastRecordedTime As Integer
    Private lastRecordedPosition As Point = Point.Empty

    Private Sub btnRecord_Click(sender As Object, e As EventArgs) Handles btnRecord.Click
        If recording Then
            MessageBox.Show("Recording is already in progress.", "AutoMouse", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If playing Then
            MessageBox.Show("Stop playback before starting a new recording.", "AutoMouse", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        recording = True

        For countdown As Integer = 3 To 1 Step -1
            lblCountdown.Text = $"Recording Starting in: {countdown}"
            lblCountdown.Refresh()
            Thread.Sleep(1000)

            If Not recording Then
                lblCountdown.Text = ""
                Return
            End If
        Next

        lblCountdown.Text = "Go!"
        lblCountdown.Refresh()
        Thread.Sleep(500)

        If Not recording Then
            lblCountdown.Text = ""
            Return
        End If

        lblCountdown.Text = ""

        SyncLock recordedActions
            recordedActions.Clear()
        End SyncLock
        lastRecordedTime = Environment.TickCount
        lastRecordedPosition = Point.Empty
        lstRecordedActions.Items.Clear()

        Dim recordThread As New Thread(AddressOf RecordMouse)
        recordThread.IsBackground = True
        recordThread.Start()
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        recording = False
    End Sub

    Private Sub RecordMouse()
        Dim lastLeftState As Boolean = False
        Dim lastRightState As Boolean = False

        While recording
            Dim cursorPos As Point
            GetCursorPos(cursorPos)

            Dim leftState As Boolean = (GetAsyncKeyState(VK_LBUTTON) <> 0)
            Dim rightState As Boolean = (GetAsyncKeyState(VK_RBUTTON) <> 0)

            Dim currentTick As Integer = Environment.TickCount
            Dim elapsedSinceLast As Integer = Math.Max(0, currentTick - lastRecordedTime)
            Dim actionsToAdd As New List(Of MouseAction)()
            Dim pendingDelay As Integer = elapsedSinceLast

            If lastRecordedPosition.IsEmpty OrElse lastRecordedPosition <> cursorPos Then
                actionsToAdd.Add(New MouseAction With {
                    .X = cursorPos.X,
                    .Y = cursorPos.Y,
                    .ClickType = "Move",
                    .Delay = pendingDelay
                })
                pendingDelay = 0
                lastRecordedPosition = cursorPos
            End If

            If leftState AndAlso Not lastLeftState Then
                actionsToAdd.Add(New MouseAction With {
                    .X = cursorPos.X,
                    .Y = cursorPos.Y,
                    .ClickType = "LeftClick",
                    .Delay = pendingDelay
                })
                pendingDelay = 0
            End If

            If rightState AndAlso Not lastRightState Then
                actionsToAdd.Add(New MouseAction With {
                    .X = cursorPos.X,
                    .Y = cursorPos.Y,
                    .ClickType = "RightClick",
                    .Delay = pendingDelay
                })
                pendingDelay = 0
            End If

            If actionsToAdd.Count > 0 Then
                lastRecordedTime = currentTick
                SyncLock recordedActions
                    recordedActions.AddRange(actionsToAdd)
                End SyncLock

                If Not Me.IsDisposed AndAlso Me.IsHandleCreated Then
                    Me.BeginInvoke(Sub()
                                        For Each action In actionsToAdd
                                            lstRecordedActions.Items.Add(action.ToString())
                                        Next
                                    End Sub)
                End If
            End If

            lastLeftState = leftState
            lastRightState = rightState

            Thread.Sleep(10)
        End While
    End Sub

    ' Start Playback
    Private Sub btnPlay_Click(sender As Object, e As EventArgs) Handles btnPlay.Click
        Dim hasActions As Boolean
        SyncLock recordedActions
            hasActions = recordedActions.Count > 0
        End SyncLock

        If Not hasActions Then
            MessageBox.Show("There are no recorded actions to play back.", "AutoMouse", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        playing = True
        Dim playbackThread As New Thread(AddressOf PlayMouse)
        playbackThread.IsBackground = True
        playbackThread.Start()
    End Sub

    ' Stop Playback
    Private Sub btnStopPlayback_Click(sender As Object, e As EventArgs) Handles btnStopPlayback.Click
        playing = False
    End Sub

    ' Play Back Recorded Mouse Movements and Clicks
    Private Sub PlayMouse()
        Dim inputBlocked As Boolean = False
        If chkBlockInput.Checked Then
            inputBlocked = BlockInput(True)
        End If

        Try
            Dim actionsToPlay As New List(Of MouseAction)()
            SyncLock recordedActions
                For Each action In recordedActions
                    actionsToPlay.Add(New MouseAction With {
                        .X = action.X,
                        .Y = action.Y,
                        .ClickType = action.ClickType,
                        .Delay = action.Delay
                    })
                Next
            End SyncLock

            For Each action As MouseAction In actionsToPlay
                If Not playing Then Exit For

                Dim delay As Integer = Math.Max(0, action.Delay)
                If delay > 0 Then
                    Thread.Sleep(delay)
                End If

                Cursor.Position = New Point(action.X, action.Y)

                Select Case action.ClickType
                    Case "LeftClick"
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero)
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero)
                    Case "RightClick"
                        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, IntPtr.Zero)
                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, IntPtr.Zero)
                End Select
            Next
        Finally
            playing = False

            If inputBlocked Then
                BlockInput(False)
            End If
        End Try
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_HOTKEY As Integer = &H312
        If m.Msg = WM_HOTKEY Then
            playing = False ' Stop playback when hotkey is pressed
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        recording = False
        playing = False
        UnregisterHotKey(Me.Handle, HOTKEY_ID)
    End Sub
End Class
