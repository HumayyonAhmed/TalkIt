Public Class Form1

    Public Shared speaks = CreateObject("sapi.spvoice")

    Private _pitch As String = "0"
    Public Property Pitch As String
        Get
            Return _pitch
        End Get
        Set(ByVal value As String)
            _pitch = value
        End Set
    End Property
    Sub Talk(ByVal Speech As String)
        With speaks
            .Speak("<pitch middle = '" + _pitch + "'/>" + Speech)
        End With
    End Sub

    Private Sub Talk(sender As Object, e As EventArgs) Handles Button1.Click
        Button2.Enabled = True
        Button1.Enabled = False
        Dim t = TextBox1.Text
        Talk(t)
        Button2.Enabled = False
        Button1.Enabled = True
    End Sub

    Private Sub StopTalk(sender As Object, e As EventArgs) Handles Button2.Click
        speaks.Pause()
    End Sub

#Region "Methods Load/Save/Open/New/Help"
    Private Sub Load2(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim t = "Welcome, To text To Speech program."

        Try
            MkDir("C:\\Talk it")
            speaks.Speak(t)
        Catch ex As Exception
            speaks.Speak(t)
        End Try
    End Sub

    Private Sub NewFile(sender As Object, e As EventArgs) Handles NewToolStripButton.Click
        TextBox1.Text = ""
    End Sub


    Private Sub SaveFile(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            Dim i = SaveFileDialog1.FileName
            Dim t = TextBox1.Text
            FileIO.FileSystem.WriteAllText(i, t, False)
            MsgBox("Saved!")
        Catch ex As Exception
            MsgBox("Cannot save file!")
        End Try
    End Sub

    Private Sub OpenFile(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim i = OpenFileDialog1.FileName
        Try
            Dim p = FileIO.FileSystem.ReadAllText(i)
            TextBox1.Text = p
        Catch ex As Exception
            MsgBox("Cannot open file!")
        End Try
    End Sub

    Private Sub showDialogSave(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub showDialogOpen(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub HelpToolStripButton_Click(sender As Object, e As EventArgs) Handles HelpToolStripButton.Click
        AboutBox1.Show()
    End Sub

#End Region

    Private Sub changePitch(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        Pitch = NumericUpDown1.Value
    End Sub

    Private Sub changeVolume(sender As Object, e As EventArgs) Handles TrackBar1.ValueChanged
        speaks.Volume = TrackBar1.Value
    End Sub

    Private Sub changeSpeed(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        speaks.Rate = NumericUpDown2.Value
    End Sub
End Class
