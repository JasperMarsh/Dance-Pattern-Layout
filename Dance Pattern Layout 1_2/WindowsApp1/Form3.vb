Public Class SaveToImageForm
    Private Sub PrintablePaint(sender As Object, e As PaintEventArgs) Handles PrintableBuild.Paint
        For a As Integer = 1 To MainForm.TotalMarkers
            Dim CurrentMarker As Object = MainForm.ReturnMarker(a)
            Dim B_Marker As New Drawing.SolidBrush(Color.Black)
            If ColoursCheck.Checked Then
                B_Marker = MainForm.MarkerColor(CurrentMarker)
            End If
            Dim dimensions As New Rectangle(CurrentMarker.Left - MainForm.ImageLeft, CurrentMarker.Top - MainForm.ImageTop, CurrentMarker.Width, CurrentMarker.Height)
            e.Graphics.FillEllipse(B_Marker, dimensions)
            Dim MarkerNumber As String = a 'Form1.ReturnNumber(CurrentMarker)
            Dim MarkerFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Bold)
            If ShowNumbersCheck.Checked Then
                Dim B_Number As New SolidBrush(Color.White)
                If ColoursCheck.Checked Then
                    B_Number = MainForm.NumberColour(CurrentMarker)
                End If
                Dim MarkerFormat As New StringFormat
                MarkerFormat.Alignment = StringAlignment.Center
                MarkerFormat.LineAlignment = StringAlignment.Center
                e.Graphics.DrawString(MarkerNumber, MarkerFont, B_Number, dimensions, MarkerFormat)
            End If
            If ShowLabelsCheck.Checked Then
                Dim LabelFont As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
                Dim B_Black As New Drawing.SolidBrush(Color.Black)
                Dim CurrentNameTag As String = MainForm.CheckNameTag(a - 1)
                Dim CurrentX As Integer = CurrentMarker.Location.X - MainForm.ImageLeft
                Dim CurrentY As Integer = CurrentMarker.Location.Y + CurrentMarker.Height - MainForm.ImageTop
                e.Graphics.DrawString(CurrentNameTag, LabelFont, B_Black, CurrentX, CurrentY)
            End If
        Next
    End Sub

    Private Sub UpdatePrintable()
        PrintableBuild.Width = MainForm.ImageWidth
        PrintableBuild.Height = MainForm.ImageHeight
        Me.Width = PrintableBuild.Left + PrintableBuild.Width + 20 + MainForm.BorderWidth
        Me.Height = PrintableBuild.Top + PrintableBuild.Height + 30 + MainForm.TitlebarHeight
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Refresh()
    'End Sub

    Private Sub UpdateAll(sender As Object, e As EventArgs) Handles MyBase.Load, UpdateButton.Click, ShowLabelsCheck.CheckedChanged, ShowNumbersCheck.CheckedChanged, ColoursCheck.CheckedChanged
        UpdatePrintable()
        Refresh()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles SaveImageButton.Click
        Dim save As New SaveFileDialog()
        'save = New SaveFileDialog()
        save.CreatePrompt = True
        save.Filter = "JPEG | *.jpeg"
        Try
            If save.ShowDialog() = DialogResult.OK Then
                Dim toFile As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, save.FileName)
                Dim toSave As New Bitmap(PrintableBuild.Width, PrintableBuild.Height)
                Dim dimensions As New Rectangle(0, 0, PrintableBuild.Width, PrintableBuild.Height)
                PrintableBuild.DrawToBitmap(toSave, dimensions)
                toSave.Save(toFile, System.Drawing.Imaging.ImageFormat.Jpeg)
                'Dim toFile As System.IO.StreamWriter
                'toFile = My.Computer.FileSystem.CombinePath(save.FileName, True)
                'PictureBox1.Image.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class