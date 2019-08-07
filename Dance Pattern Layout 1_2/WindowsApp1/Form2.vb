Public Class SaveToTextForm


    Private Sub SaveToText(sender As Object, e As EventArgs) Handles SaveTextButton.Click
        SaveBox.Text = "Dance Pattern Layout ver 1.2 save file" & vbCrLf
        SaveBox.Text = SaveBox.Text & MainForm.TotalMarkers & vbCrLf
        For a As Integer = 1 To MainForm.TotalMarkers
            Dim CurrentMarker As Object = MainForm.ReturnMarker(a)
            Dim WriteX As String = CurrentMarker.Location.X + CurrentMarker.Width / 2
            Dim WriteY As String = CurrentMarker.Location.Y + CurrentMarker.Height / 2
            Dim WriteNameTag As String = MainForm.CheckNameTag(a - 1)
            If WriteNameTag = Nothing Then
                WriteNameTag = "_None_"
            End If
            Dim WriteColour As String = MainForm.CheckColour(a - 1)
            SaveBox.Text = SaveBox.Text & WriteX & ", " & WriteY & ", " & WriteNameTag & ", " & WriteColour & vbCrLf
        Next
    End Sub

    Private Sub LoadFromText(sender As Object, e As EventArgs) Handles LoadTextButton.Click
        If SaveBox.Lines.Length >= 3 Then
            Dim SaveVersion As String = SaveBox.Lines(0)
            Dim SaveInfo As String = SaveBox.Lines(1)
            Select Case SaveVersion
                Case "Dance Pattern Layout ver 1.2 save file"
                    MainForm.TotalMarkers = SaveInfo
                    MainForm.UpdateQuantity(MainForm.TotalMarkers)
                    For a As Integer = 1 To MainForm.TotalMarkers
                        Dim CurrentLine As String = SaveBox.Lines(a + 1)
                        Dim CurrentMarker As Object = MainForm.ReturnMarker(a)
                        Dim Section As String() = CurrentLine.Split(New Char() {","c})
                        MainForm.MoveMarkerTo(a, Section(0), Section(1))
                        Dim NameTagSaved As String = Trim(Section(2))
                        If NameTagSaved = "_None_" Then
                            NameTagSaved = ""
                        End If
                        MainForm.CheckNameTag(a - 1) = NameTagSaved
                        MainForm.CheckColour(a - 1) = Trim(Section(3))
                    Next
                    MainForm.Refresh()
                Case Else
                    MsgBox("This save isn't working properly")
            End Select
        Else
            MsgBox("This isn't a valid save")
        End If
    End Sub
End Class